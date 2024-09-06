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
        public static List<List<int>> DieCuttingUp(List<List<int>> queues, float[,] pattern, int pointX, int pointY)
        {
            var queuesT = TranslatePos(queues);
            for (int y = 0; y < pattern.GetLength(1); y++)
            {
                var match = new List<int>();
                if (y + pointY < queues[0].Count)
                {
                    for (int x = pattern.GetLength(0) - 1; x >= 0; x--)
                    {
                        if (pattern[x, y] == 1)
                        {
                            if (x + pointX < queues.Count)
                            {
                                match.Add(queues[x + pointX][y + pointY]);
                                queuesT[y + pointY].RemoveAt(x + pointX);

                            }
                        }

                    }
                    match.Reverse();
                    foreach (int x in match)
                    {
                        queuesT[y + pointY].Add(x);
                    }
                }
            }
            queuesT = TranslatePos(queuesT);
            return queuesT;
        }
        public static List<List<int>> DieCuttingDown(List<List<int>> queues, float[,] pattern, int pointX, int pointY)
        {
            var queuesT = TranslatePos(queues);
            for (int y = 0; y < pattern.GetLength(1); y++)
            {
                var match = new List<int>();
                if (y + pointY < queues[0].Count)
                {
                    for (int x = pattern.GetLength(0) - 1; x >= 0; x--)
                    {
                        if (pattern[x, y] == 1)
                        {
                            if (x + pointX < queues.Count)
                            {
                                match.Add(queues[x + pointX][y + pointY]);
                                queuesT[y + pointY].RemoveAt(x + pointX);

                            }
                        }

                    }
                    queuesT[y + pointY].Reverse();
                    foreach (int x in match)
                    {
                        queuesT[y + pointY].Add(x);
                    }
                    queuesT[y + pointY].Reverse();
                }
            }
            queuesT = TranslatePos(queuesT);
            return queuesT;
        }
        public static List<List<int>> DieCuttingRight(List<List<int>> queues, float[,] pattern, int pointX, int pointY)
        {

            for (int y = 0; y < pattern.GetLength(0); y++)
            {
                var match = new List<int>();
                if (y + pointY < queues.Count)
                {
                    for (int x = pattern.GetLength(1) - 1; x >= 0; x--)
                    {
                        if (pattern[y, x] == 1)
                        {
                            if (x + pointX < queues[0].Count)
                            {
                                match.Add(queues[y + pointY][x + pointX]);
                                queues[y + pointY].RemoveAt(x + pointX);

                            }
                        }

                    }
                    queues[y + pointY].Reverse();
                    foreach (int x in match)
                    {
                        queues[y + pointY].Add(x);
                    }
                    queues[y + pointY].Reverse();
                }
            }
            return queues;
        }
        public static List<List<int>> DieCuttingLeft(List<List<int>> queues, float[,] pattern, int pointX, int pointY)
        {
            for (int y = 0; y < pattern.GetLength(0); y++)
            {
                var match = new List<int>();
                if (y + pointY < queues.Count)
                {
                    for (int x = pattern.GetLength(1) - 1; x >= 0; x--)
                    {
                        if (pattern[y, x] == 1)
                        {
                            if (x + pointX < queues[0].Count)
                            {

                                match.Add(queues[y + pointY][x + pointX]);
                                queues[y + pointY].RemoveAt(x + pointX);

                            }
                        }

                    }
                    match.Reverse();
                    foreach (int x in match)
                    {

                        queues[y + pointY].Add(x);
                    }
                }
            }
            return queues;
        }
    }
}
