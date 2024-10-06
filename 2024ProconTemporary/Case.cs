using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2024ProconTemporary
{
    public class Case
    {
        public static  List<List<int>> Transpos(List<List<int>> Ques)
        {
            var resultList = new List<List<int>>();
            foreach (var row in Ques.Select((v, i) => new { v, i }))
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


        public static  List<List<int>> DieCuttingUP(List<List<int>> Ques, float[,] Type, int PointY, int PointX)
        {
            var QuesT = Transpos(Ques);
                for (int y = 0; y < Type.GetLength(1); y++)
                {
                    var match = new List<int>();
                    if (y + PointY < Ques[0].Count )
                    {
                        for (int x = Type.GetLength(0) - 1; x >= 0; x--)
                        {
                            if (Type[x, y] == 1)
                            {
                                if (x + PointX < Ques.Count)
                                {
                                
                                    match.Add(Ques[x + PointX][y + PointY]);
                                    QuesT[y + PointY].RemoveAt(x + PointX);

                                }
                            }

                        }
                        match.Reverse();
                        foreach (int X in match)
                        {
                            QuesT[y + PointY].Add(X);
                        }
                    }
                }
            QuesT = Transpos(QuesT);
            return QuesT;
        }
        public static List<List<int>> DieCuttingDown(List<List<int>> Ques, float[,] Type, int PointY, int PointX)
        {
            var QuesT = Transpos(Ques);
            for (int y = 0; y < Type.GetLength(1); y++)
            {
                var match = new List<int>();
                if (y + PointY < Ques[0].Count + Type.GetLength(1) && y + PointY > Ques[0].Count + Type.GetLength(1))
                {
                    for (int x = Type.GetLength(0) - 1; x >= 0; x--)
                    {
                        if (Type[x, y] == 1)
                        {
                            if (x + PointX < Ques.Count + Type.GetLength(0) && x + PointX > Ques.Count + Type.GetLength(0))
                            {
                                match.Add(Ques[x + PointX][y + PointY]);
                                QuesT[y + PointY].RemoveAt(x + PointX);

                            }
                        }

                    }
                    QuesT[y + PointY].Reverse();
                    foreach (int X in match)
                    {
                        QuesT[y + PointY].Add(X);
                    }
                    QuesT[y + PointY].Reverse();
                }
            }
            QuesT = Transpos(QuesT);
            return QuesT;
        }
        public static List<List<int>> DieCuttingRight(List<List<int>> Ques, float[,] Type, int PointX, int PointY)
        {
            var ques = Copy(Ques);
            for (int y = 0; y < Type.GetLength(0); y++)
            {
                var match = new List<int>();
                if (y + PointY < Ques.Count)
                {
                    for (int x = Type.GetLength(1) - 1; x >= 0; x--)
                    {
                        if (Type[y, x] == 1)
                        {
                            if (x + PointX < Ques[0].Count)
                            {
                                match.Add(Ques[y + PointY][x + PointX]);
                                ques[y + PointY].RemoveAt(x + PointX);

                            }
                        }

                    }
                    ques[y + PointY].Reverse();
                    foreach (int X in match)
                    {
                        ques[y + PointY].Add(X);
                    }
                    ques[y + PointY].Reverse();
                }
            }
            return ques;
        }
        public static List<List<int>> DieCuttingLeft(List<List<int>> Ques, float[,] Type, int PointX, int PointY)
        {
            var ques = Copy(Ques);
            for (int y = 0; y < Type.GetLength(1); y++)
            {
                var match = new List<int>();
                if (y + PointY < Ques.Count)
                {
                    for (int x = Type.GetLength(1) - 1; x >= 0; x--)
                    {
                        if (Type[y, x] == 1)
                        {
                            if (x + PointX < Ques[0].Count)
                            {

                                match.Add(Ques[y + PointY][x + PointX]);
                                ques[y + PointY].RemoveAt(x + PointX);

                            }
                        }

                    }
                    match.Reverse();
                    foreach (int X in match)
                    {

                        ques[y + PointY].Add(X);
                    }
                }
            }
            return ques;
        }
       
    }
}
