public class PaidState : ShoppingCartState
{
    public PaidState()
    {
        Name = "Active";
    }

    public override void Next(ShoppingCart cart)
    {
        cart.CurrentState = new PaidState();
    }
}
