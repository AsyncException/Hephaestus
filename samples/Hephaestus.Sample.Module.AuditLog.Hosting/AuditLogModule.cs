using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Hephaestus.Sample.Module.AuditLog.Hosting;

public class AuditLogModule : IHephaestusModule<AuditLogModule>
{
    public static void RegisterServices(IConfiguration configuration, IServiceCollection services) {
        services.AddSingleton<ConfigProvider>();
        services.AddInteractionHandler<InteractionModule>();
        services.AddEventHandler<AuditLogMonitor>();
    }
}