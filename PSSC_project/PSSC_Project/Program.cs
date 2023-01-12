using Microsoft.Extensions.Logging;
using Delivery.Domain.DeliveryModel;
using LanguageExt;
using Mco.Domain;
using Mco.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using OrderProcessing.Domain;
using OrderProcessing.Domain.CartModel;
using static Delivery.Domain.DeliveryModel.Address;
using static Delivery.Domain.DeliveryModel.Delivery;
using Mco.Domain.Dbo;

namespace PSSC_Project
{
    class Program
	{
        private static string ConnectionString = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;Encrypt=false;";

        static async Task Main(string[] args)
		{
            //using ILoggerFactory loggerFactory = ConfigureLoggerFactory();

            //var listOfGrades = ReadListOfGrades().ToArray();
            //PayCartCommand command = new(listOfGrades);
            //PlaceOrderWorkflow workflow = new();
            //var result = await workflow.ExecuteAsync(command);

            //result.Match(
            //        whenPaidItemsFailedEvent: @event =>
            //        {
            //            Console.WriteLine($"Publish failed: {@event.Reason}");
            //            return @event;
            //        },
            //        whenPaidItemsSucceededEvent: @event =>
            //        {
            //            Console.WriteLine($"Publish succeeded.");
            //            Console.WriteLine(@event.Csv);
            //            return @event;
            //        }
            //    );

            var dbContextBuilder = new DbContextOptionsBuilder<OrdersContext>()
                                    .UseSqlServer(ConnectionString);
                                    //.UseLoggerFactory(loggerFactory);
            OrdersContext ordersContext = new OrdersContext(dbContextBuilder.Options);
            ItemsRepository itemsRepository = new(ordersContext);
            OrdersRepository ordersRepository = new(ordersContext);
            //ItemsInOrdersRepository itemsInOrdersRepository = new(ordersContext);

            Console.WriteLine("Succes");

            //var list = itemsRepository.GetAllOrders();
            Console.WriteLine("Succes");

            foreach (ItemDbo item in ordersContext.Items)
            {
                Console.WriteLine("4");
                Console.WriteLine(item.Name);
            }
        }

        private static List<UnvalidatedItem> ReadListOfGrades()
        {
            List<UnvalidatedItem> listOfGrades = new();
            do
            {
                //read registration number and grade and create a list of greads
                var itemId = ReadValue("ItemID: ");
                if (string.IsNullOrEmpty(itemId))
                {
                    break;
                }

                var amount = ReadValue("Amount: ");
                if (string.IsNullOrEmpty(amount))
                {
                    break;
                }

                listOfGrades.Add(new(itemId, amount));
            } while (true);
            return listOfGrades;
        }

        private static string? ReadValue(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        //private static ILoggerFactory ConfigureLoggerFactory()
        //{
        //    return LoggerFactory.Create(builder =>
        //                        builder.AddSimpleConsole(options =>
        //                        {
        //                            options.IncludeScopes = true;
        //                            options.SingleLine = true;
        //                            options.TimestampFormat = "hh:mm:ss ";
        //                        })
        //                        .AddProvider(new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()));
        //}



    }
}