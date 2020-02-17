using System;
using System.Collections.Generic;
using System.Text;
using BusinessModels;
using NUnit.Framework;
using Moq;
using Services;
using Contracts;
using Microsoft.Extensions.Options;
using ApplicationSettings;

namespace COPWebAppUnitTests
{
    public class PriceCalculatorServiceTests
    {
        private IOptions<IngredientPriceListSettings> ingredientPriceList = TestEntityFactory.CreateIngredienSettings();
        private IOptions<DeliveryFeePolicySettings> deliveryPolicy = TestEntityFactory.CreateDeliverySettings();
        private IPriceCalculator service = TestEntityFactory.CreatePriceCalculatorService();

        [Test]
        public void Appraise_OrderYetEmptyPizzaHasAllIngredients_TotalOrderEqualsTotalPizza()
        {
            //Arrange
            Order order = new Order();
            Pizza pizza = new Pizza();
            pizza.Ingredients.Add(Ingredient.Ham);
            pizza.Ingredients.Add(Ingredient.Kebab);
            pizza.Ingredients.Add(Ingredient.MozzarellaCheese);
            pizza.Ingredients.Add(Ingredient.TomatoSauce);

            var expectedPrice = ingredientPriceList.Value.PizzaBase +
                                ingredientPriceList.Value.Ham +
                                ingredientPriceList.Value.Kebab +
                                ingredientPriceList.Value.MozzarellaCheese +
                                ingredientPriceList.Value.TomatoSauce;

            //Act
            var result = service.Appraise(order, pizza);

            Assert.AreEqual(result.TotalPrice, pizza.TotalPrice);
            Assert.AreEqual(expectedPrice, result.TotalPrice);
        }

        [Test]
        public void Appraise_OrderHasThreeItemsInputHasTomatoAndMozzarella_TomatoAndMozzarellaPriceSubtracted()
        {
            //Arrange
            Order order = new Order();
            order.Items.Add(new Pizza());
            order.Items.Add(new Pizza());
            order.Items.Add(new Pizza());

            Pizza pizza = new Pizza();
            pizza.Ingredients.Add(Ingredient.MozzarellaCheese);
            pizza.Ingredients.Add(Ingredient.TomatoSauce);

            var expectedPrice = ingredientPriceList.Value.PizzaBase;

            //Act
            service.Appraise(order, pizza);

            Assert.AreEqual(expectedPrice, pizza.TotalPrice);
            Assert.AreEqual(expectedPrice, order.TotalPrice);
        }

        [Test]
        public void CalculateDeliveryFee_DistanceIsLessThenFiveKm_FeeIsZero()
        {
            //Arrange
            Order order = new Order();
            order.DeliveryDistance = 4;

            //Act
            service.CalculateDeliveryFee(order);

            Assert.AreEqual(0, order.DeliveryFee);
        }

        [Test]
        public void CalculateDeliveryFee_DistanceIsGreaterThenKm_FeeIsZero()
        {
            //Arrange
            Order order = new Order();
            order.DeliveryDistance = 25;

            //Act
            service.CalculateDeliveryFee(order);

            Assert.AreEqual(0, order.DeliveryFee);
        }

        [Test]
        public void CalculateDeliveryFee_DistanceIsBetweenFiveAndTenKm_FiveToTenKmPolicyApplies()
        {
            //Arrange
            Order order = new Order();
            order.DeliveryDistance = 7;

            var expected = deliveryPolicy.Value.FiveToTenKm;

            //Act
            service.CalculateDeliveryFee(order);

            Assert.AreEqual(expected, order.DeliveryFee);
        }

        [Test]
        public void CalculateDeliveryFee_DistanceIsBetweenTenAndTwentyKm_TenToTwentyKmPolicyApplies()
        {
            //Arrange
            Order order = new Order();
            order.DeliveryDistance = 13;

            var expected = deliveryPolicy.Value.TenToTwentyKm;

            //Act
            service.CalculateDeliveryFee(order);

            Assert.AreEqual(expected, order.DeliveryFee);
        }

    }
}
