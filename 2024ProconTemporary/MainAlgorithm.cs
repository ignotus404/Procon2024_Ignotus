using _2024ProconTemporary.Com;
using _2024ProconTemporary.ReadableData;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace _2024ProconTemporary
{

    public class Mainalgorithm
    {

        Case cases = new Case();
        Practice practice = new Practice();
        Pattern Pattern = new Pattern();
        static Random rnd = new Random();
        static int[][] WantListX = new int[266][];
        static List<List<int>> WantListXT = new List<List<int>>();
        static int[][] WantListXS = new int[266][];
        static int[][] WantListY = new int[4][];
        static List<List<List<int>>> NumberIndexList = new List<List<List<int>>>();
        static List<List<List<int>>> ZeroIndexList = new List<List<List<int>>>();
        static List<List<List<int>>> OneIndexList = new List<List<List<int>>>();
        static List<int> PatternSizeListOver = new List<int>();
        static List<int> PatternSizeListNotOver = new List<int>();
        static List<List<List<List<int>>>> PatternZeroDifferenceList = new List<List<List<List<int>>>>();
        static List<List<List<List<int>>>> PatternOneDifferenceList = new List<List<List<List<int>>>>();
        delegate List<List<int>> DieCutting(List<List<int>> queues, List<List<int>> pattern, int pointX, int pointY, int PatternX, int PatternY);
        public static int Ymax = 0;
        public static int Xmax = 0;

        public static void Main()
        {
            Pattern.Patterns();
            Practice.Practices();
            //dieCutting = Case.DieCuttingUP;
            //PatternDifferenceValue(Pattern.PatternList);
            //CalculationTest(0,0);
            //FirstSort(Case.TranslatePos(Practice.QuesTes),Practice.AnsTes, dieCutting);
            CalculationTest(Practice.QuesTes, Practice.AnsTes);
            Test();
        }
        public (AnswerData, List<List<int>>) Calculation(ProblemData problemData)
        {
            int N = 1000;
            int MaxN = 0;
            var MaxOps = new List<AnswerData.OperationData>();
            var Ops = new List<AnswerData.OperationData>();
            DieCutting dieCutting;
            ReadableProblemData readableProblemData = new ReadableProblemData(problemData);
            Ymax = problemData.Board.Height;
            Xmax = problemData.Board.Width;
            var queses = Case.Copy(readableProblemData.Board.Start.ToList());
            var answer = Case.Copy(readableProblemData.Board.Goal.ToList());

            var PatternList = new List<List<List<int>>>();
            var K = readableProblemData.General.Patterns.Count;
            foreach (var p in readableProblemData.General.Patterns)
            {
                PatternList.Add(new List<List<int>>());
                PatternList[p.P] = Case.Copy(p.Cells.ToList());
            }
            PatternCount(queses, Pattern.PatternList);
            PatternDifferenceValue(PatternList);
            for (int direction = 0; direction < 4; direction++)
            {
                MaxN = 0;
                if (direction == 0 || direction == 1)
                {
                    dieCutting = Case.DieCuttingUP;
                }
                else
                {
                    dieCutting = Case.DieCuttingDown;
                }
                var Items = FirstSort(queses, answer, dieCutting, Ops, PatternList);
                queses = Items.Item1;
                N = Items.Item2;
                if (direction == 0 || direction == 3)
                {
                    dieCutting = Case.DieCuttingLeft;
                }
                else
                {
                    dieCutting = Case.DieCuttingRight;
                }
                for (float Match = 0; Match < 95;)
                {
                    IndexCount(queses, Xmax, Ymax);
                    queses = MainTest(queses, answer, 0, dieCutting, Ops, PatternList);
                    N++;
                    Match = Check(queses, answer, Xmax, Ymax);
                }

                if (MaxN > N)
                {
                    MaxN = N;
                    MaxOps = Ops;
                }
            }
            return (new AnswerData
            {
                N = MaxN,
                Ops = Ops
            },
            queses);
        }
        public static void CalculationTest(List<List<int>> queses, List<List<int>> answer)
        {
            int N = 1000;
            int MaxN = 0;
            var MaxOps = new List<AnswerData.OperationData>();
            var Ops = new List<AnswerData.OperationData>();
            Ymax = queses.Count;
            Xmax = answer[0].Count;
            var answerS = QuestionShunting(queses, answer, Xmax, Ymax);
            PatternCount(queses, Pattern.PatternList);
            PatternDifferenceValue(Pattern.PatternList);
            DieCutting dieCutting;

            for (int direction = 0; direction < 4; direction++)
            {
                MaxN = 0;
                if (direction == 0 || direction == 1)
                {
                    dieCutting = Case.DieCuttingUP;
                }
                else
                {
                    dieCutting = Case.DieCuttingDown;
                }
                var Items = FirstSort(queses, answer, dieCutting, Ops, Pattern.PatternList);
                queses = Items.Item1;
                N = Items.Item2;
                if (direction == 0 || direction == 3)
                {
                    dieCutting = Case.DieCuttingLeft;
                }
                else
                {
                    dieCutting = Case.DieCuttingRight;
                }

                IndexCount(queses, Xmax, Ymax);
                queses = MainTest(queses, answer, 0, dieCutting, Ops, Pattern.PatternList);
                N++;
                //var Match = Check(queses, answer, Xmax, Ymax);

                Console.WriteLine(MaxN);
                if (MaxN > N)
                {
                    MaxN = N;
                    MaxOps = Ops;
                }
            }

        }
        static (List<List<int>>, int) FirstSort(List<List<int>> queses, List<List<int>> answer, DieCutting dieCutting, List<AnswerData.OperationData> Ops, List<List<List<int>>> PatternList)
        {
            int N = 0;
            var answerT = QuestionShunting(queses, answer, Xmax, Ymax);

            IndexCount(queses, Xmax, Ymax);
            queses = MainTest(queses, answerT, 0, dieCutting, Ops, PatternList);

            // Match = Check(answer, queses,Xmax,Ymax);
            N++;

            return (queses, N);

        }
        public static void Hyoji(List<List<int>> Ans)
        {
            for (int i = 0; i < Ans.Count; i++)
            {
                string result = string.Join(",", Ans[i]);
                result = string.Join("new List<int>", result);
                Console.WriteLine(result);
            }

        }
        public static int WarpValuationCalculation(List<List<int>> Ques, List<List<int>> Ans, int pieceX, int pieceY)
        {
            int Count = 0;
            for (int Y = 0; Y < pieceY; Y++)
            {

                WantListXS[Y] = new int[4];
                for (int number = 0; number < 4; number++)
                {
                    var WantCheckS = Ans[Y].Count(item => item == number) - Ques[Y].Count(item => item == number);

                    WantListXS[Y][number] = WantCheckS;
                    Count += WantListXS[Y].Count(item => item == 0);
                }

            }
            return Count;
        }

        public static List<List<int>> QuestionShunting(List<List<int>> Ques, List<List<int>> Ans, int pieceX, int pieceY)
        {

            var Maxresult = new List<List<int>>();
            var MaxZeroCount = 0;
            var ZeroCount = 0;
            XYWantCount(Ques, Ans, pieceX, pieceY);
            for (int i = 0; i < 10; i++)
            {
                var result = new List<List<int>>();
                for (int Y = 0; Y < pieceY; Y++)
                {
                    result.Add(new List<int>());
                    for (int X = 0; X < pieceX; X++)
                    {
                        result[Y].Add(ListWarp(X, Y));
                    }
                }
                ZeroCount = WarpValuationCalculation(result, Ans, pieceX, pieceY);
                if (ZeroCount > MaxZeroCount)
                {
                    MaxZeroCount = ZeroCount;
                    Maxresult = Case.Copy(result);
                }
            }
            return Maxresult;

        }
        public static bool XYWantCount(List<List<int>> Ques, List<List<int>> Ans, int pieceX, int pieceY)
        {
            for (int Y = 0; Y < pieceY; Y++)
            {
                WantListX[Y] = new int[4];
                WantListXT.Add(new List<int>());
                for (int number = 0; number < 4; number++)
                {
                    var WantCheck = Ans[Y].Count(item => item == number);
                    WantListXT[Y].Add(WantCheck);
                    WantListX[Y][number] = WantCheck;


                }
            }
            var QuesT = Case.TranslatePos(Ques);
            for (int Number = 0; Number < 4; Number++)
            {
                WantListY[Number] = new int[Practice.pieceX];
                for (int X = 0; X < pieceX; X++)
                {
                    var WantCheck = QuesT[X].Count(item => item == Number);
                    WantListY[Number][X] = WantCheck;


                }
            }
            return true;
        }

        public static int ListWarp(int X, int Y)
        {
            var _itemTable = WantListX[Y];
            var totalNumber = 0;
            var searchTable = new int[_itemTable.Length];
            Random rnd = new Random();
            for (int i = 0; _itemTable.Length > i; i++)
            {
                if (WantListY[i][X] == 0 || WantListX[Y][i] == 0)
                {
                    totalNumber += 0;
                    searchTable[i] = totalNumber;
                    continue;
                }
                totalNumber += _itemTable[i] + WantListY[i][X];
                searchTable[i] = totalNumber;
            }

            var randomNumber = rnd.Next(1, totalNumber + 1);
            for (int i = 0; searchTable.Length > i; i++)
            {
                if (searchTable[i] >= randomNumber)
                {
                    WantListX[Y][i] -= 1;
                    WantListY[i][X] -= 1;
                    return i;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (WantListY[i][X] == 0)
                {
                    continue;
                }
                else
                {
                    WantListY[i][X] -= 1;
                    return i;
                }

            }
            return 4;
        }

        static List<List<int>> MainTest(List<List<int>> queses, List<List<int>> answer, int N, DieCutting dieCutting, List<AnswerData.OperationData> Ops, List<List<List<int>>> PatternList)
        {
            var WantIndex = new List<List<List<int>>>();
            var TypeCanList = new List<List<int>>();
            WantIndex.Add(new List<List<int>>());
            WantIndex.Add(new List<List<int>>());
            for (int Y = 0; Y < answer.Count; Y++)
            {
                WantIndex[0].Add(new List<int>());
                WantIndex[0][Y] = Search(queses, answer, WantIndex[0][Y], 1, Y);
                WantIndex[1].Add(new List<int>());
                WantIndex[1][Y] = Search(queses, answer, WantIndex[1][Y], WantIndex[0][Y].Count + 1, Y);
                Hyoji(WantIndex[0]);
                Hyoji(WantIndex[1]);
                NextSearch(queses, answer, WantIndex, WantIndex[0][Y].Count + WantIndex[1][Y].Count + 1, Y);
            }
            var i = Patterncalculation(queses, answer, WantIndex);
            queses = dieCutting(queses, PatternList[i[0]], i[1], i[2], 0, 0);
            var S = SCheck(dieCutting);
            var operationData = new AnswerData.OperationData
            {
                P = i[0],
                X = i[1],
                Y = i[2],
                S = S
            };
            Ops.Add(operationData);
            //Console.WriteLine(Pattern.PatternList.Count);
            //Hyoji(WantIndex[1]);
            return queses;
        }
        public static List<int> Search(List<List<int>> Ques, List<List<int>> Ans, List<int> IndexList, int exclusion, int Y)
        {
            int N = Xmax;
            for (int Number = Xmax - exclusion; Number > 0; Number--)
            {

                int QuesNumberIndex = NumberIndexList[Ans[Y][Number]][Y].FindLast(item => item < N);
                //Console.WriteLine(exclusion);
                //Console.WriteLine(Number);
                //Console.WriteLine(Ans[Y][Number]);
                if (QuesNumberIndex == 0)
                {
                    if (NumberIndexList[Ans[Y][Number]][Y].Count - 1 > 0)
                    {
                        if (NumberIndexList[Ans[Y][Number]][Y][NumberIndexList[Ans[Y][Number]][Y].Count - 1] == 0)
                        {

                            IndexList.Add(QuesNumberIndex);
                            NumberIndexList[Ans[Y][Number]][Y].Remove(QuesNumberIndex);
                        }
                    }
                    break;
                }
                IndexList.Add(QuesNumberIndex);
                N = QuesNumberIndex;
                NumberIndexList[Ans[Y][Number]][Y].Remove(QuesNumberIndex);
            }
            return IndexList;
        }
        public static List<int> NextSearch(List<List<int>> Ques, List<List<int>> Ans, List<List<List<int>>> IndexList, int exclusion, int Y)
        {
            var ques = Case.Copy(Ques);

            for (int i = 0; i < 2; i++)
            {
                int N = Xmax;
                for (int Number = Xmax - exclusion; Number > 0; Number--)
                {

                    int QuesNumberIndex = NumberIndexList[Ans[Y][Number]][Y].FindLast(item => item < N);
                    if (QuesNumberIndex == 0)
                    {
                        if (NumberIndexList[Ans[Y][Number]][Y].Count - 1 > 0)
                        {
                            if (NumberIndexList[Ans[Y][Number]][Y][NumberIndexList[Ans[Y][Number]][Y].Count - 1] == 0)
                            {
                                if (i == 1)
                                {
                                    IndexList[1][Y].Add(QuesNumberIndex);
                                }

                            }
                            NumberIndexList[Ans[Y][Number]][Y].Remove(QuesNumberIndex);
                        }
                        break;
                    }
                    if (i == 1)
                    {
                        IndexList[1][Y].Add(QuesNumberIndex);
                        //exclusion--;
                    }
                    N = QuesNumberIndex;
                    NumberIndexList[Ans[Y][Number]][Y].Remove(QuesNumberIndex);
                    exclusion++;
                }

            }
            return IndexList[1][Y];
        }
        public static (List<int>, List<int>) EvaluationMaxValue(List<List<int>> Ques, List<List<int>> Ans, List<List<List<int>>> WantTypeIndex)
        {
            var MaxTypeEvaluationValueListWarp = new List<int>();
            var MaxTypeEvaluationValueListSide = new List<int>();
            var WantTypeIndexWarp = Case.TranslatePos(WantTypeIndex[1]);
            //配列大きさ順にソートして、はみ出す分はゼロに置く
            for (var Y = 0; Y < Ques.Count; Y++)
            {
                MaxTypeEvaluationValueListSide.Add(WantTypeIndex[1][Y].Count);
            }
            for (var X = 0; X < Ques[0].Count; X++)
            {
                MaxTypeEvaluationValueListWarp.Add(WantTypeIndex[1][X].Count);
            }
            return (MaxTypeEvaluationValueListSide, MaxTypeEvaluationValueListWarp);
        }
        static AnswerData.OperationData.Side SCheck(DieCutting dieCutting)
        {
            if (dieCutting == Case.DieCuttingUP)
            {
                return AnswerData.OperationData.Side.Up;

            }
            else if (dieCutting == Case.DieCuttingDown)
            {
                return AnswerData.OperationData.Side.Down;
            }
            else if (dieCutting == Case.DieCuttingLeft)
            {
                return AnswerData.OperationData.Side.Left;
            }
            else
            {
                return AnswerData.OperationData.Side.Right;
            }
        }
        public static void IndexTest(List<List<int>> Ques, List<List<int>> Ans, List<List<List<int>>> WantTypeIndex)
        {

            float[,] Test = new float[256, 256];
            for (int Y = 0; Y < 256; Y++)
            {
                for (int X = 0; X < 256; X++)
                {
                    rnd.Next(0, 2);
                }
            }
            for (int Y = 0; Y < WantTypeIndex[0].Count; Y++)
            {
                for (int X = 0; X < WantTypeIndex[0][Y].Count; X++)
                {
                    Test[Y, WantTypeIndex[0][Y][X]] = 0;
                }
            }
            for (int Y = 0; Y < WantTypeIndex[1].Count; Y++)
            {
                for (int X = 0; X < WantTypeIndex[1][Y].Count; X++)
                {
                    Test[Y, WantTypeIndex[1][Y][X]] = 1;
                }
            }


        }

        public static void Overcalculation(List<List<int>> Ques, List<List<int>> Ans, List<List<List<int>>> WantTypeIndex)
        {

        }
        public static List<int> Patterncalculation(List<List<int>> Ques, List<List<int>> Ans, List<List<List<int>>> WantTypeIndex)
        {
            int ListNumber = -1;
            var ListNumberList = new List<int>();
            var WantIndexDifferenceList = WantIndexDifferenceValue(WantTypeIndex, 0);
            //横と縦の期待値
            //var X = 0;
            foreach (var i in PatternSizeListOver)
            {
                for (int Y = 0; Y < Ans.Count; Y++)
                {

                    for (var X = 0; WantIndexDifferenceList[Y].Count < PatternZeroDifferenceList[i][Y][X].Count; X++)
                    {
                        if (PatternZeroDifferenceList[i][Y][X].Find(item => item == WantIndexDifferenceList[Y][WantIndexDifferenceList.Count - 1]) != 0)
                        {
                            var LastNumberList = PatternZeroDifferenceList[i][Y][X].FindAll(item => item == WantIndexDifferenceList[Y][WantIndexDifferenceList.Count - 1] && item > WantIndexDifferenceList[Y].Count);
                            foreach (var LastNumberIndex in LastNumberList)
                            {
                                for (var MaxX = WantIndexDifferenceList.Count - 2; 0 <= MaxX; MaxX--)
                                {
                                    if (PatternZeroDifferenceList[i][Y][X].Contains(WantIndexDifferenceList[Y][MaxX]) == false)
                                    {

                                        break;
                                    }
                                    if (MaxX - 1 < 0)
                                    {
                                        ListNumberList.Add(i);
                                        ListNumberList.Add(LastNumberIndex - WantIndexDifferenceList.Count - 1);
                                        ListNumberList.Add(Y);
                                    }

                                }
                            }
                        }
                        else
                        {
                            continue;
                        }


                    }

                    //基準評価値　→　見つかる　→　よりよいものさがす　
                    //基準評価値　→　見つからん　→　方向どんくらい抜けるか試して、1同様に評価値　

                }

            }
            ListNumberList.Add(0);
            ListNumberList.Add(0);
            ListNumberList.Add(0);

            //EvolusionValue();
            return ListNumberList;
        }
        public static int EvolusionValue(List<int> ListNumberList)
        {
            int MAxPoint = 0;
            /* for(int i = 0; i < ListNumberList.Count; i+=3)
             {
                 for(int X = ListNumberList[i +1 ]; X < )
                 {

                 }
             }
            */
            return 0;
        }
        public static int Not(List<List<int>> WantIndexDifferenceList)
        {
            return 0;
        }
        public static int Motto(List<List<int>> WantIndexDifferenceList)
        {
            //基準の1を数えて超えたやつのみの評価値
            for (int i = 0; i < 281; i++)
            {

            }

            return 0;
        }
        public static List<List<int>> WantIndexDifferenceValue(List<List<List<int>>> WantTypeIndex, int i)
        {
            var List = new List<List<int>>();
            for (int Y = 0; Y < WantTypeIndex[i].Count; Y++)
            {
                List.Add(new List<int>());
                for (int X = 0; X + 1 < WantTypeIndex[i][Y].Count; X++)
                {
                    List[Y].Add(WantTypeIndex[i][Y][X + 1] - WantTypeIndex[i][Y][X]);
                }
            }
            return List;
        }

        public static void PatternDifferenceValue(List<List<List<int>>> PatternList)
        {
            //横と縦の期待値
            //var X = 0;

            for (int i = 0; i < PatternList.Count; i++)
            {
                PatternZeroDifferenceList.Add(new List<List<List<int>>>());
                for (int Y = 0; Y < ZeroIndexList[i].Count; Y++)
                {
                    PatternZeroDifferenceList[i].Add(new List<List<int>>());
                    PatternZeroDifferenceList[i][Y].Add(new List<int>());
                    for (int IntervalX = 1; IntervalX < ZeroIndexList[i][Y].Count; IntervalX++)
                    {

                        PatternZeroDifferenceList[i][Y][0].Add(ZeroIndexList[i][Y][IntervalX] - ZeroIndexList[i][Y][0]);
                    }
                    for (int X = 1; X < PatternZeroDifferenceList[i][Y][0].Count; X++)
                    {
                        PatternZeroDifferenceList[i][Y].Add(new List<int>());
                        var Number = ZeroIndexList[i][Y][X] - ZeroIndexList[i][Y][X - 1];
                        PatternZeroDifferenceList[i][Y].Add(PatternZeroDifferenceList[i][Y][X - 1].Select(item => item - Number).ToList());
                        PatternZeroDifferenceList[i][Y].RemoveAt(0);
                    }
                }
                PatternOneDifferenceList.Add(new List<List<List<int>>>());
                for (int Y = 0; Y < OneIndexList[i].Count; Y++)
                {
                    PatternOneDifferenceList[i].Add(new List<List<int>>());
                    PatternOneDifferenceList[i][Y].Add(new List<int>());
                    for (int IntervalX = 1; IntervalX < OneIndexList[i][Y].Count; IntervalX++)
                    {

                        PatternOneDifferenceList[i][Y][0].Add(OneIndexList[i][Y][IntervalX] - OneIndexList[i][Y][0]);
                    }
                    for (int X = 1; X < PatternOneDifferenceList[i][Y][0].Count; X++)
                    {
                        PatternOneDifferenceList[i][Y].Add(new List<int>());
                        var Number = OneIndexList[i][Y][X] - OneIndexList[i][Y][X - 1];
                        PatternOneDifferenceList[i][Y].Add(PatternOneDifferenceList[i][Y][X - 1].Select(item => item - Number).ToList());
                        PatternOneDifferenceList[i][Y].RemoveAt(0);

                    }
                }
            }
            //foreach (int i in PatternSizeListOver)


        }


        public static void PatternCount(List<List<int>> Ques, List<List<List<int>>> PatternList)
        {
            ZeroIndexList = new List<List<List<int>>>();
            OneIndexList = new List<List<List<int>>>();
            for (int i = 0; i < 281; i++)
            {
                ZeroIndexList.Add(new List<List<int>>());
                OneIndexList.Add(new List<List<int>>());
                for (int Y = 0; Y < Pattern.PatternList[i].Count; Y++)
                {
                    ZeroIndexList[i].Add(new List<int>());
                    OneIndexList[i].Add(new List<int>());
                    for (int X = 0; X < PatternList[i][Y].Count; X++)
                    {
                        if (Pattern.PatternList[i][Y][X] == 0)
                        {
                            ZeroIndexList[i][Y].Add(X);
                        }
                        else if (Pattern.PatternList[i][Y][X] == 1)
                        {
                            OneIndexList[i][Y].Add(X);
                        }

                    }

                    if (ZeroIndexList[i][Y].Count == 0)
                    {
                        ZeroIndexList[i][Y].Add(0);
                    }
                    if (OneIndexList[i][Y].Count == 0)
                    {
                        OneIndexList[i][Y].Add(0);
                    }

                }
                if (PatternList[i].Count >= Ques.Count)
                {
                    if (PatternList[i][0].Count >= Ques[0].Count)
                    {
                        PatternSizeListOver.Add(i);
                        continue;
                    }
                }
                PatternSizeListNotOver.Add(i);
            }
        }
        public static void IndexCount(List<List<int>> Ques, int pieceX, int pieceY)
        {
            NumberIndexList = new List<List<List<int>>>();

            for (int Number = 0; Number < 4; Number++)
            {
                NumberIndexList.Add(new List<List<int>>());
                for (int Y = 0; Y < pieceY; Y++)
                {
                    NumberIndexList[Number].Add(new List<int>());
                    for (int X = 0; X < pieceX; X++)
                    {
                        if (Ques[Y][X] == Number)
                        {
                            NumberIndexList[Number][Y].Add(X);
                        }


                    }

                    if (NumberIndexList[Number][Y].Count == 0)
                    {
                        NumberIndexList[Number][Y].Add(0);
                    }

                }
            }


        }
        public static float Check(List<List<int>> ans, List<List<int>> queues, int pieceX, int pieceY)
        {
            var mass = pieceX * pieceY;
            float match = 100f / mass;
            float matchR = 0;
            for (int y = 0; y < pieceY; y++)
            {
                for (int x = 0; x < pieceX; x++)
                {
                    if (ans[y][x] == queues[y][x])
                    {
                        matchR += match;
                    }

                }
            }
            return matchR;
        }
        public static void Test()
        {

        }

        public static (int, int, int, int) SearchDie(List<List<int>> ques, List<List<int>> ans)
        {
            int useDieNum = 0;
            int cuttingDirection = 0;
            int usingPositionX = 0;
            int usingPositionY = 0;
            int nowMaxEffectiveScore = 0;
            List<List<int>> collectPieceArray = CreateCollectPieceArray(ques, ans);

            // 仕様メモ
            // 左端、もしくは右端を固定し、一方向にしか動かないようにする
            // for文を回して、どの型が一番効率的かを計算する
            // 効率的かどうかは、(揃ったピースの数-揃わなくなったピースの数)で判断する
            // 効率的なものを見つけたら、使うべき型、使うべき座標を返す

            for (int useDieIndex = 0; useDieIndex < 256; useDieIndex++)
            {
                for (int x = 0; x < Xmax; x++)
                {
                    for (int y = 0; y < Ymax; y++)
                    {
                        int rightCuttingEffectiveScore;
                        int leftCuttingEffectiveScore;
                        List<List<int>> tempQues = Case.Copy(ques);
                        List<List<int>> tempAns = Case.Copy(ans);
                        List<List<int>> leftCuttingQues = Case.DieCuttingLeft(tempQues, Pattern.PatternList[useDieIndex], y, x, Pattern.PatternList[useDieIndex][0].Count, Pattern.PatternList[useDieIndex].Count);
                        List<List<int>> rightCuttingQues = Case.DieCuttingRight(tempQues, Pattern.PatternList[useDieIndex], y, x, Pattern.PatternList[useDieIndex][0].Count, Pattern.PatternList[useDieIndex].Count);

                        List<List<int>> leftCuttingCollectPieceArray = CreateCollectPieceArray(leftCuttingQues, tempAns);
                        List<List<int>> rightCuttingCollectPieceArray = CreateCollectPieceArray(rightCuttingQues, tempAns);

                        rightCuttingEffectiveScore = CalculateEffectiveScore(collectPieceArray, rightCuttingCollectPieceArray);
                        leftCuttingEffectiveScore = CalculateEffectiveScore(collectPieceArray, leftCuttingCollectPieceArray);

                        if (MathF.Max(rightCuttingEffectiveScore, leftCuttingEffectiveScore) > nowMaxEffectiveScore)
                        {
                            nowMaxEffectiveScore = (int)MathF.Max(rightCuttingEffectiveScore, leftCuttingEffectiveScore);
                            useDieNum = useDieIndex;
                            usingPositionX = x;
                            usingPositionY = y;
                        }
                    }
                }
            }

            return (useDieNum, cuttingDirection, usingPositionX, usingPositionY);

        }

        static int CalculateEffectiveScore(List<List<int>> beforeCollectPieceArray, List<List<int>> afterCollectPieceArray)
        {
            int effectiveScore = 0;
            for (int y = 0; y < Ymax; y++)
            {
                for (int x = 0; x < Xmax; x++)
                {
                    effectiveScore += (int)beforeCollectPieceArray[y][x] - afterCollectPieceArray[y][x];
                }
            }
            return effectiveScore;
        }

        static List<List<int>> CreateCollectPieceArray(List<List<int>> ques, List<List<int>> ans)
        {
            List<List<int>> collectPieceArray = new List<List<int>>();
            for (int x = 0; x < Xmax; x++)
            {
                List<int> collectPieceRaw = new List<int>();
                for (int y = 0; y < Ymax; y++)
                {
                    if (ques[y][x] == ans[y][x]) collectPieceRaw.Add(1);
                    else collectPieceRaw.Add(0);
                }
                collectPieceArray.Add(collectPieceRaw);
            }
            return collectPieceArray;
        }

    }

}
