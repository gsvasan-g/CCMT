using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CCMarketoBL;
using Newtonsoft.Json;
using CCMarketoBL.CCMTManager;

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
            
                if (CCMTHelper.GetCacheValue("cc_access_token")!=null)
                {
                        accessToken = CCMTHelper.GetCacheValue("cc_access_token").ToString();
                   
                }
                if (!string.IsNullOrEmpty(accessToken))
                {
                    var ServResponse = mapper.getCCFields(accessToken);
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
                string accessToken = "";
                if (CCMTHelper.GetCacheValue("mt_access_token") != null)
                {
                    accessToken = CCMTHelper.GetCacheValue("mt_access_token").ToString();

                }
                var ServResponse = mapper.getMarketoFields(accessToken);
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

        [HttpPost, Route("mapCCMT")]
        public IHttpActionResult createMTFolder()
        {
            try
            {
                MapperManager mapper = new MapperManager();
                var result = mapper.mapCCMTEntity("","","","","");
                return Ok(JsonConvert.DeserializeObject<dynamic>(result));                
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

    }
}
