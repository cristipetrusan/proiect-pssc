// using CSharp.Choices;
// using OrderProcessing.Domain.CartModel;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace OrderProcessing.Domain
// {

// 	[AsChoice]
// 	public static partial class Cart
// 	{
// 		public interface ICart { }

// 		public class UnvalidatedCart : ICart
// 		{
// 			public IReadOnlyCollection<UnvalidatedItem> Items { get; private set; }

// 			internal UnvalidatedCart(IReadOnlyCollection<CartModel.UnvalidatedItem> items)
// 			{
// 				Items = items;
// 			}

// 		}

// 		public class InvalidatedCart : ICart
// 		{
// 			public IReadOnlyCollection<UnvalidatedItem> Items { get; private set; }
// 			public string Reason { get; }

// 			internal InvalidatedCart(IReadOnlyCollection<UnvalidatedItem> items, string reason)
// 			{
// 				Items = items;
// 				Reason = reason;
// 			}
// 		}

// 		public class ValidatedCart : ICart
// 		{
// 			public IReadOnlyCollection<ValidatedCart> Items { get; private set; }

// 			internal ValidatedCart(IReadOnlyCollection<ValidatedItem> items)
// 			{
// 				Items = (IReadOnlyCollection<ValidatedCart>)items;
// 			}
// 		}

// 		public class PaidCart : ICart
// 		{
// 			public DateTime PaymentDate { get; private set; }
// 			public IReadOnlyCollection<ValidatedItem> Items { get; private set; }
// 			public decimal Totalprice; 

// 			internal PaidCart(IReadOnlyCollection<ValidatedItem> items, DateTime paymentDate, decimal totalprice)
// 			{
// 				Items = items;
// 				PaymentDate = paymentDate;
// 				Totalprice = totalprice;
// 			}
// 		}
// 	}

// }


