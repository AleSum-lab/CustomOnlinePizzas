using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationSettings;
using BusinessModels;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace COPWebApp
{
    public class OrderModel : PageModel
    {
        private readonly IPriceCalculator _calculator;
        private IServiceClient _client;
        private readonly IOptions<EndpointSettings> _optionsEndpoint;
        private readonly IOptions<IngredientPriceListSettings> _ingredientPriceList;
        private readonly IOptions<DeliveryFeePolicySettings> _deliveriFeePolicy;

        [BindProperty]
        public Order Order { get; set; }

        [BindProperty]
        public bool TomatoChecked { get; set; }
        [BindProperty]
        public bool MozzarellaChecked { get; set; }
        [BindProperty]
        public bool HamChecked { get; set; }
        [BindProperty]
        public bool KebabChecked { get; set; }

        public int Total { get; set; }

        public OrderModel(IServiceClient client, 
                          IOptions<EndpointSettings> optionsEndpoint, 
                          IPriceCalculator calculator,
                          IOptions<IngredientPriceListSettings> ingredientPriceList,
                          IOptions<DeliveryFeePolicySettings> deliveryFeePolicy)
        {
            _calculator = calculator;
            _client = client;
            _optionsEndpoint = optionsEndpoint;
            _ingredientPriceList = ingredientPriceList;
            _deliveriFeePolicy = deliveryFeePolicy;
        }

        public async Task<IActionResult> OnGet()
        {
            PopulateView();

            Order = new Order();

            return Page();
        }

        public async Task<IActionResult> OnPostAddItem()
        {           
            if(!string.IsNullOrWhiteSpace(HttpContext.Session.GetString("Order")))
            {
                Order = JsonConvert.DeserializeObject<Order>(HttpContext.Session.GetString("Order"));
            }
                

            Pizza item = PreparePizza(_ingredientPriceList.Value.PizzaBase);
            Order = _calculator.Appraise(Order, item);
            HttpContext.Session.SetString("Order", JsonConvert.SerializeObject(Order));
            PopulateView();

            return Page();
        }

        public async Task<IActionResult> OnPostPlaceOrder()
        {
            double distance = Order.DeliveryDistance;

            if (!string.IsNullOrWhiteSpace(HttpContext.Session.GetString("Order")))
            {
                Order = JsonConvert.DeserializeObject<Order>(HttpContext.Session.GetString("Order"));
            }
            Order.DeliveryDistance = distance;
            Order = _calculator.CalculateDeliveryFee(Order);
            Order.UserId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Order.DateCreated = DateTime.UtcNow;

            var result = await _client.Post(Order, new Uri(_optionsEndpoint.Value.OrderServiceUrl)).ConfigureAwait(false);

            if(result.IsSuccessStatusCode)
            {
                ViewData["OrderResult"] = "Thank you for your order!";
            }
            else
            {
                ViewData["OrderResult"] = "Something went wrong. Your order was not created. Please try again.";
            }
            PopulateView();
            return Page();
        }

        private Pizza PreparePizza(double basePrice)
        {
            Pizza result = new Pizza();
            result.BasePrice = basePrice;

            if (TomatoChecked) result.Ingredients.Add(Ingredient.TomatoSauce);
            if (MozzarellaChecked) result.Ingredients.Add(Ingredient.MozzarellaCheese);
            if (HamChecked) result.Ingredients.Add(Ingredient.Ham);
            if (KebabChecked) result.Ingredients.Add(Ingredient.Kebab);

            return result;
        }

        private void PopulateView()
        {
            ViewData["TomatoSauce"] = _ingredientPriceList.Value.TomatoSauce.ToString("F2", CultureInfo.InvariantCulture.NumberFormat);
            ViewData["MozzarellaCheese"] = _ingredientPriceList.Value.MozzarellaCheese.ToString("F2", CultureInfo.InvariantCulture.NumberFormat);
            ViewData["Ham"] = _ingredientPriceList.Value.Ham.ToString("F2", CultureInfo.InvariantCulture.NumberFormat);
            ViewData["Kebab"] = _ingredientPriceList.Value.Kebab.ToString("F2", CultureInfo.InvariantCulture.NumberFormat);
            ViewData["PizzaBase"] = _ingredientPriceList.Value.PizzaBase.ToString("F2", CultureInfo.InvariantCulture.NumberFormat);

            ViewData["FiveToTen"] = _deliveriFeePolicy.Value.FiveToTenKm.ToString("F2", CultureInfo.InvariantCulture.NumberFormat);
            ViewData["TenToTwenty"] = _deliveriFeePolicy.Value.TenToTwentyKm.ToString("F2", CultureInfo.InvariantCulture.NumberFormat);            
            
        }
    }
}