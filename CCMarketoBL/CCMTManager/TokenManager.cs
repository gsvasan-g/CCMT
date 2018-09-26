using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace CCMT.Common
{
    public class TokenManager
    {
       
        private String host = "https://596-oey-331.mktorest.com"; //host of your marketo instance,
        private String clientId = "5eded087-ce6d-470a-ab75-a7e241ad3d47";//clientId from admin > Launchpoint
        private String clientSecret = "p21jcnHEgJzoEWVNHnfAXxlejzqYKGtr"; //clientSecret from admin > Launchpoint


        private String bodyBuilder()
        {
            var body = System.Web.HttpUtility.ParseQueryString(string.Empty);
            body.Add("access_token", getAccessToken());
           // body.Add("folderType", folderType);
            //body.Add("type", type);
            //body.Add("name", name);
            //body.Add("value", value);
            return body.ToString();
        }

        private String getAccessToken()
        {
            String url = host + "/identity/oauth/token?grant_type=client_credentials&client_id=" + clientId + "&client_secret=" + clientSecret;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            String json = reader.ReadToEnd();
            //Dictionary<String, Object> dict = JavaScriptSerializer.DeserializeObject(reader.ReadToEnd);
            Dictionary<String, String> dict = JsonConvert.DeserializeObject<Dictionary<String, String>>(json);
            return dict["access_token"];
        }

    }
  
}