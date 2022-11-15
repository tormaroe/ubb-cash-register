namespace Bouvet.CashRegister.Core.Decorators;

public class SupervisorNotifyingCommandExecutor : CommandExecutorDecorator
{
    private readonly string supervisor;
    private readonly Action<string> output;
    private List<(DateTime, Exception)> exceptions = new();

    public SupervisorNotifyingCommandExecutor(
        ICommandExecutor decorated, 
        string supervisor, 
        Action<string> output) : base(decorated)
    {
        this.supervisor = supervisor;
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
            exceptions.Add((DateTime.Now, ex));

            // Trim old exceptions
            var tenMinutesAgo = DateTime.Now.AddMinutes(-10);
            exceptions = exceptions.Where(x => x.Item1 > tenMinutesAgo).ToList();

            if (exceptions.Count >= 3)
            {
                Notify();
            }

            throw;
        }
    }

    private void Notify()
    {
        output($"WARNING: Operator has triggered {exceptions.Count} errors during the last 10 minutes");
        output($"Supervisor {supervisor} is being notified");
    }
}