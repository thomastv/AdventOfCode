namespace AdventOfCode;

public class Day05 : BaseDay
{
    private readonly string[] _input;

    public Day05()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 {Part1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 {Part2()}");
    public string Part1()
    {
        var fullInput = string.Join("\n", _input);
        var split = fullInput.Split("\n\n");
        var ranges = split[0].Split("\n").Select(r => r.Split("-").Select(long.Parse).ToList()).ToList();
        var ingredients = split[1].Split("\n").Select(long.Parse).ToList();
        var freshCount = ingredients.Count(i => ranges.Any(r => r[0] <= i && r[1] >= i));
        return "" + freshCount;
    }

    public string Part2()
    {
        var fullInput = string.Join("\n", _input);
        var split = fullInput.Split("\n\n"); ;
        var ranges = MergeRanges(split[0].Split("\n").Select(r => r.Split("-").Select(long.Parse).ToList()).ToList());
        return "" + ranges.Sum(r => 1 + r[1] - r[0]);
    }

    private static List<List<long>> MergeRanges(List<List<long>> ranges)
    {

        for (var i = 0; i < ranges.Count; i++)
        {
            var r1 = ranges[i];
            for (var j = i + 1; j < ranges.Count; j++)
            {
                var r2 = ranges[j];
                if (!Overlaps(r1, r2)) continue;
                ranges[i] = [Math.Min(r1[0], r2[0]), Math.Max(r1[1], r2[1])];
                ranges.RemoveAt(j);
                return MergeRanges(ranges);
            }
        }
        return ranges;
    }

    private static bool Overlaps(List<long> r1, List<long> r2)
    {
        return r1[0] <= r2[1] && r1[1] >= r2[0];
    }
}