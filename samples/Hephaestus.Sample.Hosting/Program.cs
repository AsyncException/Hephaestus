using Hephaestus;
using Hephaestus.Sample.Module.AuditLog.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();

builder.Configuration.AddJsonFile("appsettings.json", false);

#if DEBUG
builder.Configuration.AddUserSecrets<Program>();
#endif

builder.AddHephaestus();
builder.AddHephaestusModule<AssemblyProvider>();

IHost host = builder.Build();

host.UseHephaestus();

await host.StartAsync();
await host.WaitForShutdownAsync();