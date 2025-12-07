namespace AdventOfCode;

public class Day07 : BaseDay
{
    private readonly string[] _input;

    public Day07()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 {Part1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 {Part2()}");

    private string Part1()
    {
        var grid = _input;
        var visited = new HashSet<(int row, int col, int dr, int dc)>();
        var energized = new HashSet<(int row, int col)>();

        // Find starting position
        int startRow = 0;
        int startCol = 0;
        for (int r = 0; r < grid.Length; r++)
        {
            for (int c = 0; c < grid[r].Length; c++)
            {
                if (grid[r][c] == 'S')
                {
                    startRow = r;
                    startCol = c;
                    break;
                }
            }
        }

        // Trace beam with direction (down)
        TraceBeam(grid, startRow, startCol, 1, 0, visited, energized);

        return "" + energized.Count;
    }

    private string Part2()
    {
        throw new NotImplementedException();   
    }

    private void TraceBeam(string[] grid, int row, int col, int dr, int dc, 
        HashSet<(int, int, int, int)> visited, HashSet<(int, int)> energized)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;

        while (row >= 0 && row < rows && col >= 0 && col < cols)
        {
            // Check if we've been in this state before (infinite loop detection)
            if (visited.Contains((row, col, dr, dc)))
                return;

            visited.Add((row, col, dr, dc));
            energized.Add((row, col));

            char cell = grid[row][col];

            if (cell == '^')
            {
                // Splitter: beam splits left and right
                energized.Add((row, col));

                // Left direction
                if (col - 1 >= 0)
                    TraceBeam(grid, row, col - 1, 0, -1, visited, energized);

                // Right direction
                if (col + 1 < cols)
                    TraceBeam(grid, row, col + 1, 0, 1, visited, energized);

                return; // Stop current beam
            }

            // Move in current direction
            row += dr;
            col += dc;
        }
    }
}