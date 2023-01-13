using LanguageExt;
using static LanguageExt.Prelude;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace OrderProcessing.Domain.CartModel
{
	public class Price
	{
		public double Value { get; }
		public Price(double value)
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

		public override string ToString()
		{
			return $"{Value:0.##}";
		}

		public static Option<Price> TryParseAmount(string gradeString)
		{
			if (double.TryParse(gradeString, out double numericAmount) && IsValid(numericAmount))
			{
				return Some<Price>(new(numericAmount));
			}
			else
			{
				return None;
			}
		}

		private static bool IsValid(double price) => price > 0 ;
	}
}