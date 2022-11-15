namespace Bouvet.CashRegister.Core.Decorators;

public abstract class CommandExecutorDecorator : ICommandExecutor
{
    private readonly ICommandExecutor decorated;

    public CommandExecutorDecorator(ICommandExecutor decorated)
    {
        this.decorated = decorated;
    }
 
    public virtual CommandResult Execute(string? input)
    {
        return decorated.Execute(input);
    }
}
