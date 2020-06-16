using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rally.Framework.Core;
using Rally.Framework.Core.DomainModel;
using Rally.Framework.Facade;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RallyFramework.API.Controllers
{
    [Route("api/Nuclide")]
    [ApiController]
    public class NuclideController : ControllerBase
    {
        private INuclideManager nuclideManager = Facade.CreateNuclideManager();

        // GET: api/<NuclideController>
        [HttpGet]
        public IEnumerable<Nuclide> Get()
        {
            return this.nuclideManager.GetNuclides();

            //return new string[] { "value1", "value2" };
        }

        // GET api/<NuclideController>/5
        [HttpGet("{id}")]
        public Nuclide Get(int id)
        {
            return this.nuclideManager.GetNuclide(id.ToString());

            //return "value";
        }

        // POST api/<NuclideController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<NuclideController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NuclideController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
