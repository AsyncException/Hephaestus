namespace Hephaestus.Sample.Module.AuditLog.Hosting.Models;

public class AuditLogConfiguration
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public ulong Server { get; set; } = 0;
    public ulong ChannelId { get; set; } = 0;
}