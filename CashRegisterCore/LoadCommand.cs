namespace Bouvet.CashRegister.Core;

public class LoadCommand : ICommand
{
    public CommandResult Execute(List<string> arguments, Register register)
    {
        var filename = arguments[0];
        register.Output($"Reading {filename} ...");
        var lines = File.ReadAllLines(filename);
        Array.ForEach(lines, l => register.Execute(l));
        return new CommandResult();
    }
}
