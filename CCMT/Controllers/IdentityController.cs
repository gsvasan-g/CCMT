using System;
using System.Collections.Generic;

using System.Web.Http;
using CCMarketoBL;
using CCMarketoBL.Model;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Results;
using CCMarketoBL.CCMTManager;
using System.Configuration;

namespace CCMT.Controllers
{
    [RoutePrefix("CCMT/v1/Identity")]
    public class IdentityController : ApiController
    {
        [HttpPost]
        [Route("MT/authenticate")]  
         
        public IHttpActionResult MTAuthenticate(IdentityModel identityModel)
        {  
            try
            {
               
                IdentityManager identity = new IdentityManager();
                    // var apiResponse = identity.createIdentity(identityModel);
                    var apiResponse = identity.Authenticate_Marketo(identityModel);

                    if (apiResponse != null)
                    {
                        if (apiResponse.ContainsKey("error"))
                        {
                            return Content(HttpStatusCode.Unauthorized, apiResponse.ContainsKey("error_description") ? apiResponse["error_description"] : "error");
                        }
                        else
                        {
                        SaveCCMTCredentials(identityModel);
                           // var res = identity.saveCredentials(identityModel, CCaccessToken);

                    }

                        return Ok(apiResponse);
                    }
                    else
                        return InternalServerError();
                            
            }
            catch (Exception ex)
            {
                //log exception 
                CCMTHelper.logError(ex);
                return InternalServerError();
            }
           
        }


        public object SaveCCMTCredentials(IdentityModel model)
        {
            try
            {
                string accessToken = "";
                if (CCMTHelper.GetCacheValue("cc_access_token") == null)
                {
                    CCIdentityParam identityModel = new CCIdentityParam();
                    IdentityManager identityManager = new IdentityManager();
                    identityModel.grant_type = ConfigurationManager.AppSettings["CCGrantType"];
                    identityModel.username = ConfigurationManager.AppSettings["CCUserName"];
                    identityModel.password = ConfigurationManager.AppSettings["CCPassword"];
                    var apiResponse = identityManager.Authenticate_CC(identityModel);
                    if (apiResponse != null)
                    {
                        var expireMinutes = (Int32)Math.Round((Convert.ToDecimal(apiResponse["expires_in"])) / 60);
                        CCMTHelper.AddCache("cc_access_token", apiResponse["access_token"].ToString(), DateTimeOffset.Now.AddMinutes(expireMinutes));
                    }
                }
                
                if (CCMTHelper.GetCacheValue("cc_access_token") != null)
                {
                    accessToken = CCMTHelper.GetCacheValue("cc_access_token").ToString();

                }
                IdentityManager identity = new IdentityManager();                
                 var res = identity.saveCredentials(model, accessToken);
                   
                 return res;
               
            }
            catch (Exception ex)
            {
                CCMTHelper.logError(ex);
                return null;
            }

        }

        [HttpGet]
        [Route("GetCredentials/{enterpriseID}")]
        public IHttpActionResult CCMTGetCredentials(string enterpriseID)
        {
            try
            {
                IdentityManager identity = new IdentityManager();
                string accessToken = "";
                if (CCMTHelper.GetCacheValue("cc_access_token") != null)
                {
                    accessToken = CCMTHelper.GetCacheValue("cc_access_token").ToString();

                }
                var apiResponse = identity.getIdentityByID(enterpriseID,accessToken);
                if (apiResponse != null)
                    return Ok(JsonConvert.SerializeObject(apiResponse));
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                CCMTHelper.logError(ex);
                return BadRequest();
            }

        }
    }
}
