
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
using System.Net.Http.Headers;

namespace CCMarketoBL
{

    public class MapperManager
    {
        public String getCCFields(string accessToken)
        {
            try
            {
               IdentityManager tokenManager = new IdentityManager();
               
                   var resourceUrl = ConfigurationManager.AppSettings["CCBaseAPIURL"] + "/api/Questions/Active";// + qs.ToString();
            
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resourceUrl);
                    request.PreAuthenticate = true;
                    request.Headers.Add("Authorization", "Bearer " + accessToken);
                    
                    StreamReader reader = ServiceManager.GET_APICall(resourceUrl, request);
                    return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public String getMarketoFields(string accessToken)
        {
            try
            {
                IdentityModel identity = new IdentityModel();
                IdentityManager tokenManager = new IdentityManager();
                    var resourceUrl = "/rest/v1/leads/describe.json?access_token" + accessToken;// qs.ToString();
                    string url = CCMTHelper.GetFullUrl(resourceUrl);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    StreamReader reader = ServiceManager.GET_APICall(url, request);
                    return reader.ReadToEnd();               
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string mapCCMTEntity(string enterpriseID,string folderId,string programId,object MTEntity,string accessToken)
        {

            try
            {
                var resourceUrl = "/rest/asset/v1/programs.json?access_token=" + accessToken;// + qs.ToString();
                    string url = CCMTManager.CCMTHelper.GetFullUrl(resourceUrl);
                    //Form Encode the data
                    String requestBody = bodyBuilder();
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";

                    // request.Headers.Add(HttpRequestHeader.Authorization, "");
                    StreamReader reader = ServiceManager.POST_APICall(url, requestBody, request);
                    return reader.ReadToEnd();
                
            }
            catch (Exception ex)
            {
                return null;
            }
          
        }

        private String bodyBuilder()
        {
            try
            {
                var sb = new StringBuilder();                
                sb.Append("name=" );
                sb.Append("&costs=[{'startDate'=" );
                sb.Append("&folder= ");                
                return sb.ToString();
                 }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
