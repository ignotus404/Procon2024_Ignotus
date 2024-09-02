using EntryPoint;
using System;
using _2024ProconTemporary.CommandLine.Commands;
using _2024ProconTemporary.Base;

class Program
{
    static void Main(string[] args)
    {
        Cli.Execute<CommandClient>(args);
    }
}
