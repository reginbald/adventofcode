using Reginbald.day4;

string[] rows =  File.ReadAllLines("data.txt");

int[] numbers = rows[0].Split(',').Select(int.Parse).ToArray();

List<Board> boards = new List<Board>();

for (int i = 2; i < rows.Length; i = i + 6) {
    boards.Add(new Board(rows[i..(i+5)]));
}

bool win = false;
foreach (int number in numbers) {
    foreach (Board board in boards) {
        board.Mark(number);
        if(board.CheckWin()) {
            win = true;
            Console.WriteLine(board.Score(number));
            Console.WriteLine(board);
            break;
        }
    }
    if(win) break;
}

// Part 2
win = false;
foreach (int number in numbers) {
    for (int i = boards.Count() - 1; i >= 0; i--)
    {
        boards[i].Mark(number);
        if(boards[i].CheckWin()) {
            if (boards.Count == 1){
                Console.WriteLine(boards[i].Score(number));
                Console.WriteLine(boards[i]);
                win = true;
            }
            boards.RemoveAt(i);
        }
    }
    if(win) break;
}