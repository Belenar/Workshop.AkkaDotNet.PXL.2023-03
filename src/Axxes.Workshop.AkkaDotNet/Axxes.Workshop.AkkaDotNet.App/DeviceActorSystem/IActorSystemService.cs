using Axxes.Workshop.AkkaDotNet.App.Messages;

namespace Axxes.Workshop.AkkaDotNet.App.DeviceActorSystem;

interface IActorSystemService
{
    void SendMeasurement(Guid deviceId, MeterReadingReceived message);
    Task StopAsync(CancellationToken cancellationToken);
}