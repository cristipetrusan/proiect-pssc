using LanguageExt;
using OrderProcessing.Domain.CartModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderProcessing.Domain.Cart;


namespace OrderProcessing.Domain
{
	public static class CartOperationse
	{
		public static Task<ICart> ValidateCartItems(Func<ItemId, Option<ItemId>> checkItemExists, Cart.UnvalidatedCart examGrades) =>
			examGrades.Items
					  .Select(ValidateCart(checkItemExists))
					  .Aggregate(CreateEmptyValatedGradesList().ToAsync(), ReduceValidGrades)
					  .MatchAsync(
							Right: validatedGrades => new Cart.ValidatedCart(validatedGrades),
							LeftAsync: errorMessage => Task.FromResult((ICart)new InvalidatedCart(examGrades.Items, errorMessage))
					  );

		private static Func<CartModel.UnvalidatedCart, EitherAsync<string, CartModel.ValidatedCart>> ValidateCart(Func<ItemId, Option<ItemId>> checkStudentExists) =>
			unvalidatedStudentGrade => ValidateItems(checkStudentExists, unvalidatedStudentGrade);

		private static EitherAsync<string, CartModel.ValidatedCart> ValidateItems(Func<ItemId, Option<ItemId>> checkItemExists, CartModel.UnvalidatedCart unvalidCart) =>
			from amount in Amount.TryParseAmount(unvalidCart.amount)
								   .ToEitherAsync($"Invalid exam grade ({unvalidCart.itemId}, {unvalidCart.amount})")
			from itemId in ItemId.TryParse(unvalidCart.itemId)
								   .ToEitherAsync($"Invalid student registration number ({unvalidCart.itemId})")
			from studentExists in checkItemExists(itemId)
								   .ToEitherAsync($"Student {itemId.Value} does not exist.")
			select new ValidatedItems(itemId,order, amount);

		private static Either<string, List<CartModel.ValidatedCart>> CreateEmptyValatedGradesList() =>
			Right(new List<CartModel.ValidatedCart>());

		private static EitherAsync<string, List<CartModel.ValidatedCart>> ReduceValidGrades(EitherAsync<string, List<CartModel.ValidatedCart>> acc, EitherAsync<string, CartModel.ValidatedCart> next) =>
			from list in acc
			from nextGrade in next
			select list.AppendValidGrade(nextGrade);

		private static List<CartModel.ValidatedCart> AppendValidGrade(this List<CartModel.ValidatedCart> list, CartModel.ValidatedCart validGrade)
		{
			list.Add(validGrade);
			return list;
		}

		public static ICart CalculateFinalGrades(ICart examGrades) => examGrades.Match(
			whenUnvalidatedCart: unvalidaTedExam => unvalidaTedExam,
			whenInvalidatedCart: invalidExam => invalidExam,
			//whenFailedCart: failedExam => failedExam,
			//whenCalculatedCart: calculatedExam => calculatedExam,
			whenPaidCart: publishedExam => publishedExam
			//whenValidatedExamGrades: CalculateFinalGrade
		);

		private static ICart CalculateFinalGrade(Cart.ValidatedCart validExamGrades) =>
			new CalculatedExamGrades(validExamGrades.GradeList
													.Select(CalculateStudentFinalGrade)
													.ToList()
													.AsReadOnly());

		private static CalculatedSudentGrade CalculateStudentFinalGrade(CartModel.ValidatedCart validGrade) =>
			new CalculatedSudentGrade(validGrade.StudentRegistrationNumber,
									  validGrade.ExamGrade,
									  validGrade.ActivityGrade,
									  validGrade.ExamGrade + validGrade.ActivityGrade);

		public static ICart MergeGrades(ICart examGrades, IEnumerable<CalculatedSudentGrade> existingGrades) => examGrades.Match(
			whenUnvalidatedExamGrades: unvalidaTedExam => unvalidaTedExam,
			whenInvalidExamGrades: invalidExam => invalidExam,
			whenFailedExamGrades: failedExam => failedExam,
			whenValidatedExamGrades: validatedExam => validatedExam,
			whenPublishedExamGrades: publishedExam => publishedExam,
			whenCalculatedExamGrades: calculatedExam => MergeGrades(calculatedExam.GradeList, existingGrades));

