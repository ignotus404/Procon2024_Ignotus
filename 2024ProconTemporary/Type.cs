using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2024ProconTemporary
{
    public class Type
    {
        public static float[][,] TypeList = new float[281][,];
        float i;
        double j;
        int k = 1;
        Random Random = new Random();
        void Awake()
        {
            for (var i = 0; i < 256; i++)
            {
                TypeList[i] = new float[Random.Next(1, 257), Random.Next(1, 257)];
                for (int y = 0; y < TypeList[i].GetLength(1); y++)
                {
                    for (int x = 0; x < TypeList[i].GetLength(0); x++)
                    {
                        TypeList[i][x, y] = Random.Next(0, 2);
                    }
                }
            }
            TypeList[256] = new float[1, 1]{

            { 1 }
        };
            for (i = 1, j = 2; j <= 256; i++, j = Math.Pow(2, i))
            {
                for (int l = 1; k <= 3 * i; k++, l++)
                {

                    TypeList[k + 256] = new float[(int)j, (int)j];
                    TypeList[k + 256] = Typeassignment(TypeList[k + 256], l);
                }
            }
        }

        float[,] Typeassignment(float[,] Case, int Type)
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
