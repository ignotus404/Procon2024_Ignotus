using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2024ProconTemporary
{
    public class Pattern
    {
        public static float[][,] PatternList = new float[281][,];

        public static void Patterns()
        {
            int i;
            double j;
            int k = 1;
            Random Random = new Random();
            for (i = 0; i < 257; i++)
            {
                PatternList[i] = new float[Random.Next(0, 257), Random.Next(0, 257)];
                for (int y = 0; y < PatternList[i].GetLength(1); y++)
                {
                    for (int x = 0; x < PatternList[i].GetLength(0); x++)
                    {
                        PatternList[i][x, y] = Random.Next(0, 2);
                    }
                }
            }

            for (i = 1, j = 2; j <= 256; i++, j = Math.Pow(2, i))
            {
                for (int l = 1; k <= 3 * i; k++, l++)
                {

                    PatternList[k + 256] = new float[(int)j, (int)j];
                    PatternList[k + 256] = Typeassignment(PatternList[k + 256], l);
                }
            }
            PatternList[255] = new float[15, 32]
            {
               { 1,0,1,1,0,0,0,1,1,1,1,0,0,0,0, 1,0,1,1,0,0,0,1,1,1,1,0,0,1,0,1,1 },
               { 1,1,1,1,0,0,0,1,1,1,1,0,0,0,0, 1,0,1,1,0,0,0,1,1,1,1,0,0,0,0,1,0 },
               { 1,0,1,1,0,0,0,1,1,1,1,0,0,0,1, 1,0,1,1,0,0,1,0,0,1,1,0,1,0,1,1,1 },
               { 1,0,1,1,0,0,0,1,0,1,1,0,0,0,1, 1,0,1,1,0,1,1,1,0,1,1,0,0,0,1,1,1 },
               { 1,0,1,1,0,0,0,1,0,1,1,0,0,1,1, 1,0,1,1,0,1,0,1,0,0,1,0,0,0,1,1,1 },
               { 1,0,1,1,0,0,0,1,0,1,1,0,1,1,0, 1,0,1,1,0,1,0,1,1,1,0,0,1,0,0,1,0 },
               { 1,0,1,1,0,0,0,1,0,1,1,0,1,0,0, 1,0,1,1,0,1,0,1,1,1,1,1,0,0,1,1,0 },
               { 1,0,1,1,0,0,0,1,0,0,1,0,1,0,0, 1,0,1,1,0,1,0,1,0,1,0,1,1,0,0,1,1 },
               { 1,0,1,1,0,0,0,1,1,0,1,0,1,0,0, 1,0,1,1,0,1,0,1,0,1,1,1,0,0,0,1,0 },
               { 1,0,1,1,0,0,1,1,1,0,0,1,1,0,0, 1,0,1,1,0,1,0,1,0,1,0,1,0,0,0,1,1 },
               { 1,0,1,1,0,0,0,1,1,1,1,0,0,0,0, 1,0,1,1,0,1,0,1,1,1,1,0,0,0,0,1,0 },
               { 1,0,1,1,0,0,0,1,1,1,1,0,0,0,0, 1,0,1,1,1,1,0,1,1,1,1,0,1,1,0,1,0 },
               { 1,0,1,1,0,0,0,1,1,1,1,0,0,0,0, 1,0,1,1,0,0,0,1,1,1,1,0,0,1,1,1,0 },
               { 1,0,1,1,0,0,0,1,1,1,1,0,0,0,0, 1,0,1,1,0,0,0,1,1,1,1,0,0,1,1,1,1 },
               { 1,0,1,1,0,0,0,1,1,1,1,0,0,0,0, 1,0,1,1,0,0,0,1,1,1,1,0,0,1,1,1,0 },
               
               
            };
        }
        
        

        static float[,] Typeassignment(float[,] Case, int Type)
        {
            if (Type == 1)
            {
                for (int y = 0; y < Case.GetLength(1); y++)
                {
                    for (int x = 0; x < Case.GetLength(0); x++)
                    {
                        Case[x, y] = 1;
                    }

                }


            }
            else if (Type == 2)
            {
                for (int y = 0; y < Case.GetLength(1); y += 2)
                {
                    for (int x = 0; x < Case.GetLength(0); x++)
                    {

                        Case[x, y] = 1;
                    }
                }
            }
            else if (Type == 3)
            {
                for (int y = 0; y < Case.GetLength(1); y++)
                {
                    for (int x = 0; x < Case.GetLength(0); x += 2)
                    {

                        Case[x, y] = 1;
                    }
                }
            }

            return Case;
        }
    }
}
