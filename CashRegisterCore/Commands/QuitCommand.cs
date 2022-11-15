namespace Bouvet.CashRegister.Core.Commands;

internal class QuitCommand : ICommand
{
    public string DescriptiveName => "Quit";
    public string CommandName => "quit";
    public string UsageExample => CommandName;

    public CommandResult Execute(List<string> arguments, Register register)
    {
        return new CommandResult
        {
            IsQuit = true
        };
    }
}
