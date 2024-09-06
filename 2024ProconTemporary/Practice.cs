using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2024ProconTemporary
{
    //Test
    //
    public class Practice
    {
        public static int pieceX;
        public static int pieceY;
        Random random = new Random();
        public static List<List<int>> answer = new List<List<int>>();
        public static List<List<int>> queues = new List<List<int>>();
        public void Awake()
        {
            Question();
            Answer(answer);
        }

        public void Question()
        {
            for (int y = 0; y < pieceY; y++)
            {
                List<int> ansOne = new List<int>();
                for (int x = 0; x < pieceX; x++)
                {
                    ansOne.Add(random.Next(0, 4));
                }
                answer.Add(ansOne);

            }


        }
        public void Answer(List<List<int>> ans)
        {

            Random rand = new Random();
            List<List<int>> ansChange = new List<List<int>>();
            List<int> shuffle = new List<int>();
            int j = 0;
            for (int y = 0; y < pieceY; y++)
            {
                for (int x = 0; x < pieceX; x++)
                {
                    shuffle.Add(ans[y][x]);
                }
            }
            List<int> result = shuffle.OrderBy(x => rand.Next()).ToList();
            for (int y = 0; y < pieceY; y++)
            {
                List<int> queuesOne = new List<int>();
                for (int x = 0; x < pieceX; x++, j++)
                {
                    queuesOne.Add(result[j]);
                }
                queues.Add(queuesOne);
            }
        }
    }
}
