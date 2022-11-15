namespace Bouvet.CashRegister.Core;

public interface ICommandExecutor
{
    CommandResult Execute(string? input);
}
