namespace Bouvet.CashRegister.Core.Commands;

internal class PrintMenuCommand : ICommand
{
    public string DescriptiveName => "Print menu";
    public string CommandName => "menu";
    public string UsageExample => CommandName;

    public CommandResult Execute(List<string> arguments, Register register)
    {
        register.PrintMenu();
        return new CommandResult();
    }
}
