using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderProcessing.Domain.Cart;


namespace OrderProcessing.Domain
{
	public static class CartOperations
	{
		public static ICart AddItem(ICart cart, CartItem itemToAdd)
		{
			ICart newCart = cart.Match(
					emptyCart =>
					{
						var items = new List<CartItem>() { itemToAdd };
						var activeCart = new ActiveCart(items);
						return activeCart;
					},
					activeCart =>
					{	
						var items = new List<CartItem>();
						items.AddRange(activeCart.Items);
						items.Add(itemToAdd); //Aici facem si validarea produsului
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
	}
}
