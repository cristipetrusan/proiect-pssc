using LanguageExt;
using Mco.Domain.Dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mco.Domain.Repositories
{
    class ItemsInOrdersRepository
    {
        private readonly OrdersContext _context;

        public ItemsInOrdersRepository(OrdersContext context)
        {
            _context = context;
        }

        //public Order GetOrderById(int id)
        //{
        //	return _context.Orders.Find(id);
        //}

        public void InsertOrder(ItemsInOrderDbo itemInOrder)
        {
            _context.ItemsInOrders.Add(itemInOrder);
            _context.SaveChanges();
        }

        public void UpdateOrder(ItemsInOrderDbo itemInOrder)
        {
            _context.Entry(itemInOrder).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

    }
}
