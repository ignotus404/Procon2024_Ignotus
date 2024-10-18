using System;
using _2024ProconTemporary.Com;
using _2024ProconTemporary.ReadableData;

namespace _2024ProconTemporary.CommandLine.Commands
{
    public class SolveAlgorithm
    {
        public AnswerData Solve(ReadableProblemData problemData, IReadOnlyList<ReadablePatternData> dieList)
        {
            Console.WriteLine("回答を計算しています...");
            AnswerData answerData = Answer.Create();
            // ここに回答アルゴリズムを記述
            foreach (var die in dieList)
            {
                for (int y = -die.Height; y < problemData.Board.Height; y++)
                {
                    for (int x = -die.Width; x < problemData.Board.Width; x++)
                    {

                    }
                }
            }

            return answerData;
        }

        private List<List<int>> DieCuttingUp(List<List<int>> board, ReadablePatternData die, int x, int y)
        {
            List<List<int>> newBoard = new List<List<int>>();
            List<List<int>> cutBoard = board.ToList();
            List<List<int>> CuttingBoardPiece = new List<List<int>>();
            DieCutting(board, die, x, y, out CuttingBoardPiece, out cutBoard);

            for (int dy = 0; dy < die.Height; dy++)
            {
                List<int> row = new List<int>();
                for (int dx = 0; dx < die.Width; dx++)
                {
                    if (x + dx >= 0 && x + dx < board[0].Count && y + dy >= 0 && y + dy < board.Count)
                    {
                        row.Add(board[y + dy][x + dx]);
                    }
                }
                newBoard.Add(row);
            }


            return newBoard;
        }

        private void DieCutting(List<List<int>> board, ReadablePatternData die, int x, int y, out List<List<int>> CuttingBoardPiece, out List<List<int>> cutBoard)
        {
            cutBoard = board.ToList();
            CuttingBoardPiece = new List<List<int>>();
            for (int dy = 0; dy < die.Height; dy++)
            {
                List<int> row = new List<int>();
                for (int dx = 0; dx < die.Width; dx++)
                {
                    if (x + dx >= 0 && x + dx < board[0].Count && y + dy >= 0 && y + dy < board.Count)
                    {
                        row.Add(board[y + dy][x + dx]);
                        cutBoard[y + dy][x + dx] = -1;
                    }
                }
                CuttingBoardPiece.Add(row);
            }
        }
    }
}