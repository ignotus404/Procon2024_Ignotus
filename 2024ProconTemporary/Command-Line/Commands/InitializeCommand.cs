using System;
using EntryPoint;

namespace _2024ProconTemporary.CommandLine.Commands
{
    public class InitializeCommandClient : BaseCliArguments
    {
        public InitializeCommandClient() : base("Initialize Command") { }
    }

    public class InitializeCommand
    {
        public void Handle(InitializeCommandClient args)
        {
            Console.WriteLine("Initialize Command 開発中");
        }
    }
}