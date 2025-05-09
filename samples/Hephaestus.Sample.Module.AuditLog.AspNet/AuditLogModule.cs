using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Sample.Module.AuditLog.AspNet;

public class AuditLogModule : IHephaestusModule<AuditLogModule>
{
    public static void RegisterServices(IConfiguration configuration, IServiceCollection services) {
        services.AddSingleton<ConfigProvider>();
        services.AddInteractionHandler<InteractionModule>();
        services.AddEventHandler<AuditLogMonitor>();
    }
}