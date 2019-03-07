using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace JsonHttpContentConverter.JsonNet
{
    /// <summary>
    /// Convert between an object and <see cref="HttpContent"/> that includes JSON with using Json.NET.
    /// </summary>
    public class JsonNetHttpContentConverter : IJsonHttpContentConverter
    {
        private readonly JsonSerializerSettings _settings;

        /// <summary>
        /// Create a new instance of <see cref="JsonNetHttpContentConverter"/>.
        /// </summary>
        public JsonNetHttpContentConverter()
            : this(new JsonSerializerSettings())
        {
        }

        /// <summary>
        /// Create a new instance of <see cref="JsonNetHttpContentConverter"/> with an instance of <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="settings">An instance of <see cref="JsonSerializerSettings"/>.</param>
        public JsonNetHttpContentConverter(JsonSerializerSettings settings)
            => _settings = settings ?? throw new ArgumentNullException(nameof(settings));

        /// <inheritdoc />
        public HttpContent ToJsonHttpContent<T>(T value)
        {
            var json = JsonConvert.SerializeObject(value, _settings);

            return new StringContent(json);
        }

        /// <inheritdoc />
        public async Task<T> FromJsonHttpContent<T>(HttpContent content)
        {
            var json = await content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(json, _settings);
        }
    }
}
