namespace Bouvet.CashRegister.Core;

public interface ICommand
{
    CommandResult Execute(List<string> arguments, Register register);
}