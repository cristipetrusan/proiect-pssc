using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessing.Domain.CartModel
{
    [AsChoice]
    public static partial class PaidItemsEvent
    {
        public interface IPaidItemsEvent { }

        public record PaidItemsSucceededEvent : IPaidItemsEvent 
        {
            public string Csv{ get;}
            public DateTime PublishedDate { get; }

            internal PaidItemsSucceededEvent(string csv, DateTime publishedDate)
            {
                Csv = csv;
                PublishedDate = publishedDate;
            }
        }

        public record PaidItemsFailedEvent : IPaidItemsEvent 
        {
            public string Reason { get; }

            internal PaidItemsFailedEvent(string reason)
            {
                Reason = reason;
            }
        }
    }
}
