using Axxes.Workshop.AkkaDotNet.App.Messages;

namespace Axxes.Workshop.AkkaDotNet.App.DeviceActorSystem;

class ActorSystemService : IActorSystemService
{
    private const string ActorSystemName = "DeviceActorSystem";

    public ActorSystemService()
    {
        //TODO: Create an ActorSystem
    }

    public void SendMeasurement(Guid deviceId, MeterReadingReceived message)
    {
        //TODO: Check if the actor for this device needs to be created.
        //TODO: send the message to the actor;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        //TODO: shutdown the ActorSystem correctly.
    }
}