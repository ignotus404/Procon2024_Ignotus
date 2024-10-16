
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2024ProconTemporary
{
    public class Case
    {
        public static List<List<int>> TranslatePos(List<List<int>> queues)
        {
            var resultList = new List<List<int>>();
            foreach (var row in queues.Select((v, i) => new { v, i }))
            {
                while (resultList.Count < row.v.Count)
                    resultList.Add(new List<int>());

                foreach (var col in row.v.Select((v, i) => new { v, i }))
                {
                    resultList[col.i].Add((col.v));
                }
            }

            return resultList;
        }
        public static List<List<int>> Copy(List<List<int>> Ques)
        {
            var kari = new List<List<int>>();
            for (int i = 0; i < Ques.Count; i++)
            {
                var kari2 = new List<int>();
                for (int j = 0; j < Ques[i].Count; j++)
                {
                    kari2.Add(Ques[i][j]);
                }
                kari.Add(kari2);
            }
            return kari;
        }


        public static  List<List<int>> DieCuttingUP(List<List<int>> queues, float[,] pattern, int PointY, int PointX, int PatternX, int PatternY)
        {
            var queuesT = TranslatePos(queues);
                for (int y = 0; y < pattern.GetLength(1); y++)
                {
                    var match = new List<int>();
                    if (y + PointY < queues[0].Count )
                    {
                        for (int x = pattern.GetLength(0) - 1; x >= 0; x--)
                        {
                            if (pattern[x, y] == 1)
                            {
                                if (x + PointX < queues.Count)
                                {
                                
                                    match.Add(queues[x + PointX][y + PointY]);
                                    queuesT[y + PointY].RemoveAt(x + PointX);

                                }
                            }

                        }
                        match.Reverse();
                        foreach (int X in match)
                        {
                            queuesT[y + PointY].Add(X);
                        }
                    }
                }
            queuesT = TranslatePos(queuesT);
            return queuesT;
        }
        public static List<List<int>> DieCuttingDown(List<List<int>> queues, float[,] pattern, int PointY, int PointX, int PatternX, int PatternY)
        {
            var queuesT = TranslatePos(queues);
            for (int y = 0; y < pattern.GetLength(1); y++)
            {
                var match = new List<int>();
                if (y + PointY < queues[0].Count + pattern.GetLength(1) && y + PointY > queues[0].Count + pattern.GetLength(1))
                {
                    for (int x = pattern.GetLength(0) - 1; x >= 0; x--)
                    {
                        if (pattern[x, y] == 1)
                        {
                            if (x + PointX < queues.Count + pattern.GetLength(0) && x + PointX > queues.Count + pattern.GetLength(0))
                            {
                                match.Add(queues[x + PointX][y + PointY]);
                                queuesT[y + PointY].RemoveAt(x + PointX);

                            }
                        }

                    }
                    queuesT[y + PointY].Reverse();
                    foreach (int X in match)
                    {
                        queuesT[y + PointY].Add(X);
                    }
                    queuesT[y + PointY].Reverse();
                }
            }
            queuesT = TranslatePos(queuesT);
            return queuesT;
        }
        public static List<List<int>> DieCuttingRight(List<List<int>> queues, float[,] pattern, int PointX, int PointY,int PatternX,int PatternY)
        {
            for (int y = 0; y < pattern.GetLength(0); y++)
            {
                var match = new List<int>();
                if (y + PointY < queues.Count)
                {
                    for (int x = pattern.GetLength(1) - 1; x >= 0; x--)
                    {
                        if (pattern[y, x] == 1)
                        {
                            if (x + PointX < queues[0].Count)
                            {
                                match.Add(queues[y + PointY][x + PointX]);
                                queues[y + PointY].RemoveAt(x + PointX);

                            }
                        }

                    }
                    queues[y + PointY].Reverse();
                    foreach (int X in match)
                    {
                        queues[y + PointY].Add(X);
                    }
                    queues[y + PointY].Reverse();
                }
            }
            return queues;
        }
        public static List<List<int>> DieCuttingLeft(List<List<int>> queues, float[,] pattern, int PointX, int PointY, int PatternX, int PatternY)
        {
            for (int y = 0; y < pattern.GetLength(0); y++)
            {
                var match = new List<int>();
                if (y + PointY < queues.Count)
                {
                    for (int x = pattern.GetLength(1) - 1; x >= 0; x--)
                    {
                        if (pattern[y, x] == 1)
                        {
                            if (x + PointX < queues[0].Count)
                            {

                                match.Add(queues[y + PointY][x + PointX]);
                                queues[y + PointY].RemoveAt(x + PointX);

                            }
                        }

                    }
                    match.Reverse();
                    foreach (int X in match)
                    {

                        queues[y + PointY].Add(X);
                    }
                }
            }
            return queues;
        }
       
    }
}
