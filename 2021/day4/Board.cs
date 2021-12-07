namespace Reginbald.day4;

using System.Linq;
using System.Text.RegularExpressions;

public class Board {
    private string[,] board = new string[5,5];
    private bool[,] score = new bool[5,5];
    
    public Board(string[] rows) {
        for (int i = 0; i < rows.Length; i++) {
            for (int j = 0; j < 5; j++) {
                this.board[i,j] = rows[i].Substring(j * 3, 2);
            }
        }
        
    }

    public void Mark(int number) {
        for (int i = 0; i < this.board.GetLength(0); i++) {
            for(int j = 0; j < this.board.GetLength(1); j++) {
                if(int.Parse(this.board[i,j]) == number) {
                    this.score[i,j] = true;
                }
            }
        }
    }

    public bool CheckWin(){
        for(int i = 0; i < this.score.GetLength(0); i++){
            bool row = Enumerable.Range(0, this.score.GetLength(0))
                .Select(x => this.score[i, x])
                .ToArray().All(x => x);
            if (row) return true;
            bool column =Enumerable.Range(0, this.score.GetLength(1))
                .Select(x => this.score[x, i])
                .ToArray().All(x => x);
                
            if (column) return true;
        }
        return false;
    }

    public int Score(int number) {
        int sumUnmarked = 0;
        for (int i = 0; i < this.board.GetLength(0); i++) {
            for(int j = 0; j < this.board.GetLength(1); j++) {
                if(!this.score[i,j]) {
                    sumUnmarked += int.Parse(this.board[i,j]);
                }
            }
        }
        return number * sumUnmarked;
    }

    public override string ToString() {
        string output = $"---------------{System.Environment.NewLine}";
        for (int i = 0; i < this.board.GetLength(0); i++) {
            for(int j = 0; j < this.board.GetLength(1); j++) {
                if(this.score[i,j]){
                    output += $"{Regex.Replace( this.board[i,j], "[0-9]", "X" )} ";
                } else {
                    output += $"{this.board[i,j]} ";
                }
            }
            output += System.Environment.NewLine;
        }
        output += "---------------";
        return output;
    }
}