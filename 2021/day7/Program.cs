int[] positions =  File.ReadAllLines("data.txt")[0]
    .Split(',').Select(x => int.Parse(x)).ToArray();

int median = Median(positions);
int sum = positions.Aggregate(0, (acc, x) => acc + Math.Abs(x - median));
Console.WriteLine($"Part1: {sum}");

// Part 2
int avg = (int) Math.Floor(positions.Average());
sum = positions.Aggregate(0, (acc, x) => acc + TriangularNumber(Math.Abs(x - avg)));
Console.WriteLine($"Part2: {sum}");

int Median(int[] array) {
  Array.Sort(array);
  return array[array.Length / 2];
}

int TriangularNumber(int n){
    return n*(n+1)/2;
}