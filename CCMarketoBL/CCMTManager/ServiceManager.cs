using CCMarketoBL.CCMTManager;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace CCMarketoBL
{
    public class ServiceManager
    {
       
        public static StreamReader POST_APICall(string url, string requestBody, HttpWebRequest request)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

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
                CCMTHelper.logError(ex);
                return null;
            }
        }
       
        public static StreamReader GET_APICall(string url, HttpWebRequest request)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream resStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                return reader;
            }
            catch (Exception ex)
            {
                CCMTHelper.logError(ex);
                return null;
            }
        }

       
    }

}