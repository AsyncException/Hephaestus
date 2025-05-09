using Hephaestus.Sample.Module.AuditLog.Hosting.Models;

namespace Hephaestus.Sample.Module.AuditLog.Hosting;

//This would normally be a database or file storage.
public class ConfigProvider
{
    public List<AuditLogConfiguration> Config { get; set; } = [];
}
