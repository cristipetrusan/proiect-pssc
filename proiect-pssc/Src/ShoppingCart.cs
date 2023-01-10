public class ShoppingCart
{
    public List<Product> Items { get; set; } = new List<Product>();
    public ShoppingCartState? CurrentState { get; set; }

    public void AddItem(Product product)
    {
        Items.Add(product);
    }

    public void StartCheckout()
    {
        // Code to start the checkout process goes here
    }
}
