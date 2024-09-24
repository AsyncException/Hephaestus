using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus;

public static class IServiceProviderExtensions
{
    /// <summary>
    /// Deserializes requested section to a Class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="service_provider"></param>
    /// <param name="section"></param>
    /// <returns><see cref="{T}"/> from config</returns>
    public static T FromConfiguration<T>(this IServiceProvider service_provider, string section) => service_provider.GetRequiredService<IConfiguration>().GetRequiredSection(section).Get<T>()!;

    /// <summary>
    /// Deserializes requested section to a Class
    /// </summary>
    /// <param name="service_provider"></param>
    /// <param name="type"></param>
    /// <param name="section"></param>
    /// <returns>object from config</returns>
    public static object? FromConfiguration(this IServiceProvider service_provider, Type type, string section) => service_provider.GetRequiredService<IConfiguration>().GetRequiredSection(section).Get(type);

    /// <summary>
    /// Adds <seealso cref="{TOptions}"/> as a singleton and fills the properties from the config
    /// </summary>
    /// <typeparam name="TOption"></typeparam>
    /// <param name="service_collection"></param>
    /// <param name="section"></param>
    /// <returns><see cref="IServiceCollection"/> for chaining</returns>
    public static IServiceCollection AddOption<TOption>(this IServiceCollection service_collection, string section) where TOption : class => service_collection.AddSingleton(sp => sp.FromConfiguration<TOption>(section));

    /// <summary>
    /// Adds <paramref name="type"/> as a singleton and fills the properties from the config
    /// </summary>
    /// <typeparam name="TOption"></typeparam>
    /// <param name="service_collection"></param>
    /// <param name="section"></param>
    /// <returns><see cref="IServiceCollection"/> for chaining</returns>
    public static IServiceCollection AddOption(this IServiceCollection service_collection, Type type, string section) => service_collection.AddSingleton(type, sp => sp.FromConfiguration(type, section) ?? throw new NullReferenceException($"Configuration returned null object for {type.Name}"));
}