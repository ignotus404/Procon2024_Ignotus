using System;
using _2024ProconTemporary.Com;
using System.Linq;

namespace _2024ProconTemporary.ReadableData
{
    public class ReadableProblemData
    {
        public ReadableBoardData Board { get; set; }
        public ReadableGeneralData General { get; set; }

        public ReadableProblemData(ProblemData problemData)
        {
            Board = new ReadableBoardData(problemData.Board.Width, problemData.Board.Height, problemData.Board.Start, problemData.Board.Goal);
            General = new ReadableGeneralData(problemData.General.N, problemData.General.Patterns.Select(p => new ReadableGeneralData.ReadablePatternData(p.P, p.Width, p.Height, p.Cells)).ToList());
        }

        public void Print()
        {
            Console.WriteLine("Board Data:");
            Console.WriteLine($"Width: {Board.Width}");
            Console.WriteLine($"Height: {Board.Height}");
            Console.WriteLine("Start:");
            foreach (var s in Board.Start)
            {
                Console.WriteLine(string.Join(" ", s));
            }
            Console.WriteLine("Goal:");
            foreach (var g in Board.Goal)
            {
                Console.WriteLine(string.Join(" ", g));
            }

            Console.WriteLine("General Data:");
            Console.WriteLine($"N: {General.N}");
            Console.WriteLine("Patterns:");
            foreach (var p in General.Patterns)
            {
                Console.WriteLine($"P: {p.P}");
                Console.WriteLine($"Width: {p.Width}");
                Console.WriteLine($"Height: {p.Height}");
                Console.WriteLine("Cells:");
                foreach (var c in p.Cells)
                {
                    Console.WriteLine(string.Join(" ", c));
                }
            }
        }

        public class ReadableBoardData
        {
            public int Width { get; set; }
            public int Height { get; set; }
            public IList<List<int>> Start { get; set; }
            public IList<List<int>> Goal { get; set; }
            public ReadableBoardData(int width, int height, IList<string> start, IList<string> goal)
            {
                Width = width;
                Height = height;

                Start = new List<List<int>>();

                foreach (var s in start)
                {
                    var list = new List<int>();
                    IEnumerable<char> index = s.ToCharArray();
                    foreach (var c in index)
                    {
                        list.Add(c - '0');
                    }
                    Start.Add(list);
                }

                Goal = new List<List<int>>();

                foreach (var g in goal)
                {
                    var list = new List<int>();
                    IEnumerable<char> index = g.ToCharArray();
                    foreach (var c in index)
                    {
                        list.Add(c - '0');
                    }
                    Goal.Add(list);
                }
            }
        }

        public class ReadableGeneralData
        {
            public int N;
            public IList<ReadablePatternData> Patterns { get; set; }

            public ReadableGeneralData(int n, IList<ReadablePatternData> patterns)
            {
                N = n;
                Patterns = patterns;
            }

            public class ReadablePatternData
            {
                public int P { get; set; }
                public int Width { get; set; }
                public int Height { get; set; }
                public IList<List<int>> Cells { get; set; }
                public ReadablePatternData(int p, int width, int height, IList<string> cells)
                {
                    P = p;
                    Width = width;
                    Height = height;

                    Cells = new List<List<int>>();
                    foreach (var c in cells)
                    {
                        var list = new List<int>();
                        IEnumerable<char> index = c.ToCharArray();
                        foreach (var i in index)
                        {
                            list.Add(i - '0');
                        }
                        Cells.Add(list);
                    }
                }


            }
        }
    }
}
