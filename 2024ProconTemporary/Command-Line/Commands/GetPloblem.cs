using System;
using EntryPoint;

namespace _2024ProconTemporary.CommandLine.Commands
{
    public class GetProblemCommandClient : BaseCliArguments
    {
        public GetProblemCommandClient() : base("GetProblem Command") { }
    }

    public class GetProblemCommand
    {
        public void Handle(GetProblemCommandClient args)
        {
            Console.WriteLine("GetProblem Command 開発中");

        }
    }
}