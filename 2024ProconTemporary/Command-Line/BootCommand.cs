using EntryPoint;
using System;
using _2024ProconTemporary.ReadableData;
using _2024ProconTemporary.Com;
using _2024ProconTemporary;
using System.Linq;
using System.Formats.Asn1;


namespace _2024ProconTemporary.CommandLine.Commands
{
    public class BootCommandClient : BaseCliArguments
    {
        public BootCommandClient() : base("Initialize Command") { }

        [OptionParameter(ShortName: 'v', LongName: "view")]
        [Help("問題データを表示します")]
        public bool isView { get; set; }

        [OptionParameter(ShortName: 'm', LongName: "manual")]
        [Help("手動で回答を作成するモードに移行します")]
        public bool isManual { get; set; }
    }

    public class BootCommand
    {
        public void Handle(BootCommandClient args)
        {
            // 問題データを取得する
            Console.WriteLine("問題データを取得しています...");
            HttpClient client = Networking.CreateClient();
            var problemData = Networking.GetProblemData(client);

            if (problemData == null)
            {
                Console.WriteLine("エラー: ProblemDataのダウンロードに失敗しました!");
                Console.WriteLine("ネットワーク接続を確認して、もう一度お試しください。");
                return;
            }
            Console.WriteLine("Done!");

            Console.WriteLine("Converting Problem Data...");
            // 問題データ、回答データをstringの配列からintの2次元配列に変換する
            ReadableProblemData convertedProblemData = new ReadableProblemData(problemData);
            Console.WriteLine("Done!");

            convertedProblemData.Print();

            AnswerData answerData = Answer.Create();

            // 手動で回答を作成するモードに移行する
            if (args.isManual)
            {
                Console.WriteLine("手動回答モードに移行します");
                ManualMode(convertedProblemData);
            }

            else
            {
                // 自動回答モードに移行する
                Console.WriteLine("回答を計算しています...");
                // ここで問題データをMainAlgorithmに渡して、回答を計算する(引数はReadableProblemData型)
                // Mainalgorithm.MatchCalculate();

                Console.WriteLine("Done!");

                // 回答結果を表示する(間違っている場所、かかった手数など) 未実装
                // CompareAnswers(convertedProblemData, );

                // 手動で回答を作成するモードに移行する
                Console.WriteLine("手動回答モードに移行します");
                answerData = ManualMode(convertedProblemData);

                // これで提出するか聞く
                Console.WriteLine("これで提出しますか? (Y/n)");
                string input = Console.ReadLine() ?? "";

                if (input == "N" || input == "n")
                {
                    // 手動で回答を作成するモードに移行する
                    Console.WriteLine("手動回答モードに移行します");
                    answerData = ManualMode(convertedProblemData);
                }

                // 回答を提出する
                Console.WriteLine("回答を提出しています...");
                Networking.SendAnswerData(client, answerData);
                Console.WriteLine("Done!");

            }
        }


