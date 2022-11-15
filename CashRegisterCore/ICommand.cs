namespace Bouvet.CashRegister.Core;

internal interface ICommand
{
    string DescriptiveName { get; }
    string CommandName { get; }
    string UsageExample { get; }

    CommandResult Execute(List<string> arguments, Register register);
}
