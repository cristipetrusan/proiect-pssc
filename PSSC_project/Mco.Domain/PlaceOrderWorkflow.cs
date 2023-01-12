using System;
using System.Threading.Tasks;
using OrderProcessing.Domain.CartModel;
using static OrderProcessing.Domain.CartModel.Items;
using static OrderProcessing.Domain.CartModel.PaidItemsEvent;
using OrderProcessing.Domain;
using LanguageExt;

namespace OrderProcessing.Domain
{
    public class PlaceOrderWorkflow
    {
        public async Task<IPaidItemsEvent> ExecuteAsync(PayCartCommand command)
        {
            // UnvalidatedExamGrades unvalidatedGrades = new UnvalidatedExamGrades(command.InputExamGrades);
			UnvalidatedItems unvalidatedItems = new UnvalidatedItems(command.InputCartItems);
            
			// IExamGrades grades = await ValidateExamGrades(checkStudentExists, unvalidatedGrades);
			IItems items = CartOperations.ValidateCartItems(unvalidatedItems);

            // grades = CalculateFinalGrades(grades); NU NI L TREBE

            // grades = PublishExamGrades(grades);
			items = CartOperations.PayItems(items);

            return items.Match(
                    whenUnvalidatedItems: unvalidatedItems => (IPaidItemsEvent)(new PaidItemsFailedEvent("Unexpected unvalidated state") as IPaidItemsEvent),
                    whenInvalidItems: invalidItems => (IPaidItemsEvent)new PaidItemsFailedEvent(invalidItems.Reason),
                    whenValidatedItems: validatedItems => (IPaidItemsEvent)new PaidItemsFailedEvent("Unexpected validated state"),
                    // whenCalculatedExamGrades: calculatedGrades => new PaidCartFailedEvent("Unexpected calculated state"),
                    whenPaidItems: paidItems => (IPaidItemsEvent)new PaidItemsSucceededEvent(paidItems.Price, paidItems.PublishedDate)
                );
        }

        // private IItems PayItems(IItems items)
        // {
        //     throw new NotImplementedException();
        // }
    }
}
