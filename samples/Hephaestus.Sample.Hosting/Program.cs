using Discord;
using Hephaestus;
using Hephaestus.Sample.Module.AuditLog.Hosting;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();
builder.AddHephaestus(() => new() {
    GatewayIntents = GatewayIntents.AllUnprivileged,
    MessageCacheSize = 100,
    AuditLogCacheSize = 100
});

builder.AddHephaestusModule<AssemblyProvider>();

IHost host = builder.Build();

host.UseHephaestus();
host.Start();