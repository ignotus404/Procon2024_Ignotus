using EntryPoint;
using _2024ProconTemporary.CommandLine.Commands;


namespace _2024ProconTemporary.Base
{
    public class CommandClient : BaseCliCommands
    {
        [DefaultCommand]
        [Command("Initialize")]
        [Help("登録された情報を初期化するコマンド")]
        public void Initialize(string[] args)
        {
            var options = Cli.Parse<InitializeCommandClient>(args);
            var command = new InitializeCommand();
            command.Handle(options);
        }

        [Command("GetProblem")]
        [Help("問題を取得するコマンド")]
        public void GetProblem(string[] args)
        {
            var options = Cli.Parse<GetProblemCommandClient>(args);
            var command = new GetProblemCommand();
            command.Handle(options);
        }

        [Command("EnterAnswerMode")]
        [Help("解答モードに入るコマンド")]
        public void EnterAnswerMode(string[] args)
        {
            var options = Cli.Parse<EnterAnswerModeCommandClient>(args);
            var command = new EnterAnswerModeCommand();
            command.Handle(options);
        }

        [Command("PostAnswer")]
        [Help("解答を投稿するコマンド")]
        public void PostAnswer(string[] args)
        {
            var options = Cli.Parse<PostAnswerCommandClient>(args);
            var command = new PostAnswerCommand();
            command.Handle(options);
        }
    }
}