using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace _2024ProconTemporary
{

    public class Mainalgorithm
    {
        Case cases = new Case();
        Practice practice = new Practice();
        Type Type = new Type();

        List<List<int>> MatchInfo = new List<List<int>>();
        List<List<int>> MatchInfos = new List<List<int>>();
        delegate List<List<int>> DieCutting(List<List<int>> Ques, float[,] Type, int PointX, int PointY);
        static string path = @"C:\Users\suzuk\Downloads\2024ProconTemporary\2024ProconTemporary\bin\Debug\net8.0\sample.txt";
        static void Main()
        {
            Practice.Practices();
            Type.Types();
            File.WriteAllText(path, string.Empty);
            //Hyoji(Practice.Ans);
            //Hyoji(Practice.Ques);
            NewAl.Kari();
           //Matchcalculate();
            //MatchcalculateTes();
            //MatchcalculateLeft();
        }

        static void Matchcalculate()
        {
            Case cases = new Case();
            List<List<int>> MatchInfo = new List<List<int>>();
            float MAXmatch = 0f;
            int u = 0;
            int imax = 0;
            int XMax = 0;
            int YMax = 0;
            var ques = new List<List<int>>();
            DieCutting DieCutting;
            while (MAXmatch < 95)
            {
                for (int direction = 0; direction < 4; direction++)
                {
                    if (direction == 0)
                    {
                        DieCutting = Case.DieCuttingUP;
                    }
                    else if (direction == 1)
                    {
                        DieCutting = Case.DieCuttingDown;
                    }
                    else if (direction == 2)
                    {
                        DieCutting = Case.DieCuttingLeft;
                    }
                    else
                    {
                        DieCutting = Case.DieCuttingRight;
                    }
                    for (int i = 0; i < 281; i++)
                    {
                        for (int y = 0; y < Practice.pieceY + Type.TypeList[i].GetLength(0); y++)
                        {
                            for (int x = 0; x < Practice.pieceX + Type.TypeList[i].GetLength(1); x++)
                            {
                                float MatchR = Check(Practice.AnsTes, DieCutting(Practice.QuesTes, Type.TypeList[i], x, y));
                                if (MatchR > MAXmatch)
                                {
                                    MAXmatch = MatchR;
                                    //Hyoji2(MAXmatch);
                                    //Hyoji2(i);
                                    imax = i;
                                    XMax = x;
                                    YMax = y;
                                    ques = DieCutting(Practice.QuesTes, Type.TypeList[i], x, y);
                                    //MatchInfo.Add(new List<int> { i, x, y, direction });
                                }
                            }
                        }
                    }
                }
                u++;
                var Ques = Case.Copy(ques);
                Practice.QuesTes = Ques;
                Hyoji3(Type.TypeList[imax]);
                Hyoji2(XMax);
                Hyoji2(YMax);
                Hyoji(Practice.QuesTes);
                Console.WriteLine(MAXmatch);
            }
        }
        public static float Check(List<List<int>> Ans, List<List<int>> Ques)
        {
            var mass = Practice.pieceX * Practice.pieceY;
            float match = 100f / mass;
            float matchR = 0;
            for (int y = 0; y < Practice.pieceY; y++)
            {
                for (int x = 0; x < Practice.pieceX; x++)
                {
                    if (Ans[y][x] == Ques[y][x])
                    {
                        matchR += match;
                    }

                }
            }
            return matchR;
        }
        public static float[,] CheckWarp(List<List<int>> Ans, List<List<int>> Ques)
        {
            var mass = Practice.pieceX * Practice.pieceY;
            var Row = new float[Practice.pieceX, Practice.pieceY];
            float match = 100f / mass;
            float matchR = 0;
            for (int y = 0; y < Practice.pieceY; y++)
            {
                for (int x = 0; x < Practice.pieceX; x++)
                {
                    if (Ans[y][x] == Ques[y][x])
                    {
                        matchR += match;
                    }

                }
            }
            return Row;
        }
        public static float[,] CheckSide(List<List<int>> Ans, List<List<int>> Ques)
        {
            var mass = Practice.pieceX * Practice.pieceY;
            var Row = new float[Practice.pieceX, Practice.pieceY];
            float match = 100f / mass;
            float matchR = 0;
            for (int y = 0; y < Practice.pieceY; y++)
            {
                for (int x = 0; x < Practice.pieceX; x++)
                {
                    if (Ans[y][x] == Ques[y][x])
                    {
                        matchR += match;
                    }

                }
            }
            return Row;
        }
        //予め一致してる部分が動かないような型と位置を逆算する？
        //理想は型を選別する
        //移動しないマスの一致率を比較してカットをパスする
        public static void Hyoji(List<List<int>> Ans)
        {
            var fileName = "sample.txt";
            using (StreamWriter sw = new StreamWriter(fileName, true, Encoding.GetEncoding("UTF-8")))
            {
                sw.WriteLine("");
                for (int i = 0; i < Ans.Count; i++)
                {
                    sw.WriteLine("");
                    for (int j = 0; j < Ans[i].Count; j++)
                    {
                        sw.Write(Ans[i][j]);
                    }
                }
            }
        }
        public static void Hyoji2(float match)
        {
            var fileName = "sample.txt";
            using (StreamWriter sw = new StreamWriter(fileName, true, Encoding.GetEncoding("UTF-8")))
            {
                sw.WriteLine(match);
            }
        }
        public static void Hyoji3(float[,] TypeList)
        {
            var fileName = "sample.txt";
            using (StreamWriter sw = new StreamWriter(fileName, true, Encoding.GetEncoding("UTF-8")))
            {
                sw.WriteLine("");
                for (int y = 0; y < TypeList.GetLength(1); y++)
                {
                    sw.WriteLine("");
                    for (int x = 0; x < TypeList.GetLength(0); x++)
                    {
                        sw.Write(TypeList[x, y]);
                    }
                }
            }
        }
        static void MatchcalculateTes()
        {
            Case cases = new Case();
            List<List<int>> MatchInfo = new List<List<int>>();
            float MAXmatch = 0f;
            int u = 0;
            var ques = new List<List<int>>();
            DieCutting DieCutting;
            while (MAXmatch < 95)
            {
                for (int direction = 0; direction < 4; direction++)
                {
                    if (direction == 0)
                    {
                        DieCutting = Case.DieCuttingUP;
                    }
                    else if (direction == 1)
                    {
                        DieCutting = Case.DieCuttingDown;
                    }
                    else if (direction == 2)
                    {
                        DieCutting = Case.DieCuttingLeft;
                    }
                    else
                    {
                        DieCutting = Case.DieCuttingRight;
                    }
                    for (int i = 0; i < 281; i++)
                    {
                        for (int y = 0; y < Practice.pieceY + Type.TypeList[i].GetLength(0); y++)
                        {
                            for (int x = 0; x < Practice.pieceX + Type.TypeList[i].GetLength(1); x++)
                            {
                                float MatchR = Check(Practice.Ans, DieCutting(Practice.Ques, Type.TypeList[i], x, y));
                                if (MatchR > MAXmatch)
                                {
                                    MAXmatch = MatchR;
                                    //Hyoji2(MAXmatch);
                                    //Hyoji2(i);
                                    ques = DieCutting(Practice.Ques, Type.TypeList[i], x, y);
                                    MatchInfo.Add(new List<int> { i, x, y, direction });
                                }
                            }
                        }
                    }
                }
                u++;
                var Ques = Case.Copy(ques);
                Practice.Ques = Ques;

                Console.WriteLine(MAXmatch);
            }
        }



    }
}

