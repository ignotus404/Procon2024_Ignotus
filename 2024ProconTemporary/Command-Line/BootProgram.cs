using EntryPoint;
using System;
using _2024ProconTemporary.CommandLine.Commands;
using _2024ProconTemporary.Base;
using _2024ProconTemporary.Com;
using System.Security.Cryptography.X509Certificates;

class BootProgram
{

    static void Main(string[] args)
    {
        Cli.Execute<CommandClient>(args);
    }
}