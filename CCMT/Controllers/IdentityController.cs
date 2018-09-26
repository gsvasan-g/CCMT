using System;
using System.Collections.Generic;

using System.Web.Http;
using CCMarketoBL;
using CCMarketoBL.Model;
using System.Net;
using Newtonsoft.Json;

namespace CCMT.Controllers
{
    [RoutePrefix("CCMT/v1/Identity")]
    public class IdentityController : ApiController
    {
        [HttpPost]
        [Route("SaveCredentials")]
        public IHttpActionResult CCMTSaveCredentials(IdentityModel identityModel)
        {  
            try
            {               
                IdentityManager identity = new IdentityManager();
                var apiResponse = identity.createIdentity(identityModel);
                if(apiResponse.ContainsKey("error"))
                {
                    return Content(HttpStatusCode.Unauthorized, apiResponse.ContainsKey("error_description") ? apiResponse["error_description"]:"error");
                }
                return Ok( apiResponse);                
            }
            catch (Exception ex)
            {                

                return BadRequest();
            }
           
        }

        [HttpGet]
        [Route("GetCredentials/{enterpriseID}")]
        public IHttpActionResult CCMTGetCredentials(string enterpriseID)
        {
            try
            {
                IdentityManager identity = new IdentityManager();
                var apiResponse = identity.getIdentityByID(enterpriseID);
                if (apiResponse != null)
                    return Ok(JsonConvert.SerializeObject(apiResponse));
                else
                    return NotFound();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }

        }
    }
}
