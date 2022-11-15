using Bouvet.CashRegister.Core.Commands;

namespace Bouvet.CashRegister.Core;

public class Register
{
    internal Catalogue Catalogue { get; } = new();
    internal ShoppingCart Cart { get; set; } = new();

    public IReadOnlyShoppingCart ShoppingCart => Cart;

    public Action<string> Output { get; init; } = Console.WriteLine;

    public Register()
    {
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("EN-US");
    }

    public void PrintMenu()
    {
        Output("----------------------------------------------------------------------------------------------------");
        Output("  Catalogue product:  cat <name> <price> <stock amount>");
        Output("  List products:      list");
        Output("  Buy product:        buy <name> {optional amount}");
        Output("  Checkout:           pay <amount>");
        Output("  Load file:          load <filename>");
        Output("  Save state          save <filename>");
        Output("  Print menu:         menu");
        Output("  Quit:               quit");
        Output(string.Empty);
    }

    private ICommand GetCommand(List<string>? input)
    {
        /* Fancy new switch syntax.. */
        return input?.First() switch
        {
            "cat" => new CatalogueCommand(),
            "list" => new ListCommand(),
            "buy" => new BuyCommand(),
            "pay" => new PayCommand(),
            "load" => new LoadCommand(),
            "save" => new SaveCommand(),
            "menu" => new PrintMenuCommand(),
            "quit" => new QuitCommand(),
            _ => new NoOpCommand()
        };
    }

    public CommandResult Execute(string? input)
    {
        try
        {
            var arguments = ParseInput(input);
            var command = GetCommand(arguments);

            return command.Execute(
                arguments: arguments.Skip(1).ToList(), 
                register: this);

        }
        catch (Exception ex)
        {
            var result = new CommandResult();
            result.Error(ex);
            Output(" !!! " + result.ErrorMessage);
            return result;
        }
    }

    private List<string> ParseInput(string? input)
    {
        return
            input?.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList()
            ?? new();
    }

    internal string FormatMoney(decimal amount)
    {
        return amount.ToString("0.00");
    }
}
