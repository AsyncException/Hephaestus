using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hephaestus;

public interface IHephaestusModule<TModule> where TModule : IHephaestusModule<TModule>
{
    public static abstract void RegisterServices(IConfiguration configuration, IServiceCollection services);
}