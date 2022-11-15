using Bouvet.CashRegister.Core.Decorators;

namespace Bouvet.CashRegister.Core;

public class CommandExecutorBuilder
{
    private ICommandExecutor executor;
    private readonly Register register;

    public CommandExecutorBuilder()
    {
        register = new Register(new List<string>{});
        executor = register;
    }

    public CommandExecutorBuilder WithCommands(List<string> commands)
    {
        commands.ForEach(register.AddCommand);
        return this;
    }

    public CommandExecutorBuilder WithLogging(string filepath)
    {
        executor = new LoggingCommandExecutor(executor, filepath);
        return this;
    }

    public CommandExecutorBuilder WithAuthorization(string secretPassword)
    {
        executor = new AuthorizingCommandExecutor(executor, secretPassword);
        return this;
    }

    public CommandExecutorBuilder WithSupervisorNotification(string supervisor)
    {
        executor = new SupervisorNotifyingCommandExecutor(executor, supervisor, register.Output);
        return this;
    }

    public ICommandExecutor Build()
    {
        // Finally add exception handling
        executor = new CommandExceptionCatcher(executor, register.Output);
        return executor;
    }
}