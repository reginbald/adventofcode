namespace Reginbald.day5;

public class Map {
    private int[,] map = new int[10,10];

    public int Intersections { get; private set; }

    public Map() {

    }

    public void AddLine(string line, bool vhOnly = true) {
        var cords = line.Split(" -> ").Select(x => x.Split(",")).ToArray();
        int x1 = int.Parse(cords[0][0]);
        int y1 = int.Parse(cords[0][1]);
        int x2 = int.Parse(cords[1][0]);
        int y2 = int.Parse(cords[1][1]);

        if (vhOnly && x1 != x2 && y1 != y2) return;

        int max = (new int[]{x1, x2, y1, y2}).Max();
        if (max > this.map.Length) this.ResizeMap(max);
        
        if (x1 == x2 || y1 == y2){
            // Vertical or Horizontal
            if (x1 > x2 || y1 > y2) {
                int tmp = x1; x1 = x2; x2 = tmp;
                tmp = y1; y1 = y2; y2 = tmp;
            } 
            for (int x = x1; x <= x2; x++) {
                for (int y = y1; y <= y2; y++) {
                    this.map[x, y] += 1;
                    if (this.map[x, y] == 2) this.Intersections += 1;
                }
            }
        }
        else {
            // Diagonal
            this.map[x1, y1] += 1;
            if (this.map[x1, y1] == 2) this.Intersections += 1;
            this.map[x2, y2] += 1;
            if (this.map[x2, y2] == 2) this.Intersections += 1;

            if (x1 < x2 && y1 < y2) {
                for (int i = 1; i < x2 - x1; i++) {
                    this.map[x1 + i, y1 + i] += 1;
                    if (this.map[x1 + i, y1 + i] == 2) this.Intersections += 1;
                }
            } else if (x1 < x2 && y1 > y2) {    
                for (int i = 1; i < x2 - x1; i++) {
                    this.map[x1 + i, y1 - i] += 1;
                    if (this.map[x1 + i, y1 - i] == 2) this.Intersections += 1;
                }
            } else if (x1 > x2 && y1 < y2) {    
                for (int i = 1; i < x1 - x2; i++) {
                    this.map[x2 + i, y2 - i] += 1;
                    if (this.map[x2 + i, y2 - i] == 2) this.Intersections += 1;
                }
             } else {    
                for (int i = 1; i < x1 - x2; i++) {
                    this.map[x2 + i, y2 + i] += 1;
                    if (this.map[x2 + i, y2 + i] == 2) this.Intersections += 1;
                }
            }
        }
    }

    public override string ToString()
    {
        string output = "";
        for (int x = 0; x < this.map.GetLength(0); x++)
        {
            for (int y = 0; y < this.map.GetLength(1); y++)
            {
                output += this.map[y, x];
            }
            output += System.Environment.NewLine;
        }
        return output;
    }

    private void ResizeMap(int x){
        int val = (int) (Math.Pow(10, Math.Ceiling(Math.Log10(x))));

        int[,] tmp = new int[val, val];
        for (int i = 0; i < this.map.GetLength(0); ++i)
            Array.Copy(this.map, i * this.map.GetLength(0), tmp, i * val, this.map.GetLength(0));
        
        Console.WriteLine($"Map resized: {this.map.Length} -> {val}");
        this.map = tmp;
    }
}