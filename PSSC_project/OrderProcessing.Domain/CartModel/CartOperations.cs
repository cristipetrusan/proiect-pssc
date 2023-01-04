using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderProcessing.Domain.Cart;


namespace OrderProcessing.Domain
{
	public static class CartOperations
	{

		public static bool ValidateCartItem(CartItem item)
		{
			if (item.ItemId > 0) return true;
			return false;
		}


		public static ICart AddItem(ICart cart, CartItem itemToAdd)
		{
			ICart newCart = cart.Match(
					emptyCart =>
					{
						if (ValidateCartItem(itemToAdd)) //Aici facem validarea produsului
						{
							var items = new List<CartItem>() { itemToAdd };
							var activeCart = new ActiveCart(items);
							return activeCart;
						}
						return emptyCart;
					},
					activeCart =>
					{	
						var items = new List<CartItem>();
						items.AddRange(activeCart.Items);
						if(ValidateCartItem(itemToAdd)) //Aici facem validarea produsului
							items.Add(itemToAdd); 
						var newActiveCart = new ActiveCart(items);
						return newActiveCart;
					},
					paidCart =>
					{
						return paidCart;
					}
				);

			return newCart;
		}

		public static ICart RemoveItem(ICart cart, CartItem itemToRemove)
		{
			ICart newCart = cart.Match(
					emptyCart =>
					{
						return emptyCart;
					},
					activeCart =>
					{
						var items = new List<CartItem>();
						items.AddRange(activeCart.Items);
						items.Remove(itemToRemove);
						if(items.Count==0)
						{
							var newEmptyCart = new EmptyCart();
							return newEmptyCart;
						}
						var newActiveCart = new ActiveCart(items);
						return newActiveCart;
					},
					paidCart =>
					{
						return paidCart;
					}
				);
			return newCart;
		}

		public static ICart PayItems(ICart cart)
		{
			ICart newCart = cart.Match(
					emptyCart =>
					{
						return emptyCart;
					},
					activeCart =>
					{
						var items = new List<CartItem>();
						items.AddRange(activeCart.Items);
						decimal orderPrice = 0;
						foreach (var item in items)
						{
							orderPrice = orderPrice + item.Total;
						}
						DateTime PaymentDate = DateTime.Now;
						var newPaidCart = new PaidCart(items, PaymentDate, orderPrice);
						return newPaidCart;
					},
					paidCart =>
					{
						return paidCart;
					}
				);
			return newCart;
		}

	}
}
