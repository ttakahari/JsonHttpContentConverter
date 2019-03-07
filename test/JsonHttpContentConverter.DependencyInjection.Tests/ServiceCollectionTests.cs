using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace JsonHttpContentConverter.DependencyInjection.Tests
{
    public class ServiceCollectionTests
    {
        [Fact]
        public void AddJsonHttpContentConverter_Tests()
        {
            // by type.
            {
                var services = new ServiceCollection();

                services.AddJsonHttpContentConverter<NullJsonHttpContentConverter>();

                var service = services.BuildServiceProvider();

                var converter = service.GetService<IJsonHttpContentConverter>();

                Assert.NotNull(converter);
                Assert.IsType<NullJsonHttpContentConverter>(converter);
            }

            // by instance.
            {
                var services = new ServiceCollection();

                Assert.Throws<ArgumentNullException>(() => services.AddJsonHttpContentConverter(null));

                services.AddJsonHttpContentConverter(new NullJsonHttpContentConverter());

                var service = services.BuildServiceProvider();

                var converter = service.GetService<IJsonHttpContentConverter>();

                Assert.NotNull(converter);
                Assert.IsType<NullJsonHttpContentConverter>(converter);
            }
        }
    }

    public class NullJsonHttpContentConverter : IJsonHttpContentConverter
    {
        public HttpContent ToJsonHttpContent<T>(T value)
            => throw new NotImplementedException();

        public Task<T> FromJsonHttpContent<T>(HttpContent content)
            => throw new NotImplementedException();
    }
}
