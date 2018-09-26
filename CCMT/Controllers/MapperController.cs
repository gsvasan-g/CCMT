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
    
    public class MapperController : ApiController
    {
        [HttpGet]
        [Route("CCFields")]
        public IHttpActionResult GetCCFieldsForMapping()
        {
            ServiceManager serv = new ServiceManager();
            List<string> MTFieldList = new List<string>();
            try
            {

                var ServResponse = serv.CC_GetServResponse("cccc");
                                   

                var apiObject = JsonConvert.DeserializeObject<dynamic>(ServResponse);
                var fieldresult = JsonConvert.DeserializeObject<dynamic>(apiObject.result.ToString());

                foreach (var tmp in fieldresult)
                {
                    var obj = JsonConvert.DeserializeObject<dynamic>(tmp.rest.ToString());
                    MTFieldList.Add(obj.name);
                }
                return Ok(JsonConvert.SerializeObject(MTFieldList));
            }
            catch (Exception ex)
            {                

                return BadRequest();
            }
           
        }
        [HttpGet]
        [Route("MTFields")]
        public IHttpActionResult GetMTFieldsForMapping()
        {
            ServiceManager serv = new ServiceManager();
            List<string> MTFieldList = new List<string>();
            try
            {

                var ServResponse = serv.MT_GetServResponse("/rest/v1/leads/describe.json?access_token=eae4d363-69c9-4694-b98c-c9d553dd01df:sj");
                                    

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
