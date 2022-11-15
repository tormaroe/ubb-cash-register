namespace Bouvet.CashRegister.Core.Commands;

internal class PayCommand : ICommand
{
    public string DescriptiveName => "Checkout";
    public string CommandName => "pay";
    public string UsageExample => "pay <amount>";

    public CommandResult Execute(List<string> arguments, Register register)
    {
        var paid = decimal.Parse(arguments[0]);
        register.Output($"  Amount paid: {register.FormatMoney(paid)}");

        var rest = paid - register.Cart.TotalPrice;

        if (rest < 0m)
        {
            throw new ArgumentException("Not enough");
        }
        else
        {
            register.Output($"  Cash back: {register.FormatMoney(rest)}");
        }

        register.Cart = new();
        return new CommandResult();
    }
}
