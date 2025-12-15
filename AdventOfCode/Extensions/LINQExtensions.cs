namespace AdventOfCode.Extensions;

public static class LINQExtensions
{
    public static IEnumerable<T> TakeEvery<T>(this IEnumerable<T> source, int step)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(step);

        int index = 0;
        foreach (var item in source)
        {
            if (index % step == 0)
            {
                yield return item;
            }
            index++;
        }
    }
}