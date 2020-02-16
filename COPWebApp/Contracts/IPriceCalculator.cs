using BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IPriceCalculator
    {
        Order Appraise(Order order, Pizza item);
        Order CalculateDeliveryFee(Order order);
    }
}
