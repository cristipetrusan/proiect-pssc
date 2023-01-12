using LanguageExt;
using static LanguageExt.Prelude;
using OrderProcessing.Domain.CartModel;
using System.Text.RegularExpressions;

namespace OrderProcessing.Domain
{
	public class OrderId
	{
		private static readonly Regex Pattern = new("^OR[0-9]{2}$");
		public string Value { get; }
		private OrderId(string value)
		{
			if (IsValid(value))
			{
				Value = value;
			}
			else
			{
				throw new InvalidOrderIdException("");
			}
		}

		private static bool IsValid(string value) => Pattern.IsMatch(value);

		public static Option<OrderId> TryParse(string valueString)
		{
			if (IsValid(valueString))
			{
				return Some<OrderId>(new(valueString));
			}
			return None;
		}
	}
}