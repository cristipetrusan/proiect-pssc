using OrderProcessing.Domain.CartModel;

namespace OrderProcessing.Domain
{
	public class CartTotalPrice
	{
		public decimal Price { get; set; }
		public Amount Amount { get; set; }
	}
}