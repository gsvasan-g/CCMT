using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MarketoCanvasAPI.Controllers
{
    public class ProgramController : ApiController
    {
        // GET: api/Program
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Program/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Program
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Program/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Program/5
        public void Delete(int id)
        {
        }
    }
}
