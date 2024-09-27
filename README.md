# Hephaestus - A C# Framework for Building Discord Bots

Hephaestus is an easy to setup framework for running Discord Bots using [Discord .net](https://github.com/discord-net/Discord.Net) together with Microsoft.Extensions.Hosting or AspNet.

## Features

- **Easy Command Handling**: Simplified setup for slash commands.
- **Event Handling**: Simplified setup for handling events.
- **Modular Architecture**: Create feature modules that can be easily plugged into your bot.
- **Hosting**: Integration with AspNet or Microsofts Hosting model.
- **EntityFramework**: Integration with EntityFramework so you can setup your database out of the box.

## Installation
```bash
git clone https://github.com/AsyncException/Hephaestus.git
cd hephaestus
dotnet restore
dotnet build
```

From here you can either reference the project in the /src folder or grab the nuget package from the artifacts.

## Usage

For samples see the projects in the [/sample](https://github.com/AsyncException/Hephaestus/tree/main/samples) folder.

### Main project

Adding namespace:
```cs
using Hephaestus
```

Add Hephaestus services and discord configuration
```cs
HostApplicationBuilder builder = Host.CreateApplicationBuilder();
builder.AddHephaestus((config) => {
    config.GatewayIntents = GatewayIntents.AllUnprivileged;
    config.MessageCacheSize = 100;
    config.AuditLogCacheSize = 100;
});
```

Adding your modules
```cs
builder.AddHephaestusModule<AssemblyProvider>();
```

Use Heaphaestus before starting your host app
```cs
host.UseHephaestus();
```

### Modules

Create your IAssemblyProvider
```cs
public class ExampleAssemblyProvider : IAssemblyProvider
{
    public Assembly Assembly { get; } = typeof(ExampleAssemblyProvider).Assembly;

    public void OptionalModules(IHostApplicationBuilder builder) {}
    public void OptionalDependencies(IHost host) {}
}
```

Add optional module: in the OptionalModules() method you can add extra services or a DbContext. This gets run before your host application gets build.
```cs
public void OptionalModules(IHostApplicationBuilder builder) {
    builder.Services.AddDbContext<DatabaseContext>(Database.GetDbContextConfiguration(builder));
}
```

Add optional dependancies: in the OptionalDepenencies() method you can run code after your app gets build but before it starts
```cs
public void OptionalDependencies(IHost host) {
    using IServiceScope scope = host.Services.CreateScope();
    DatabaseContext context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    context.Database.EnsureCreated();
}
```

Creating slash commands modules:

This is the same process as with a standar Discord .net project. You class must inherit 

```cs
public class InteractionModule(ILogger<InteractionModule> logger) : InteractionModuleBase<SocketInteractionContext>
{
    private readonly ILogger<InteractionModule> logger = logger;

    [SlashCommand("log", "log a message")]
    public async Task LogMessage([Summary(description: "Text to send to the logger")]string message)
    {
        await DeferAsync(ephemeral: true);
        logger.LogInformation(message);
        await FollowupAsync("Message was send to the logger");
    }
}
```

Responding to events:

Handling events in Hephasetus is slightly different then in Discord .net. Instead of subscribing to an event, you inherit from a handler class. The naming convention is based on the name of the event + Handler. Every handler has a Context property that contains the parameters of the event and a Client property that contains the DiscordSocketClient. You can inject services from the DI container using the constructor.

```cs
public class AuditLogMonitor(ILogger<AuditLogMonitor> logger) : AuditLogCreatedHandler
{
    public override async Task Execute() {
        logger.LogDebug("[auditlog created] received audit log created event");

        ITextChannel channel = (ITextChannel)await Client.GetChannelAsync(1266361985579352094);

        await channel.SendMessageAsync(embed: new EmbedBuilder()
            .WithTitle("AuditLog")
            .WithDescription(Context.SocketAuditLogEntry.Reason)
            .WithFields(new EmbedFieldBuilder().WithName("Action").WithValue(Context.SocketAuditLogEntry.Action).WithIsInline(true))
            .WithFooter($"{Context.SocketAuditLogEntry.CreatedAt} - {Context.SocketAuditLogEntry.User.Username}")
            .Build());
    }
}
```

## License
Hephaestus is licensed under the [MIT License](https://github.com/AsyncException/Hephaestus?tab=MIT-1-ov-file).
