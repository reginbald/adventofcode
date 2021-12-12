int[][] points = File.ReadAllLines("data.txt")
    .Select(x => x.ToCharArray().Select(x => x - '0').ToArray()).ToArray();

List<(int, int)> lowPoints = new List<(int, int)>();
int sumHeight = 0;
for (int i = 0; i < points.Length; i++) {
    for (int y = 0; y < points[i].Length; y++) {
        int up = i != 0 ? points[i - 1][y] : int.MaxValue;
        int down = i != points.Length - 1 ? points[i + 1][y] : int.MaxValue;
        int left = y != 0 ? points[i][y - 1] : int.MaxValue;;
        int right = y != points[i].Length - 1 ? points[i][y + 1] : int.MaxValue;

        if (points[i][y] < up
            && points[i][y] < down 
            && points[i][y] < left 
            && points[i][y] < right) {
                sumHeight += points[i][y] + 1;
                lowPoints.Add((i,y));
            }
    }
}

Console.WriteLine($"Part1: {sumHeight}");

// Part 2

int[] basinSizes = new int[lowPoints.Count()];
int index = 0;
foreach ((int y, int x) in lowPoints) {
    basinSizes[index++] = ExploreBasin(x, y, points[0].Length, points.Length, new HashSet<(int, int)>()).Count();
}

Array.Sort(basinSizes);
Console.WriteLine($"Part2: {basinSizes[(basinSizes.Length - 3)..].Aggregate(1, (agg, x) => agg * x)}");


HashSet<(int, int)> ExploreBasin(int x, int y, int maxX, int maxY, HashSet<(int, int)> set){
    if(set.Contains((x,y))) return set;
    if (x < 0 || x >= maxX || y < 0 || y >= maxY) return set;
    if (points[y][x] == 9) return set;
    
    set.Add((x,y));
    set.UnionWith(ExploreBasin(x, y + 1, maxX, maxY, set));
    set.UnionWith(ExploreBasin(x, y - 1, maxX, maxY, set));
    set.UnionWith(ExploreBasin(x + 1, y, maxX, maxY, set));
    set.UnionWith(ExploreBasin(x - 1, y, maxX, maxY, set));

    return set;
}