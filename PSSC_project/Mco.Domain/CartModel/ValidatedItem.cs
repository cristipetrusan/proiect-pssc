using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessing.Domain.CartModel
{
	public record ValidatedItem(ItemId itemId, Amount amount, Price price);
}
