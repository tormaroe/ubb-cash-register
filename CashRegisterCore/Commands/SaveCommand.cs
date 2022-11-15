namespace Bouvet.CashRegister.Core.Commands;

public class SaveCommand : ICommand
{
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
