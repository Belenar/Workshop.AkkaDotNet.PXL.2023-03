using Axxes.Workshop.AkkaDotNet.App.Messages;

namespace Axxes.Workshop.AkkaDotNet.App.DeviceActorSystem;

public interface IActorSystemService
{
    void SendMeasurement(Guid deviceId, MeterReadingReceived message);
    Task StopAsync(CancellationToken cancellationToken);
}