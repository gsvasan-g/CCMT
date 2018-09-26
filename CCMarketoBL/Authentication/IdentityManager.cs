using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using CCMarketoBL.Model;
using System.Threading.Tasks;

namespace CCMarketoBL
{
    public class IdentityManager
    {

        private String host = ConfigurationManager.AppSettings["MTBaseURL"].ToString();// Utilities.Host;
        private String clientId = ConfigurationManager.AppSettings["ClientId"].ToString();//Utilities.ClientId;
        private String clientSecret = ConfigurationManager.AppSettings["ClientSecret"].ToString();//Utilities.ClientSecret;

        public Dictionary<string, string> getToken(IdentityModel identityModel)
        {
            //getCredentialsByEnterpriseID- to get clientid,client_secret
            identityModel = new IdentityModel();
            identityModel.ClientID = clientId;
            identityModel.ClientSecret = clientSecret;
            String url = host + "/identity/oauth/token?grant_type=client_credentials&client_id=" + identityModel.ClientID + "&client_secret=" + identityModel.ClientSecret;
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.ContentType = "application/json";
            //var res = request.GetResponse();
            //HttpWebResponse response = (HttpWebResponse)res;
            //Stream resStream = response.GetResponseStream();
            //StreamReader reader = new StreamReader(resStream);
            //String json = reader.ReadToEnd();

            ServiceManager serv = new ServiceManager();
          var json=  serv.MT_GetServResponse(url);
            Dictionary<String, String> dict = JsonConvert.DeserializeObject<Dictionary<String, String>>(json);
            return dict;
        }
        public Dictionary<String, String> createIdentity(IdentityModel identityModel)
        {
             var apiResponse = getToken(identityModel);
            if (apiResponse.ContainsKey("access_token"))
            {
                Task.Run(() => saveCredentials(identityModel));                              
            }
           
            return apiResponse;
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
    }
  
}