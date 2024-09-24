using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Hephaestus.Sample.Module.AuditLog.AspNet;

public class AssemblyProvider : IAssemblyProvider
{
    public Assembly Assembly { get; } = typeof(AssemblyProvider).Assembly;

    public void OptionalModules(IHostApplicationBuilder builder) {
        builder.Services.AddDbContext<DatabaseContext>(Database.GetDbContextConfiguration(builder));
    }

    public void OptionalDependencies(IHost host) {
        using IServiceScope scope = host.Services.CreateScope();
        DatabaseContext context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        context.Database.EnsureCreated();
    }
}