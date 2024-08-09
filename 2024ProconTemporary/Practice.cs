using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2024ProconTemporary
{
    //Test
    public class Practice
    {
        public static int pieceX;
        public static int pieceY;
        Random Random = new Random();
        public static List<List<int>> Anser = new List<List<int>>();
        public static List<List<int>> Ques = new List<List<int>>();
        public void Awake()
        {
            Question();
            Answer(Anser);
        }

        public void Question()
        {
            for (int y = 0; y < pieceY; y++)
            {
                List<int> Ansone = new List<int>();
                for (int x = 0; x < pieceX; x++)
                {
                    Ansone.Add(Random.Next(0, 4));
                }
                Anser.Add(Ansone);

            }


        }
        public void Answer(List<List<int>> Ans)
        {

            Random rand = new Random();
            List<List<int>> AnsChange = new List<List<int>>();
            List<int> shufle = new List<int>();
            int j = 0;
            for (int y = 0; y < pieceY; y++)
            {
                for (int x = 0; x < pieceX; x++)
                {
                    shufle.Add(Ans[y][x]);
                }
            }
            List<int> result = shufle.OrderBy(x => rand.Next()).ToList();
            for (int y = 0; y < pieceY; y++)
            {
                List<int> Quesone = new List<int>();
                for (int x = 0; x < pieceX; x++, j++)
                {
                    Quesone.Add(result[j]);
                }
                Ques.Add(Quesone);
            }
        }
    }
}
