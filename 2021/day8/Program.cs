var data =  File.ReadAllLines("data.txt")
    .Select(x => x.Split(" | ")).Select(x => (x[0].Split(" "), x[1].Split(" "))).ToArray();

int[] count = new int[10];
int sum = 0;
foreach ((string[] input, string[] output) in data) {
    int[] numbers = Numbers(input, output);
    for (int i = 0; i < numbers.Length; i++) {
        count[numbers[i]] += 1;
    }
    sum += numbers.Select((num, i) => num * (int) Math.Pow(10, numbers.Length - i - 1)).Sum();
}

// Part 1
Console.WriteLine($"Part1: {count[1] + count[4] + count[7] + count[8]}");
// Part2
Console.WriteLine($"Part2: {sum}");

int[] Numbers(string[] inputs, string[] outputs) {
    int[] output = new int[4];
    HashSet<char>[] numbers = new HashSet<char>[10].Select(h => new HashSet<char>()).ToArray();
    Queue<string> queue = new Queue<string>(inputs);

    while (queue.Count > 0) { // Find mapping between input and number 
        string input = queue.Dequeue();
        HashSet<char> arr = new HashSet<char>(input.ToCharArray());

        if (arr.Count == 2) {
            numbers[1].UnionWith(arr); continue;
        } else if(arr.Count == 3) {
            numbers[7].UnionWith(arr); continue; 
        } else if(arr.Count == 4) {
            numbers[4].UnionWith(arr); continue;
        } else if(arr.Count == 5) {
            if ( numbers[7].Count > 0 && numbers[7].IsSubsetOf(arr) ) {
                numbers[3].UnionWith(arr); continue;
            } else if(numbers[1].Count > 0 && numbers[6].Count > 0) {
                if (new HashSet<char>(numbers[1].Intersect(numbers[6])).IsSubsetOf(arr)) {
                    numbers[5].UnionWith(arr); continue;
                } else {
                    numbers[2].UnionWith(arr); continue;
                }   
            }
        } else if (arr.Count == 6) {
            if (numbers[4].Count > 0) {
                if(numbers[4].IsSubsetOf(arr)){
                    numbers[9].UnionWith(arr); continue;
                } else if (numbers[7].Count > 0){
                    if (numbers[7].IsSubsetOf(arr)){
                        numbers[0].UnionWith(arr); continue;
                    } else {
                        numbers[6].UnionWith(arr); continue;
                    }
                }
            }
        } else if(arr.Count == 7) {
            numbers[8].UnionWith(arr); continue;  
        }
        queue.Enqueue(input);   
    }

    for (int i = 0; i < outputs.Length; i++) { // Map output to number
        HashSet<char> arr = new HashSet<char>(outputs[i].ToCharArray());
        for (int y = 0; y < numbers.Length; y++) {
            if(numbers[y].SetEquals(arr)) {
                output[i] = y;
            }
        }
    }
    return output;
}