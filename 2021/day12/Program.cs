string[] paths = File.ReadAllLines("data.txt");

Dictionary<string, Cave> caves = new Dictionary<string, Cave>();

foreach (string path in paths)
{
    string[] points = path.Split("-");
    if(!caves.ContainsKey(points[0])) {
        caves[points[0]] = new Cave() { Name = points[0], IsLarge = points[0].All(c => char.IsUpper(c)) };
    }
    if(!caves.ContainsKey(points[1])) {
        caves[points[1]] = new Cave() { Name = points[1], IsLarge = points[1].All(c => char.IsUpper(c)) };
    }
    caves[points[0]].Caves.Add(caves[points[1]]);
    caves[points[1]].Caves.Add(caves[points[0]]);
}

int part1 = Traverse(caves["start"], new HashSet<string>());
int part2 = Traverse2(caves["start"], new HashSet<string>());

Console.WriteLine($"Part1: {part1}");
Console.WriteLine($"Part2: {part2}");

int Traverse(Cave cave, HashSet<string> visited) {
    if (cave.Name == "end") return 1;
    if (visited.Contains(cave.Name) && !cave.IsLarge) return 0;
    visited.Add(cave.Name);
    int count = 0;
    foreach (Cave c in cave.Caves)
    {
        count += Traverse(c, new HashSet<string>(visited));
    }

    return count;
}

int Traverse2(Cave cave, HashSet<string> visited, bool twice = false) {
    if (cave.Name == "end") return 1;
    if (visited.Contains("start") && cave.Name == "start") return 0;
    if (visited.Contains(cave.Name) && !cave.IsLarge && twice) return 0;
    if (visited.Contains(cave.Name) && !cave.IsLarge) twice = true;
    visited.Add(cave.Name);
    int count = 0;
    foreach (Cave c in cave.Caves)
    {
        count += Traverse2(c, new HashSet<string>(visited), twice);
    }

    return count;
}

struct Cave {
    public string Name { get; init; } = "";
    public List<Cave> Caves { get; } = new List<Cave>();
    public bool IsLarge { get; init; }
}