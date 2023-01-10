using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

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
    public void AddItem(int cartId, [FromBody]Item item)
    {
        int itemId = item.ItemId;

        var workflow = GetWorkflow(cartId);
        string connectionString = "server=localhost; database=pssc; user=root; password=root";
        MySqlConnection conn = new MySqlConnection(connectionString);
        conn.Open();
        string query = String.Format("INSERT INTO ITEMS_IN_CART VALUES ('{0}', '{1}')", cartId, itemId);
        Console.WriteLine(query);
        MySqlCommand cmd = new MySqlCommand(query, conn);
        MySqlDataReader reader = cmd.ExecuteReader();

        workflow.Cart.AddItem(item);
        // if (workflow.Cart.CurrentState == "Empty")
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

    [HttpGet]
    [Route("/carts")]
    public IEnumerable<ShoppingCart> GetCarts()
    {
        List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();

        string connectionString = "server=localhost; database=pssc; user=root; password=root";
        MySqlConnection conn = new MySqlConnection(connectionString);
        conn.Open();
        string query = "SELECT * FROM CART";
        MySqlCommand cmd = new MySqlCommand(query, conn);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            shoppingCartList.Add(new ShoppingCart{ 
                CartId = Convert.ToInt32(reader["cart_id"]), 
                Name = reader["owner"].ToString()}
                );
        }
        return shoppingCartList; 

    }

    [HttpGet]
    [Route("/items")]
    public IEnumerable<Item> GetItems()
    {
        List<Item> itemList = new List<Item>();

        string connectionString = "server=localhost; database=pssc; user=root; password=root";
        MySqlConnection conn = new MySqlConnection(connectionString);
        conn.Open();
        string query = "SELECT * FROM ITEM";
        MySqlCommand cmd = new MySqlCommand(query, conn);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            itemList.Add(new Item{ 
                ItemId = Convert.ToInt32(reader["item_id"]), 
                Name = reader["name"].ToString()}
                );
        }
        return itemList; 
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
