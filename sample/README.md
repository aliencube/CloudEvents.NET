# CloudEvents.NET Sample Applications #

There are two sample applications using `CloudEvents.NET`.


## TO-DO App ##

This is a console application that sends an event to ASP.NET Core Web API.


## ASP.NET Core Web API App ##

This is a Web API application that handles CloudEvent messages. This consists of two parts:

1. It receives a CloudEvent message from the TO-DO console app, and
1. It receives a POST request to pass the payload from the user to Azure Event Grid using CloudEvent.

In order to build the Web API application, there are some prerequisites.

* Azure Subscription
* Azure Event Grid Topic instance

Once they are ready, you need to modify the codelines below, with your details:

```csharp
[Route("api/eventgrid")]
public class EventGridCallerController : Controller
{
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> PostAsync([FromBody]ToDo payload)
    {
        ...

        // Get the subscription Id, resource group name, Azure Event Grid topic name.
        // Event subject name can be anything you want to enter.
        ce.Source = "/subscriptions/[SUBSCRIPTION_ID]/resourceGroups/[RESOURCE_GROUP_NAME]/providers/microsoft.eventgrid/topics/[TOPIC_NAME]#[EVENT_SUBJECT_NAME]";

        ...

        // Get the Azure Event Grid topic endpoint URL.
        var requestUri = "[EVENT_GRID_TOPIC_ENDPOINT_URL]";

        using (var client = new HttpClient())
        using (var content = new StructuredCloudEventContent<ToDo>(ce))
        {
            ...

            // Get the Azure Event Grid topic access token.
            content.Headers.Add("AEG-SAS-KEY", "EVENT_GRID_TOPIC_ACCESS_TOKEN");

            ...
        }
    }
}
```

When you send a POST request through Postman or something similar tool, with a payload of:

```csharp
{
  "title": "my first to-do"
}
```

This will be sent to Azure Event Grid topic. Then, use a tool like RequestBin or Azure Logic Apps to check the actual request body.
