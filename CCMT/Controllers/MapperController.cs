using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CCMarketoBL;
using Newtonsoft.Json;


namespace CCMT.Controllers
{
 
    [RoutePrefix("CCMT/v1/Fields")]
    public class MapperController : ApiController
    {
        [HttpGet]
        [Route("CC")]
        [CCAuthenticate]
        public IHttpActionResult GetCCFieldsForMapping()
        {
            MapperManager mapper = new MapperManager();
            Dictionary<string, string> CCFieldList = new Dictionary<string, string>();
            try
            {
                string accessToken = "";
                var coo = Request.Headers.GetCookies("cc_access_token");
                if (coo.Count > 0)
                {
                    var cookie = coo[0].Cookies[0];
                    if (cookie != null)
                    {
                        accessToken = cookie.Value;
                    }
                }
                var ServResponse= mapper.getCCFields(accessToken);
                if (ServResponse != null)
                {
                    var apiObject = JsonConvert.DeserializeObject<dynamic>(ServResponse);
                    
                   
                    foreach (var tmp in apiObject)
                    {
                        if (!CCFieldList.ContainsKey(tmp.id.ToString()))
                        {
                            CCFieldList.Add(tmp.id.ToString(), tmp.text.ToString());
                        }
                    }
                    return Ok(new { CCFields = CCFieldList });
                }
                return BadRequest();
            }
            catch (Exception ex)
            {                

                return InternalServerError();
            }
           
        }
        [HttpGet]
        [Route("MT")]
        public IHttpActionResult GetMTFieldsForMapping()
        {
            ServiceManager serv = new ServiceManager();
            List<string> MTFieldList = new List<string>();
            MapperManager mapper = new MapperManager();
            try
            {
                var ServResponse = mapper.getMarketoFields("");
                var apiObject = JsonConvert.DeserializeObject<dynamic>(ServResponse);
                var fieldresult = JsonConvert.DeserializeObject<dynamic>(apiObject.result.ToString());

                foreach (var tmp in fieldresult)
                {
                    var obj = JsonConvert.DeserializeObject<dynamic>(tmp.rest.ToString());
                    MTFieldList.Add(obj.name.ToString());
                }
                return Ok(new { MTFields = MTFieldList });
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }


    }
}
