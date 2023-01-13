using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mco.Domain.DeliveryModel
{
	public static partial class DeliveredOrderEvent
	{
		public interface IDeliveredOrderEvent { }

		public record DeliverSucceededEvent : IDeliveredOrderEvent
		{
			public string Csv { get; }
			public DateTime PublishedDate { get; }

			internal DeliverSucceededEvent(string csv, DateTime publishedDate)
			{
				Csv = csv;
				PublishedDate = publishedDate;
			}
		}

		public record DeliverFailedEvent : IDeliveredOrderEvent
		{
			public string Reason { get; }

			internal DeliverFailedEvent(string reason)
			{
				Reason = reason;
			}
		}
	}
}
