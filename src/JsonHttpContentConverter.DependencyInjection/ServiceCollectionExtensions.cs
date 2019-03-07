using JsonHttpContentConverter;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions of <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add <see cref="IJsonHttpContentConverter"/> to resolve an instance with DI.
        /// </summary>
        /// <typeparam name="TConverter">A type of <see cref="IJsonHttpContentConverter"/>.</typeparam>
        /// <param name="services"><see cref="IServiceCollection"/> to add servcies to.</param>
        /// <returns>Added <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddJsonHttpContentConverter<TConverter>(this IServiceCollection services)
            where TConverter : IJsonHttpContentConverter
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton(typeof(IJsonHttpContentConverter), typeof(TConverter));

            return services;
        }

        /// <summary>
        /// Add <see cref="IJsonHttpContentConverter"/> to resolve an instance with DI.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> to add servcies to.</param>
        /// <param name="converter">An instance of <see cref="IJsonHttpContentConverter"/>.</param>
        /// <returns>Added <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddJsonHttpContentConverter(this IServiceCollection services, IJsonHttpContentConverter converter)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (converter == null) throw new ArgumentNullException(nameof(converter));

            services.AddSingleton(converter);

            return services;
        }
    }
}
