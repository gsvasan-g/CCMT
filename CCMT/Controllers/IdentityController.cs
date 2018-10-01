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

namespace CCMT.Controllers
{
    [RoutePrefix("CCMT/v1/Identity")]
    public class IdentityController : ApiController
    {
        [HttpPost]
        [Route("authenticate")]   
        [MTAuthenticate]     
        public IHttpActionResult CCMTSaveCredentials(IdentityModel identityModel)
        {  
            try
            {
                HttpResponseMessage cookieResponse = new HttpResponseMessage();
                var myCookie = getCookie("access_token");
                if (string.IsNullOrEmpty(myCookie))
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
                            var res = identity.saveCredentials(identityModel);
                          cookieResponse=  setCookies("access_token", apiResponse["access_token"]);
                        }

                        return Content(HttpStatusCode.OK, new ResponseMessageResult(cookieResponse));
                    }
                    else
                        return InternalServerError();
                }
                return Ok();                
            }
            catch (Exception ex)
            {                

                return InternalServerError();
            }
           
        }

        public HttpResponseMessage setCookies(string cookieKey,object value)
        { 
            var resp = new HttpResponseMessage();

            var cookie = new CookieHeaderValue(cookieKey, value.ToString());
            cookie.Expires = DateTimeOffset.Now.AddMinutes(1);
            cookie.Domain = Request.RequestUri.Host;
            cookie.Path = "/";

            resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            return resp;
        }
        public string getCookie(string cookieKey)
        {
            string cookieValue = "";
            var coo = Request.Headers.GetCookies(cookieKey);
            if (coo.Count > 0)
            {
                var cookie = coo[0].Cookies[0];
                if (cookie != null)
                {
                    cookieValue = cookie.Value;
                }
            }
            //else
            //{
            //    IdentityModel identity = new IdentityModel();
            //     setCookies(identity);
            //}
            return cookieValue;
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
