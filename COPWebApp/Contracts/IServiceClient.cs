using BusinessModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IServiceClient
    {
        Task<HttpResponseMessage> Get(Uri resourceUri);
        Task<HttpResponseMessage> Post<T>(T model, Uri resourceUri);
    }
}
