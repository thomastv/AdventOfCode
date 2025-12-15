namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly string[] _input;
    private int startPosition = 50;
    private const int LockLength = 100;
    public Day01()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 {Solve1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 {Solve2()}");

    //Solves the problem of counting how many times the lock lands on position 0
    public int Solve1()
    {
        var d = 50;
        const int maxD = 100;
        var a = 0;
        foreach (var line in _input)
        {
            var v = line[0] == 'R' ? 1 : -1;
            if (!int.TryParse(line[1..], out var c)) throw new Exception("Invalid number detected!");
            var distToZero = v == 1 ? maxD - d : d;
            if (distToZero > 0 && c >= distToZero) a++;
            a += (c - distToZero) / maxD;

            d += (v * c);
            d %= maxD;
            if (d < 0) d += maxD;
        }

        return a;
    }

    //you're actually supposed to count the number of times any click causes the dial to point at 0, regardless of whether it happens during a rotation or at the end of one.
    public int Solve2()
    {
        startPosition = 50;
        int ans = 0;
        foreach (var line in _input)
        {
            var v = line[0] == 'R' ? 1 : -1;
            if (!int.TryParse(line[1..], out var c))
            {
                throw new InvalidDataException($"Data, {line} is not of proper format");
            }
            var distToZero = v == 1 ? LockLength - startPosition : startPosition;
            if (distToZero > 0 && c >= distToZero) ans++;

            ans += (c - distToZero) / LockLength;

            startPosition += (v * c);
            startPosition %= LockLength;
            if (startPosition < 0)
            {
                startPosition += LockLength;
            }
        }
        return ans;
    }
}
