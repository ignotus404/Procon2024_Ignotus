using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2024ProconTemporary
{
    public class Pattern
    {
        public static float[][,] patternList = new float[281][,];
        float i;
        double j;
        int k = 1;
        Random random = new Random();
        void Awake()
        {
            for (var i = 0; i < 256; i++)
            {
                patternList[i] = new float[random.Next(1, 257), random.Next(1, 257)];
                for (int y = 0; y < patternList[i].GetLength(1); y++)
                {
                    for (int x = 0; x < patternList[i].GetLength(0); x++)
                    {
                        patternList[i][x, y] = random.Next(0, 2);
                    }
                }
            }
            patternList[256] = new float[1, 1]{

            { 1 }
        };
            for (i = 1, j = 2; j <= 256; i++, j = Math.Pow(2, i))
            {
                for (int l = 1; k <= 3 * i; k++, l++)
                {

                    patternList[k + 256] = new float[(int)j, (int)j];
                    patternList[k + 256] = PatternAssignment(patternList[k + 256], l);
                }
            }
        }

        float[,] PatternAssignment(float[,] @case, int type)
        {
            if (type == 1)
            {
                for (int y = 0; y < @case.GetLength(1); y++)
                {
                    for (int x = 0; x < @case.GetLength(0); x++)
                    {
                        @case[x, y] = 1;
                    }

                }


            }
            else if (type == 2)
            {
                for (int y = 0; y < @case.GetLength(1); y += 2)
                {
                    for (int x = 0; x < @case.GetLength(0); x++)
                    {

                        @case[x, y] = 1;
                    }
                }
            }
            else if (type == 3)
            {
                for (int y = 0; y < @case.GetLength(1); y++)
                {
                    for (int x = 0; x < @case.GetLength(0); x += 2)
                    {

                        @case[x, y] = 1;
                    }
                }
            }

            return @case;
        }
    }
}
