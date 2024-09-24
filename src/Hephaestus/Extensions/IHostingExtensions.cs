using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hephaestus;

public static class IHostingExtensions
{
    public static IHost UseHephaestus(this IHost app) {
        IEnumerable<IAssemblyProvider> providers = app.Services.GetServices<IAssemblyProvider>();
        foreach (IAssemblyProvider provider in providers) {
            provider.OptionalDependencies(app);
        }

        return app;
    }
}