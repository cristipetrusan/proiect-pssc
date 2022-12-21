using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessing.Domain
{
	public class CartItem
	{
		public int ProductId { get; private set; }
		public int Quantity { get; private set; }

		public CartItem(int productId, int quantity)
		{
			ProductId = productId;
			Quantity = quantity;
		}
	}
}
