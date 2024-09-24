using Hephaestus.Sample.Module.AuditLog.Hosting.Models;
using Microsoft.EntityFrameworkCore;

namespace Hephaestus.Sample.Module.AuditLog.Hosting;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<AuditLogConfiguration> AuditLogConfigurations { get; set; }
}