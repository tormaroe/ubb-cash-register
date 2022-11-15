namespace Bouvet.CashRegister.Core.Commands;

internal class SaveCommand : ICommand
{
    public string DescriptiveName => "Save state";
    public string CommandName => "save";
    public string UsageExample => "save <filename>";

    public CommandResult Execute(List<string> arguments, Register register)
    {
        var filename = arguments[0];
        List<string> instructions = new();
        instructions.AddRange(register.Catalogue.Items.Select(x => x.ToInstruction()));
        File.WriteAllLines(filename, instructions);
        register.Output($"State saved to {filename}");
        return new CommandResult();
    }
}
