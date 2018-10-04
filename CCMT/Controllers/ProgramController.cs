using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CCMarketoBL;
using CCMarketoBL.Model;
using Newtonsoft.Json;
using CCMarketoBL.CCMTManager;
using CCMT;

namespace CCMarketoAPI.Controllers
{
    [RoutePrefix("CCMT/v1/Program")]
    public class ProgramController : ApiController
    {
        [HttpGet, Route("list/{enterpriseID}")]
        [MTAuthenticate]
        public IHttpActionResult getMTFolder(string enterpriseID)
        {
            try
            {
                ProgramManager prog = new ProgramManager();
                string accessToken = "";
                if (CCMTHelper.GetCacheValue("mt_access_token") != null)
                {
                    accessToken = CCMTHelper.GetCacheValue("mt_access_token").ToString();

                }
                var result = prog.getProgramsList(enterpriseID, accessToken);
                return Ok(JsonConvert.DeserializeObject<dynamic>(result));
            }
            catch (Exception ex)
            {
                CCMTHelper.logError(ex);
                return InternalServerError();
            }
        }

        [HttpPost, Route("create/{enterpriseID}")]
        [MTAuthenticate]
        public IHttpActionResult createMTFolder(string enterpriseID, ProgramParam progParam)
        {
            try
            {
                string accessToken = "";
                if (CCMTHelper.GetCacheValue("mt_access_token") != null)
                {
                    accessToken = CCMTHelper.GetCacheValue("mt_access_token").ToString();

                }
                ProgramManager prog = new ProgramManager();
                var result = prog.createNewProgram(enterpriseID,progParam, accessToken);
                return Ok(JsonConvert.DeserializeObject<dynamic>(result));
                //  return Ok(new { Message = "Program created successfully" });
            }
            catch (Exception ex)
            {
                CCMTHelper.logError(ex);
                return InternalServerError();
            }
        }
    }
}
