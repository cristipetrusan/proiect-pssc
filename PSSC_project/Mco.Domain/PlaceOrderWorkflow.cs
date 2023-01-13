using System;
using System.Threading.Tasks;
using OrderProcessing.Domain.CartModel;
using static OrderProcessing.Domain.CartModel.Items;
using static OrderProcessing.Domain.CartModel.PaidItemsEvent;
using OrderProcessing.Domain;
using LanguageExt;
using Mco.Domain;

namespace OrderProcessing.Domain
{
    public class PlaceOrderWorkflow
    {
        public async Task<IPaidItemsEvent> ExecuteAsync(PayCartCommand command, OrdersContext context)
        {
			UnvalidatedItems unvalidatedItems = new UnvalidatedItems(command.InputCartItems);
            
			IItems items = CartOperations.ValidateCartItems(unvalidatedItems, context);

			items = CartOperations.PayItems(items, context);

            return items.Match(
                    whenUnvalidatedItems: unvalidatedItems => (IPaidItemsEvent)(new PaidItemsFailedEvent("Unexpected unvalidated state") as IPaidItemsEvent),
                    whenInvalidItems: invalidItems => (IPaidItemsEvent)new PaidItemsFailedEvent(invalidItems.Reason),
                    whenValidatedItems: validatedItems => (IPaidItemsEvent)new PaidItemsFailedEvent("Unexpected validated state"),
                    // whenCalculatedExamGrades: calculatedGrades => new PaidCartFailedEvent("Unexpected calculated state"),
                    whenPaidItems: paidItems => (IPaidItemsEvent)new PaidItemsSucceededEvent(paidItems.Price, paidItems.PublishedDate)
                );
        }
    }
}
