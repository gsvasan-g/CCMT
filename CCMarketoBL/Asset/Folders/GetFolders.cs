
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using CCMarketoBL.Model;
using System.Web;

namespace CCMarketoBL
{
    public class BrowseFolders
    {
        public String getData(string enterpriseID)
        {
            IdentityModel identity = new IdentityModel();
            IdentityManager token = new IdentityManager();
            ServiceManager serv = new ServiceManager();
            var qs = HttpUtility.ParseQueryString(string.Empty);
            // var identityResponse= token.getIdentityByID(enterpriseID);
            identity.ClientID = "1e4603f3-ee9e-476c-a3f9-59c5b9c4837b";
            identity.ClientSecret = "Udmc9mUFHe1J3qUsw59qdzC58H2a6Lnh";
            qs.Add("access_token",token.getToken(identity)["access_token"]);            
            String url = ConfigurationManager.AppSettings["MTBaseURL"] + "/rest/asset/v1/folders.json?" + qs.ToString();
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);          
            //request.Accept = "application/json";
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //Stream resStream = response.GetResponseStream();
            //StreamReader reader = new StreamReader(resStream);
           var servResponse= serv.MT_GetServResponse(url);
            return servResponse;
        }               
    }
}
