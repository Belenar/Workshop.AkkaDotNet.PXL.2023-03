using System.Collections.Immutable;

namespace Axxes.Workshop.AkkaDotNet.App.Messages;

public class ReturnLastNormalizedReadings
{
    public ImmutableArray<NormalizedMeterReading> Readings { get; }

    public ReturnLastNormalizedReadings(ImmutableArray<NormalizedMeterReading> readings)
    {
        Readings = readings;
    }
}