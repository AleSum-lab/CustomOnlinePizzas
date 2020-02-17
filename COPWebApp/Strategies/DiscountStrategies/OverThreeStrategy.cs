using ApplicationSettings;
using BusinessModels;
using Contracts;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strategies.DiscountStrategies
{
    public class OverThreeStrategy : IDiscountStrategy
    {
        private readonly IOptions<IngredientPriceListSettings> _ingredientPriceSettings;

        public OverThreeStrategy(IOptions<IngredientPriceListSettings> ingredientPriceSettings)
        {
            _ingredientPriceSettings = ingredientPriceSettings;
        }

        public Pizza CalculateFinalPrice(Pizza pizza)
        {
            if(pizza.Ingredients.Contains(Ingredient.TomatoSauce))
            {
                pizza.TotalPrice -= Math.Round(_ingredientPriceSettings.Value.TomatoSauce, 2);
            }
            if(pizza.Ingredients.Contains(Ingredient.MozzarellaCheese))
            {
                pizza.TotalPrice -= Math.Round(_ingredientPriceSettings.Value.MozzarellaCheese, 2);
            }

            return pizza;
        }
    }
}
