using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.DeliveryModel
{
	[AsChoice]
	public static partial class Address
	{
		public interface IAddress { }

		public class UnvalidatedAddress : IAddress
		{
			public string Street { get; private set; }
			public string PostalCode { get; private set; }
			public string City { get; private set; }

			public UnvalidatedAddress(string postalCode, string city, string street)
			{
				Street = street;
				City = city;
				PostalCode = postalCode;
			}
		}

		public class ValidatedAddress : IAddress
		{

			public string Street { get; private set; }
			public string PostalCode { get; private set; }
			public string City { get; private set; }

			internal ValidatedAddress(string postalCode, string city, string street)
			{
				Street = street;
				City = city;
				PostalCode = postalCode;
			}
		}
	}
}
