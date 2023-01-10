public class EmptyState : ShoppingCartState
{
    public EmptyState()
    {
        Name = "Empty";
    }

    public override void Next(ShoppingCart cart)
    {
        cart.CurrentState = new ActiveState();
    }
}
