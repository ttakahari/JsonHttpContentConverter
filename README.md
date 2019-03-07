# JsonHttpContentConverter

Convert JSON to System.Net.Http.HttpContent and vice versa.

Supported JSON serializers are [Json.NET](https://github.com/JamesNK/Newtonsoft.Json)(Standard JSON Library of .NET), [Jil](https://github.com/kevin-montrose/Jil)(Fastest Text-Format JSON Library) or [Utf8Json](https://github.com/neuecc/Utf8Json)(Fastest Binary-Format JSON Library).

[![AppVeyor](https://img.shields.io/appveyor/ci/gruntjs/grunt.svg?style=plastic)](https://ci.appveyor.com/project/ttakahari/JsonHttpContentConverter)
[![NuGet](https://img.shields.io/nuget/v/JsonHttpContentConverter.svg?style=plastic)](https://www.nuget.org/packages/JsonHttpContentConverter/)
[![NuGet](https://img.shields.io/nuget/v/JsonHttpContentConverter.JsonNet.svg?style=plastic)](https://www.nuget.org/packages/JsonHttpContentConverter.JsonNet/)
[![NuGet](https://img.shields.io/nuget/v/JsonHttpContentConverter.Jil.svg?style=plastic)](https://www.nuget.org/packages/JsonHttpContentConverter.Jil/)
[![NuGet](https://img.shields.io/nuget/v/JsonHttpContentConverter.Utf8Json.svg?style=plastic)](https://www.nuget.org/packages/JsonHttpContentConverter.Utf8Json/)
[![NuGet](https://img.shields.io/nuget/v/JsonHttpContentConverter.DependencyInjection.svg?style=plastic)](https://www.nuget.org/packages/JsonHttpContentConverter.DependencyInjection/)

## Install

from NuGet - [JsonHttpContentConverter](https://www.nuget.org/packages/JsonHttpContentConverter/)  
from NuGet - [JsonHttpContentConverter.JsonNet](https://www.nuget.org/packages/JsonHttpContentConverter.JsonNet/)  
from NuGet - [JsonHttpContentConverter.Jil](https://www.nuget.org/packages/JsonHttpContentConverter.Jil/)  
from NuGet - [JsonHttpContentConverter.Utf8Json](https://www.nuget.org/packages/JsonHttpContentConverter.Utf8Json/)

```ps1
PM > Install-Package JsonHttpContentConverter
PM > Install-Package JsonHttpContentConverter.JsonNet
PM > Install-Package JsonHttpContentConverter.Jil
PM > Install-Package JsonHttpContentConverter.Utf8Json
```

## How to use

Create an instance of `IJsonHttpContentConverter`, and call methods `ToHttpContent` and `FromHttpContent`.

```csharp
var foo = new Foo { Bar = "AAA", Baz = 1};

// Create an instance of class implementing IJsonHttpContentConverter.
var converter = new JsonNetHttpContentConverter();

// Convert to HttpContent.
var content = converter.ToHttpContent(foo);

// Post data.
var client = new HttpClient();
var response = await client.PostAsync("[Your POST URL]", content);

// Convert from HttpContent.
var result = await converter.FromHttpContent<Foo>(response.Content);
```

## Dependency Injection

JsonHttpContentConverter has an extension that resolves an instance of IJsonHttpContentConverter with Microsoft.Extensions.DependencyInjection.

```csharp
var services = new ServiceCollection();

// add JsonHttpContentConverter.
services.AddJsonHttpContentConverter<JsonNetHttpContentConverter>();

var service = services.BuildServiceProvider();

// resolve JsonHttpContentConverter.
var converter = service.GetService<IJsonHttpContentConverter>();
```

## Lisence

under [MIT Lisence](https://opensource.org/licenses/MIT).
