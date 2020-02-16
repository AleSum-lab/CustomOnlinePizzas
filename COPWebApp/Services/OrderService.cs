using BusinessModels;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IServiceClient
    {
        private HttpClient _client;
        private readonly string _userName;
        private readonly string _password;

        public OrderService(string userName, string password)
        {
            _client = new HttpClient();
            _userName = userName;
            _password = password;
            SetBasicAuthenticationHeader(_client);

        }

        public async Task<HttpResponseMessage> Get(Uri resourceUri)
        {
            try
            {
                var response = await _client.GetAsync(resourceUri);
               
                return response;
            }
            catch (Exception ex)
            {

                return new HttpResponseMessage() { StatusCode = HttpStatusCode.InternalServerError };
            }
        }

        public async Task<HttpResponseMessage> Post<T>(T model, Uri resourceUri)
        {
            try
            {
                string json = JsonConvert.SerializeObject(model, new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    DateParseHandling = DateParseHandling.None
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(resourceUri, content);

                return response;
            }
            catch (Exception ex)
            {

                return new HttpResponseMessage() { StatusCode = HttpStatusCode.InternalServerError };
            }
        }

        
        private void SetBasicAuthenticationHeader(HttpClient client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            string authInfo = _userName + ":" + _password;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);
        }
    }
}
