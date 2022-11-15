namespace Bouvet.CashRegister.Core.Decorators;

public class AuthorizingCommandExecutor : CommandExecutorDecorator
{
    private static List<string> elevatedCommands = new List<string>
    {
        "cat",
        "load"
    };

    private readonly string secretPassword;

    public AuthorizingCommandExecutor(ICommandExecutor decorated, string secretPassword) : base(decorated)
    {
        this.secretPassword = secretPassword;
    }

    public override CommandResult Execute(string? input)
    {
        if(RequiresAuthorization(input))
        {
            Challange();
        }

        return base.Execute(input);
    }

    private void Challange()
    {
        Console.WriteLine("*** AUTHORIZATION REQUIRED ***");
        Console.Write("Password? ");
        var userPassword = Console.ReadLine();

        if (userPassword != secretPassword)
        {
            throw new Exception("Wrong passord!");
        }

        Console.WriteLine("Authorization granted");
    }

    private bool RequiresAuthorization(string? input)
    {
        if (input == null)
            return false;

        return elevatedCommands.Any(x => input.Trim().StartsWith(x));
    }
}