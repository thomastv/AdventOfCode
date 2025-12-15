using System.Collections.Immutable;
using System.Numerics;

namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly string[] _input;
    private int _gridLength = 1000;
    private int _gridWidth = 1000;

    public Day04()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 {Solve1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 {Solve2()}");
    public string Solve1()
    {
        int ans = 0;
        for (int i = 0; i < _input.Length; i++)
        {
            var row = _input[i];
            // System.Console.WriteLine($"Processing row {i}: {row} Size: {row.Length}");
            for (int j = 0; j < row.Length; j++)
            {
                if (_input[i][j] != '@')
                {
                    continue;
                }
                int cnt = 0;
                //Find all 8 adjacent positions
                int t = 0;
                for (int di = -1; di <= 1; di++)
                {
                    for (int dj = -1; dj <= 1; dj++)
                    {
                        t++;
                        if (di == 0 && dj == 0)
                        {
                            continue;
                        }
                        int newI = i + di;
                        int newJ = j + dj;
                        if (IsValidPos(newI, newJ) && _input[newI][newJ] == '@')
                        {
                            cnt++;
                        }
                    }
                }
                // System.Console.WriteLine($"Position ({i},{j}) = {row[j]} has {cnt} adjacent @ out of 8 directions checked and {t} total directions checked");
                if (cnt < 4)
                {
                    ans++;
                }
            }

        }
        return ans + "";
    }

    private bool IsValidPos(int i, int j)
    {
        return i >= 0 && i < _gridLength && j >= 0 && j < _gridWidth;
    }
    #region AOC Dictionary
    List<Complex> Neighbours = new()
    {
        Complex.ImaginaryOne, // down
        -Complex.ImaginaryOne, // up
        1, // right
        -1, // left
        Complex.ImaginaryOne + 1, // se
        Complex.ImaginaryOne - 1, // sw
        -Complex.ImaginaryOne + 1, // ne
        -Complex.ImaginaryOne - 1, // nw
    };
    public string Solve2()
    {
        var grid = (
            from y in Enumerable.Range(0, _input.Length)
            from x in Enumerable.Range(0, _input[0].Length)
            where _input[y][x] == '@'
            select Complex.ImaginaryOne * y + x
        ).ToImmutableHashSet();

        return "" + RemoveMaximumRolls(grid);
    }

    private int RemoveMaximumRolls(ImmutableHashSet<Complex> grid)
    {
        var removed = grid.Count(k => Neighbours.Count(n => grid.Contains(k + n)) < 4);
        if (removed == 0) return removed;
        return removed + RemoveMaximumRolls([.. grid.Where(k => Neighbours.Count(n => grid.Contains(k + n)) >= 4)]);
    }
    #endregion
}