# CloudEvents.NET #

This is a .NET implementation of CloudEvents (OpenEvents) spec defined by [CloudEvents](https://openevents.io/).

> Current spec of CloudEvents is the version of [`0.1`](https://github.com/cloudevents/spec).

* [![Build status](https://ci.appveyor.com/api/projects/status/um0krn2e8fm9femb/branch/dev?svg=true)](https://ci.appveyor.com/project/justinyoo/cloudevents-net/branch/dev) [AppVeyor](https://ci.appveyor.com/project/justinyoo/cloudevents-net)
* [![](https://img.shields.io/nuget/dt/Aliencube.CloudEventsNet.Abstractions.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Abstractions/) [![](https://img.shields.io/nuget/v/Aliencube.CloudEventsNet.Abstractions.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Abstractions/) [`Aliencube.CloudEventsNet.Abstractions`](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Abstractions/)
* [![](https://img.shields.io/nuget/dt/Aliencube.CloudEventsNet.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet/) [![](https://img.shields.io/nuget/v/Aliencube.CloudEventsNet.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet/) [`Aliencube.CloudEventsNet`](https://www.nuget.org/packages/Aliencube.CloudEventsNet/)
* [![](https://img.shields.io/nuget/dt/Aliencube.CloudEventsNet.Http.Abstractions.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Http.Abstractions/) [![](https://img.shields.io/nuget/v/Aliencube.CloudEventsNet.Http.Abstractions.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Http.Abstractions/) [`Aliencube.CloudEventsNet.Http.Abstractions`](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Http.Abstractions/)
* [![](https://img.shields.io/nuget/dt/Aliencube.CloudEventsNet.Http.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Http/) [![](https://img.shields.io/nuget/v/Aliencube.CloudEventsNet.Http.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Http/) [`Aliencube.CloudEventsNet.Http`](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Http/)


## `Aliencube.CloudEventsNet.Abstractions` ##

[![](https://img.shields.io/nuget/dt/Aliencube.CloudEventsNet.Abstractions.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Abstractions/) [![](https://img.shields.io/nuget/v/Aliencube.CloudEventsNet.Abstractions.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Abstractions/)

This defines interfaces and abstract classes for CloudEvents based on the [CloudEvents spec](https://github.com/cloudevents/spec/blob/master/spec.md).


## `Aliencube.CloudEventsNet` ##

[![](https://img.shields.io/nuget/dt/Aliencube.CloudEventsNet.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet/) [![](https://img.shields.io/nuget/v/Aliencube.CloudEventsNet.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet/)

This implements CloudEvents based on the [CloudEvents spec](https://github.com/cloudevents/spec/blob/master/spec.md). According to the spec, the `data` property of the CloudEvent payload can be either `string`, `binary` (base-64 encoded string) or `object`. Therfore, the actual implementation also has three different types, `StringEvent`, `BinaryEvent` and `ObjectEvent` respectively. Each event object has its own distinctive content type.


### `ObjectEvent` or `ObjectEvent<T>` ###

`ObjectEvent` or `ObjectEvent<T>` object takes care of the content type of `application/json` or of having suffix of `+json`. If the content type implies the data format of JSON, the `ObjectEvent<T>` also takes care of it.

```csharp
var todo = new ToDo() { Title = "My Todo" };

var ev = new ObjectEvent<ToDo>();
ev.ContentType = "application/json";
ev.Data = todo;
...
```


### `StringEvent` ###

`StringEvent` object only takes care of string content types, which is basically of all MIME type starting with `text/`, except `text/json`.

```csharp
var todo = "<todo>My Todo</todo>";
var ev = new StringEvent();
ev.ContentType = "text/xml";
ev.Data = todo;
...
```


### `BinaryEvent` ###

`BinaryEvent` object takes care of the rest of all content types.

```csharp
var todo = "My ToDo";
var ev = new BinaryEvent();
ev.ContentType = "application/octet-stream";
ev.Data = Encoding.UTF8.GetBytes(todo);
...
```


### `CloudEventFactory` ###

Instead of directly instantiating a `CloudEvent<T>` object, you can use the factory method like:

```csharp
var todo = new ToDo() { Title = "My Todo" };
var contentType = "application/json";

var ev = CloudEventFactory.Create(contentType, todo);
...
```


## `Aliencube.CloudEventsNet.Http.Abstractions` ##

[![](https://img.shields.io/nuget/dt/Aliencube.CloudEventsNet.Http.Abstractions.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Http.Abstractions/) [![](https://img.shields.io/nuget/v/Aliencube.CloudEventsNet.Http.Abstractions.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Http.Abstractions/)

This defines interfaces and abstract classes for CloudEvents [transported over HTTP](https://github.com/cloudevents/spec/blob/master/http-transport-binding.md).


## `Aliencube.CloudEventsNet.Http` ##

[![](https://img.shields.io/nuget/dt/Aliencube.CloudEventsNet.Http.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Http/) [![](https://img.shields.io/nuget/v/Aliencube.CloudEventsNet.Http.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Http/)

This implements CloudEvents [transported over HTTP](https://github.com/cloudevents/spec/blob/master/http-transport-binding.md). According to the document, when a CloudEvent object is passed over HTTP, it can be under a `Binary` mode or `Structured` mode.


### `StructuredCloudEventContent<T>` ###

`StructuredCloudEventContent<T>` takes only `ObjectEvent`, and store the entire `ObjectEvent` instance into the payload. Therefore, the `ObjectEvent` instance itself is serialised and converted to byte array. All properties other than `Data` are also stored into the request/response header.


### `BinaryCloudEventContent<T>` ###

`BinaryCloudEventContent<T>` takes either `StringEvent` or `BinaryEvent`, and store only their `Data` property value into the payload. Therefore, the `StringEvent` instance converts its `Data` property value to byte array, while the `BinaryEvent` instance simply passes the `Data` property value as it is already byte array. All other properties are stored into the request/response header.


### `CloudEventContentFactory` ###

Instead of directly instantiating the `CloudEventContent<T>` object, you can use the factory method like:

```csharp
var todo = new ToDo() { Title = "My Todo" };
var contentType = "application/json";

var ev = CloudEventFactory.Create(contentType, todo);
ev.EventType = "org.aliencube.ToDos.OnToDoCreated";
...

var requestUri = "https://localhost:443/path/to";
var client = new HttpClient();

var content = CloudEventContentFactory.Create(ev);
var response = await client.PostAsync(requestUri, content).ConfigureAwait(false);
...
```


## Contribution ##

Your contributions are always welcome! All your work should be done in your forked repository. Once you finish your work with corresponding tests, please send us a pull request onto our `dev` branch for review.


## License ##

**CloudEvents.NET** is released under [MIT License](http://opensource.org/licenses/MIT)

> The MIT License (MIT)
>
> Copyright (c) 2018 [aliencube.org](https://aliencube.org)
> 
> Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
> 
> The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
> 
> THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
