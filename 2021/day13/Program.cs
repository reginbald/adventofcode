string[] data = File.ReadAllLines("data.txt");
List<string> instructions  = new List<string>();
int maxY = 0, maxX = 0;
bool inst = false;

// Extract instructions and find max values
foreach(string line in data) { 
    if (string.IsNullOrEmpty(line)) {
        inst = true;
    } else if (inst) {
        instructions.Add(line.Split(" ")[2]);
    } else {
        int[] tmp = line.Split(",").Select(int.Parse).ToArray();
        maxX = maxX < tmp[0] ? tmp[0] : maxX;
        maxY = maxY < tmp[1] ? tmp[1] : maxY;
    }
}

// Construct paper grid
int[,] paper = new int[maxY + 1, maxX + 1];
foreach (var line in data)
{
    if (string.IsNullOrEmpty(line)) break;
    int[] coords = line.Split(",").Select(int.Parse).ToArray();
    paper[coords[1], coords[0]] = 1;
}

// Perform instructions
int printX = 0, printY = 0;
foreach(string instruction in instructions) {
    string[] tmp = instruction.Split("=");
    if(tmp[0] == "x") {
        int fold = int.Parse(tmp[1]);
        printX = fold;
        for (int y = 0; y < paper.GetLength(0); y++) {
            for (int x = fold; x < paper.GetLength(1); x++) {
                if (x == fold) {
                    paper[y,x] = -1;
                } else {
                    if (fold - (x - fold) >= 0) {
                        paper[y, fold - (x - fold)] += paper[y, x];
                    }
                    paper[y,x] = -1;
                }
            }
        }
    } else {
        int fold = int.Parse(tmp[1]);
        printY = fold;
        for (int y = fold; y < paper.GetLength(0); y++) {
            for (int x = 0; x < paper.GetLength(1); x++) {
                if (y == fold) {
                    paper[y,x] = -1;
                } else {
                    if (fold - (y - fold) >= 0) {
                        paper[fold - (y - fold), x] += paper[y, x];
                    }
                    paper[y,x] = -1;
                }
            }
        }
    }
}

Console.WriteLine($"Dots left: {paper.Cast<int>().Aggregate(0, (agg, x) => agg + (x > 0 ? 1 : 0))}");
Print(printY, printX);

void Print(int maxY, int maxX) { 
    string output = "";
    for (int y = 0; y < maxY; y++)
    {
        for (int x = 0; x < maxX; x++)
        {
            output += paper[y,x] > 0 ? "#" : paper[y,x] < 0 ? "-" : ".";
        }
        output += System.Environment.NewLine;
    }
    Console.WriteLine(output);
}