using System;
using System.Collections.Generic;
using LanguageExt;
using OrderProcessing.Domain;
using static OrderProcessing.Domain.Cart;
using static OrderProcessing.Domain.CartItem;
using static OrderProcessing.Domain.CartOperations;


namespace PSSC_Project
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("No items: ");
			ICart cart = new EmptyCart();
			ShowCart(cart);

			Console.WriteLine("Add an item: ");
			var item = new CartItem(1, 15, 1);
			cart = AddItem(cart, item);
			ShowCart(cart);

			Console.WriteLine("Remove an item: ");
			cart = RemoveItem(cart, item);
			ShowCart(cart);

			Console.WriteLine("Add 2 more items: ");
			item = new CartItem(2, 5, 1);
			cart = AddItem(cart, item);
			item = new CartItem(3, 10, 2);
			cart = AddItem(cart, item);
			ShowCart(cart);


			Console.WriteLine("Remove an item: ");
			cart = RemoveItem(cart, item);
			ShowCart(cart);

			Console.WriteLine("Pay items: ");
			cart = PayItems(cart);
			ShowCart(cart);
	

			Console.ReadLine();
		}

		private static void ShowCart(ICart cart)
		{
			cart.Match(
				empty =>
				{
					Console.WriteLine("Empty cart\n");
					return Unit.Default;
				},
				active =>
				{
					Console.WriteLine("Active cart\n");
					return Unit.Default;
				},
				paid =>
				{
					Console.WriteLine("Paid cart\n");
					return Unit.Default;
				});
		}
	}
}