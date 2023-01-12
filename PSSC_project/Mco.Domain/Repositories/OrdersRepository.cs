using Mco.Domain.Dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mco.Domain.Repositories
{
    public class OrdersRepository
    {
        private readonly OrdersContext _context;

        public OrdersRepository(OrdersContext context)
        {
            _context = context;
        }

        public void InsertOrder(OrderDbo order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
