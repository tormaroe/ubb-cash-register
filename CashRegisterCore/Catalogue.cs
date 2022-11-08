namespace Bouvet.CashRegister.Core;

internal class Catalogue
{
    private Dictionary<string, CatalogueItem> items = new();

    public IEnumerable<CatalogueItem> Items => items.Values;

    public CatalogueItem Add(CatalogueItem item)
    {
        items[item.Name] = item;
        return item;
    }

    internal CatalogueItem Consume(string itemName, int amount)
    {
        if (items.TryGetValue(itemName, out CatalogueItem item))
        {
            if (amount > item.Amount)
            {
                throw new ArgumentException($"Can't consume {amount} of '{itemName}', only {item.Amount} in stock");
            }

            items[item.Name] = item with { Amount = item.Amount - amount };
            return item with { Amount = amount };
        }
        else
        {
            throw new ArgumentException($"Unknown product '{itemName}'");
        }
    }
}
