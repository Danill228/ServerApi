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
    public class NewsController : ControllerBase
    {
        // GET: api/<NewsController>
        [HttpGet]
        public async Task<ActionResult<List<Info>>> Get()
        {
            try
            {
                List<Info> listInfo = InfoStore.GetAllInfo();
                return listInfo;
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // GET api/<NewsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Info>> Get(int id)
        {
            try
            {
                Info info = InfoStore.GetInfoById(id);
                if (info.Id == 0)
                {
                    return NotFound();
                }

                return info;
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // POST api/<NewsController>
        [HttpPost]
        public async Task<ActionResult<Info>> Post([FromBody] Info info)
        {
            if (info.Data == null || info.Title == null)
            {
                return StatusCode(412);
            }
            info.DatePublication = DateTime.Now.ToString();

            InfoStore.CreateInfo(info);

            return StatusCode(200);
        }

        // PUT api/<NewsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Info>> Put(int id, [FromBody] Info info)
        {

            Info findInfo = InfoStore.GetInfoById(id);
            if (findInfo.Id == 0)
            {
                return NotFound();
            }
            info.Id = id;

            InfoStore.UpdateInfo(info);

            return StatusCode(200);
        }

        // DELETE api/<NewsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Info>> Delete(int id)
        {

            InfoStore.DeleteInfoById(id);
            return Ok();
        }
    }
}
