using Hephaestus.Sample.Module.AuditLog.AspNet.Models;
using Microsoft.EntityFrameworkCore;

namespace Hephaestus.Sample.Module.AuditLog.AspNet;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<AuditLogConfiguration> AuditLogConfigurations { get; set; }
}