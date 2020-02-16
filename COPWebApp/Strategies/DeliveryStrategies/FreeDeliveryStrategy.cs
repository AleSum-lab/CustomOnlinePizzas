using BusinessModels;
using Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strategies.DeliveryStrategies
{
    public class FreeDeliveryStrategy : IDeliveryStrategy
    {
        public Order CalculateFee(Order order)
        {
            return order;
        }
    }
}
