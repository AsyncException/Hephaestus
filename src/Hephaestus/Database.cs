using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Hephaestus;

public static class Database
{
    public static Action<DbContextOptionsBuilder> GetDbContextConfiguration(IHostApplicationBuilder builder) =>
        options_builder => options_builder.UseSqlite(builder.Configuration.GetConnectionString("Default"), b => b.MigrationsAssembly(Assembly.GetEntryAssembly()?.GetName().Name));
}