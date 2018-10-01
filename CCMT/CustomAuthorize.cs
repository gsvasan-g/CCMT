using System;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using CCMarketoBL;
using CCMarketoBL.Model;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace CCMT
{
    class CCAuthenticate : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var reqBody = actionContext.Request.Content.ReadAsStringAsync().GetAwaiter().GetResult();


            var coo = actionContext.Request.Headers.GetCookies("cc_access_token");
            if (coo.Count <=0)
            { 
                CCIdentityParam identityModel = new CCIdentityParam();
              
                var resp = new HttpResponseMessage();
                IdentityManager identityManager = new IdentityManager();
                identityModel.grant_type = ConfigurationManager.AppSettings["CCGrantType"];
                identityModel.username = ConfigurationManager.AppSettings["CCUserName"];
                identityModel.password = ConfigurationManager.AppSettings["CCPassword"];
                var apiResponse = identityManager.Authenticate_CC(identityModel);

                var cookie = new CookieHeaderValue("cc_access_token", apiResponse["access_token"].ToString());
                var expireMinutes = (Int32)Math.Round((Convert.ToDecimal(apiResponse["expires_in"])) / 60);
                cookie.Expires = DateTimeOffset.Now.AddSeconds(expireMinutes);
                cookie.Domain = actionContext.Request.RequestUri.Host;
                cookie.Path = "/";

                resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
                actionContext.Response = resp;
               
            }
            return;

        }
    }
    class MTAuthenticate : AuthorizeAttribute
    {
     
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var reqBody = actionContext.Request.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            

            var coo = actionContext.Request.Headers.GetCookies("mt_access_token");
            if (coo.Count <= 0)
            {
                IdentityModel identityModel = new IdentityModel();
                if (!string.IsNullOrEmpty(reqBody))
                {
                    identityModel = JsonConvert.DeserializeObject<IdentityModel>(reqBody);
                }
                var resp = new HttpResponseMessage();
                IdentityManager identity = new IdentityManager();

                var apiResponse = identity.Authenticate_Marketo(identityModel);

                var cookie = new CookieHeaderValue("mt_access_token", apiResponse["access_token"].ToString());
                var expireMinutes = (Int32)Math.Round((Convert.ToDecimal(apiResponse["expires_in"])) / 60);
                cookie.Expires = DateTimeOffset.Now.AddSeconds(expireMinutes);
                cookie.Domain = actionContext.Request.RequestUri.Host;
                cookie.Path = "/";

                resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
                actionContext.Response = resp;
               
            }
            return;

        }
    }
}
