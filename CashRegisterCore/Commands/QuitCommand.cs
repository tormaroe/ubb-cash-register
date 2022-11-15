namespace Bouvet.CashRegister.Core.Commands;

public class QuitCommand : ICommand
{
    public CommandResult Execute(List<string> arguments, Register register)
    {
        return new CommandResult
        {
            IsQuit = true
        };
    }
}
