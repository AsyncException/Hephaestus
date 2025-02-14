using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Hephaestus.Sample.Module.AuditLog.AspNet;

public class AssemblyProvider : IAssemblyProvider
{
    public Assembly Assembly { get; } = typeof(AssemblyProvider).Assembly;

    public void OptionalModules(IHostApplicationBuilder builder) {
        string connection_string = builder.Configuration.GetConnectionString("Default") ?? "Data Source=Application.db";
        string assembly_name = Assembly.GetEntryAssembly()?.GetName().Name ?? throw new Exception("Cannot get the name of the EntryAssembly");

        builder.Services.AddDbContext<DatabaseContext>(options => {
            options.UseSqlite(connection_string, b => b.MigrationsAssembly(assembly_name));
        });
    }

    public void OptionalDependencies(IHost host) {
        using IServiceScope scope = host.Services.CreateScope();
        DatabaseContext context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        context.Database.EnsureCreated();
    }
}