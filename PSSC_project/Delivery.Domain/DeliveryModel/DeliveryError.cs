using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.DeliveryModel
{
	public class DeliveryError
	{
		public string ErrorMessage { get; private set; }

		internal DeliveryError(string errorMessage)
		{
			ErrorMessage = errorMessage;
		}
	}
}
