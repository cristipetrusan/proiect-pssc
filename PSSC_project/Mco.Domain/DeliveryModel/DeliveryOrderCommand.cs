using OrderProcessing.Domain;
using OrderProcessing.Domain.CartModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mco.Domain.DeliveryModel
{
	public record DeliveryOrderCommand
	{
		public DeliveryOrderCommand(IReadOnlyCollection<UnvalidatedOrder> inputOrderToDeliver)
		{
			InputOrderToDeliver = inputOrderToDeliver;
		}

		public IReadOnlyCollection<UnvalidatedOrder> InputOrderToDeliver { get; }
	}
}
