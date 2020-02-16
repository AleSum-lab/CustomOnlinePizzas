using BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IDeliveryStrategy
    {
        Order CalculateFee(Order order);
    }
}
