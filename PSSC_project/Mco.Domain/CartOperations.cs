using LanguageExt;
using static LanguageExt.Prelude;
using OrderProcessing.Domain.CartModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderProcessing.Domain.CartModel.Items;
using Mco.Domain;
using Mco.Domain.Dbo;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace OrderProcessing.Domain
{
	public static class CartOperations
	{
		public static IItems ValidateCartItems(UnvalidatedItems unvalidatedItems, OrdersContext context)
		{
			List<ValidatedItem> validatedItems = new List<ValidatedItem>();
			
			foreach (UnvalidatedItem item in unvalidatedItems.ItemList)
			{
				if (!((ItemId.TryParse(item.itemId) != None) && (Amount.TryParseAmount(item.amount) != None)))
				{
					Console.WriteLine("ItemID does not respect the format");
					return (IItems)new InvalidItems(unvalidatedItems.ItemList, "ItemID " + item.itemId+ " does not respect the format");
				}


				if ((context.Items.Find(i => i.ItemId == item.itemId) == null))
				{
                    Console.WriteLine("ItemID is not in the table");
                    return (IItems)new InvalidItems(unvalidatedItems.ItemList, "ItemID is not in the table");
                }

                validatedItems.Add(new ValidatedItem(new ItemId(item.itemId), new Amount(Convert.ToInt32(item.amount)),
					new Price(Convert.ToDouble(item.price))));
			}
			return (IItems)new ValidatedItems(validatedItems);
		} 

		// public static IItems PayItems(IItems validatedItems) => validatedItems.Match(
		// 	whenUnvalidatedItems: unvalidatedItem => unvalidatedItem,
		// 	whenInvalidItems: invalidItem => invalidItem,
		// 	whenPaidItems: paidItem => paidItem,
		// 	whenValidatedItems: ProcessPayment
		// );

        // private static IItems ProcessPayment(ValidatedItems validatedItems) =>
        //     new CalculatedExamGrades(validatedItems.ItemList
        //                                             .Select(CalculateStudentFinalGrade)
        //                                             .ToList()
        //                                             .AsReadOnly());

		public static IItems PayItems(IItems validatedItems, OrdersContext ordersContext) => validatedItems.Match(
			whenUnvalidatedItems: unvalidatedItem => unvalidatedItem,
			whenInvalidItems: invalidItem => invalidItem,
			whenPaidItems: paidItem => paidItem,
			whenValidatedItems: validatedItem => ExportData(validatedItem.ItemList, ordersContext)
		);

        private static IItems ExportData(IReadOnlyCollection<ValidatedItem> itemList, OrdersContext ordersContext)
        {
            double totalPrice = 0;
            Random rnd = new Random();
            int num = rnd.Next();

            foreach (ValidatedItem validatedItem in itemList)
            {
                totalPrice += validatedItem.amount.Value * validatedItem.price.Value;
				//ItemsInOrderDbo item = new ItemsInOrderDbo();
				//item.ItemId = validatedItem.itemId.Value;
				//item.Amount = validatedItem.amount.Value;
				//item.CartId = num.ToString();
    //            ordersContext.ItemsInOrders.Add(item);
                //ordersContext.SaveChanges();
            }
            OrderDbo order = new OrderDbo();
			order.CartId = num.ToString();
			order.TotalPrice = totalPrice;
			ordersContext.Orders.Add(order);
			ordersContext.SaveChanges();

            return new PaidItems(itemList, totalPrice.ToString(), DateTime.Now);
        }            

		// =>
		// 	unvalidatedItems.ItemList
		// 			  .Select(ValidateCart())
		// 			  .Aggregate(CreateEmptyValidatedGradesList().ToAsync(), ReduceValidGrades)
		// 			  .MatchAsync(
		// 					Right: validatedGrades => new ValidatedItems(validatedGrades),
		// 					LeftAsync: errorMessage => Task.FromResult(new InvalidItems(unvalidatedItems.ItemList, errorMessage))
		// 			  );
        

        // private static Func<UnvalidatedItem, EitherAsync<string, ValidatedItem>> ValidateStudentGrade() =>
        //     unvalidatedItem => ValidateStudentGrade(unvalidatedItem);

        // private static EitherAsync<string, ValidatedItem> ValidateStudentGrade(UnvalidatedItem unvalidatedItem)=>
        //     from item in Item.TryParseItem(unvalidatedItem.Item)
        //                            .ToEitherAsync(() => $"Invalid exam grade ({unvalidatedItem.StudentRegistrationNumber}, {unvalidatedItem.ExamGrade})")
        //     from activityGrade in Grade.TryParseGrade(unvalidatedItem.ActivityGrade)
        //                            .ToEitherAsync(() => $"Invalid activity grade ({unvalidatedItem.StudentRegistrationNumber}, {unvalidatedItem.ActivityGrade})")
        //     from studentRegistrationNumber in StudentRegistrationNumber.TryParse(unvalidatedItem.StudentRegistrationNumber)
        //                            .ToEitherAsync(() => $"Invalid student registration number ({unvalidatedItem.StudentRegistrationNumber})")
        //     from studentExists in checkStudentExists(studentRegistrationNumber)
        //                            .ToEither(error => error.ToString())
        //     select new ValidatedStudentGrade(studentRegistrationNumber, examGrade, activityGrade);

        // private static Either<string, List<ValidatedItem>> CreateEmptyValidatedGradesList() =>
		// 	Right(new List<ValidatedItem>());

		// private static EitherAsync<string, List<ValidatedItem>> ReduceValidGrades(EitherAsync<string, List<ValidatedItem>> acc, EitherAsync<string, ValidatedItem> next) =>
		// 	from list in acc
		// 	from nextItem in next
		// 	select list.AppendValidItem(nextItem);

		// private static List<CartModel.ValidatedItem> AppendValidItem(this List<CartModel.ValidatedItem> list, CartModel.ValidatedItem validItem)
		// {
		// 	list.Add(validItem);
		// 	return list;
		// }

		// public static IItem CalculateFinalGrades(IItem examGrades) => examGrades.Match(
		// 	whenUnvalidatedCart: unvalidaTedExam => unvalidaTedExam,
		// 	whenInvalidatedCart: invalidExam => invalidExam,
		// 	//whenFailedCart: failedExam => failedExam,
		// 	//whenCalculatedCart: calculatedExam => calculatedExam,
		// 	whenPaidCart: publishedExam => publishedExam
		// 	//whenValidatedExamGrades: CalculateFinalGrade
		// );

		// private static IItem CalculateFinalGrade(ValidatedCart validExamGrades) =>
		// 	new CalculatedExamGrades(validExamGrades.GradeList
		// 											.Select(CalculateStudentFinalGrade)
		// 											.ToList()
		// 											.AsReadOnly());

		// private static CalculatedSudentGrade CalculateStudentFinalGrade(ValidatedCart validGrade) =>
		// 	new CalculatedSudentGrade(validGrade.StudentRegistrationNumber,
		// 							  validGrade.ExamGrade,
		// 							  validGrade.ActivityGrade,
		// 							  validGrade.ExamGrade + validGrade.ActivityGrade);

		// public static IItem MergeGrades(IItem examGrades, IEnumerable<CalculatedSudentGrade> existingGrades) => examGrades.Match(
		// 	whenUnvalidatedExamGrades: unvalidaTedExam => unvalidaTedExam,
		// 	whenInvalidExamGrades: invalidExam => invalidExam,
		// 	whenFailedExamGrades: failedExam => failedExam,
		// 	whenValidatedExamGrades: validatedExam => validatedExam,
		// 	whenPublishedExamGrades: publishedExam => publishedExam,
		// 	whenCalculatedExamGrades: calculatedExam => MergeGrades(calculatedExam.GradeList, existingGrades));

		// private static CalculatedExamGrades MergeGrades(IEnumerable<CalculatedSudentGrade> newList, IEnumerable<CalculatedSudentGrade> existingList)
		// {
		// 	var updatedAndNewGrades = newList.Select(grade => grade with { GradeId = existingList.FirstOrDefault(g => g.StudentRegistrationNumber == grade.StudentRegistrationNumber)?.GradeId ?? 0, IsUpdated = true });
		// 	var oldGrades = existingList.Where(grade => !newList.Any(g => g.StudentRegistrationNumber == grade.StudentRegistrationNumber));
		// 	var allGrades = updatedAndNewGrades.Union(oldGrades)
		// 									   .ToList()
		// 									   .AsReadOnly();
		// 	return new CalculatedExamGrades(allGrades);
		// }

		// public static IItem PublishExamGrades(IItem examGrades) => examGrades.Match(
		// 	whenUnvalidatedExamGrades: unvalidaTedExam => unvalidaTedExam,
		// 	whenInvalidExamGrades: invalidExam => invalidExam,
		// 	whenFailedExamGrades: failedExam => failedExam,
		// 	whenValidatedExamGrades: validatedExam => validatedExam,
		// 	whenPublishedExamGrades: publishedExam => publishedExam,
		// 	whenCalculatedExamGrades: GenerateExport);

		// private static IItem GenerateExport(CalculatedExamGrades calculatedExam) =>
		// 	new PublishedExamGrades(calculatedExam.GradeList,
		// 							calculatedExam.GradeList.Aggregate(new StringBuilder(), CreateCsvLine).ToString(),
		// 							DateTime.Now);

		// private static StringBuilder CreateCsvLine(StringBuilder export, CalculatedSudentGrade grade) =>
		// 	export.AppendLine($"{grade.StudentRegistrationNumber.Value}, {grade.ExamGrade}, {grade.ActivityGrade}, {grade.FinalGrade}");


		// public static bool ValidateCartItem(CartItem item)
		// {
		// 	if (item.ItemId > 0) return true;
		// 	return false;
		// }


		// public static IDelivery AddItem(IDelivery cart, CartItem itemToAdd)
		// {
		// 	IDelivery newCart = cart.Match(
		// 			emptyCart =>
		// 			{
		// 				if (ValidateCartItem(itemToAdd)) //Aici facem validarea produsului
		// 				{
		// 					var items = new List<CartItem>() { itemToAdd };
		// 					var activeCart = new ActiveCart(items);
		// 					return activeCart;
		// 				}
		// 				return emptyCart;
		// 			},
		// 			activeCart =>
		// 			{	
		// 				var items = new List<CartItem>();
		// 				items.AddRange(activeCart.Items);
		// 				if(ValidateCartItem(itemToAdd)) //Aici facem validarea produsului
		// 					items.Add(itemToAdd); 
		// 				var newActiveCart = new ActiveCart(items);
		// 				return newActiveCart;
		// 			},
		// 			paidCart =>
		// 			{
		// 				return paidCart;
		// 			}
		// 		);

		// 	return newCart;
		// }

		// public static IDelivery RemoveItem(IDelivery cart, CartItem itemToRemove)
		// {
		// 	IDelivery newCart = cart.Match(
		// 			emptyCart =>
		// 			{
		// 				return emptyCart;
		// 			},
		// 			activeCart =>
		// 			{
		// 				var items = new List<CartItem>();
		// 				items.AddRange(activeCart.Items);
		// 				items.Remove(itemToRemove);
		// 				if(items.Count==0)
		// 				{
		// 					var newEmptyCart = new EmptyCart();
		// 					return newEmptyCart;
		// 				}
		// 				var newActiveCart = new ActiveCart(items);
		// 				return newActiveCart;
		// 			},
		// 			paidCart =>
		// 			{
		// 				return paidCart;
		// 			}
		// 		);
		// 	return newCart;
		// }

		// public static IDelivery PayItems(IDelivery cart)
		// {
		// 	IDelivery newCart = cart.Match(
		// 			emptyCart =>
		// 			{
		// 				return emptyCart;
		// 			},
		// 			activeCart =>
		// 			{
		// 				var items = new List<CartItem>();
		// 				items.AddRange(activeCart.Items);
		// 				decimal orderPrice = 0;
		// 				foreach (var item in items)
		// 				{
		// 					orderPrice = orderPrice + item.Total;
		// 				}
		// 				DateTime PaymentDate = DateTime.Now;
		// 				var newPaidCart = new ToPayCart(items, PaymentDate, orderPrice);
		// 				return newPaidCart;
		// 			},
		// 			paidCart =>
		// 			{
		// 				return paidCart;
		// 			}
		// 		);
		// 	return newCart;
		// }

	}
}
