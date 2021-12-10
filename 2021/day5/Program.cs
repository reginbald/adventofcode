using Reginbald.day5;

string[] rows =  File.ReadAllLines("data.txt");

Map map = new Map();

foreach (var row in rows){
    map.AddLine(row);
}

Console.WriteLine(map.Intersections);

// part2
map = new Map();

foreach (var row in rows){
    map.AddLine(row, false);
}

Console.WriteLine(map.Intersections);
