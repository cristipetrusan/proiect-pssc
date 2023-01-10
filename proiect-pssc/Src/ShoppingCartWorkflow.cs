public class ShoppingCartWorkflow
{
    public ShoppingCart Cart { get; set; }

    public ShoppingCartWorkflow(ShoppingCart cart)
    {
        Cart = cart;
    }

    public void MoveToNextState()
    {
        switch (Cart.CurrentState.Name)
        {
            case "Empty":
                Cart.CurrentState = new ActiveState();
                break;
            case "Active":
                Cart.CurrentState = new PaidState();
                break;
            case "Paid":
                break;
        }
    }
}
