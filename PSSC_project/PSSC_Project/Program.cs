using System;
using System.Collections.Generic;
using Delivery.Domain.DeliveryModel;
using LanguageExt;
using OrderProcessing.Domain;
using static Delivery.Domain.DeliveryModel.Address;
using static Delivery.Domain.DeliveryModel.Delivery;
using static Delivery.Domain.DeliveryModel.DeliveryOperations;
using static OrderProcessing.Domain.Order;
using static OrderProcessing.Domain.CartItem;
using static OrderProcessing.Domain.CartOperations;


namespace PSSC_Project
{
	class Program
	{
		static void Main(string[] args)
		{
			//Cart workflow
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

			//Delivery Workflow
			var order = new Undelivered(new UnvalidatedAddress("10", "Lugoj", "Bd. Parvan"));

			var result = DeliveryOperations.Deliver(AddressService.CheckAddress, order);

			result.Match(
					validDelivery =>
					{
						Console.WriteLine("The order was delivered");
					},
					error =>
					{
						Console.WriteLine($"The order can not be delivered. Reason: {error.ErrorMessage}");
					}
				);

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