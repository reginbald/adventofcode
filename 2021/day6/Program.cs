int[] data =  File.ReadAllLines("data.txt")[0]
    .Split(',').Select(x => int.Parse(x)).ToArray();

long[] fishes = new long[9];
foreach (int num in data)
{
    fishes[num] += 1;
}

for (int day = 0; day <= 256; day++)
{
    Console.WriteLine($"Day {day}: {fishes.Sum()}");
    long[] tmp = new long[9];
    for (int i = fishes.Length - 1; i >= 0 ; i--)
    {   
        if(i == 0) {
            tmp[8] += fishes[0];
            tmp[6] += fishes[0];
        } else {
            tmp[i - 1] += fishes[i];
        }
    }
    fishes = tmp;
}