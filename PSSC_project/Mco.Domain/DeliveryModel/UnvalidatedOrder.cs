namespace OrderProcessing.Domain
{
	public record UndeliveredOrder(string orderId, string status);
}