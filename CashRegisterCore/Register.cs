using Bouvet.CashRegister.Core.Commands;

namespace Bouvet.CashRegister.Core;

public class Register : ICommandExecutor
{
    private static List<ICommand> availableCommands = CommandDiscovery.FindAllCommands();

    private SortedSet<ICommand> commands = new(
        Comparer<ICommand>.Create((a, b) => a.DescriptiveName.CompareTo(b.DescriptiveName))
    );

    public void AddCommand(string name) =>
        commands.Add(availableCommands.First(c => c.CommandName == name));

    internal Catalogue Catalogue { get; } = new();
    internal ShoppingCart Cart { get; set; } = new();

    public IReadOnlyShoppingCart ShoppingCart => Cart;

    public Action<string> Output { get; init; } = Console.WriteLine;

    public Register(List<string> commandsToLoad)
    {
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("EN-US");

        /*
            Core commands are always added:
        */
        AddCommand("menu");
        AddCommand("quit");

        commandsToLoad.ForEach(c => AddCommand(c));
    }

    public void PrintMenu()
    {
        Output("----------------------------------------------------------------------------------------------------");
        foreach (var c in commands)
        {
            Output($"  {c.DescriptiveName + ":",-19} {c.UsageExample}");
        }
        Output(string.Empty);
    }

    private ICommand GetCommand(List<string>? input)
    {
        return commands.SingleOrDefault(
            c => c.CommandName == input?.First(),
            defaultValue: new NoOpCommand());
    }

    public CommandResult Execute(string? input)
    {
        var arguments = ParseInput(input);
        var command = GetCommand(arguments);

        return command.Execute(
            arguments: arguments.Skip(1).ToList(),
            register: this);
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
