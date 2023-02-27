using Microsoft.AspNetCore.Mvc;
using ServerBelPodryad.Models;
using ServerBelPodryad.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        // GET: api/<OrdersController>
        [HttpGet]
        public async Task<ActionResult<List<Order>>> Get()
        {
            try
            {
                List<Order> listOrder = OrderStore.GetAllOrders();
                foreach (Order order in listOrder)
                {
                    order.Region = RegionStore.GetRegionById(order.IdRegion);
                    order.Currency = CurrencyStore.GetCurrencyById(order.IdCurrency);
                    order.JobType = JobTypeStore.GetJobTypeById(order.JobTypeId);
                }

                return listOrder;
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult<Order>> Post([FromBody] Order order)
        {
            OrderStore.CreateOrder(order);
            return Ok();
        }

        [HttpPost("respond")]
        public async Task<ActionResult<Order>> Post([FromBody] PerformerOrder performerOrder)
        {
            if (performerOrder.IdOrder == 0 || performerOrder.IdPerformer == 0)
            {
                return StatusCode(412);
            }

            if (PerformerOrderStore.IsRespondOrderExists(performerOrder.IdPerformer, performerOrder.IdOrder))
            {
                return StatusCode(409, "already exists");
            }

            PerformerOrderStore.SaveRespond(performerOrder.IdPerformer, performerOrder.IdOrder);

            return Ok();
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> Put(int id, [FromBody] Order order)
        {
            order.Id = id;
            OrderStore.UpdateOrder(order);
            return Ok();
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> Delete(int id)
        {
            OrderStore.DeleteOrderById(id);
            return Ok();
        }
    }
}
