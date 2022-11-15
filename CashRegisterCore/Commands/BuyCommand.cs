namespace Bouvet.CashRegister.Core.Commands;

public class BuyCommand : ICommand
{
    public CommandResult Execute(List<string> arguments, Register register)
    {
        var itemName = arguments[0];
        var amount = arguments.Count > 1 ? int.Parse(arguments[1]) : 1;
        var catalogueItem = register.Catalogue.Consume(itemName, amount);
        register.Cart.Add(catalogueItem);
        PrintCart(register);
        return new CommandResult();
    }
    
    private void PrintCart(Register register)
    {
        register.Output(string.Empty);

        foreach (var item in register.Cart.Items)
        {
            register.Output($"  > {item.Name,25} {register.FormatMoney(item.Price),10} {(item.Amount > 1 ? "x" + item.Amount : ""),10}");
        }
        register.Output($"  = TOTAL                    {register.FormatMoney(register.Cart.TotalPrice),10}");
    }

}
