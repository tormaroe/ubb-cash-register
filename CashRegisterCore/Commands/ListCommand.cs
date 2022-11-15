namespace Bouvet.CashRegister.Core.Commands;

public class ListCommand : ICommand
{
    public CommandResult Execute(List<string> arguments, Register register)
    {
        register.Output(string.Empty);
        register.Output($" ------ PRODUCT CATALOGUE ----------------------------------------------------------------------");
        foreach (var item in register.Catalogue.Items)
        {
            register.Output($"  - {item.Name,-24} {register.FormatMoney(item.Price),10} {item.Amount,10}");
        }
        return new CommandResult();
    }
}
