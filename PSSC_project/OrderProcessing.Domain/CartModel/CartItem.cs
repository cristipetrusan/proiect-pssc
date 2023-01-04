using LanguageExt.ClassInstances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessing.Domain
{
	public class CartItem
	{
		public int ItemId { get; private set; }
		public int Quantity { get; private set; }
		public decimal Price { get; set; }
		public decimal Total
		{
			get { return Quantity * Price; }
		}

		public CartItem(int itemId, decimal price, int quantity)
		{
			ItemId = itemId;
			Price = price;
			Quantity = quantity;
		}

	}
}
