using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace CCMarketoBL
{
    public class ServiceManager
    {
        public static async Task<string> Make_APICallAync(HttpRequestMessage request)
        {
            try
            {
                using (HttpClient APIclient = new HttpClient())
                {
                    var result = APIclient.SendAsync(request).Result;
                    result.EnsureSuccessStatusCode();
                    var responseBody = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

                    return responseBody;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public string MT_GetServResponse(string url)
        {
            try
            {
                using (HttpClient APIclient = new HttpClient())
                {
                    APIclient.BaseAddress = new Uri(ConfigurationManager.AppSettings["MTApiURL"]);

                    HttpResponseMessage servResponse = APIclient.GetAsync(url).Result;

                    return servResponse.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

       
        public static StreamReader POST_APICall(string url, string requestBody, HttpWebRequest request)
        {
            try
            {
                StreamWriter wr = new StreamWriter(request.GetRequestStream());
                wr.Write(requestBody);
                wr.Flush();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream resStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                return reader;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string POST_APICall_2(string url, HttpContent content)
        {
            using (HttpClient client = new HttpClient())
            {
               // HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                
                var response = client.PostAsync(url, content).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }
        public static StreamReader GET_APICall(string url, HttpWebRequest request)
        {
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream resStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                return reader;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

}