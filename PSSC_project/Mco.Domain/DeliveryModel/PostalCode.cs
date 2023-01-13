using LanguageExt;
using static LanguageExt.Prelude;
using OrderProcessing.Domain.CartModel;
using System.Text.RegularExpressions;

namespace OrderProcessing.Domain
{
	public class PostalCode
	{
		private static readonly Regex Pattern = new("^[0-9]{6}$");
		public string Value { get; }
		private PostalCode(string value)
		{
			if (IsValid(value))
			{
				Value = value;
			}
			else
			{
				throw new InvalidPostalCodeException("");
			}
		}

		private static bool IsValid(string value) => Pattern.IsMatch(value);

		public static Option<PostalCode> TryParse(string valueString)
		{
			if (IsValid(valueString))
			{
				return Some<PostalCode>(new(valueString));
			}
			return None;
		}
	}
}