string[] data = File.ReadAllLines("data.txt");
string template = data[0];
string[] insertions = data[2..];
Dictionary<string, char> insert = new Dictionary<string, char>();
Dictionary<char, ulong> score = new Dictionary<char, ulong>();

// Prepare insertions
foreach (string insertion in insertions) {
    string[] tmp = insertion.Split(" -> ");
    insert.Add(tmp[0], char.Parse(tmp[1]));
    score[char.Parse(tmp[1])] = 0;
}

// Perform insertions
LinkedList<char> polymer = new LinkedList<char>(template.ToCharArray());
for (int step = 0; step < 10; step++) {
    for (var node = polymer.First; node != null; node = node.Next) {
        if (node.Previous == null) continue;
        if (insert.ContainsKey("" + node.Previous.Value + node.Value)){
            score[insert["" + node.Previous.Value + node.Value]] += 1;
            polymer.AddBefore(node, new LinkedListNode<char>(insert["" + node.Previous.Value + node.Value]));
        }
    }
}

// Calculate results
foreach(char c in template.ToCharArray()) {
    score[c] += 1;
}

ulong high = score.Values.Max(), low = score.Values.Min();
Console.WriteLine($"Part1: {high} - {low} = {high - low}");
//Console.WriteLine(polymer.Aggregate("", (agg, x) => agg + x));

// Part 2
Dictionary<string, ulong> tracker = new Dictionary<string, ulong>();
score = new Dictionary<char, ulong>();

for (int i = 0; i < template.Length - 1; i++) {
    tracker[template[i..(i + 2)]] = tracker.GetValueOrDefault(template[i..(i + 2)]) + 1;
}

// Perform insertions
for (int step = 0; step < 40; step++) {
    var tmp = new Dictionary<string, ulong>();
    foreach ((string key, ulong value) in tracker)
    {
        char c = insert[key];
        score[c] = score.GetValueOrDefault(c) + value;
        string t1 = $"{key[0]}{c}", t2 = $"{c}{key[1]}";
        tmp[t1] = tmp.GetValueOrDefault(t1) + value;
        tmp[t2] = tmp.GetValueOrDefault(t2) + value;
    }
    tracker = tmp;
}

// Calculate results
foreach(char c in template.ToCharArray()) {
    score[c] += 1;
}

high = score.Values.Max();
low = score.Values.Min();
Console.WriteLine($"Part2: {high} - {low} = {(high - low)}");