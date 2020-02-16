using BusinessModels;
using Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strategies.DiscountStrategies
{
    public class DefaultStrategy : IDiscountStrategy
    {
        public Pizza CalculateFinalPrice(Pizza pizza)
        {
            return pizza;
        }
    }
}
