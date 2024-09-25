using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Hephaestus;

public static class Database
{
    public static Action<DbContextOptionsBuilder> GetDbContextConfiguration(IHostApplicationBuilder builder) {
        string connection_string = builder.Configuration.GetConnectionString("Default") ?? "Data Source=Application.db";
        string assembly_name = Assembly.GetEntryAssembly()?.GetName().Name ?? throw new Exception("Cannot get the name of the EntryAssembly");

        return options_builder => options_builder.UseSqlite(
                connection_string,
                b => b.MigrationsAssembly(assembly_name)
            );
    }
}