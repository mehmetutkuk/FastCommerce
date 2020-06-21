using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FastCommerce.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class General : ControllerBase
    {
        // GET: api/<General>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<General>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<General>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<General>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<General>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
