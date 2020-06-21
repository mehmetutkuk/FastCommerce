using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FastCommerce.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FastCommerce.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        private readonly ProductContext _context;

        public GeneralController(IDistributedCache distributedCache,ProductContext context)
        {
            _context = context;
            this.distributedCache = distributedCache;
        }
        private IDistributedCache distributedCache;

        [HttpGet, Route("GetData")]
        public async Task<List<string>> GetData(string UserName){
            var cacheKey = UserName.ToLower();
            List<string> userList;
            string serializedUsers;

            var encodedUsers = await distributedCache.GetAsync(cacheKey);

            if(encodedUsers != null)
            {
                serializedUsers = Encoding.UTF8.GetString(encodedUsers);
                userList = JsonSerializer.Deserialize<List<string>>(serializedUsers);
            }
            else
            {
                userList =  _context.Users.Select(s => s.Surname).ToList();
                serializedUsers = JsonSerializer.Serialize<List<string>>(userList);
                encodedUsers = Encoding.UTF8.GetBytes(serializedUsers);
                var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5)).SetAbsoluteExpiration(DateTime.Now.AddHours(6));
                await distributedCache.SetAsync(cacheKey, encodedUsers, options);
            }



            return userList;
        }

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
