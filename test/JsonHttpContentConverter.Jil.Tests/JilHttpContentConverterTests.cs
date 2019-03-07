using Jil;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace JsonHttpContentConverter.Jil.Tests
{
    public class JilHttpContentConverterTests
    {
        [Fact]
        public void Constructor_Tests()
        {
            Assert.Throws<ArgumentNullException>(() => new JilHttpContentConverter(null));

            {
                var converter = new JilHttpContentConverter();

                Assert.NotNull(converter);
            }

            {
                var converter = new JilHttpContentConverter(Options.Default);

                Assert.NotNull(converter);
            }
        }

        [Fact]
        public async Task ToHttpContent_Tests()
        {
            var converter = new JilHttpContentConverter();

            {
                const int value = 1;

                var content = converter.ToJsonHttpContent(value);

                Assert.IsType<StringContent>(content);
                Assert.Equal(value.ToString(), await content.ReadAsStringAsync());
            }

            {
                var value = new Foo
                {
                    Bar = "AAA",
                    Baz = 1
                };
                var json = JSON.Serialize(value);

                var content = converter.ToJsonHttpContent(value);

                Assert.IsType<StringContent>(content);
                Assert.Equal(json, await content.ReadAsStringAsync());
            }
        }

        [Fact]
        public async Task FromHttpContent_Tests()
        {
            var converter = new JilHttpContentConverter();

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

                var json = JSON.Serialize(value);
                var content = new StringContent(json);

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
