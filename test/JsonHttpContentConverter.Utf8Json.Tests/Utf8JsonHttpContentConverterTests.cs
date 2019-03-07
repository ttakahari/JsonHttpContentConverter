using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utf8Json;
using Utf8Json.Resolvers;
using Xunit;

namespace JsonHttpContentConverter.Utf8Json.Tests
{
    public class Utf8JsonHttpContentConverterTests
    {
        [Fact]
        public void Constructor_Tests()
        {
            Assert.Throws<ArgumentNullException>(() => new Utf8JsonHttpContentConverter(null));

            {
                var converter = new Utf8JsonHttpContentConverter();

                Assert.NotNull(converter);
            }

            {
                var converter = new Utf8JsonHttpContentConverter(StandardResolver.Default);

                Assert.NotNull(converter);
            }
        }

        [Fact]
        public async Task ToHttpContent_Tests()
        {
            var converter = new Utf8JsonHttpContentConverter();

            {
                const int value = 1;

                var content = converter.ToJsonHttpContent(value);

                Assert.IsType<ByteArrayContent>(content);
                Assert.Equal(value.ToString(), await content.ReadAsStringAsync());
            }

            {
                var value = new Foo
                {
                    Bar = "AAA",
                    Baz = 1
                };
                var json = JsonSerializer.Serialize(value);

                var content = converter.ToJsonHttpContent(value);

                Assert.IsType<ByteArrayContent>(content);
                Assert.Equal(Encoding.UTF8.GetString(json), await content.ReadAsStringAsync());
            }
        }

        [Fact]
        public async Task FromHttpContent_Tests()
        {
            var converter = new Utf8JsonHttpContentConverter();

            {
                const int value = 1;

                var json = value.ToString();
                var content = new StringContent(json);

                var result = await converter.FromJsonHttpContent<int>(content);

                Assert.Equal(value, result);
            }

            {
                var value = new Foo
                {
                    Bar = "AAA",
                    Baz = 1
                };

                var json = JsonSerializer.Serialize(value);
                var content = new ByteArrayContent(json);

                var result = await converter.FromJsonHttpContent<Foo>(content);

                Assert.Equal(value.Bar, result.Bar);
                Assert.Equal(value.Baz, result.Baz);
            }
        }
    }

    public class Foo
    {
        public string Bar { get; set; }

        public int Baz { get; set; }
    }
}