		private static CalculatedExamGrades MergeGrades(IEnumerable<CalculatedSudentGrade> newList, IEnumerable<CalculatedSudentGrade> existingList)
		{
			var updatedAndNewGrades = newList.Select(grade => grade with { GradeId = existingList.FirstOrDefault(g => g.StudentRegistrationNumber == grade.StudentRegistrationNumber)?.GradeId ?? 0, IsUpdated = true });
			var oldGrades = existingList.Where(grade => !newList.Any(g => g.StudentRegistrationNumber == grade.StudentRegistrationNumber));
			var allGrades = updatedAndNewGrades.Union(oldGrades)
											   .ToList()
											   .AsReadOnly();
			return new CalculatedExamGrades(allGrades);
		}

		public static ICart PublishExamGrades(ICart examGrades) => examGrades.Match(
			whenUnvalidatedExamGrades: unvalidaTedExam => unvalidaTedExam,
			whenInvalidExamGrades: invalidExam => invalidExam,
			whenFailedExamGrades: failedExam => failedExam,
			whenValidatedExamGrades: validatedExam => validatedExam,
			whenPublishedExamGrades: publishedExam => publishedExam,
			whenCalculatedExamGrades: GenerateExport);

		private static ICart GenerateExport(CalculatedExamGrades calculatedExam) =>
			new PublishedExamGrades(calculatedExam.GradeList,
									calculatedExam.GradeList.Aggregate(new StringBuilder(), CreateCsvLine).ToString(),
									DateTime.Now);

		private static StringBuilder CreateCsvLine(StringBuilder export, CalculatedSudentGrade grade) =>
			export.AppendLine($"{grade.StudentRegistrationNumber.Value}, {grade.ExamGrade}, {grade.ActivityGrade}, {grade.FinalGrade}");


















		//public static bool ValidateCartItem(CartItem item)
		//{
		//	if (item.ItemId > 0) return true;
		//	return false;
		//}


		//public static IDelivery AddItem(IDelivery cart, CartItem itemToAdd)
		//{
		//	IDelivery newCart = cart.Match(
		//			emptyCart =>
		//			{
		//				if (ValidateCartItem(itemToAdd)) //Aici facem validarea produsului
		//				{
		//					var items = new List<CartItem>() { itemToAdd };
		//					var activeCart = new ActiveCart(items);
		//					return activeCart;
		//				}
		//				return emptyCart;
		//			},
		//			activeCart =>
		//			{	
		//				var items = new List<CartItem>();
		//				items.AddRange(activeCart.Items);
		//				if(ValidateCartItem(itemToAdd)) //Aici facem validarea produsului
		//					items.Add(itemToAdd); 
		//				var newActiveCart = new ActiveCart(items);
		//				return newActiveCart;
		//			},
		//			paidCart =>
		//			{
		//				return paidCart;
		//			}
		//		);

		//	return newCart;
		//}

		//public static IDelivery RemoveItem(IDelivery cart, CartItem itemToRemove)
		//{
		//	IDelivery newCart = cart.Match(
		//			emptyCart =>
		//			{
		//				return emptyCart;
		//			},
		//			activeCart =>
		//			{
		//				var items = new List<CartItem>();
		//				items.AddRange(activeCart.Items);
		//				items.Remove(itemToRemove);
		//				if(items.Count==0)
		//				{
		//					var newEmptyCart = new EmptyCart();
		//					return newEmptyCart;
		//				}
		//				var newActiveCart = new ActiveCart(items);
		//				return newActiveCart;
		//			},
		//			paidCart =>
		//			{
		//				return paidCart;
		//			}
		//		);
		//	return newCart;
		//}

		//public static IDelivery PayItems(IDelivery cart)
		//{
		//	IDelivery newCart = cart.Match(
		//			emptyCart =>
		//			{
		//				return emptyCart;
		//			},
		//			activeCart =>
		//			{
		//				var items = new List<CartItem>();
		//				items.AddRange(activeCart.Items);
		//				decimal orderPrice = 0;
		//				foreach (var item in items)
		//				{
		//					orderPrice = orderPrice + item.Total;
		//				}
		//				DateTime PaymentDate = DateTime.Now;
		//				var newPaidCart = new ToPayCart(items, PaymentDate, orderPrice);
		//				return newPaidCart;
		//			},
		//			paidCart =>
		//			{
		//				return paidCart;
		//			}
		//		);
		//	return newCart;
		//}

	}
}
