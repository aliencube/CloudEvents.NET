# CloudEvents.NET #

This is a .NET implementation of CloudEvents (OpenEvents) spec defined by [CloudEvents](https://openevents.io/).

> Current spec of CloudEvents is the version of [`0.1`](https://github.com/cloudevents/spec).

[![Build status](https://ci.appveyor.com/api/projects/status/um0krn2e8fm9femb/branch/dev?svg=true)](https://ci.appveyor.com/project/justinyoo/cloudevents-net/branch/dev)


## Versions ##

**CloudEvents.NET** intends to follow the same versioning approach as CloudEvents spec. However, it has two different variations for .NET Standard 1.3 - 1.6, and .NET Standard 2.0+.

* `*.*.1.*`: This version targets .NET Standard 1.3 - 1.6. `eg) 0.1.1.0`
* `*.*.2.*`: This version targets .NET Standard 2.0 and later. `eg) 0.1.2.0`


## `Aliencube.CloudEventsNet.Abstractions` ##

[![](https://img.shields.io/nuget/dt/Aliencube.CloudEventsNet.Abstractions.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Abstractions/) [![](https://img.shields.io/nuget/v/Aliencube.CloudEventsNet.Abstractions.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Abstractions/)

This defines interfaces and abstract classes for CloudEvents based on the [CloudEvents spec](https://github.com/cloudevents/spec/blob/master/spec.md).

TBD


## `Aliencube.CloudEventsNet` ##

[![](https://img.shields.io/nuget/dt/Aliencube.CloudEventsNet.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet/) [![](https://img.shields.io/nuget/v/Aliencube.CloudEventsNet.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet/)

This implements CloudEvents based on the [CloudEvents spec](https://github.com/cloudevents/spec/blob/master/spec.md).

TBD


## `Aliencube.CloudEventsNet.Http.Abstractions` ##

[![](https://img.shields.io/nuget/dt/Aliencube.CloudEventsNet.Http.Abstractions.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Http.Abstractions/) [![](https://img.shields.io/nuget/v/Aliencube.CloudEventsNet.Http.Abstractions.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Http.Abstractions/)

This defines interfaces and abstract classes for CloudEvents [transported over HTTP](https://github.com/cloudevents/spec/blob/master/http-transport-binding.md).

TBD


## `Aliencube.CloudEventsNet.Http` ##

[![](https://img.shields.io/nuget/dt/Aliencube.CloudEventsNet.Http.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Http/) [![](https://img.shields.io/nuget/v/Aliencube.CloudEventsNet.Http.svg)](https://www.nuget.org/packages/Aliencube.CloudEventsNet.Http/)

This implements CloudEvents [transported over HTTP](https://github.com/cloudevents/spec/blob/master/http-transport-binding.md).

TBD


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
