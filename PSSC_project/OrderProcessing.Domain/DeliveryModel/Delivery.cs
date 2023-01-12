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

		public class UnvalidatedDelivery : IDelivery
		{
			public IReadOnlyCollection<UnvalidatedCart> Items { get; private set; }

			internal UnvalidatedDelivery(IReadOnlyCollection<UnvalidatedCart> items)
			{
				Items = items;
			}

		}


		public class Delivered : IDelivery
		{
			public DateTime PaymentDate { get; private set; }
			public IReadOnlyCollection<ValidatedCart> Items { get; private set; }

			internal Delivered(IReadOnlyCollection<ValidatedCart> items, DateTime deliveryDate)
			{
				Items = items;
				PaymentDate = deliveryDate;
			}
		}
	}

}


