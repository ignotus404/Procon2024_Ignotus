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
        static float i;
        static double j;
        public static void Types()
        {
            int k = 1;
            Random Random = new Random();
            for (var i = 0; i < 257; i++)
            {
                TypeList[i] = new float[Random.Next(0, 257), Random.Next(0, 257)];
                for (int y = 0; y < TypeList[i].GetLength(1); y++)
                {
                    for (int x = 0; x < TypeList[i].GetLength(0); x++)
                    {
                        TypeList[i][x, y] = Random.Next(0, 2);
                    }
                }
            }
            TypeList[255] = new float[1, 32]{
                  {1,1,1,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,0,1,0,0,0},
                //{0,0,1,0,1,0,2,2,1,3,0,0,3,2,2,3,3,3,2,3,2,1,3,2,1,2,2,2,0,3,1,1},
                //{3,0,2,1,1},Ques
                //{0,2,2,0,2,3,0,3},Ques
                //{0,0,1,2,2,3}
                //{,0,1,1,3,,2,3,3,3,,1,2,1,2,2}
                //{1,1,2,1,3,2,0,3,1,2,1,3,0,3,1,2,2,3,   0,2,0,2,2,0,2,3,0,3   ,3,0,2,1},Ans 答えの間から次の答えグループになるようにぬく
            };
            TypeList[256] = new float[15, 27]{

            { 1,1,1,0,1,0,1,1,1,0,1,1,1,0,1,0,1,1,1,1,1,0,1,1,1,0,1},
            { 0,0,0,1,1,0,0,0,1,1,0,1,1,0,0,0,1,1,1,1,0,1,1,1,0,0,1},
            { 0,0,1,0,1,0,1,0,0,1,0,0,0,1,0,0,0,1,1,1,0,1,0,1,1,0,0} ,
            { 0,0,0,1,0,0,1,1,1,1,0,0,1,1,0,1,0,1,1,0,0,0,0,0,0,1,1},
            { 1,1,1,0,0,1,1,0,1,0,0,1,0,1,1,0,1,1,1,0,1,0,0,0,1,0,1},
            { 0,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,0,0,0,1,1,0},
            { 0,1,0,0,0,1,0,0,0,1,0,0,1,0,0,1,1,0,1,0,0,0,1,1,1,0,1},
            { 0,0,1,1,0,1,1,0,1,0,1,0,1,1,1,0,0,0,1,0,0,1,1,0,1,1,1},
            { 0,1,1,0,0,0,0,0,1,0,1,0,0,0,1,1,1,1,0,1,1,1,0,0,0,1,0},
            { 0,1,1,0,1,0,0,0,1,1,0,1,0,1,1,1,0,0,0,1,0,0,1,1,0,0,1},
            { 0,1,0,1,0,0,0,0,0,0,0,1,0,1,0,0,1,0,1,0,0,0,1,1,1,0,0},
            { 0,1,0,0,0,0,1,0,0,0,1,1,0,1,1,0,0,0,1,1,1,0,0,0,1,0,1},
            { 0,0,1,1,1,1,0,0,1,0,1,0,0,1,0,1,0,0,0,0,1,1,1,0,0,1,1},
            { 0,1,1,0,0,1,0,0,1,1,0,0,0,0,0,0,1,1,0,1,1,1,0,0,1,0,1},
            { 1,1,1,0,1,0,0,0,0,0,0,1,1,1,0,0,0,0,1,1,0,0,1,0,0,0,1},

        };

            for ( i = 1, j = 2; j <= 256; i++, j = Math.Pow(2, i))
              {
                  for (int l = 1; k <= 3 * i; k++, l++)
                  {

                      TypeList[k + 256] = new float[(int)j, (int)j];
                      TypeList[k + 256] = Typeassignment(TypeList[k + 256], l);
                  }
              }
            
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
