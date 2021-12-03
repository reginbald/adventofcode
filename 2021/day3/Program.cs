string[] rows =  File.ReadAllLines("data.txt");

// Part 1
int[] count = new int[rows[0].Length];
foreach (string row in rows){
    for(int i = 0; i < row.Length; i++){
        count[i] += row[i] - '0';
    }
}

string gamma = "";
string epsilon = "";
foreach(int c in count){
    gamma += (float) c / rows.Length >= 0.5 ? "1" : "0";
    epsilon += (float) c / rows.Length < 0.5 ? "1" : "0";
}

Console.WriteLine(Convert.ToInt64(gamma, 2) * Convert.ToInt64(epsilon, 2));

// Part 2
List<string> oxy = rows.ToList();
for(int y = 0; y < count.Length; y++) {
    int c = 0;
    foreach (string row in oxy){
            c += row[y] - '0';
    }
    char mc = (float) c / oxy.Count() >= 0.5 ? '1' : '0';
    for (int i = oxy.Count() - 1; i >= 0; i--)
    {
        if (oxy.Count() == 1) break;
        if (oxy[i][y] != mc) {
            oxy.RemoveAt(i);
        }
    }
    if (oxy.Count() == 1) break;
}
List<string> co2 = rows.ToList();
for(int y = 0; y < count.Length; y++) {
    int c = 0;
    foreach (string row in co2){
            c += row[y] - '0';
    }
    char lc = (float) c / co2.Count() < 0.5 ? '1' : '0';
    for (int i = co2.Count() - 1; i >= 0; i--)
    {
        if (co2.Count() == 1) break;
        if (co2[i][y] != lc) {
            co2.RemoveAt(i);
        }
    }
    if (co2.Count() == 1) break;
}
Console.WriteLine(Convert.ToInt64(oxy[0], 2) * Convert.ToInt64(co2[0], 2));
