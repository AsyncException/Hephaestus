using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Hephaestus;

public interface IAssemblyProvider
{
    public Assembly Assembly { get; }

    public void OptionalModules(IHostApplicationBuilder builder);

    public void OptionalDependencies(IHost app);
}