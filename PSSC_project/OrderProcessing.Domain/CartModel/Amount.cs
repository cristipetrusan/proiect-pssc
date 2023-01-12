using LanguageExt;
using static LanguageExt.Prelude;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace OrderProcessing.Domain.CartModel
{
	public class Amount
	{
		public int Value { get; }
		private Amount(int value)
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

		public static Option<Amount> TryParseAmount(string gradeString)
		{
			if (int.TryParse(gradeString, out int numericAmount) && IsValid(numericAmount))
			{
				return Some<Amount>(new(numericAmount));
			}
			else
			{
				return None;
			}
		}

		private static bool IsValid(int amount) => amount > 0 && amount <= 10;
	}
}