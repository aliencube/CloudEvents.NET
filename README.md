# CloudEvents.NET #

This is a .NET implementation of CloudEvents (OpenEvents) spec defined by [CloudEvents](https://openevents.io/).

> Current spec of CloudEvents is the version of [`0.1`](https://github.com/cloudevents/spec).

[![Build status](https://ci.appveyor.com/api/projects/status/um0krn2e8fm9femb/branch/dev?svg=true)](https://ci.appveyor.com/project/justinyoo/cloudevents-net/branch/dev)


## `Aliencube.CloudEventsNet.Abstractions` ##

[![](https://img.shields.io/nuget/dt/Aliencube.CloudEventsNet.Abstractions.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Abstractions/) [![](https://img.shields.io/nuget/v/Aliencube.CloudEventsNet.Abstractions.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Abstractions/)

This defines interfaces and abstract classes for CloudEvents based on the [CloudEvents spec](https://github.com/cloudevents/spec/blob/master/spec.md).


## `Aliencube.CloudEventsNet` ##

[![](https://img.shields.io/nuget/dt/Aliencube.CloudEventsNet.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet/) [![](https://img.shields.io/nuget/v/Aliencube.CloudEventsNet.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet/)

This implements CloudEvents based on the [CloudEvents spec](https://github.com/cloudevents/spec/blob/master/spec.md). According to the spec, the `data` property of the CloudEvent payload can be either `string`, `binary` (base-64 encoded string) or `object`. Therfore, the actual implementation also has three different types, `StringEvent`, `BinaryEvent` and `ObjectEvent` respectively. Each event object has its own distinctive content type.


### ObjectEvent ###

`ObjectEvent` or `ObjectEvent<T>` object only takes care of the content type of `application/json` or of having suffix of `+json`. Therefore, the implementation has the validation logic like below:

```csharp
public class ObjectEvent<T> : CloudEvent<T> where T : class
{
    ...

    protected override bool IsValidDataType(T data)
    {
        if (this.ContentType.Equals("application/json", StringComparison.CurrentCultureIgnoreCase))
        {
            return true;
        }

        if (this.ContentType.EndsWith("+json", StringComparison.CurrentCultureIgnoreCase))
        {
            return true;
        }

        return false;
    }
}
```


### StringEvent ###

`StringEvent` object only takes care of string content types, which is basically of all MIME type starting with `text/`. Therefore, the implementation has the validation logic like below:

```csharp
public class StringEvent : CloudEvent<string>
{
    ...

    protected override bool IsValidDataType(string data)
    {
        if (this.ContentType.StartsWith("text/", StringComparison.CurrentCultureIgnoreCase))
        {
            return true;
        }

        return false;
    }
}
```


### BinaryEvent ###

`BinaryEvent` object takes care of the rest of all content types. Therefore, the implementation has the validation logic like below:

```csharp
public class BinaryEvent : CloudEvent<byte[]>
{
    ...

    protected override bool IsValidDataType(byte[] data)
    {
        if (this.ContentType.StartsWith("text/", StringComparison.CurrentCultureIgnoreCase))
        {
            return false;
        }

        if (this.ContentType.Equals("application/json", StringComparison.CurrentCultureIgnoreCase))
        {
            return false;
        }

        if (this.ContentType.EndsWith("+json", StringComparison.CurrentCultureIgnoreCase))
        {
            return false;
        }

        return true;
    }
}
```


## `Aliencube.CloudEventsNet.Http.Abstractions` ##

[![](https://img.shields.io/nuget/dt/Aliencube.CloudEventsNet.Http.Abstractions.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Http.Abstractions/) [![](https://img.shields.io/nuget/v/Aliencube.CloudEventsNet.Http.Abstractions.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Http.Abstractions/)

This defines interfaces and abstract classes for CloudEvents [transported over HTTP](https://github.com/cloudevents/spec/blob/master/http-transport-binding.md).


## `Aliencube.CloudEventsNet.Http` ##

[![](https://img.shields.io/nuget/dt/Aliencube.CloudEventsNet.Http.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Http/) [![](https://img.shields.io/nuget/v/Aliencube.CloudEventsNet.Http.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Http/)

This implements CloudEvents [transported over HTTP](https://github.com/cloudevents/spec/blob/master/http-transport-binding.md). According to the document, when a CloudEvent object is passed over HTTP, it can be under a `Binary` mode or `Structured` mode.


### `StructuredCloudEventContent<T>` ###

`StructuredCloudEventContent<T>` takes only `ObjectEvent`, and store the entire `ObjectEvent` instance into the payload. Therefore, the `ObjectEvent` instance itself is serialised and converted to byte array.

```csharp
public class StructuredCloudEventContent<T> : CloudEventContent<T>
{
    ...

    private static byte[] GetContentByteArray(CloudEvent<T> ce)
    {
        ...
        var serialised = JsonConvert.SerializeObject(ce);
        return Encoding.UTF8.GetBytes(serialised);
    }
}
```


### `BinaryCloudEventContent<T>` ###

`BinaryCloudEventContent<T>` takes either `StringEvent` or `BinaryEvent`, and store their `Data` property value into the payload. Therefore, the `StringEvent` instance converts its `Data` property value to byte array, while the `BinaryEvent` instance simply passes the `Data` property value as it is already byte array.

```csharp
public class BinaryCloudEventContent<T> : CloudEventContent<T>
{
    private static byte[] GetContentByteArray(CloudEvent<T> ce)
    {
        ...

        if (ce.Data is string)
        {
            return Encoding.UTF8.GetBytes(ce.Data as string);
        }

        if (ce.Data is byte[])
        {
            return ce.Data as byte[];
        }

        var serialised = JsonConvert.SerializeObject(ce.Data);
        return Encoding.UTF8.GetBytes(serialised);
    }
}
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
