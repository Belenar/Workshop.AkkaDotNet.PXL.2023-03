using Akka.Actor;
using Axxes.Workshop.AkkaDotNet.App.Helpers;
using Axxes.Workshop.AkkaDotNet.App.Messages;

namespace Axxes.Workshop.AkkaDotNet.App.Actors;

public class NormalizationActor: ReceiveActor, IWithUnboundedStash
{
    private readonly ValueNormalizationHelper _helper = new();

    public NormalizationActor()
    {
        Become(Started);
    }

    private void Started()
    {
        Receive<MeterReadingReceived>(_ => Stash.Stash());
        Receive<ReturnLastNormalizedReadings>(Initialize);
    }

    private void Initialized()
    {
        Receive<MeterReadingReceived>(HandleMeterReading);
    }

    private void Initialize(ReturnLastNormalizedReadings readings)
    {
        var lastReading = readings.Readings.FirstOrDefault();
        if (lastReading != null)
            _helper.GetNormalizedReadingsUntil(lastReading.Timestamp, lastReading.MeterReading);

        Become(Initialized);
        Stash.UnstashAll();
    }

    

    protected override void PreStart()
    {
        Context.Parent.Tell(new RequestLastNormalizedReadings(1));
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

    public IStash Stash { get; set; }
}