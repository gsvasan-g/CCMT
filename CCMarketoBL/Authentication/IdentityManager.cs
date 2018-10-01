using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using CCMarketoBL.Model;
using System.Threading.Tasks;
using CCMarketoBL.CCMTManager;
using System.Text;
using System.Web;

namespace CCMarketoBL
{
    public class IdentityManager
    {

        public Dictionary<string, string> Authenticate_Marketo(IdentityModel identityModel)
        {
            try
            {
                //getCredentialsByEnterpriseID- to get clientid,client_secret
                identityModel = new IdentityModel();
                identityModel.ClientID = ConfigurationManager.AppSettings["ClientId"];
                identityModel.ClientSecret = ConfigurationManager.AppSettings["ClientSecret"];

                var resourceUrl = "/identity/oauth/token?grant_type=client_credentials&client_id=" + identityModel.ClientID + "&client_secret=" + identityModel.ClientSecret;
                string url = CCMTHelper.GetFullUrl(resourceUrl);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                StreamReader reader = ServiceManager.GET_APICall(url, request);
                String json = reader.ReadToEnd();
                //  ServiceManager serv = new ServiceManager();
                //var json=  serv.MT_GetServResponse(url);
                Dictionary<String, String> dict = JsonConvert.DeserializeObject<Dictionary<String, String>>(json);
                return dict;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public Dictionary<string, string> Authenticate_CC(CCIdentityParam ccIdentityParam)
        {
            //getCredentialsByEnterpriseID- to get clientid,client_secret

            try
            {
               
                var resourceUrl = ConfigurationManager.AppSettings["CCBaseAPIURL"] + "/api/LoginToken";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resourceUrl);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                String body = bodyBuilder(ccIdentityParam);
            
                StreamReader reader = ServiceManager.POST_APICall(resourceUrl, body, request);
                String json = reader.ReadToEnd();
                //  HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(ccIdentityParam), Encoding.UTF8, "application/x-www-form-urlencoded");
                // String json = ServiceManager.POST_APICall_2(resourceUrl, contentPost);

                Dictionary<String, String> dict = JsonConvert.DeserializeObject<Dictionary<String, String>>(json);
                return dict;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Dictionary<String, String> createIdentity(IdentityModel identityModel)
        {
            try
            {
                var identityResponse = Authenticate_Marketo(identityModel);
                if (identityResponse.ContainsKey("access_token"))
                {
                    Task.Run(() => saveCredentials(identityModel));
                }

                return identityResponse;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool saveCredentials(IdentityModel identityModel)
        {
            // save data into DB

            return true;
        }

        public IdentityModel getIdentityByID(string enterpriseID)
        {
            // get  data from DB based on provided ID
            IdentityModel identity = new IdentityModel();
            return identity;
        }
        private String bodyBuilder(CCIdentityParam ccIdentityParam)
        {
            try
            {
                var sb = new StringBuilder();
               
                sb.Append("grant_type=" + ccIdentityParam.grant_type);
                sb.Append("&username=" + ccIdentityParam.username);
                sb.Append("&password=" + ccIdentityParam.password);
                return sb.ToString();
                }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

}