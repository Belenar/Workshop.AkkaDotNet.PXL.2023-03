namespace Axxes.Workshop.AkkaDotNet.App.Messages;

public class RequestLastNormalizedReadings
{
    public int NumberOfReadings { get; }

    public RequestLastNormalizedReadings(int numberOfReadings)
    {
        NumberOfReadings = numberOfReadings;
    }
}