        /// <summary>
        /// 手動で回答を作成するモードに移行する 未実装
        /// </summary>
        /// <param name="problemData">使用する問題データ</param>
        /// <returns></returns>
        AnswerData ManualMode(ReadableProblemData problemData, List<List<int>> board = null)
        {

            AnswerData answerData = Answer.Create();
            if (board == null) board = problemData.Board.Start.ToList();

            int useDieIndex = 0;
            bool isMovingMode = false;
            (int left, int top) cursorPosition = (Console.CursorLeft, Console.CursorTop);
            (int x, int y) diePosition = (0, 0);
            List<List<List<int>>> history = new List<List<List<int>>>();
            Console.WriteLine("Manual Mode");
            Console.WriteLine("Please input answer.");



            // キー入力を受付、回答を作成する
            while (true)
            {
                ShowNowBoard(problemData, board, cursorPosition.left, cursorPosition.top, useDieIndex, diePosition.x, diePosition.y);
                Console.WriteLine("Please press key...");
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (isMovingMode) Console.WriteLine("抜いたあとに詰める方向を選択してください");
                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        Console.WriteLine("これで提出しますか? (Y/n)");
                        string input = Console.ReadLine() ?? "";
                        if (input == "N" || input == "n") break;
                        else return answerData;

                    // 抜く場所の移動
                    case ConsoleKey.W:
                        if (isMovingMode)
                        {
                            history.Add(board);
                            board = Case.DieCuttingUP(board, ConvertCellsToFloatArray(problemData.General.Patterns[useDieIndex].Cells), diePosition.x, diePosition.y, 0, 0);
                        }
                        else if (diePosition.y > -(problemData.General.Patterns[useDieIndex].Height)) diePosition.y--;
                        break;

                    case ConsoleKey.S:
                        if (isMovingMode)
                        {
                            history.Add(board);
                            board = Case.DieCuttingDown(board, ConvertCellsToFloatArray(problemData.General.Patterns[useDieIndex].Cells), diePosition.x, diePosition.y, 0, 0);
                        }
                        else if (diePosition.y < problemData.Board.Height - 1) diePosition.y++;
                        break;

                    case ConsoleKey.A:
                        if (isMovingMode)
                        {
                            history.Add(board);
                            board = Case.DieCuttingLeft(board, ConvertCellsToFloatArray(problemData.General.Patterns[useDieIndex].Cells), diePosition.x, diePosition.y, 0, 0);
                        }
                        else if (diePosition.x > -(problemData.General.Patterns[useDieIndex].Width)) diePosition.x--;
                        break;

                    case ConsoleKey.D:
                        if (isMovingMode)
                        {
                            history.Add(board);
                            board = Case.DieCuttingRight(board, ConvertCellsToFloatArray(problemData.General.Patterns[useDieIndex].Cells), diePosition.x, diePosition.y, 0, 0);
                        }
                        else if (diePosition.x < problemData.Board.Width - 1) diePosition.x++;
                        break;

                    // 型の選択
                    case ConsoleKey.LeftArrow:
                        useDieIndex = (useDieIndex - 1 + problemData.General.N) % problemData.General.N;
                        break;

                    case ConsoleKey.RightArrow:
                        useDieIndex = (useDieIndex + 1) % problemData.General.N;
                        break;

                    // 決定
                    case ConsoleKey.Enter:
                        isMovingMode = !isMovingMode;
                        break;

                    case ConsoleKey.H:
                        Console.WriteLine("Help");
                        break;

                    case ConsoleKey.Backspace:
                        break;

                }

            }
        }

        public float[,] ConvertCellsToFloatArray(IList<List<int>> cells)
        {
            int rows = cells.Count;
            int cols = cells[0].Count;
            float[,] array = new float[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = (float)cells[i][j];
                }
            }

            return array;
        }

        void ShowNowBoard(ReadableProblemData problemData, List<List<int>> board, int cursorLeft, int cursorTop, int useDieIndex, int diePositionX, int diePositionY)
        {
            Console.SetCursorPosition(cursorLeft, cursorTop);
            for (int y = 0; y < board.Count; y++)
            {
                for (int x = 0; x < board[y].Count; x++)
                {
                    if (y > diePositionY && y < diePositionY + problemData.General.Patterns[useDieIndex].Height && x > diePositionX && x < diePositionX + problemData.General.Patterns[useDieIndex].Width)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(board[y][x]);
                        Console.ResetColor();
                    }
                    else Console.Write(board[y][x]);
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// 回答結果を表示する
        /// </summary>
        /// <param name="problemData">問題データ</param>
        /// <param name="answerData">回答データ</param>
        /// <param name="answerBoard">回答データ通りに動かした際のBoardを2次元配列に変換したもの</param>
        void CompareAnswers(ReadableProblemData problemData, AnswerData answerData, List<List<int>> answerBoard)
        {
            int collectPieceNum = 0;
            // 問題データと回答データを比較する
            for (int y = 0; y < problemData.Board.Height; y++)
            {
                for (int x = 0; x < problemData.Board.Width; x++)
                {
                    if (problemData.Board.Goal[y][x] != answerBoard[y][x])
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(answerBoard[y][x] + " ");
                        Console.ResetColor();
                    }

                    else
                    {
                        collectPieceNum++;
                        Console.Write(answerBoard[y][x] + " ");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine($"一致率: {collectPieceNum / (problemData.Board.Width * problemData.Board.Height) * 100}%");
            Console.WriteLine($"一致ピース数: {collectPieceNum}");
            Console.WriteLine($"かかった手数: {answerData.N}");
        }
    }


}