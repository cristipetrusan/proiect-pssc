using Mco.Domain;
using Mco.Domain.Dbo;
using Microsoft.AspNetCore.Mvc;
using OrderProcessing.Domain;
using OrderProcessing.Domain.CartModel;
using static OrderProcessing.Domain.CartModel.PaidItemsEvent;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Mco.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private static double totalPrice;
    private readonly ILogger<ItemsController> _logger;
    private static OrdersContext _context;
    public ItemsController(ILogger<ItemsController> logger, OrdersContext ordersContext)
    {
        _logger = logger;
        _context = ordersContext;
    }

    [HttpGet("items")]
    public List<ItemDbo> GetItems()
    {
        return _context.Items.ToList();
    }

    [HttpPost]
    public async Task<IActionResult> TotalPrice([FromServices] PlaceOrderWorkflow placeOrderWorkflow, 
        [FromBody]InputItem[] items)
    {
        var unvalidatedItems = items.Select(MapInputItemToUnvalidatedItem).ToList().AsReadOnly();
        PayCartCommand command = new(unvalidatedItems);


        var result = await placeOrderWorkflow.ExecuteAsync(command, _context);

        return result.Match<IActionResult>(
            whenPaidItemsFailedEvent: failedEvent => StatusCode(StatusCodes.Status500InternalServerError, failedEvent.Reason),
            whenPaidItemsSucceededEvent: succeededEvent => StatusCode(StatusCodes.Status200OK, "Workflow succesful"));
    }

    private static UnvalidatedItem MapInputItemToUnvalidatedItem(InputItem item) => new UnvalidatedItem(
            itemId: item.ItemId,
            amount: item.Amount.ToString(),
            price: item.Price.ToString());
}
