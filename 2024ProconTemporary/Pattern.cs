using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2024ProconTemporary
{
    public class Pattern
    {
       public static List<List<List<int>>> PatternList = new List<List<List<int>>>();

        public static void Patterns()
        {
            int i;
            double j;
            int k = 1;
            Random Random = new Random();
            for (i = 0; i < 256; i++)
            {
                PatternList.Add(new List<List<int>>());
                int Warp = Random.Next(0, 257);
                int Side = Random.Next(0, 257);
                for (int y = 0; y < Warp; y++)
                {
                    PatternList[i].Add(new List<int>());
                    for (int x = 0; x < Side; x++)
                    {
                        PatternList[i][y].Add(Random.Next(0, 2));
                    }
                }
            }
            PatternList.Add(new List<List<int>>());
            PatternList[256].Add(new List<int>());
            PatternList[256][0].Add(1);

            
            for (i = 1, j = 2; j <= 256; i++, j = Math.Pow(2, i))
            { 
                for (int l = 1; k <= 3 * i; k++, l++)
                {
                    PatternList.Add(new List<List<int>>());
                    PatternList[k + 256] = PatternAssignment(PatternList[k + 256], l,i,i);
                }
            }
            
        }
        
        

        static List<List<int>> PatternAssignment(List<List<int>> PatternList, int Type, int Warp,int Side)
        {
            
            if (Type == 1)
            {

                for (int y = 0; y < Warp; y++)
                {
                    PatternList.Add(new List<int>());
                    for (int x = 0; x < Side; x++)
                    {
                        PatternList[y].Add(1);
                    }

                }


            }
            else if (Type == 2)
            {
                for (int y = 0; y < Warp; y ++)
                {
                    PatternList.Add(new List<int>());
                    for (int x = 0; x < Side; x++)
                    {
                        if (y / 2 == 0)
                        {
                            PatternList[y].Add(1);
                        }
                        else
                        {
                            PatternList[y].Add(0);
                        }
                    }
                }
            }
            else if (Type == 3)
            {
                for (int y = 0; y < Warp; y++)
                {
                    PatternList.Add(new List<int>());
                    for (int x = 0; x < Side; x += 2)
                    {

                        if (x / 2 == 0)
                        {
                            PatternList[y].Add(1);
                        }
                        else
                        {
                            PatternList[y].Add(0);
                        }
                    }
                }
            }

            return PatternList;
        }
    }
}
