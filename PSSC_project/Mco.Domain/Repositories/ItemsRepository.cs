using Mco.Domain.Dbo;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mco.Domain.Repositories
{
    public class ItemsRepository
    {
        private readonly OrdersContext _context;

        public ItemsRepository(OrdersContext context)
        {
            _context = context;
        }

        public IEnumerable<ItemDbo> GetAllOrders()
        {
            Console.WriteLine("3");
            return _context.Items.ToList();
        }

        //public ItemsRepository.GetItems()
        //{
        //    return _context.Items;
        //}

        //public TryAsync<List<OrderRegistrationCode>> TryGetExistingOrders(IEnumerable<string> ordersToCheck) => async () =>
        //{
        //	var orders = await orderContext.Products
        //									  .Where(order => ordersToCheck.Contains(order.RegistrationCode))
        //									  .AsNoTracking()
        //									  .ToListAsync();

        //	return orders.Select(order => new OrderRegistrationCode(order.RegistrationCode))
        //				   .ToList();
        //};
    }
}
