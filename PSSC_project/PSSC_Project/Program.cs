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
			ICart cart = new EmptyCart();
			ShowCart(cart);
			var item = new CartItem(1, 1);
			cart = AddItem(cart, item);
			ShowCart(cart);

			Console.ReadLine();
		}

		private static void ShowCart(ICart cart)
		{
			cart.Match(
				empty =>
				{
					Console.WriteLine("Empty cart");
					return Unit.Default;
				},
				active =>
				{
					Console.WriteLine("Active cart");
					return Unit.Default;
				},
				paid =>
				{
					Console.WriteLine("Paid cart");
					return Unit.Default;
				});
		}
	}
}