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
        public IHttpActionResult GetCCFieldsForMapping()
        {
            CCMTMapper mapper = new CCMTMapper();
            List<string> MTFieldList = new List<string>();
            try
            {               
              var ServResponse= mapper.getCCFields();
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
        [Route("MT")]
        public IHttpActionResult GetMTFieldsForMapping()
        {
            ServiceManager serv = new ServiceManager();
            List<string> MTFieldList = new List<string>();
            CCMTMapper mapper = new CCMTMapper();
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
