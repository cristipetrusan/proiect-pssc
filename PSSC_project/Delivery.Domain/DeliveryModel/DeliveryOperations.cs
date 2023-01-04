using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Delivery.Domain.DeliveryModel.Address;
using static Delivery.Domain.DeliveryModel.Delivery;
using System.Linq;
using LanguageExt;

namespace Delivery.Domain.DeliveryModel
{
	public static class DeliveryOperations 
	{
		public static Either<DeliveryError, Delivered> Deliver(
				Func<UnvalidatedAddress, Either<DeliveryError, ValidatedAddress>> checkAddress,
				Undelivered order)
		{
			var addressValidationResult = checkAddress(order.Address);
			DateTime date = DateTime.Now;

			return addressValidationResult.Match<Either<DeliveryError, Delivered>>(
				  validAddress => new Delivered(validAddress, date),
				  error => error
				);
		}
	}
}
