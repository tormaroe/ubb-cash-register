namespace Bouvet.CashRegister.Core;

internal class ShoppingCart : IReadOnlyShoppingCart
{
    private readonly List<CatalogueItem> items = new();

    public decimal TotalPrice => items.Sum(x => x.Amount * x.Price);
    public int TotalAmount => items.Sum(x => x.Amount);
    public IEnumerable<CatalogueItem> Items => items;

    internal void Add(CatalogueItem catalogueItem)
    {
        items.Add(catalogueItem);
    }
}