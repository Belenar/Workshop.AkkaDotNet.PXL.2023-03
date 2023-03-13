using Axxes.Workshop.AkkaDotNet.App.DeviceActorSystem;
using Axxes.Workshop.AkkaDotNet.App.MessageReader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Axxes.Workshop.AkkaDotNet.App;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton(new IotHubSettings(hostContext.Configuration));
                services.AddSingleton<IActorSystemService, ActorSystemService>();
                services.AddHostedService<MessageReaderService>();
            });
}