$eventData = Get-Content .\DiscordSocketEvents.json | ConvertFrom-Json

foreach($event in $eventData){
    $parametersIndexCounter = 0;
    
    if($event.parameters.count -gt 0){
        $combinedParameters = [string]::Join(", ", ($event.parameters | ForEach-Object { $data = "$($_) arg$($parametersIndexCounter)"; $parametersIndexCounter++; return $data } ));
    }
    else{
        $combinedParameters = "";
    }
    
    $combinedIntents = [string]::Join(", ", ($event.intents | ForEach-Object { return "GatewayIntents.$($_)" }))

    $template = "
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class $($event.Name)Handler<THandler> : IEventHandler<THandler> where THandler : $($event.name)Handler<THandler> {

	public abstract Task Execute($($combinedParameters));

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ $($combinedIntents) ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.$($event.name) += services.GetRequiredService<THandler>().Execute;
}
"
    
        Set-Content -Path ".\$($event.name)Handler.cs" -Value $template
}