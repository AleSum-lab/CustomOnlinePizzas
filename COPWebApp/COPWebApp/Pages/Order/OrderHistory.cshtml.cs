using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApplicationSettings;
using BusinessModels;
using Contracts;
using COPWebApp.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace COPWebApp
{
    public class OrderHistoryModel : PageModel
    {
        private IServiceClient _client;
        private readonly IOptions<EndpointSettings> _optionsEndpoint;

        public IList<Order> Orders { get; set; }

        public OrderHistoryModel(IServiceClient client, IOptions<EndpointSettings> optionsEndpoint)
        {
            _client = client;
            _optionsEndpoint = optionsEndpoint;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Uri address = BuildRequestUri();

            var apiResponse = await _client.Get(address).ConfigureAwait(false);

            Orders = await HttpToObjectMapper.MapToCollection(apiResponse).ConfigureAwait(false);

            return Page();

        }

        private Uri BuildRequestUri()
        {
            string userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value).ToString();

            string baseUrl = _optionsEndpoint.Value.OrderServiceUrl;

            return new Uri($"{baseUrl}/{userId}");
        }
    }
}