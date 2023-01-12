using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessing.Domain.CartModel
{
	public record UnvalidatedItem(string itemId, string amount);
}
