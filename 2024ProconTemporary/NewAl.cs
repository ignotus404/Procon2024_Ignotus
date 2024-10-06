using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _2024ProconTemporary
{
    internal class NewAl
    {
        Case cases = new Case();
        Practice practice = new Practice();
        Type Type = new Type();
        static int[][] WantListX = new int[Practice.pieceX][];
        static List<List<int>> WantListXT = new List<List<int>>();
        static int[][] WantListXS = new int[Practice.pieceX][];
        static int[][] WantListY = new int[4][];
        static List<List<List<int>>> NumberIndexList = new List<List<List<int>>>();
        static List<List<List<int>>> ZeroIndexList = new List<List<List<int>>>();
        static List<List<List<int>>> OneIndexList = new List<List<List<int>>>();
        
        public static int Ymax = 0;
        public static int Xmax = 0;
        public static void Kari()
        {

                Ymax = Practice.Ans.Count -1;
                Xmax = Practice.Ans[0].Count -1;
                IndexCount(Practice.QuesTes2);
                Hyoji(NumberIndexList[0]);
                MainTest(Practice.QuesTes2,Practice.AnsTes2,0,false);
                
        }
        public static void Hyoji(List<List<int>> Ans)
        {
            for (int i = 0; i < Ans.Count; i++)
            {
                /* for (int j = 0; j < Ans[i].Count; j++)
                 {
                     Console.Write(Ans[i][j]);
                 }
                */
                string result = string.Join(",", Ans[i]);
                result = string.Join("new List<int>", result);
                Console.WriteLine(result);
            }

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
            
            for(int i = 0; i < 4; i++)
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
       
        public static List<List<int>> MainTest(List<List<int>> Ques, List<List<int>> Ans,int N,bool T)
        {
            List<List<int>> WantZeroIndex = new List<List<int>>();
            List<List<int>> WantOneIndex = new List<List<int>>();
            int Value = 0;
            var TypeCanList = new List<List<int>>();
            var RemainList = Case.Copy(Ques);
            for (int Y = 0; Y < Ans.Count; Y++)
            {
                WantZeroIndex.Add(new List<int>());
                WantZeroIndex[Y] =  Search(Ques,Ans, WantZeroIndex[Y],1,Y);
                WantOneIndex.Add(new List<int>());
                WantOneIndex[Y] = NextSearch(Ques, Ans, WantOneIndex[Y],Y, WantOneIndex[Y][WantOneIndex[Y].Count],WantZeroIndex);
            }
            return Ques;

        }
        public static List<int> Search(List<List<int>> Ques, List<List<int>> Ans, List<int> IndexList,int exclusion,int Y)
        {
            int N = 0;
            for (int Number = Xmax; Number > 0 + exclusion; Number--)
            {
                int QuesNumberIndex = NumberIndexList[Ans[Y][Number - exclusion - 1]][Y].FindLast(item => item > N);
                IndexList.Add(QuesNumberIndex);
                N += QuesNumberIndex;
                NumberIndexList[Ans[Y][Number - exclusion - 1]][Y].Remove(QuesNumberIndex);
                if (QuesNumberIndex < 0)
                {
                    IndexList = IndexList.FindAll(item => item >= 0);
                    break;
                }
            }
            return IndexList;
        }
        public static List<int> NextSearch(List<List<int>> Ques, List<List<int>> Ans, List<int> IndexList, int Y, int AddIndex, List<List<int>> WantTypeIndex)
        {
            int N = AddIndex -1;
            int QuesNumberIndex = 0;
            for (int Number = Xmax; Number >= 0; Number--)
            {
                if (N <= AddIndex)
                {
                     QuesNumberIndex = NumberIndexList[Ans[Y][Number - WantTypeIndex[Y].Count - 1]][Y].Find(item => item < N);
                    if (QuesNumberIndex >= 0)
                    {
                        IndexList.Add(QuesNumberIndex);
                        N += QuesNumberIndex;
                        NumberIndexList[Ans[Y][Number - WantTypeIndex[Y].Count - 1]][Y].Remove(QuesNumberIndex);
                    }
                    else
                    {
                        N = Xmax - 1;
                        continue;
                    }
                }
                else if (N > AddIndex)
                {
                    QuesNumberIndex = NumberIndexList[Ans[Y][Number - WantTypeIndex[Y].Count - 1]][Y].Find(item => item <= N && item > AddIndex);
                    IndexList.Add(QuesNumberIndex);
                    N += QuesNumberIndex;
                    NumberIndexList[Ans[Y][Number - WantTypeIndex[Y].Count - 1]][Y].Remove(QuesNumberIndex);
                }
                if(QuesNumberIndex < 0)
                {
                    IndexList = IndexList.FindAll(item => item >= 0);
                    break;
                }
            }
            return IndexList;
        }
        public static void EvaluationValue(List<List<int>> Ques, List<List<int>> Ans, List<List<List<int>>> WantTypeIndex)
        {
            for (int i = 0; i < 281; i++)
            {
                for (int Y = 0; Y < Type.TypeList[i].GetLength(0); Y++)
                {
                    
                }
            }

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
                for (int Y = 0; Y < Type.TypeList[i].GetLength(0); Y++)
                {
                    ZeroIndexList[i].Add(new List<int>());
                    OneIndexList[i].Add(new List<int>());
                    for (int X = 0; X < Type.TypeList[i].GetLength(1); X++)
                    {
                        if (Type.TypeList[i][Y,X] == 0)
                        {
                            ZeroIndexList[i][Y].Add(X);
                        }
                        else if (Type.TypeList[i][Y, X] == 1)
                        {
                            OneIndexList[i][Y].Add(X);
                        }

                    }

                    if (ZeroIndexList[i][Y].Count == 0)
                    {
                        ZeroIndexList[i][Y].Add(0);
                    } if (OneIndexList[i][Y].Count == 0)
                    {
                        OneIndexList[i][Y].Add(0);
                    }

                }
            }

        }

    }
}
