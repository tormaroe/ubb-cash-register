using System.Globalization;

namespace Bouvet.CashRegister.Core.Decorators;

public class LoggingCommandExecutor : CommandExecutorDecorator
{
    private readonly string filepath;

    public LoggingCommandExecutor(ICommandExecutor decorated, string filepath) : base(decorated)
    {
        this.filepath = filepath;
    }

    public override CommandResult Execute(string? input)
    {
        using(var writer = File.AppendText(filepath))
        {
            writer.Write(DateTime.Now.ToString("s", CultureInfo.InvariantCulture));
            writer.Write(" ");
            writer.WriteLine(input);
        }
        
        return base.Execute(input);
    }
}
