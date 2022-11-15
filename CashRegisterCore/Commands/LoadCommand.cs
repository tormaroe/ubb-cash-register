namespace Bouvet.CashRegister.Core.Commands;

internal class LoadCommand : ICommand
{
    public string DescriptiveName => "Load file";
    public string CommandName => "load";
    public string UsageExample => "load <filename>";

    public CommandResult Execute(List<string> arguments, Register register)
    {
        var filename = arguments[0];
        register.Output($"Reading {filename} ...");
        var lines = File.ReadAllLines(filename);
        Array.ForEach(lines, l => register.Execute(l));
        return new CommandResult();
    }
}
