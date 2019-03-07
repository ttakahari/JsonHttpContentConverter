using Jil;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace JsonHttpContentConverter.Jil
{
    /// <summary>
    /// Convert between an object and <see cref="HttpContent"/> that includes JSON with using Jil.
    /// </summary>
    public class JilHttpContentConverter : IJsonHttpContentConverter
    {
        private readonly Options _options;

        /// <summary>
        /// Create a new instance of <see cref="JilHttpContentConverter"/>.
        /// </summary>
        public JilHttpContentConverter()
            : this(Options.Default)
        {
        }

        /// <summary>
        /// Create a new instance of <see cref="JilHttpContentConverter"/> with an instance of <see cref="Options"/>.
        /// </summary>
        /// <param name="options">An instance of <see cref="Options"/>.</param>
        public JilHttpContentConverter(Options options)
            => _options = options ?? throw new ArgumentNullException(nameof(options));

        /// <inheritdoc />
        public HttpContent ToJsonHttpContent<T>(T value)
        {
            var json = JSON.Serialize(value, _options);

            return new StringContent(json);
        }

        /// <inheritdoc />
        public async Task<T> FromJsonHttpContent<T>(HttpContent content)
        {
            var json = await content.ReadAsStringAsync().ConfigureAwait(false);

            return JSON.Deserialize<T>(json, _options);
        }
    }
}
