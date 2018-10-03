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
using CCMarketoBL.CCMTManager;
namespace CCMT
{
    class CCAuthenticate : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {

            if(CCMTHelper.GetCacheValue("cc_access_token") ==null)
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
           
            return;

        }
    }
    class MTAuthenticate : AuthorizeAttribute
    {
     
        public override void OnAuthorization(HttpActionContext actionContext)
        {

            if (CCMTHelper.GetCacheValue("mt_access_token") == null)
            {
                IdentityModel identityModel = new IdentityModel();
                IdentityManager identityManager = new IdentityManager();
                var reqBody = actionContext.Request.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                if (!string.IsNullOrEmpty(reqBody))
                {
                    identityModel = JsonConvert.DeserializeObject<IdentityModel>(reqBody);
                }
                var apiResponse = identityManager.Authenticate_Marketo(identityModel);
                if (apiResponse != null)
                {
                    var expireMinutes = (Int32)Math.Round((Convert.ToDecimal(apiResponse["expires_in"])) / 60);
                    CCMTHelper.AddCache("mt_access_token", apiResponse["access_token"].ToString(), DateTimeOffset.Now.AddMinutes(expireMinutes));
                }
            }
           
            return;

        }
    }
}
