using Akka.Actor;
using Axxes.Workshop.AkkaDotNet.App.Actors;
using Axxes.Workshop.AkkaDotNet.App.Messages;

namespace Axxes.Workshop.AkkaDotNet.App.DeviceActorSystem;

class ActorSystemService : IActorSystemService
{
    private readonly ActorSystem _system;
    private const string ActorSystemName = "DeviceActorSystem";
    private readonly Dictionary<Guid, IActorRef> _deviceActors = new ();

    public ActorSystemService()
    {
        _system = ActorSystem.Create(ActorSystemName);
    }

    public void SendMeasurement(Guid deviceId, MeterReadingReceived message)
    {
        if (!_deviceActors.ContainsKey(deviceId))
        {
            CreateDeviceActor(deviceId);
        }
        _deviceActors[deviceId].Tell(message);
    }

    private void CreateDeviceActor(Guid deviceId)
    {
        var props = DeviceActor.CreateProps(deviceId);
        var name = $"device-{deviceId}";
        _deviceActors[deviceId] = _system.ActorOf(props, name);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        var shutdown = CoordinatedShutdown.Get(_system);

        await shutdown.Run( CoordinatedShutdown.ClrExitReason.Instance);
    }
}