using BusinessModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace COPWebApp.Mappers
{
    public static class HttpToObjectMapper
    {
        public static async Task<Order> MapToBusinessObject(Task<HttpResponseMessage> message)
        {

            if (message == null) throw new ArgumentNullException(nameof(message));
            Order order = new Order();

            try
            {
                order = JsonConvert.DeserializeObject<Order>(await message.Result.Content.ReadAsStringAsync().ConfigureAwait(false));
            }
            catch (Exception ex)
            {

                throw;
            }            
            return order;
        }

        public static async Task<IList<Order>>MapToCollection(HttpResponseMessage message)
        {

            if (message == null) throw new ArgumentNullException(nameof(message));
            IList<Order> orders = new List<Order>();

            try
            {
                orders = JsonConvert.DeserializeObject<IList<Order>>(await message.Content.ReadAsStringAsync().ConfigureAwait(false));
            }
            catch (Exception ex)
            {

                throw;
            }
            return orders;
        }
    }
}
