using System;
using EntryPoint;

namespace _2024ProconTemporary.Base.Commands
{
    public class PostAnswerCommandClient : BaseCliArguments
    {
        public PostAnswerCommandClient() : base("PostAnswer Command") { }
    }

    public class PostAnswerCommand
    {
        public void Handle(PostAnswerCommandClient args)
        {
            Console.WriteLine("PostAnswer Command 開発中");

        }
    }
}