using System.Net.Http;
using System.Threading.Tasks;

namespace JsonHttpContentConverter
{
    /// <summary>
    /// Defines methods to convert between an object and <see cref="HttpContent"/> that includes JSON.
    /// </summary>
    public interface IJsonHttpContentConverter
    {
        /// <summary>
        /// Convert an object to <see cref="HttpContent"/> that includes JSON.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>A converted <see cref="HttpContent"/>.</returns>
        HttpContent ToJsonHttpContent<T>(T value);

        /// <summary>
        /// Convert <see cref="HttpContent"/> that includes JSON to an object.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="content"><see cref="HttpContent"/> that includes JSON.</param>
        /// <returns>A converted object.</returns>
        Task<T> FromJsonHttpContent<T>(HttpContent content);
    }
}
