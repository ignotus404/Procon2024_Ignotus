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

            // 問題データ、回答データをstringの配列からintの2次元配列に変換する
            Console.WriteLine("Converting Problem Data...");
            ReadableProblemData convertedProblemData = new ReadableProblemData(problemData);
            Console.WriteLine("Done!");
            convertedProblemData.Print();

            // 回答データの初期化
            AnswerData answerData = Answer.Create();

            // 手動操作モード用の抜き型を列挙したリストを作成
            List<ReadablePatternData> dieList = CreateDieList(convertedProblemData);

            // デバッグ用
            Console.WriteLine("DieList:");
            foreach (var die in dieList)
            {
                Console.WriteLine($"P: {die.P}");
                Console.WriteLine($"Width: {die.Width}");
                Console.WriteLine($"Height: {die.Height}");
                Console.WriteLine("Cells:");
                foreach (var cell in die.Cells)
                {
                    Console.WriteLine(string.Join(" ", cell));
                }
            }
            // ここまで


            // 手動で回答を作成するモードに移行する
            if (args.isManual)
            {
                Console.WriteLine("手動回答モードに移行します");
                ManualMode(convertedProblemData, dieList);
            }

            else
            {
                // 自動回答モードに移行する
                Console.WriteLine("回答を計算しています...");
                // ここで問題データをMainAlgorithmに渡して、回答を計算する(引数はReadableProblemData型)
                var _mainalgorithm = new Mainalgorithm();
                _mainalgorithm.Calculation(problemData);

                Console.WriteLine("Done!");

                // 回答結果を表示する(間違っている場所、かかった手数など) 未実装
                // CompareAnswers(convertedProblemData, );




                // これで提出するか聞く
                Console.WriteLine("これで提出しますか? (Y/n)");
                string input = Console.ReadLine() ?? "";

                if (input == "N" || input == "n")
                {
                    // 手動で回答を作成するモードに移行する
                    Console.WriteLine("手動回答モードに移行します");
                    answerData = ManualMode(convertedProblemData, dieList);
                }

                // 回答を提出する
                Console.WriteLine("回答を提出しています...");
                Networking.SendAnswerData(client, answerData);
                Console.WriteLine("Done!");
            }
        }

        /// <summary>
        /// 手動操作モード用の抜き型を列挙したリストを作成
        /// </summary>
        /// <param name="problemData">使用する問題データ</param>
        /// <returns>一般抜き型と特殊抜き型をあわせた二次元配列の配列</returns>
        List<ReadablePatternData> CreateDieList(in ReadableProblemData problemData)
        {
            List<ReadablePatternData> dieList = new List<ReadablePatternData>();
            // 一般抜き型を追加
            dieList.Add(new ReadablePatternData(1, 1, 1, new List<List<int>> { new List<int> { 1 } }));
            for (int i = 2; i < 257; i *= 2)
            {
                for (int j = 0; j < 3; j++)
                {
                    List<List<int>> cell = new List<List<int>>();
                    switch (j)
                    {
                        case 0:
                            cell = CreateGeneralCuttingDieTypeI(i);
                            break;
                        case 1:
                            cell = CreateGeneralCuttingDieTypeII(i);
                            break;
                        case 2:
                            cell = CreateGeneralCuttingDieTypeIII(i);
                            break;
                    }
                    dieList.Add(new ReadablePatternData(dieList.Count + 1, i, i, cell));
                }
            }

            // 特殊抜き型を追加
            for (int i = 0; i < problemData.General.N; i++)
            {
                dieList.Add(problemData.General.Patterns[i]);
            }
            return dieList;
        }

        List<List<int>> CreateGeneralCuttingDieTypeI(int size)
        {
            List<List<int>> die = new List<List<int>>();
            for (int y = 0; y < size; y++)
            {
                List<int> row = new List<int>();
                for (int x = 0; x < size; x++)
                {
                    row.Add(1);
                }
                die.Add(row);
            }
            return die;
        }

        List<List<int>> CreateGeneralCuttingDieTypeII(int size)
        {
            List<List<int>> die = new List<List<int>>();
            for (int y = 0; y < size; y++)
            {
                List<int> row = new List<int>();
                for (int x = 0; x < size; x++)
                {
                    if (y % 2 == 0) row.Add(1);
                    else row.Add(0);
                }
                die.Add(row);
            }

            return die;
        }

        List<List<int>> CreateGeneralCuttingDieTypeIII(int size)
        {
            List<List<int>> die = new List<List<int>>();
            for (int y = 0; y < size; y++)
            {
                List<int> row = new List<int>();
                for (int x = 0; x < size; x++)
                {
                    if (x % 2 == 0) row.Add(1);
                    else row.Add(0);
                }
                die.Add(row);
            }
            return die;
        }
        /// <summary>
        /// 手動で回答を作成するモードに移行する 未実装
        /// </summary>
        /// <param name="problemData">使用する問題データ</param>
        /// <param name="dieList">使用する抜き型のリスト</param>
        /// <param name="board">初期盤面</param>
        /// <returns>手動操作モードで作成した回答データ</returns>
        AnswerData ManualMode(ReadableProblemData problemData, IReadOnlyList<ReadablePatternData> dieList, List<List<int>> board = null)
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
                ShowNowBoard(problemData, dieList, board, cursorPosition.left, cursorPosition.top, useDieIndex, diePosition.x, diePosition.y);
                Console.WriteLine("Please press key...");
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (isMovingMode) Console.WriteLine("抜いたあとに詰める方向を選択してください");
                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        Console.WriteLine("これで提出しますか? (Y/n)");
                        string input = Console.ReadLine() ?? "";
                        if (input == "N" || input == "n") break;
                        else if (input == "Y" || input == "y" || input == "") return answerData;
                        break;

                    // 抜く場所の移動
                    case ConsoleKey.W:
                        if (isMovingMode)
                        {
                            history.Add(board);
                            board = Case.DieCuttingUP(board, dieList[useDieIndex].Cells, diePosition.x, diePosition.y, 0, 0);
                            answerData.N++;
                            answerData.Ops.Add(new AnswerData.OperationData { P = useDieIndex, X = diePosition.x, Y = diePosition.y, S = AnswerData.OperationData.Side.Up });
                            isMovingMode = !isMovingMode;
                        }
                        else if (diePosition.y > -(dieList[useDieIndex].Height - 1)) diePosition.y--;
                        break;

                    case ConsoleKey.S:
                        if (isMovingMode)
                        {
                            history.Add(board);
                            board = Case.DieCuttingDown(board, dieList[useDieIndex].Cells, diePosition.x, diePosition.y, 0, 0);
                            answerData.N++;
                            answerData.Ops.Add(new AnswerData.OperationData { P = useDieIndex, X = diePosition.x, Y = diePosition.y, S = AnswerData.OperationData.Side.Down });
                            isMovingMode = !isMovingMode;
                        }
                        else if (diePosition.y < problemData.Board.Height - 1) diePosition.y++;
                        break;

                    case ConsoleKey.A:
                        if (isMovingMode)
                        {
                            history.Add(board);
                            board = Case.DieCuttingLeft(board, dieList[useDieIndex].Cells, diePosition.x, diePosition.y, 0, 0);
                            answerData.N++;
                            answerData.Ops.Add(new AnswerData.OperationData { P = useDieIndex, X = diePosition.x, Y = diePosition.y, S = AnswerData.OperationData.Side.Left });
                            isMovingMode = !isMovingMode;
                        }
                        else if (diePosition.x > -(dieList[useDieIndex].Width - 1)) diePosition.x--;
                        break;

                    case ConsoleKey.D:
                        if (isMovingMode)
                        {
                            history.Add(board);
                            board = Case.DieCuttingRight(board, dieList[useDieIndex].Cells, diePosition.x, diePosition.y, 0, 0);
                            answerData.N++;
                            answerData.Ops.Add(new AnswerData.OperationData { P = useDieIndex, X = diePosition.x, Y = diePosition.y, S = AnswerData.OperationData.Side.Right });
                            isMovingMode = !isMovingMode;
                        }
                        else if (diePosition.x < problemData.Board.Width - 1) diePosition.x++;
                        break;

                    // 型の選択
                    case ConsoleKey.LeftArrow:
                        useDieIndex = (useDieIndex - 1 + dieList.Count) % dieList.Count;
                        break;

                    case ConsoleKey.RightArrow:
                        useDieIndex = (useDieIndex + 1) % dieList.Count;
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

                // デバッグ用
                Console.WriteLine($"useDieIndex: {useDieIndex}");
                Console.WriteLine($"DiePosition: {diePosition.x}, {diePosition.y}");
                Console.WriteLine($"Cursor: {cursorPosition.left}, {cursorPosition.top}");
                // ここまで

            }
        }

        void ShowNowBoard(ReadableProblemData problemData, IReadOnlyList<ReadablePatternData> dieList, List<List<int>> board, int cursorLeft, int cursorTop, int useDieIndex, int diePositionX, int diePositionY)
        {
            Console.SetCursorPosition(cursorLeft, cursorTop);
            for (int y = 0; y < board.Count; y++)
            {
                for (int x = 0; x < board[y].Count; x++)
                {
                    try
                    {
                        if
                        (
                        y >= diePositionY &&
                        y < diePositionY + dieList[useDieIndex].Height &&
                        x >= diePositionX &&
                        x < diePositionX + dieList[useDieIndex].Width &&
                        dieList[useDieIndex].Cells[y - diePositionY][x - diePositionX] == 1
                        )
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(board[y][x]);
                            Console.ResetColor();
                        }
                        else Console.Write(board[y][x]);
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error");
                        Console.WriteLine(e.Message);
                        Console.WriteLine($"y: {y}, x: {x}");
                        Console.WriteLine($"diePositionY: {diePositionY}, diePositionX: {diePositionX}");
                        Console.WriteLine($"y - diePositionY: {y - diePositionY}, x - diePositionX: {x - diePositionX}");
                        Console.WriteLine($"dieList[useDieIndex].Height: {dieList[useDieIndex].Height}, dieList[useDieIndex].Width: {dieList[useDieIndex].Width}");
                    }

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