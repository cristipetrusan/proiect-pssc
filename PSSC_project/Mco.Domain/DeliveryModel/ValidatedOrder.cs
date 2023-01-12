namespace OrderProcessing.Domain
{
	public record ValidatedOrder(string orderId, string status);
}