using Microsoft.Extensions.Hosting;

namespace Hephaestus;

public interface IAssemblyProvider
{
    public void OptionalModules(IHostApplicationBuilder builder) { }

    public void OptionalDependencies(IHost app) { }
}