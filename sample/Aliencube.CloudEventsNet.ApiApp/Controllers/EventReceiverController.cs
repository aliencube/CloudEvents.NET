using System.Threading.Tasks;

using Aliencube.CloudEventsNet.AppModels;

using Microsoft.AspNetCore.Mvc;

namespace Aliencube.CloudEventsNet.ApiApp.Controllers
{
    [Route("api/events")]
    public class EventReceiverController : Controller
    {
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> PostAsync([FromBody]ObjectEvent<ToDo> payload)
        {
            var response = new { Headers = this.Request.Headers, Body = payload };

            return await Task.FromResult(new OkObjectResult(response)).ConfigureAwait(false);
        }
    }
}