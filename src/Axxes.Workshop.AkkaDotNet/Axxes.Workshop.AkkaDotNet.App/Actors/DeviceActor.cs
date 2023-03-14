using Akka.Actor;
using Axxes.Workshop.AkkaDotNet.App.Messages;

namespace Axxes.Workshop.AkkaDotNet.App.Actors;

class DeviceActor : ReceiveActor
{
    private const string NormalizationActorName = "value-normalization";
    private const string PersistenceActorName = "value-storage";

    private readonly Guid _deviceId;

    public DeviceActor(Guid deviceId)
    {
        _deviceId = deviceId;
        Receive<MeterReadingReceived>(HandleMeterReading);
        Receive<NormalizedMeterReading>(HandleNormalizedReading);
        Receive<RequestLastNormalizedReadings>(
            msg => Context.Child(PersistenceActorName).Forward(msg));

        CreateChildren();
    }

    private void HandleMeterReading(MeterReadingReceived reading)
    {
        Context.Child(NormalizationActorName).Tell(reading);
    }

    private void HandleNormalizedReading(NormalizedMeterReading reading)
    {
        Context.Child(PersistenceActorName).Tell(reading);
    }

    private void CreateChildren()
    {
        var normalizationProps = NormalizationActor.CreateProps();
        Context.ActorOf(normalizationProps, NormalizationActorName);
        var persistenceProps = ValuePersistenceActor.CreateProps(_deviceId);
        Context.ActorOf(persistenceProps, PersistenceActorName);
    }

    public static Props CreateProps(Guid deviceId)
    {
        return Props.Create<DeviceActor>(deviceId);
    }
}