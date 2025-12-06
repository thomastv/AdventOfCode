using AdventOfCode.Extensions;

namespace AdventOfCode;

public class Day06 : BaseDay
{
    private readonly string[] _input;

    public Day06()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 {Part1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 {Part2()}");

    private string Part1()
    {
        var rows = _input[..^1]
            .Select(line => line.ParseLongs())
            .ToList();

        return "" + _input[^1]
            .Where(c => !char.IsWhiteSpace(c))
            .Select((op, i) => Solve(op, operands: rows.Select(row => row[i])))
            .Sum();
    }

    private string Part2()
    {
        var total = 0L;
        var rows = _input.Length;
        var cols = _input[0].Length;

        var operands = new List<long>();
        for (var c = cols - 1; c >= 0; c--)
        {
            operands.Add(0L);
            for (var r = 0; r < rows - 1; r++)
            {
                if (!char.IsWhiteSpace(_input[r][c]))
                {
                    operands[^1] *= 10L;
                    operands[^1] += _input[r][c].AsDigit();
                }
            }

            var op = _input[rows - 1][c];
            if (!char.IsWhiteSpace(op))
            {
                total += Solve(op, operands);
                operands.Clear();
                c--;
            }
        }

        return "" + total;
    }

    private static long Solve(char op, IEnumerable<long> operands)
    {
        return operands.Aggregate(
            seed: op == '+' ? 0L : 1L,
            func: (acc, n) => op == '+' ? acc + n : acc * n);
    }
}