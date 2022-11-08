namespace Bouvet.CashRegister.Core;

public interface IReadOnlyShoppingCart
{
    decimal TotalPrice { get; }
    int TotalAmount { get; }
}
