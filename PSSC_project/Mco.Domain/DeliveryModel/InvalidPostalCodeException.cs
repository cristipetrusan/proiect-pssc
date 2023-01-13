using System;
using System.Runtime.Serialization;

namespace OrderProcessing.Domain
{
	[Serializable]
	internal class InvalidPostalCodeException : Exception
	{
		public InvalidPostalCodeException()
		{
		}

		public InvalidPostalCodeException(string message) : base(message)
		{
		}

		public InvalidPostalCodeException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected InvalidPostalCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}