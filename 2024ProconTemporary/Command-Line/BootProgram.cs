using EntryPoint;
using System;
using _2024ProconTemporary.CommandLine.Commands;
using _2024ProconTemporary.Base;
using _2024ProconTemporary.Com;

class BootProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("Booting...");

        // 問題データを取得する
        Console.WriteLine("Getting Problem Data...");
        HttpClient client = Networking.CreateClient();
        var problemData = Networking.GetProblemData(client);
        if (problemData == null)
        {
            Console.WriteLine("ERROR: ProblemData Download Failed!");
            Console.WriteLine("Please check your network connection and try again.");
            return;
        }
        Console.WriteLine("Done!");


        // 問題の処理部分
        // 入力された文字列のコマンドによって処理を分岐する
        while (true)
        {
            Console.WriteLine("Please input command:");
            string command = Console.ReadLine();
            if (command == "exit")
            {
                break;
            }
            /*
            else if (command == "get")
            {
                GetProblemCommandClient getProblemCommandClient = new GetProblemCommandClient();
                GetProblemCommand getProblemCommand = new GetProblemCommand();
                getProblemCommand.Handle(getProblemCommandClient);
            }
            else if (command == "show")
            {
                ShowProblemCommandClient showProblemCommandClient = new ShowProblemCommandClient();
                ShowProblemCommand showProblemCommand = new ShowProblemCommand();
                showProblemCommand.Handle(showProblemCommandClient, problemData);
            }
            */
            else
            {
                Console.WriteLine("Invalid Command!");
            }
        }
    }
}
