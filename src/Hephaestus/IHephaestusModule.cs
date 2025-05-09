using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hephaestus;

public interface IHephaestusModule<TModule> where TModule : IHephaestusModule<TModule>
{
    public static abstract void RegisterServices(IConfiguration configuration, IServiceCollection services);
}

public interface IHephaestusLifeCycle<TModule> where TModule : IHephaestusModule<TModule>, IHephaestusLifeCycle<TModule> {
    public static abstract void AfterAppBuild(IHost host);
}