using Akka.Actor;
using Axxes.Workshop.AkkaDotNet.App.Helpers;
using Axxes.Workshop.AkkaDotNet.App.Messages;

namespace Axxes.Workshop.AkkaDotNet.App.Actors;

public class NormalizationActor: ReceiveActor
{
    private readonly ValueNormalizationHelper _helper = new();
    public NormalizationActor()
    {
        Receive<MeterReadingReceived>(HandleMeterReading);
    }

    private void HandleMeterReading(MeterReadingReceived reading)
    {
        var newNormalizedMeterReadings = 
            _helper.GetNormalizedReadingsUntil(reading.TimestampUtc, reading.MeterReading);

        foreach (var normalizedReading in newNormalizedMeterReadings)
        {
            Context.Parent.Tell(normalizedReading);
        }
    }

    public static Props CreateProps()
    {
        return Props.Create<NormalizationActor>();
    }
}