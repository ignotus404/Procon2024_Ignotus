using System;

namespace _2024ProconTemporary
{

    public class Mainalgorithm
    {




        Case cases = new Case();
        Practice practice = new Practice();
        Type Type = new Type();
        List<List<int>> MatchInfo = new List<List<int>>();
        List<List<int>> MatchInfos = new List<List<int>>();
        delegate List<List<int>> DieCutting(List<List<int>> Ques, float[,] Type, int PointX, int PointY);
        static void Main()
        {
            
            
            Matchcalculate();
        }

        static void Matchcalculate()
        {

            Case cases = new Case();
            List<List<int>> MatchInfo = new List<List<int>>();

            float MAXmatch = 0f;
            int u = 0;
            var ques = new List<List<int>>();
            DieCutting DieCutting;
            while (u < 10)
            {
                for (int direction = 0; direction < 4; direction++)
                {

                    if (direction == 0)
                    {
                        DieCutting = Case.DieCuttingUP;
                    }
                    else if (direction == 1)
                    {
                        DieCutting = Case.DieCuttingDown;
                    }
                    else if (direction == 2)
                    {
                        DieCutting = Case.DieCuttingLeft;
                    }
                    else
                    {

                        DieCutting = Case.DieCuttingRight;
                    }
                    for (int i = 0; i < 281; i++)
                    {
                        for (int y = 0; y < Practice.pieceY; y++)
                        {
                            for (int x = 0; x < Practice.pieceX; x++)
                            {
                                float MatchR = Mainalgorithm.Check(Practice.Anser, DieCutting(Practice.Ques, Type.TypeList[i], x, y));
                                if (MatchR > MAXmatch)
                                {
                                    MAXmatch = MatchR;
                                    ques = DieCutting(Practice.Ques, Type.TypeList[i], x, y);
                                    MatchInfo.Add(new List<int> { i, x, y, direction });
                                }

                            }
                        }
                    }
                }
                u++;
                Console.WriteLine(MAXmatch);
            }
        }
        public static float Check(List<List<int>> Ans, List<List<int>> Ques)
        {
            var mass = Practice.pieceX * Practice.pieceY;
            float match = 100f / mass;
            float matchR = 0;
            for (int y = 0; y < Practice.pieceY; y++)
            {
                for (int x = 0; x < Practice.pieceX; x++)
                {
                    if (Ans[y][x] == Ques[y][x])
                    {
                        matchR += match;
                    }

                }
            }
            return matchR;
        }

    }
    
}
