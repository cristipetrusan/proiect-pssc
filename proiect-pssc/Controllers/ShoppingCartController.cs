using Microsoft.AspNetCore.Mvc;

namespace proiect_pssc.Controllers;

[ApiController]
[Route("[controller]")]
public class ShoppingCartController : ControllerBase
{
    private static Dictionary<int, ShoppingCartWorkflow> _workflows;

    public ShoppingCartController()
    {
        _workflows = new Dictionary<int, ShoppingCartWorkflow>();
    }

    [HttpPost]
    [Route("{cartId}/additem")]
    public void AddItem(int cartId, [FromBody]Product product)
    {
        var workflow = GetWorkflow(cartId);
        workflow.Cart.AddItem(product);
        workflow.MoveToNextState();
    }

    [HttpPost]
    [Route("{cartId}/processpayment")]
    public void ProcessPayment(int cartId)
    {
        var workflow = GetWorkflow(cartId);
        //Code to process payment
        workflow.MoveToNextState();
    }

    private ShoppingCartWorkflow GetWorkflow(int cartId)
    {
        if (!_workflows.ContainsKey(cartId))
        {
            var cart = new ShoppingCart { CurrentState = new EmptyState() };
            var workflow = new ShoppingCartWorkflow(cart);
            _workflows.Add(cartId, workflow);
        }

        return _workflows[cartId];
    }
}
