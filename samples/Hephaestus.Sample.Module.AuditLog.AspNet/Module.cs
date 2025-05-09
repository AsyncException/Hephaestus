using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Hephaestus.Sample.Module.AuditLog.AspNet;

public class Module : IHephaestusModule<Module>, IHephaestusLifeCycle<Module>
{
    public static void RegisterServices(IConfiguration configuration, IServiceCollection services) {
        services.AddDbContext<DatabaseContext>(options => {
            string connection_string = configuration.GetConnectionString("Default") ?? "Data Source=Application.db";
            string assembly_name = Assembly.GetEntryAssembly()?.GetName().Name ?? throw new Exception("Cannot get the name of the EntryAssembly");
            options.UseSqlite(connection_string, b => b.MigrationsAssembly(assembly_name));
        });

        services.AddInteractionHandler<InteractionModule>();
    }

    public static void AfterAppBuild(IHost host) {
        using IServiceScope scope = host.Services.CreateScope();
        DatabaseContext context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        context.Database.EnsureCreated();
    }
}