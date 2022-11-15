namespace Bouvet.CashRegister.Core.Commands;

internal class CatalogueCommand : ICommand
{
    public string DescriptiveName => "Catalogue product";
    public string CommandName => "cat";
    public string UsageExample => "cat <name> <price> <stock amount>";

    public CommandResult Execute(List<string> arguments, Register register)
    {
        var item = register.Catalogue.Add(new CatalogueItem(
            Name: arguments[0],
            Price: decimal.Parse(arguments[1]),
            Amount: int.Parse(arguments[2])
        ));
        register.Output($"{item.Name} added to catalogue");
        return new CommandResult();
    }
}
