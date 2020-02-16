using ApplicationSettings;
using BusinessModels;
using Contracts;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strategies.DeliveryStrategies
{
    public class TenToTwentyKmStretegy : IDeliveryStrategy
    {
        IOptions<DeliveryFeePolicySettings> _deliveryPolicy;
        public TenToTwentyKmStretegy(IOptions<DeliveryFeePolicySettings> deliveryPolicy)
        {
            _deliveryPolicy = deliveryPolicy;
        }

        public Order CalculateFee(Order order)
        {
            order.DeliveryFee = _deliveryPolicy.Value.TenToTwentyKm;
            return order;
        }
    }
}
