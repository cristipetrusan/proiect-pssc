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
			public IReadOnlyCollection<UnvalidatedItem> Items { get; private set; }

			internal UnvalidatedDelivery(IReadOnlyCollection<UnvalidatedItem> items)
			{
				Items = items;
			}

		}


		public class Delivered : IDelivery
		{
			public DateTime PaymentDate { get; private set; }
			public IReadOnlyCollection<ValidatedItem> Items { get; private set; }

			internal Delivered(IReadOnlyCollection<ValidatedItem> items, DateTime deliveryDate)
			{
				Items = items;
				PaymentDate = deliveryDate;
			}
		}
	}

}


