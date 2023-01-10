public class ActiveState : ShoppingCartState
{
    public ActiveState()
    {
        Name = "Active";
    }

    public override void Next(ShoppingCart cart)
    {
        cart.CurrentState = new PaidState();
    }
}
