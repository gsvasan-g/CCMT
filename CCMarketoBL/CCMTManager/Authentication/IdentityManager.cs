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
            {var resourceUrl = "/identity/oauth/token?grant_type=client_credentials&client_id=" + identityModel.ClientID + "&client_secret=" + identityModel.ClientSecret;
                string url = CCMTHelper.GetFullUrl(resourceUrl);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                StreamReader reader = ServiceManager.GET_APICall(url, request);
                String json = reader.ReadToEnd();
                
                Dictionary<String, String> dict = JsonConvert.DeserializeObject<Dictionary<String, String>>(json);
                return dict;
            }
            catch (Exception ex)
            {
                CCMTHelper.logError(ex);
                return null;
            }
        }
        public Dictionary<string, string> Authenticate_CC(CCIdentityParam ccIdentityParam)
        {
           
            try
            {               
                var resourceUrl = ConfigurationManager.AppSettings["CCBaseAPIURL"] + "/api/LoginToken";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resourceUrl);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                String body = bodyBuilder(ccIdentityParam);
            
                StreamReader reader = ServiceManager.POST_APICall(resourceUrl, body, request);
                String json = reader.ReadToEnd();
                
                Dictionary<String, String> dict = JsonConvert.DeserializeObject<Dictionary<String, String>>(json);
                return dict;
            }
            catch (Exception ex)
            {
                CCMTHelper.logError(ex);
                return null;
            }
        }
      

        public object saveCredentials(IdentityModel identityModel,string token)
        {
            try
            {
                // token = ConfigurationManager.AppSettings["token"];
                var resourceUrl = ConfigurationManager.AppSettings["CCBaseAPIURL"] + "/api/Settings/Update";

                var ccSetting = getCCSettings(token);
                var apiObject = JsonConvert.DeserializeObject<dynamic>(ccSetting);
                apiObject.account.keyValues[0].key = "Integration.Marketo";
                apiObject.account.keyValues[0].value = JsonConvert.SerializeObject(identityModel);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resourceUrl);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.PreAuthenticate = true;
                request.Headers.Add("Authorization", "Bearer " + token);
                StreamReader reader = ServiceManager.POST_APICall(resourceUrl, apiObject.ToString(), request);
                String json = reader.ReadToEnd();
                // Dictionary<String, String> dict = JsonConvert.DeserializeObject<Dictionary<String, String>>(json);


                return apiObject;
            }
            catch (Exception ex)
            {
                CCMTHelper.logError(ex);
                return null;
            }
        }

        public object getIdentityByID(string enterpriseID,string token)
        {
            throw new NotImplementedException();
        }

        public string getCCSettings(string accessToken)
        {
            try
            {

                IdentityModel identity = new IdentityModel();
                var resourceUrl = ConfigurationManager.AppSettings["CCBaseAPIURL"] + "/api/Settings";


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resourceUrl);
                request.PreAuthenticate = true;
                request.Headers.Add("Authorization", "Bearer " + accessToken);
                StreamReader reader = ServiceManager.GET_APICall(resourceUrl, request);
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                CCMTHelper.logError(ex);
                return null;
            }
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
                CCMTHelper.logError(ex);
                return null;
            }
        }
    }

}