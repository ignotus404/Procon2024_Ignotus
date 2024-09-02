using System;
using EntryPoint;

namespace _2024ProconTemporary.Base.Commands
{
    public class EnterAnswerModeCommandClient : BaseCliArguments
    {
        public EnterAnswerModeCommandClient() : base("EnterAnswerMode Command") { }
    }

    public class EnterAnswerModeCommand
    {
        public void Handle(EnterAnswerModeCommandClient args)
        {
            Console.WriteLine("EnterAnswerMode Command 開発中");

        }
    }
}