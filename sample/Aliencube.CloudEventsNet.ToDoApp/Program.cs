using System;
using System.Net.Http;
using System.Threading.Tasks;

using Aliencube.CloudEventsNet.AppModels;
using Aliencube.CloudEventsNet.Http;

namespace Aliencube.CloudEventsNet.ToDoApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var ce = new ObjectEvent<ToDo>();
            ce.EventType = "org.aliencube.ToDos.OnToDoCreated";
            ce.EventTypeVersion = "1.0";
            ce.Source = (new Uri("http://localhost")).ToString();
            ce.EventId = Guid.NewGuid().ToString();
            ce.EventTime = DateTimeOffset.UtcNow;
            ce.ContentType = "application/json";
            ce.Data = new ToDo() { Title = "sample todo" };

            var requestUri = "http://localhost:5604/api/events";

            using (var client = new HttpClient())
            using (var content = new StructuredCloudEventContent<ToDo>(ce))
            {
                var body = GetResponseAsync(client, requestUri, content).Result;

                Console.WriteLine(body);
            }
        }

        public static async Task<string> GetResponseAsync(HttpClient client, string requestUri, HttpContent content)
        {
            using (var response = await client.PostAsync(requestUri, content).ConfigureAwait(false))
            {
                var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return body;
            }
        }
    }
}