using ApplicationSettings;
using BusinessModels;
using Contracts;
using Microsoft.Extensions.Options;
using Strategies.DeliveryStrategies;
using Strategies.DiscountStrategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class PriceCalculatorService : IPriceCalculator
    {
        private readonly IOptions<DeliveryFeePolicySettings> _deliverySettings;
        private readonly IOptions<IngredientPriceListSettings> _ingredientPriceSettings;
        private IDiscountStrategy _discountStrategy;
        private IDeliveryStrategy _deliveryStrategy;

        public PriceCalculatorService(IOptions<DeliveryFeePolicySettings> deliverySettings, 
                                      IOptions<IngredientPriceListSettings> ingredientPriceSettings)
        {
            _deliverySettings = deliverySettings;
            _ingredientPriceSettings = ingredientPriceSettings;
        }

        public Order Appraise(Order order, Pizza item)
        {
            item = AppraisePizza(item);
            SetDiscountStrategy(order);
            
            item = _discountStrategy.CalculateFinalPrice(item);
            order.Items.Add(item);

            return order;
        }

        public Order CalculateDeliveryFee(Order order)
        {
            SetDeliveryStrategy(order);

            return _deliveryStrategy.CalculateFee(order);
        }

        private Pizza AppraisePizza(Pizza input)
        {
            var pricelist = _ingredientPriceSettings.Value;

            double totalPrice = 0;
            totalPrice += pricelist.PizzaBase;

            foreach (var item in input.Ingredients)
            {
                switch(item)
                {
                    case Ingredient.TomatoSauce:
                        totalPrice += pricelist.TomatoSauce;
                        break;
                    case Ingredient.MozzarellaCheese:
                        totalPrice += pricelist.MozzarellaCheese;
                        break;
                    case Ingredient.Ham:
                        totalPrice += pricelist.Ham;
                        break;
                    case Ingredient.Kebab:
                        totalPrice += pricelist.Kebab;
                        break;
                }
            }
            input.TotalPrice = Math.Round(totalPrice, 2);
            return input;
        }

        private void SetDiscountStrategy(Order order)
        {
            if(order.Items.Count >= 3)
            {
                _discountStrategy = new OverThreeStrategy(_ingredientPriceSettings);
            }
            else
            {
                _discountStrategy = new DefaultStrategy();
            }
        }

        private void SetDeliveryStrategy(Order order)
        {
            double distance = order.DeliveryDistance;
            if (distance < 5 || distance > 20)
            {
                _deliveryStrategy = new FreeDeliveryStrategy();
            }
            else if(distance >= 5 && distance < 10)
            {
                _deliveryStrategy = new FiveToTenKmStrategy(_deliverySettings);
            }
            else if(distance >= 10 && distance <= 20)
            {
                _deliveryStrategy = new TenToTwentyKmStretegy(_deliverySettings);
            }
                
        }

    }
}
