using EntryPoint;
using System;
using _2024ProconTemporary.Base.Commands;
using _2024ProconTemporary.Base;

class Program
{
    static void Main(string[] args)
    {
        Cli.Execute<CommandClient>(args);
    }
}
