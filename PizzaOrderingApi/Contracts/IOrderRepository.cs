using DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IOrderRepository
    {
        Task<bool> SaveOrder(Order order);
        Task<IList<Order>> RetrieveOrders(Guid userId);

    }
}
