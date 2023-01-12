using LanguageExt;
using static LanguageExt.Prelude;
using System.Text.RegularExpressions;

namespace OrderProcessing.Domain.CartModel
{
	public record ItemId
	{
		private static readonly Regex Pattern = new("[0-9]{3}$");
		public string Value { get; }
		private ItemId(string value)
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

		public static Option<ItemId> TryParse(string valueString)
		{
			if (IsValid(valueString))
			{
				return Some<ItemId>(new(valueString));
			}
			return None; 
		}

	}
}