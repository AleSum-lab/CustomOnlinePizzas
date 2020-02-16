using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataModels;
using PizzaOrderingApi.Authentication;
using Contracts;

namespace PizzaOrderingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repository;

        public OrdersController(IOrderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public string Get()
        {
            return "Hello from Pizza Ordering Api!";
        }

        // GET: api/Orders
        [HttpGet("{id}")]
        [BasicAuth]
        public async Task<IEnumerable<Order>> GetOrders(Guid id)
        {
            IList<Order> result = new List<Order>();
            try
            {
                result = await _repository.RetrieveOrders(id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
               
            }
            return result;
        }


        // POST: api/Orders
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [BasicAuth]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            try
            {
                await _repository.SaveOrder(order).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw;
            }

            return Created("", order);
        }

        //private bool OrderExists(int id)
        //{
        //    return _context.Orders.Any(e => e.Id == id);
        //}
    }
}
