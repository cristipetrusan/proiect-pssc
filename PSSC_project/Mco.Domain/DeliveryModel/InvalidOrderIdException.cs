using System;
using System.Runtime.Serialization;

namespace OrderProcessing.Domain
{
	[Serializable]
	internal class InvaldOrderIdException : Exception
	{
		public InvaldOrderIdException()
		{
		}

		public InvaldOrderIdException(string message) : base(message)
		{
		}

		public InvaldOrderIdException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected InvaldOrderIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}