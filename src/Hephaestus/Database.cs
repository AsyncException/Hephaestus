using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Hephaestus;

public static class Database
{
    public static Action<DbContextOptionsBuilder> GetDbContextConfiguration(IHostApplicationBuilder builder) => options_builder => options_builder.UseSqlite(builder.Configuration.GetConnectionString("Default"), b => b.MigrationsAssembly("Async.Discord.Bot"));
}