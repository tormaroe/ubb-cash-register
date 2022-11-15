namespace Bouvet.CashRegister.Core.Decorators;

public class CommandExceptionCatcher : CommandExecutorDecorator
{
    private readonly Action<string> output;

    public CommandExceptionCatcher(ICommandExecutor decorated, Action<string> output) : base(decorated)
    {
        this.output = output;
    }

    public override CommandResult Execute(string? input)
    {
        try
        {
            return base.Execute(input);
        }
        catch (Exception ex)
        {
            var result = new CommandResult();
            result.Error(ex);
            output(" !!! " + result.ErrorMessage);
            return result;
        }
    }
}
