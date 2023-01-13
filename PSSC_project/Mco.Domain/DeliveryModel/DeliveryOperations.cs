using OrderProcessing.Domain;
using OrderProcessing.Domain.CartModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LanguageExt.Prelude;
using static OrderProcessing.Domain.Delivery;

namespace Mco.Domain.DeliveryModel
{
	internal class DeliverryOperations
	{
		public static IDelivery ValidateOrderToDeliver(Undelivered unvalidatedorders)
		{
			List<ValidatedOrder> validatedItems = new List<ValidatedOrder>();

			foreach (UnvalidatedOrder item in unvalidatedorders.Orders)
			{
				if (ItemId.TryParse(item.postalCode) == None)
				{
					Console.WriteLine("PostalCode does not respect the format");
					return (IDelivery)new InvalidDelivery(unvalidatedorders.Orders, "PostalCode does not respect the format");
				}
				else
					validatedItems.Add(new ValidatedOrder(item.orderId, new PostalCode(Convert.ToString(item.postalCode))));

			}
			return (IDelivery)new Delivered(validatedItems, DateTime.Now);
		}

	}
}
