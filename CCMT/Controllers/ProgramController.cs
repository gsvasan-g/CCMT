using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CCMarketoBL;
using CCMarketoBL.Model;
using Newtonsoft.Json;

namespace CCMarketoAPI.Controllers
{
    [RoutePrefix("CCMT/v1/Program")]
    public class ProgramController : ApiController
    {
        [HttpGet, Route("list/{enterpriseID}")]
        public IHttpActionResult getMTFolder(string enterpriseID)
        {
            try
            {
                ProgramManager prog = new ProgramManager();
                var result = prog.getProgramsList(enterpriseID);
                return Ok(JsonConvert.DeserializeObject<dynamic>(result));
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPost, Route("create")]
        public IHttpActionResult createMTFolder(ProgramParam progParam)
        {
            try
            {
                ProgramManager prog = new ProgramManager();
                var result = prog.createNewProgram(progParam);
                return Ok(JsonConvert.DeserializeObject<dynamic>(result));
                //  return Ok(new { Message = "Program created successfully" });
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
