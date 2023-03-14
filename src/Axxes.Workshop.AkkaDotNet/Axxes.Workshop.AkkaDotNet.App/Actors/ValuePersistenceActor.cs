using System.Collections.Immutable;
using Akka.Actor;
using Akka.Persistence;
using Axxes.Workshop.AkkaDotNet.App.Messages;
using Axxes.Workshop.AkkaDotNet.App.State;

namespace Axxes.Workshop.AkkaDotNet.App.Actors;

public class ValuePersistenceActor : ReceivePersistentActor
{
    public override string PersistenceId { get; }

    private NormalizedReadingPersistenceState _state = new();

    public ValuePersistenceActor(Guid deviceId)
    {
        PersistenceId = $"device-{deviceId}";

        Command<NormalizedMeterReading>(msg => Persist(msg, HandleReading));
        Recover<NormalizedMeterReading>(HandleReading);

        Command<RequestLastNormalizedReadings>(HandleReadingRequest);
    }

    private void HandleReadingRequest(RequestLastNormalizedReadings request)
    {
        var readings = 
            ImmutableArray.Create(_state.GetLastReadings(request.NumberOfReadings));
        Sender.Tell(new ReturnLastNormalizedReadings(readings));
    }

    private void HandleReading(NormalizedMeterReading reading)
    {
        _state.Add(reading);
    }

    public static Props CreateProps(Guid deviceId)
    {
        return Props.Create<ValuePersistenceActor>(deviceId);
    }
}