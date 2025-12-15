namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly string[] _input;
    public Day02()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 {Solve1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 {Solve2()}");
    public long Solve1()
    {
        var input = File.ReadAllText(InputFilePath);
        ReadOnlySpan<char> inputSpan = input.AsSpan();
        long ans = 0;
        int startIdx = 0;
        while (startIdx < inputSpan.Length)
        {
            int commaIdx = inputSpan[startIdx..].IndexOf(',');
            ReadOnlySpan<char> rangeSpan;
            if (commaIdx == -1)
            {
                rangeSpan = inputSpan[startIdx..];
                startIdx = inputSpan.Length;
            }
            else
            {
                rangeSpan = inputSpan.Slice(startIdx, commaIdx);
                startIdx += commaIdx + 1;
            }

            int dashIdx = rangeSpan.IndexOf('-');
            if (dashIdx == -1) throw new Exception($"Invalid range: {rangeSpan.ToString()}");
            var startSpan = rangeSpan[..dashIdx];
            var endSpan = rangeSpan[(dashIdx + 1)..];
            if (!long.TryParse(startSpan, out var start)) throw new Exception($"Invalid number detected! {startSpan.ToString()}");
            if (!long.TryParse(endSpan, out var end)) throw new Exception($"Invalid number detected! {endSpan.ToString()}");

            for (long i = start; i <= end; i++)
            {
                var current = i.ToString();
                var span = current.AsSpan();
                int len = span.Length;
                if (len % 2 == 0)
                {
                    var firstHalf = span[..(len / 2)];
                    var secondHalf = span[(len / 2)..];
                    if (firstHalf.SequenceEqual(secondHalf))
                    {
                        ans += i;
                    }
                }
            }
        }
        return ans;
    }


    public long Solve2()
    {
        var input = File.ReadAllText(InputFilePath);
        var ranges = input.Split(',').Select(range => range.Split('-'));
        long ans = 0;
        foreach (var range in ranges)
        {
            if (!long.TryParse(range[0], out var start)) throw new Exception($"Invalid number detected! {range[0]}");
            if (!long.TryParse(range[1], out var end)) throw new Exception("Invalid number detected!");

            for (long i = start; i <= end; i++)
            {
                var current = i.ToString();
                var span = current.AsSpan();
                int len = span.Length;
                //Check for all possible lengths of repeated sequences
                int maxGroupLength = len / 2;

                for (int groupLength = 1; groupLength <= maxGroupLength; groupLength++)
                {
                    if (len % groupLength != 0) continue;

                    bool allMatch = true;
                    var firstGroup = span[..groupLength];

                    for (int pos = groupLength; pos < len; pos += groupLength)
                    {
                        var nextGroup = span.Slice(pos, groupLength);
                        if (!firstGroup.SequenceEqual(nextGroup))
                        {
                            allMatch = false;
                            break;
                        }
                    }

                    if (allMatch)
                    {
                        ans += i;
                        break; // No need to check larger group lengths
                    }
                }
            }
        }
        return ans;
    }
}