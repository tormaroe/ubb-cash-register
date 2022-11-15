namespace Bouvet.CashRegister.Core.Commands;

public class PayCommand : ICommand
{
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
