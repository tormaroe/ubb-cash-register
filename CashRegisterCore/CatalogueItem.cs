namespace Bouvet.CashRegister.Core;

internal readonly record struct CatalogueItem(string Name, decimal Price, int Amount)
{
    internal string ToInstruction()
    {
        return $"cat {Name} {Price} {Amount}";
    }
}
