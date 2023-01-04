using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Delivery.Domain.DeliveryModel.Address;
using static Delivery.Domain.DeliveryModel.Delivery;
using System.Linq;


namespace Delivery.Domain.DeliveryModel
{
	public static class OrderOperations //error?
	{
		public static Either<DeliveryError, Delivered> Deliver(
				Func<UnvalidatedAddress, Either<DeliveryError, ValidatedAddress>> checkAddress,
				Undelivered order)
		{
			var addressValidationResult = checkAddress(order.Address);
			DateTime date = DateTime.Now;

			return addressValidationResult.Match(
				  validAddress => new Delivered(validAddress, date),
				  error => error
				);
		}
	}
}
