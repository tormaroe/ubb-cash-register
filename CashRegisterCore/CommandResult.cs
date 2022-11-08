namespace Bouvet.CashRegister.Core;

public record CommandResult
{
    private Exception? exception;

    public bool IsError => exception != null;
    public string? ErrorMessage => exception?.Message;
    public bool IsQuit { get; set; }

    internal void Error(Exception ex)
    {
        exception = ex;
    }
}