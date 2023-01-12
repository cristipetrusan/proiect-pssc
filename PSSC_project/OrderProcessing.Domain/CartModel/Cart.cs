using CSharp.Choices;
using OrderProcessing.Domain.CartModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessing.Domain
{

	[AsChoice]
	public static partial class Cart
	{
		public interface ICart { }

		public class UnvalidatedCart : ICart
		{
			public IReadOnlyCollection<CartModel.UnvalidatedCart> Items { get; private set; }

			internal UnvalidatedCart(IReadOnlyCollection<CartModel.UnvalidatedCart> items)
			{
				Items = items;
			}

		}

		public class InvalidatedCart : ICart
		{
			public IReadOnlyCollection<CartModel.UnvalidatedCart> Items { get; private set; }
			public string Reason { get; }

			internal InvalidatedCart(IReadOnlyCollection<CartModel.UnvalidatedCart> items, string reason)
			{
				Items = items;
				Reason = reason;
			}
		}

		public class ValidatedCart : ICart
		{
			public IReadOnlyCollection<CartModel.ValidatedCart> Items { get; private set; }

			internal ValidatedCart(IReadOnlyCollection<CartModel.ValidatedCart> items)
			{
				Items = items;
			}
		}

		public class PaidCart : ICart
		{
			public DateTime PaymentDate { get; private set; }
			public IReadOnlyCollection<CartModel.ValidatedCart> Items { get; private set; }
			public decimal Totalprice; 

			internal PaidCart(IReadOnlyCollection<CartModel.ValidatedCart> items, DateTime paymentDate, decimal totalprice)
			{
				Items = items;
				PaymentDate = paymentDate;
				Totalprice = totalprice;
			}
		}
	}

}


