using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    public class CurrenciesController : ControllerBase
    {
        // GET: api/<CurrenciesController>
        [HttpGet]
        public async Task<ActionResult<List<Currency>>> Get()
        {
            try
            {
                List<Currency> listCurrencies = CurrencyStore.GetAllCurrencies();
                return listCurrencies;
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // GET api/<CurrenciesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Currency>> Get(int id)
        {
            try
            {
                Currency currency = CurrencyStore.GetCurrencyById(id);
                if (currency.Id == 0)
                {
                    return NotFound();
                }

                return currency;
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // POST api/<CurrenciesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CurrenciesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CurrenciesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
