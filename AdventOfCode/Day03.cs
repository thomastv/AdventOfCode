namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string[] _input;
    public Day03()
    {
        _input = File.ReadAllLines(InputFilePath);
    }
    override public ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 {Solve1()}");
    override public ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 {Solve2()}");
    public string Solve1()
    {
        int ans = 0;
        foreach (var line in _input)
        {
            int max = -1, secondMax = -1;
            int len = line.Length;
            int prevMax = -1;
            for (int i = 0; i < len; i++)
            {
                if (!char.IsDigit(line[i]))
                {
                    throw new Exception($"Invalid character detected: {line[i]} in line: {line}");
                }
                int digit = line[i] - '0';
                if (digit > max)
                {
                    //Reset second max before updating max       
                    prevMax = max;
                    secondMax = -1;
                    max = digit;
                }
                else if (digit > secondMax && digit != max)
                {
                    secondMax = digit;
                }
            }
            int result = 0;
            if (secondMax == -1)
            {
                result = prevMax * 10 + max;
            }
            else
            {
                result = max * 10 + secondMax;
            }
            System.Console.WriteLine($"Line: {line}, Max: {max}, SecondMax: {secondMax}, Result: {result}");
            ans += result;
        }

        return $"Day3 Solve1 Result: {ans}";
    }
    public static string Solve2()
    {
        return "Day3 Solve2 Result";
    }
}