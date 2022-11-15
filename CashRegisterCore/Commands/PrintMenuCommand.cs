namespace Bouvet.CashRegister.Core.Commands;

public class PrintMenuCommand : ICommand
{
    public CommandResult Execute(List<string> arguments, Register register)
    {
        register.PrintMenu();
        return new CommandResult();
    }
}
