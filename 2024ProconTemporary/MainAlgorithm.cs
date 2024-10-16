
using _2024ProconTemporary.Com;
using _2024ProconTemporary.ReadableData;

using _2024ProconTemporary.Com;
using _2024ProconTemporary.ReadableData;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
namespace _2024ProconTemporary
{

    public class Mainalgorithm
    public class Mainalgorithm
    {


        Case cases = new Case();
        Practice practice = new Practice();
        Pattern Pattern = new Pattern();
        static int[][] WantListX = new int[266][];
        static List<List<int>> WantListXT = new List<List<int>>();
        static int[][] WantListXS = new int[266][];
        static int[][] WantListY = new int[4][];
        static List<List<List<int>>> NumberIndexList = new List<List<List<int>>>();
        static List<List<List<int>>> ZeroIndexList = new List<List<List<int>>>();
        static List<List<List<int>>> OneIndexList = new List<List<List<int>>>();

        public static int Ymax = 0;
        public static int Xmax = 0;
        public static void MainAlgorithm()
        {
            Pattern.Patterns();
            Practice.Practices();
            Ymax = Practice.AnsTes2.Count;
            Xmax = Practice.AnsTes2[0].Count;
            //Practice.QuesTes2 = Case.DieCuttingRight(Practice.QuesTes2, Type.TypeList[255], 0, 0,0,0);
            //Case.DieCuttingRight 型座標指定
            Hyoji(Practice.QuesTes2);
            //Practice.QuesTes = QuestionShunting(Practice.QuesTes2,Practice.AnsTes2);
            // Console.WriteLine("");
            for (int i = 0; i < 1; i++)
            {
                IndexCount(Practice.QuesTes2);
                MainTest(Practice.QuesTes2, Practice.AnsTes2, 0);
            }


        }
        public AnswerData Calculation(ProblemData problemData)
        {
            Pattern.Patterns();
            Practice.Practices();
            Ymax = problemData.Board.Height;
            Xmax = problemData.Board.Width;
            //var Ques = Case.Copy(problemData.Board.Start);
            for (int CoincidentRatio = 0; CoincidentRatio < 90;)
            {

                IndexCount(Practice.QuesTes2);
                //Ques =  MainTest(Ques,Ansewr, 0);
            }
            return new AnswerData
            {
                N = 0,
                Ops = new List<AnswerData.OperationData>()
            };
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

        public static List<List<int>> QuestionShunting(List<List<int>> Ques, List<List<int>> Ans)
        {
            Random rnd = new Random();
            var Maxresult = new List<List<int>>();
            var MaxZeroCount = 0;
            var ZeroCount = 0;
            XYWantCount(Ques, Ans, Practice.pieceX, Practice.pieceY);
            for (int i = 0; i < 10; i++)
            {
                var result = new List<List<int>>();
                for (int Y = 0; Y < Practice.pieceY; Y++)
                {
                    result.Add(new List<int>());
                    for (int X = 0; X < Practice.pieceX; X++)
                    {
                        result[Y].Add(ListWarp(X, Y));
                    }
                }
                ZeroCount = WarpValuationCalculation(result, Ans, Practice.pieceX, Practice.pieceY);
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

        public static List<List<int>> MainTest(List<List<int>> Ques, List<List<int>> Ans, int N)
        {
            var WantIndex = new List<List<List<int>>>();
            var TypeCanList = new List<List<int>>();
            WantIndex.Add(new List<List<int>>());
            WantIndex.Add(new List<List<int>>());
            for (int Y = 0; Y < Ans.Count; Y++)
            {
                WantIndex[0].Add(new List<int>());
                WantIndex[0][Y] = Search(Ques, Ans, WantIndex[0][Y], 1, Y);
                WantIndex[1].Add(new List<int>());
                WantIndex[1][Y] = Search(Ques, Ans, WantIndex[1][Y], WantIndex[0][Y].Count + 2, Y);
                NextSearch(Ques, Ans, WantIndex, WantIndex[0][Y].Count + WantIndex[1][Y].Count + 2, Y);

            }

            //Console.WriteLine(WantIndex[0][0].Count);
            //Hyoji(WantIndex[0]);
            //Hyoji(WantIndex[1]);
            return Ques;

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
                    if (NumberIndexList[Ans[Y][Number]][Y][NumberIndexList[Ans[Y][Number]][Y].Count - 1] == 0)
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

        public static List<List<int>> MainTest(List<List<int>> Ques, List<List<int>> Ans, int N)
        {
            var WantIndex = new List<List<List<int>>>();
            var TypeCanList = new List<List<int>>();
            WantIndex.Add(new List<List<int>>());
            WantIndex.Add(new List<List<int>>());
            for (int Y = 0; Y < Ans.Count; Y++)
            {
                WantIndex[0].Add(new List<int>());
                WantIndex[0][Y] = Search(Ques, Ans, WantIndex[0][Y], 1, Y);
                WantIndex[1].Add(new List<int>());
                WantIndex[1][Y] = Search(Ques, Ans, WantIndex[1][Y], WantIndex[0][Y].Count + 2, Y);
                NextSearch(Ques, Ans, WantIndex, WantIndex[0][Y].Count + WantIndex[1][Y].Count + 2, Y);

            }

            //Console.WriteLine(WantIndex[0][0].Count);
            //Hyoji(WantIndex[0]);
            //Hyoji(WantIndex[1]);
            return Ques;

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
                    if (NumberIndexList[Ans[Y][Number]][Y][NumberIndexList[Ans[Y][Number]][Y].Count - 1] == 0)
                    {
                        IndexList.Add(QuesNumberIndex);
                        NumberIndexList[Ans[Y][Number]][Y].Remove(QuesNumberIndex);
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
            int N = Xmax;
            for (int i = 0; i < 2; i++)
            {
                foreach (var remove in IndexList[i][Y])
                {
                    ques[Y].RemoveAt(remove);
                }
            }
            for (int i = 0; i < 2; i++)
            {
                for (int Number = Xmax - exclusion; Number > 0; Number--)
                {
                    //最低値を取る
                    int QuesNumberIndex = NumberIndexList[Ans[Y][Number]][Y].FindLast(item => item < N);
                    if (QuesNumberIndex == 0)
                    {
                        if (NumberIndexList[Ans[Y][Number]][Y][NumberIndexList[Ans[Y][Number]][Y].Count - 1] == 0)
                        {
                            ques[Y].RemoveAt(QuesNumberIndex);
                            NumberIndexList[Ans[Y][Number]][Y].Remove(QuesNumberIndex);
                        }
                        break;
                    }
                    if (i == 1)
                    {
                        ques[Y].RemoveAt(QuesNumberIndex);
                        IndexList.Add(QuesNumberIndex);
                        NumberIndexList[Ans[Y][Number]][Y].Remove(QuesNumberIndex);
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
            int N = Xmax;
            for (int i = 0; i < 2; i++)
            {
                foreach (var remove in IndexList[i][Y])
                {
                    ques[Y].RemoveAt(remove);
                }
            }
            for (int i = 0; i < 2; i++)
            {
                for (int Number = Xmax - exclusion; Number > 0; Number--)
                {
                    //最低値を取る
                    int QuesNumberIndex = NumberIndexList[Ans[Y][Number]][Y].FindLast(item => item < N);
                    if (QuesNumberIndex == 0)
                    {
                        if (NumberIndexList[Ans[Y][Number]][Y][NumberIndexList[Ans[Y][Number]][Y].Count - 1] == 0)
                        {
                            ques[Y].RemoveAt(QuesNumberIndex);
                            NumberIndexList[Ans[Y][Number]][Y].Remove(QuesNumberIndex);
                        }
                        break;
                    }
                    if (i == 1)
                    {
                        ques[Y].RemoveAt(QuesNumberIndex);
                    }
                    else
                    {
                        IndexList[1][Y].Add(QuesNumberIndex);
                    }
                    N = QuesNumberIndex;
                    NumberIndexList[Ans[Y][Number]][Y].Remove(QuesNumberIndex);
                    exclusion++;
                }

            }
            return IndexList[1][Y];
        }
        public static void EvaluationValue(List<List<int>> Ques, List<List<int>> Ans, List<List<List<int>>> WantTypeIndex)
        {

            var TypeList = new List<int>();
            var TypeEvaluationValueList = new int[281];
            //配列大きさ順にソートして、はみ出す分はゼロに置く
            //
            for (int i = 0; i < 281; i++)
            {
                for (int Y = 0; Y < Pattern.PatternList[i].GetLength(0); Y++)
                {
                    for (int X = 0; X < ZeroIndexList[i][Y].Count; X++)
                    {
                        foreach (var Zero in WantTypeIndex[0])
                        {

                        }
                    }
                }
            }

        }
        public static void Check(List<List<int>> Ques, List<List<int>> Ans, List<List<List<int>>> WantTypeIndex)
        {

        }

        public static void Overcalculation(List<List<int>> Ques, List<List<int>> Ans, List<List<List<int>>> WantTypeIndex)
        {

        }
        public static void IndexCount(List<List<int>> Ques)
        {
            for (int Number = 0; Number < 4; Number++)
            {
                NumberIndexList.Add(new List<List<int>>());
                for (int Y = 0; Y < Practice.pieceY; Y++)
                {
                    NumberIndexList[Number].Add(new List<int>());
                    for (int X = 0; X < Practice.pieceX; X++)
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
            for (int i = 0; i < 281; i++)
            {
                ZeroIndexList.Add(new List<List<int>>());
                OneIndexList.Add(new List<List<int>>());
                for (int Y = 0; Y < Pattern.PatternList[i].GetLength(0); Y++)
                {
                    ZeroIndexList[i].Add(new List<int>());
                    OneIndexList[i].Add(new List<int>());
                    for (int X = 0; X < Pattern.PatternList[i].GetLength(1); X++)
                    {
                        if (Pattern.PatternList[i][Y, X] == 0)
                        {
                            ZeroIndexList[i][Y].Add(X);
                        }
                        else if (Pattern.PatternList[i][Y, X] == 1)
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
            }

        }

        ///<summary>
        /// 列ごとを揃えたあとに、行ごとで揃えるために必要な型の種類と抜く座標を返す
        ///</summary>
        public (int, int, int, int) SearchDie(ReadableProblemData problemData, List<List<int>> ques, List<List<int>> ans)
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

            for (int useDieIndex = 0; useDieIndex < problemData.General.Patterns.Count; useDieIndex++)
            {
                for (int x = 0; x < Xmax; x++)
                {
                    for (int y = 0; y < Ymax; y++)
                    {
                        int rightCuttingEffectiveScore;
                        int leftCuttingEffectiveScore;
                        List<List<int>> tempQues = Case.Copy(ques);
                        List<List<int>> tempAns = Case.Copy(ans);
                        List<List<int>> leftCuttingQues = Case.DieCuttingLeft(tempQues, Pattern.PatternList[useDieIndex], y, x, Pattern.PatternList[useDieIndex].GetLength(1), Pattern.PatternList[useDieIndex].GetLength(0));
                        List<List<int>> rightCuttingQues = Case.DieCuttingRight(tempQues, Pattern.PatternList[useDieIndex], y, x, Pattern.PatternList[useDieIndex].GetLength(1), Pattern.PatternList[useDieIndex].GetLength(0));

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

        int CalculateEffectiveScore(List<List<int>> beforeCollectPieceArray, List<List<int>> afterCollectPieceArray)
        {
            int effectiveScore = 0;
            for (int y = 0; y < Ymax; y++)
                IndexList[1][Y].Add(QuesNumberIndex);
        }
        N = QuesNumberIndex;
                    NumberIndexList[Ans[Y][Number]][Y].Remove(QuesNumberIndex);
        exclusion++;
                }

}
return IndexList[1][Y];
        }
        public static void EvaluationValue(List<List<int>> Ques, List<List<int>> Ans, List<List<List<int>>> WantTypeIndex)
{

    var TypeList = new List<int>();
    var TypeEvaluationValueList = new int[281];
    //配列大きさ順にソートして、はみ出す分はゼロに置く
    //
    for (int i = 0; i < 281; i++)
    {
        for (int Y = 0; Y < Pattern.PatternList[i].GetLength(0); Y++)
        {
            for (int X = 0; X < ZeroIndexList[i][Y].Count; X++)
            {
                foreach (var Zero in WantTypeIndex[0])
                {

                }
            }
        }
    }

}
public static void Check(List<List<int>> Ques, List<List<int>> Ans, List<List<List<int>>> WantTypeIndex)
{

}

public static void Overcalculation(List<List<int>> Ques, List<List<int>> Ans, List<List<List<int>>> WantTypeIndex)
{

}
public static void IndexCount(List<List<int>> Ques)
{
    for (int Number = 0; Number < 4; Number++)
    {
        NumberIndexList.Add(new List<List<int>>());
        for (int Y = 0; Y < Practice.pieceY; Y++)
        {
            NumberIndexList[Number].Add(new List<int>());
            for (int X = 0; X < Practice.pieceX; X++)
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
    for (int i = 0; i < 281; i++)
    {
        ZeroIndexList.Add(new List<List<int>>());
        OneIndexList.Add(new List<List<int>>());
        for (int Y = 0; Y < Pattern.PatternList[i].GetLength(0); Y++)
        {
            ZeroIndexList[i].Add(new List<int>());
            OneIndexList[i].Add(new List<int>());
            for (int X = 0; X < Pattern.PatternList[i].GetLength(1); X++)
            {
                if (Pattern.PatternList[i][Y, X] == 0)
                {
                    ZeroIndexList[i][Y].Add(X);
                }
                else if (Pattern.PatternList[i][Y, X] == 1)
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
    }

}

///<summary>
/// 列ごとを揃えたあとに、行ごとで揃えるために必要な型の種類と抜く座標を返す
///</summary>
public (int, int, int, int) SearchDie(ReadableProblemData problemData, List<List<int>> ques, List<List<int>> ans)
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

    for (int useDieIndex = 0; useDieIndex < problemData.General.Patterns.Count; useDieIndex++)
    {
        for (int x = 0; x < Xmax; x++)
        {
            for (int y = 0; y < Ymax; y++)
            {
                int rightCuttingEffectiveScore;
                int leftCuttingEffectiveScore;
                List<List<int>> tempQues = Case.Copy(ques);
                List<List<int>> tempAns = Case.Copy(ans);
                List<List<int>> leftCuttingQues = Case.DieCuttingLeft(tempQues, Pattern.PatternList[useDieIndex], y, x, Pattern.PatternList[useDieIndex].GetLength(1), Pattern.PatternList[useDieIndex].GetLength(0));
                List<List<int>> rightCuttingQues = Case.DieCuttingRight(tempQues, Pattern.PatternList[useDieIndex], y, x, Pattern.PatternList[useDieIndex].GetLength(1), Pattern.PatternList[useDieIndex].GetLength(0));

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

int CalculateEffectiveScore(List<List<int>> beforeCollectPieceArray, List<List<int>> afterCollectPieceArray)
{
    int effectiveScore = 0;
    for (int y = 0; y < Ymax; y++)
    {
        for (int x = 0; x < Xmax; x++)
            for (int x = 0; x < Xmax; x++)
            {
                effectiveScore += (int)beforeCollectPieceArray[y][x] - afterCollectPieceArray[y][x];
            }
    }
    return effectiveScore;
}

List<List<int>> CreateCollectPieceArray(List<List<int>> ques, List<List<int>> ans)
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
    return collectPieceArray;
}

    }
}