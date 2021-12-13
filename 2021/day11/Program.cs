int[][] grid = File.ReadAllLines("data.txt").Select(x => x.ToCharArray().Select(x => x - '0').ToArray()).ToArray();
int flashes = 0, part1 = 0, part2 = 0, step = 1;

while (step > 0) {
    List<(int, int)> coords = new List<(int, int)>();
    for (int y = 0; y < grid.Length; y++) {
        for (int x = 0; x < grid[0].Length; x++) {
            grid[y][x] += 1;
            if (grid[y][x] > 9) coords.Add((y, x));
        }
    }
    foreach ((int y, int x) in coords) {
        flashes += Flash(y, x, grid.Length - 1, grid[0].Length - 1);
        if(step == 100) part1 = flashes;
    }
    if(grid.Aggregate(0, (agg, line) => agg + line.Aggregate(0, (agg, p) => agg +p)) == 0) {
        part2 = step;
        step = -1;
    }
    step++;
}

Console.WriteLine($"Part1: {part1}");
Console.WriteLine($"Part2: {part2}");

int Flash(int y, int x, int maxY, int maxX){
    if (y < 0 || x < 0 || y > maxY || x > maxX) return 0;
    if (grid[y][x] == 0) return 0;
    grid[y][x] += 1;
    if (grid[y][x] > 9) {
        grid[y][x] = 0;
        int count = Flash(y+1, x, maxY, maxX);
        count += Flash(y, x+1, maxY, maxX);
        count += Flash(y+1, x+1, maxY, maxX);

        count += Flash(y-1, x, maxY, maxX);
        count += Flash(y, x-1, maxY, maxX);
        count += Flash(y-1, x-1, maxY, maxX);

        count += Flash(y+1, x-1, maxY, maxX);
        count += Flash(y-1, x+1, maxY, maxX);

        return count + 1;
    }
    return 0;
}

void Print() {
    string output = "";
    foreach (var line in grid)
    {
        foreach (var point in line)
        {
            output += point;
        }
        output += System.Environment.NewLine;
    }
    Console.WriteLine(output);
}