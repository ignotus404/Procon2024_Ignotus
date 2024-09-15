using System;

namespace _2024ProconTemporary
{

    public class MainAlgorithm
    {
        Case cases = new Case();
        Practice practice = new Practice();
        Pattern _pattern = new Pattern();
        List<List<int>> MatchInfo = new List<List<int>>();
        List<List<int>> MatchInfos = new List<List<int>>();
        delegate List<List<int>> DieCutting(List<List<int>> queues, float[,] pattern, int pointX, int pointY);
        static void Main()
        {


            MatchCalculate();
        }

        static void MatchCalculate()
        {

            Case cases = new Case();
            List<List<int>> matchInfo = new List<List<int>>();

            float maxMatch = 0f;
            int u = 0;
            var queues = new List<List<int>>();
            DieCutting dieCutting;
            while (u < 10)
            {
                for (int direction = 0; direction < 4; direction++)
                {

                    if (direction == 0)
                    {
                        dieCutting = Case.DieCuttingUp;
                    }
                    else if (direction == 1)
                    {
                        dieCutting = Case.DieCuttingDown;
                    }
                    else if (direction == 2)
                    {
                        dieCutting = Case.DieCuttingLeft;
                    }
                    else
                    {

                        dieCutting = Case.DieCuttingRight;
                    }
                    for (int i = 0; i < 281; i++)
                    {
                        for (int y = 0; y < Practice.pieceY; y++)
                        {
                            for (int x = 0; x < Practice.pieceX; x++)
                            {
                                float matchR = MainAlgorithm.Check(Practice.answer, dieCutting(Practice.queues, Pattern.patternList[i], x, y));
                                if (matchR > maxMatch)
                                {
                                    maxMatch = matchR;
                                    queues = dieCutting(Practice.queues, Pattern.patternList[i], x, y);
                                    matchInfo.Add(new List<int> { i, x, y, direction });
                                }

                            }
                        }
                    }
                }
                u++;
                Console.WriteLine(maxMatch);
            }
        }
        public static float Check(List<List<int>> ans, List<List<int>> queues)
        {
            var mass = Practice.pieceX * Practice.pieceY;
            float match = 100f / mass;
            float matchR = 0;
            for (int y = 0; y < Practice.pieceY; y++)
            {
                for (int x = 0; x < Practice.pieceX; x++)
                {
                    if (ans[y][x] == queues[y][x])
                    {
                        matchR += match;
                    }

                }
            }
            return matchR;
        }

    }

}
