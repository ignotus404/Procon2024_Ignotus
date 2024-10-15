using EntryPoint;
using _2024ProconTemporary.CommandLine.Commands;


namespace _2024ProconTemporary.Base
{
    public class CommandClient : BaseCliCommands
    {
        [DefaultCommand]
        [Command("Boot")]
        [Help("受け取った問題を解くコマンド")]
        public void Boot(string[] args)
        {
            var options = Cli.Parse<BootCommandClient>(args);
            var command = new BootCommand();
            command.Handle(options);
        }


    }
}