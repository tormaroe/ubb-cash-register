namespace Bouvet.CashRegister.Core;

public class Register
{
    private Catalogue catalogue = new();
    private ShoppingCart cart = new();

    public IReadOnlyShoppingCart ShoppingCart => cart;

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

    public CommandResult Execute(string? input)
    {
        var result = new CommandResult();
        try
        {
            var arguments = ParseInput(input);

            if (arguments.Count == 0)
            {
                return result;
            }

            switch (arguments[0])
            {
                case "cat":
                    {
                        var item = catalogue.Add(new CatalogueItem(
                            Name: arguments[1],
                            Price: decimal.Parse(arguments[2]),
                            Amount: int.Parse(arguments[3])
                        ));
                        Output($"{item.Name} added to catalogue");
                        break;
                    }

                case "list":
                    {
                        Output(string.Empty);
                        Output($" ------ PRODUCT CATALOGUE ----------------------------------------------------------------------");
                        foreach (var item in catalogue.Items)
                        {
                            Output($"  - {item.Name,-24} {FormatMoney(item.Price),10} {item.Amount,10}");
                        }

                        break;
                    }

                case "buy":
                    Buy(arguments.Skip(1).ToList());
                    break;
                case "pay":
                    {
                        var paid = decimal.Parse(arguments[1]);
                        Output($"  Amount paid: {FormatMoney(paid)}");

                        var rest = paid - cart.TotalPrice;

                        if (rest < 0m)
                        {
                            throw new ArgumentException("Not enough");
                        }
                        else
                        {
                            Output($"  Cash back: {FormatMoney(rest)}");
                        }

                        cart = new();
                        break;
                    }

                case "load":
                    {
                        var filename = arguments[1];
                        Output($"Reading {filename} ...");
                        var lines = File.ReadAllLines(filename);
                        Array.ForEach(lines, l => Execute(l));
                        break;
                    }

                case "save":
                    {
                        var filename = arguments[1];
                        List<string> instructions = new();
                        instructions.AddRange(catalogue.Items.Select(x => x.ToInstruction()));
                        File.WriteAllLines(filename, instructions);
                        Output($"State saved to {filename}");
                        break;
                    }

                case "menu":
                    PrintMenu();
                    break;
                case "quit":
                    result.IsQuit = true;
                    break;
            }

        }
        catch (Exception ex)
        {
            result.Error(ex);

            Output(" !!! " + result.ErrorMessage);
        }
        return result;
    }

    private void Buy(List<string> arguments)
    {
        var itemName = arguments[0];
        var amount = arguments.Count > 1 ? int.Parse(arguments[1]) : 1;

        var catalogueItem = catalogue.Consume(itemName, amount);
        cart.Add(catalogueItem);

        PrintCart();
    }

    private void PrintCart()
    {
        Output(string.Empty);

        foreach (var item in cart.Items)
        {
            Output($"  > {item.Name,25} {FormatMoney(item.Price),10} {(item.Amount > 1 ? "x" + item.Amount : ""),10}");
        }
        Output($"  = TOTAL                    {FormatMoney(cart.TotalPrice),10}");
    }

    private List<string> ParseInput(string? input)
    {
        return
            input?.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList()
            ?? new();
    }

    private string FormatMoney(decimal amount)
    {
        return amount.ToString("0.00");
    }
}
