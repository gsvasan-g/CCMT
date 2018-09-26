using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace CCMarketoBL
{
    public class ServiceManager
    {
        public static HttpClient APIclient;
        public string MT_GetServResponse(string url)
        {
            try
            {
                using (HttpClient APIclient = new HttpClient())
                {
                    APIclient.BaseAddress = new Uri(ConfigurationManager.AppSettings["MTApiURL"]);

                    HttpResponseMessage servResponse =  APIclient.GetAsync(url).Result;

                    return servResponse.Content.ReadAsStringAsync().Result;
                }
            }
            catch(Exception ex)
            {

                return null;
            }
        }
        public string CC_GetServResponse(string url)
        {
            try
            {

                using (HttpClient APIclient = new HttpClient())
                {
                    APIclient.BaseAddress = new Uri(ConfigurationManager.AppSettings["MTApiURL"]);
                    HttpResponseMessage servResponse =  APIclient.GetAsync(url).Result;
                    return servResponse.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public string MT_PostRequest(string url, object body)
        {
            try
            {
                using (HttpClient APIclient = new HttpClient())
                {
                    APIclient.BaseAddress = new Uri(ConfigurationManager.AppSettings["MTApiURL"]);

                    HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

                    // string queryString = "login/authenticate/" + username + "/" + password;
                    var response = APIclient.PostAsync(url, contentPost).Result;

                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {

                return null;
            }

        }
        public string CC_PostRequest(string url, object body)
        {
            try
            {
                using (HttpClient APIclient = new HttpClient())
                {
                    APIclient.BaseAddress = new Uri(ConfigurationManager.AppSettings["MTApiURL"]);

                    HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

                    // string queryString = "login/authenticate/" + username + "/" + password;
                    var response = APIclient.PostAsync(url, contentPost).Result;

                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {

                return null;
            }

        }
       
        public string MT_UpdateRequest(string url, object body)
        {
            try
            {
                using (HttpClient APIclient = new HttpClient())
                {
                    APIclient.BaseAddress = new Uri(ConfigurationManager.AppSettings["SOCServiceURL"]);

                    HttpContent contentUpdate = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

                    // string queryString = "login/authenticate/" + username + "/" + password;
                    var response = APIclient.PutAsync(url, contentUpdate).Result;

                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {

                return null;
            }

        }
        public string CC_UpdateRequest(string url, object body)
        {
            try
            {
                using (HttpClient APIclient = new HttpClient())
                {
                    APIclient.BaseAddress = new Uri(ConfigurationManager.AppSettings["SOCServiceURL"]);

                    HttpContent contentUpdate = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

                    // string queryString = "login/authenticate/" + username + "/" + password;
                    var response = APIclient.PutAsync(url, contentUpdate).Result;

                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {

                return null;
            }

        }


        public static StreamReader POST_APICall(string url, string requestBody, HttpWebRequest request)
        {

            StreamWriter wr = new StreamWriter(request.GetRequestStream());
            wr.Write(requestBody);
            wr.Flush();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            return reader;
        }
        public static StreamReader GET_APICall(string url, HttpWebRequest request)
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            return reader;
        }
    }
  
}