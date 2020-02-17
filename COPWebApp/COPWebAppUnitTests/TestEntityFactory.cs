using ApplicationSettings;
using Contracts;
using Microsoft.Extensions.Options;
using Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace COPWebAppUnitTests
{
    public static class TestEntityFactory
    {
        public static IPriceCalculator CreatePriceCalculatorService()
        {
            var deliveryPolicySettings = CreateDeliverySettings();
            var ingredientSettings = CreateIngredienSettings();

            return new PriceCalculatorService(deliveryPolicySettings, ingredientSettings);
        }

        public static IOptions<DeliveryFeePolicySettings> CreateDeliverySettings()
        {
            return Options.Create(new DeliveryFeePolicySettings()
            {
                FiveToTenKm = 4.99,
                TenToTwentyKm = 14.99
            });
        }

        public static IOptions<IngredientPriceListSettings> CreateIngredienSettings()
        {
            return Options.Create(new IngredientPriceListSettings()
            {
                Ham = 1.49,
                Kebab = 2.99,
                MozzarellaCheese = 1,
                TomatoSauce = 1,
                PizzaBase = 5
            });
        }

    } 
}
