using System;
using EntryPoint;
using _2024ProconTemporary.Com;

namespace _2024ProconTemporary.CommandLine.Commands
{
    public class GetProblemCommandClient : BaseCliArguments
    {
        public GetProblemCommandClient() : base("GetProblem Command") { }
    }

    public class GetProblemCommand
    {
        public ProblemData Handle(GetProblemCommandClient args)
        {
            Console.WriteLine("GetProblem Command 開発中");
            var problemData = Networking.GetProblemData(Networking.CreateClient());

            if (problemData == null)
            {
                Console.WriteLine("ProblemData is null");
                return null;
            }

            // オプションが指定されている場合、ここで問題データを表示する





            return problemData;
        }
    }
}