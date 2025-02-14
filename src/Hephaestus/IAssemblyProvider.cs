using Microsoft.Extensions.Hosting;

namespace Hephaestus;

public interface IAssemblyProvider
{
    public virtual void OptionalModules(IHostApplicationBuilder builder) { }

    public virtual void OptionalDependencies(IHost app) { }
}