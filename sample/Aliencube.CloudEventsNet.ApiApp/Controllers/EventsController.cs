using System;
using System.Net.Http;
using System.Threading.Tasks;

using Aliencube.CloudEventsNet.ApiApp.Models;
using Aliencube.CloudEventsNet.Http;

using Microsoft.AspNetCore.Mvc;

namespace Aliencube.CloudEventsNet.ApiApp.Controllers
{
    [Route("api/events")]
    public class EventsController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]Product payload)
        {
            var ce = new ObjectEvent<Product>();
            ce.EventType = "org.aliencube.Product.OnProductCreated";
            ce.EventTypeVersion = "1.0";
            ce.Source = new Uri("https://localhost/api/events");
            ce.EventId = Guid.NewGuid().ToString();
            ce.EventTime = DateTimeOffset.UtcNow;
            ce.ContentType = "application/json";
            ce.Data = payload;

            var requestUri = "";

            using (var client = new HttpClient())
            using (var content = new StructuredCloudEventContent<Product>(ce))
            {
                var response = await client.PostAsync(requestUri, content).ConfigureAwait(false);

                return new OkObjectResult(content);
            }
        }
    }
}