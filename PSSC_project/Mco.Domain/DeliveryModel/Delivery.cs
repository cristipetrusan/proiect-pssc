using CSharp.Choices;
using OrderProcessing.Domain.CartModel;
using System;
using System.Collections.Generic;

namespace OrderProcessing.Domain
{

	[AsChoice]
	public static partial class Delivery
	{
		public interface IDelivery { }

		public class Undelivered : IDelivery
		{
			public IReadOnlyCollection<UnvalidatedOrder> Orders { get; private set; }

			internal Undelivered(IReadOnlyCollection<UnvalidatedOrder> oredrs)
			{
				Orders = oredrs;
			}

		}

		public class InvalidDelivery : IDelivery
		{
			public IReadOnlyCollection<UnvalidatedOrder> Orders { get; private set; }
			public string Message { get; set; }

			internal InvalidDelivery(IReadOnlyCollection<UnvalidatedOrder> oredrs, string message)
			{
				Orders = oredrs;
				Message = message;
			}

		}

		public class Delivered : IDelivery
		{
			public DateTime PaymentDate { get; private set; }
			public IReadOnlyCollection<ValidatedOrder> Orders { get; private set; }

			internal Delivered(IReadOnlyCollection<ValidatedOrder> oredrs, DateTime deliveryDate)
			{
				Orders = oredrs;
				PaymentDate = deliveryDate;
			}
		}
	}

}


