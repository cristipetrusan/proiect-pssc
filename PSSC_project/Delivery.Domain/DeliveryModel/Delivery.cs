using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Delivery.Domain.DeliveryModel.Address;

namespace Delivery.Domain.DeliveryModel
{

	[AsChoice]
	public static partial class Delivery
	{
		public interface IDelivery { }

		public class Undelivered : IDelivery
		{
			public UnvalidatedAddress Address { get; private set; }

			public Undelivered(UnvalidatedAddress address)
			{
				Address = address;
			}
		}

		public class Delivered : IDelivery
		{
			public DateTime PaymentDate { get; private set; }

			public ValidatedAddress Address { get; private set; }

			internal Delivered(ValidatedAddress address, DateTime paymentDate)
			{
				Address = address;
				PaymentDate = paymentDate;
			}

		}
	}
}
