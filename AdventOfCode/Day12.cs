using System.Text.RegularExpressions;

namespace AdventOfCode;

public partial class Day12 : BaseDay
{
    private readonly string[] _input;
    private (string, string) _solution = ("", "");
    public Day12()
    {
        _input = File.ReadAllLines(InputFilePath);
        _solution = Solve(_input);
    }
    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 {_solution.Item1}");
    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 {_solution.Item2}");

    private static (string, string) Solve(string[] input)
    {
        var part1 = input.Skip(30)
                    .Select(l => LineRegex.Match(l))
                    .Count(
                        m =>
                            int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value)
                                >= 9 * m.Groups.OfType<Group>().Skip(3).Sum(x => int.Parse(x.Value))
                    );

        var part2 = 0L;

        return (part1.ToString(), part2.ToString());
    }

    [GeneratedRegex(@"^(\d+)x(\d+): (\d+) (\d+) (\d+) (\d+) (\d+) (\d+)")]
    private static partial Regex LineRegex { get; }
}