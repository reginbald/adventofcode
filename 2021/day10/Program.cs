char[][] data = File.ReadAllLines("data.txt")
    .Select(x => x.ToCharArray()).ToArray();

Stack<char> charStack = new Stack<char>();
Dictionary<char, int> score = new Dictionary<char, int>() {
    {')', 0}, {']', 0}, {'}', 0}, {'>', 0}
};
Dictionary<char, int> points = new Dictionary<char, int>() {
    {')', 1}, {']', 2}, {'}', 3}, {'>', 4}
};
List<long> scores = new List<long>();

foreach (var line in data)
{
    for (int i = 0; i < line.Length; i++)
    {
        if(line[i] == '(' || line[i] == '[' || line[i] == '{' || line[i] == '<') {
            charStack.Push(line[i]); continue;
        }
        char peek = charStack.Peek();
        if ((line[i] == ')' && peek == '(') || (line[i] == ']' && peek == '[')
            || (line[i] == '}' && peek == '{')  || (line[i] == '>' && peek == '<')) { 
            charStack.Pop(); continue;
        }

        char expected = peek == '(' ? ')' : peek == '[' ? ']' : peek == '{' ? '}' : '>';
        score[line[i]] += 1;
        Console.WriteLine($"Line corrupt: {line.Aggregate("", (agg, x) => agg + x)} - Expected {expected}, but found {line[i]} instead.");
        charStack.Clear();
        break;
    }
    if (charStack.Count > 0) {
        string missing = "";
        while (charStack.Count > 0) {
            char pop = charStack.Pop();
            char expected = pop == '(' ? ')' : pop == '[' ? ']' : pop == '{' ? '}' : '>';
            missing += expected;
        }
        Console.WriteLine($"{line.Aggregate("", (agg, x) => agg + x)} - Complete by adding {missing}.");
        scores.Add(missing.ToCharArray().Aggregate((long) 0, (agg, x) => 5 * agg + points[x]));
    }
    charStack.Clear();
}

Console.WriteLine($"Part1: {3 * score[')'] + 57 * score[']'] + 1197 * score['}'] + 25137 * score['>']}");
scores.Sort();
Console.WriteLine($"Part2: {scores[scores.Count / 2]}");
