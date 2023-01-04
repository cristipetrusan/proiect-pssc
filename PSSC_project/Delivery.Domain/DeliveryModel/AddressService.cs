using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Delivery.Domain.DeliveryModel.Address;

namespace Delivery.Domain.DeliveryModel
{
	public static class AddressService
	{
		public static Either<DeliveryError, ValidatedAddress> CheckAddress(UnvalidatedAddress address)
		{
			if (address.City == "Timisoara")
			{
				return new ValidatedAddress(address.PostalCode, address.City, address.Street);
			}
			else
			{
				return new DeliveryError("Invalid city");
			}
		}
	}
}
