using Hephaestus;
using Hephaestus.Sample.Module.AuditLog.AspNet;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddHephaestus(() => new() {
    GatewayIntents = Discord.GatewayIntents.AllUnprivileged,
    MessageCacheSize = 100,
    AuditLogCacheSize = 100,
});

builder.AddHephaestusModule<AssemblyProvider>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHephaestus();

app.Run();