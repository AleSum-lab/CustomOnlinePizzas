
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using DataModels;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveOrder(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<IList<Order>> RetrieveOrders(Guid userId)
        {
            if(userId != Guid.Empty)
            {
                return await _context.Orders.Include(p => p.Items).Where(o => o.UserId == userId).ToListAsync();
            }
            else
            {
                throw new ArgumentException(nameof(userId));
            }


        }
    }
}
