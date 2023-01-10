public class ShoppingCart
{
    public int CartId { get; set; }
    public string? Name { get; set; }
    public List<Item> Items { get; set; } = new List<Item>();
    public ShoppingCartState? CurrentState { get; set; }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public void StartCheckout()
    {
        // Code to start the checkout process goes here
    }
}
