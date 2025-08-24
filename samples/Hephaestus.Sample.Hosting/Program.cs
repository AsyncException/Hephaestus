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
builder.AddHephaestusModule<AuditLogModule>();

IHost host = builder.Build();

await host.StartAsync();
await host.WaitForShutdownAsync();