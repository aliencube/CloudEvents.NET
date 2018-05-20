using System;
using System.Net.Http;
using System.Threading.Tasks;

using Aliencube.CloudEventsNet.AppModels;
using Aliencube.CloudEventsNet.Http;

using Microsoft.AspNetCore.Mvc;

namespace Aliencube.CloudEventsNet.ApiApp.Controllers
{
    [Route("api/eventgrid")]
    public class EventGridCallerController : Controller
    {
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> PostAsync([FromBody]ToDo payload)
        {
            var contentType = "application/json";

            var ce = CloudEventFactory.Create(contentType, payload);
            ce.EventType = "org.aliencube.ToDos.OnToDoCreated";
            ce.EventTypeVersion = "1.0";
            ce.Source = "/subscriptions/[SUBSCRIPTION_ID]/resourceGroups/[RESOURCE_GROUP_NAME]/providers/microsoft.eventgrid/topics/[TOPIC_NAME]#[EVENT_SUBJECT_NAME]";

            ce.EventId = Guid.NewGuid().ToString();
            ce.EventTime = DateTimeOffset.UtcNow;
            ce.ContentType = "application/json";

            var requestUri = "[EVENT_GRID_TOPIC_ENDPOINT_URL]";

            using (var client = new HttpClient())
            using (var content = CloudEventContentFactory.Create(ce))
            {
                content.Headers.Add("AEG-SAS-KEY", "EVENT_GRID_TOPIC_ACCESS_TOKEN");

                var response = await client.PostAsync(requestUri, content).ConfigureAwait(false);

                return new OkObjectResult(response.ReasonPhrase);
            }
        }
    }
}