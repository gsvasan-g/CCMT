
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Collections;
using Newtonsoft.Json;
using System.Configuration;
using CCMarketoBL.Model;
using CCMarketoBL.CCMTManager;
using System.Web;

namespace CCMarketoBL
{

    public class CCMTMapper
    {
        public String getCCFields()
        {
            try
            {
                // CCIdentityParam identity = new CCIdentityParam();
                IdentityManager tokenManager = new IdentityManager();
                CCIdentityParam identity = new CCIdentityParam();
                identity.grant_type = ConfigurationManager.AppSettings["CCGrantType"];
                identity.username = ConfigurationManager.AppSettings["CCUserName"];
                identity.password = ConfigurationManager.AppSettings["CCPassword"];

                var qs = HttpUtility.ParseQueryString(string.Empty);
                var identityResponse = tokenManager.Authenticate_CC(identity);
                if (identityResponse.ContainsKey("access_token"))
                {
                    qs.Add("access_token", identityResponse["access_token"]);

                    var resourceUrl = ConfigurationManager.AppSettings["CCBaseAPIURL"] + "/api/Questions/Active" + qs.ToString();
                    string url = CCMTHelper.GetFullUrl(resourceUrl);

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    StreamReader reader = ServiceManager.GET_APICall(url, request);
                    return reader.ReadToEnd();
                }
                return string.Empty;
                // var servResponse= serv.MT_GetServResponse(url);
                // return servResponse;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public String getMarketoFields(string enterpriseID)
        {
            try
            {
                IdentityModel identity = new IdentityModel();
                IdentityManager tokenManager = new IdentityManager();
                //  ServiceManager serv = new ServiceManager();

                // var identityResponse= token.getIdentityByID(enterpriseID);
                identity.ClientID = ConfigurationManager.AppSettings["ClientId"];
                identity.ClientSecret = ConfigurationManager.AppSettings["ClientSecret"];

                var qs = HttpUtility.ParseQueryString(string.Empty);
                var identityResponse = tokenManager.Authenticate_Marketo(identity);
                if (identityResponse.ContainsKey("access_token"))
                {
                    qs.Add("access_token", identityResponse["access_token"]);

                    var resourceUrl = "/rest/v1/leads/describe.json?" + qs.ToString(); ;
                    string url = CCMTHelper.GetFullUrl(resourceUrl);

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    StreamReader reader = ServiceManager.GET_APICall(url, request);
                    return reader.ReadToEnd();
                }
                return string.Empty;
                // var servResponse= serv.MT_GetServResponse(url);
                // return servResponse;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
