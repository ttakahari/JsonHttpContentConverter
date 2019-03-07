using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Utf8Json;
using Utf8Json.Resolvers;

namespace JsonHttpContentConverter.Utf8Json
{
    /// <summary>
    /// Convert between an object and <see cref="HttpContent"/> that includes JSON with using Utf8Json.
    /// </summary>
    public class Utf8JsonHttpContentConverter
    {
        private readonly IJsonFormatterResolver _resolver;

        /// <summary>
        /// Create a new instance of <see cref="Utf8JsonHttpContentConverter"/>.
        /// </summary>
        public Utf8JsonHttpContentConverter()
            : this(StandardResolver.Default)
        {
        }

        /// <summary>
        /// Create a new instance of <see cref="Utf8JsonHttpContentConverter"/> with an instance of <see cref="IJsonFormatterResolver"/>.
        /// </summary>
        /// <param name="resolver">An instance of <see cref="IJsonFormatterResolver"/>.</param>
        public Utf8JsonHttpContentConverter(IJsonFormatterResolver resolver)
            => _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));

        /// <inheritdoc />
        public HttpContent ToJsonHttpContent<T>(T value)
        {
            var json = JsonSerializer.Serialize(value, _resolver);

            var content = new ByteArrayContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return content;
        }

        /// <inheritdoc />
        public async Task<T> FromJsonHttpContent<T>(HttpContent content)
        {
            var json = await content.ReadAsByteArrayAsync().ConfigureAwait(false);

            return JsonSerializer.Deserialize<T>(json, _resolver);
        }
    }
}
