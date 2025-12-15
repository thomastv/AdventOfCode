using AdventOfCode.Extensions;

namespace AdventOfCode;

public class Day09: BaseDay
{
    private readonly string[] _input;
    private (string,string) _solution = ("","");
    public Day09()
    {
        _input = File.ReadAllLines(InputFilePath);
         _solution = Solve(_input);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 {Part1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 {Part2()}");

    private string Part1()
    {
        return _solution.Item1;
    }

    private string Part2()
    {
        return _solution.Item2;
    }
    private static (string, string) Solve(string[] input)
    {
        //Inupt is of the format, each line is a coordinate of a red tile, get input without using regex
        //1,2
        //3,4
        //6,7
        var redTiles = input.Select(s => s.Split(',').Select(int.Parse).ToList()).ToList();
        
        var boxes = redTiles.SelectMany( a => redTiles.Where(b => b[0] != a[0] && b[1] != a[1] && b[0] > a[0]), 
                            (a, b) =>  (a, b, area: 1L*Math.Abs(b[0] - a[0] + 1) * Math.Abs(b[1] - a[1] + 1)))
                            .OrderByDescending(x => x.area)
                            .ToList();   

        return (boxes.First().area.ToString(), "");                                                      
    }
}