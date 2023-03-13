namespace Axxes.Workshop.AkkaDotNet.App.Helpers;

public static class DateTimeExtensions
{
    private static readonly DateTime ReferenceDate = new (2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static int BucketNumber(this DateTime date)
    {
        var timespan = date - ReferenceDate;

        return (int)Math.Floor(timespan.TotalMinutes / 5);
    }

    public static DateTime BucketDate(this int bucketNumber)
    {
        return ReferenceDate.AddMinutes(bucketNumber * 5);
    }
}