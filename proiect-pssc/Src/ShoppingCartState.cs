public abstract class ShoppingCartState
{
    public string? Name { get; set; }
    public abstract void Next(ShoppingCart cart);
}
