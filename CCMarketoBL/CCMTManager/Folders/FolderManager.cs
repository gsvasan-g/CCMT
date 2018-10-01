
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

    public class FolderManager

    {
        public String getFolderList(string token)
        {
            try
            {
                    var resourceUrl = "/rest/asset/v1/folders.json?access_token=" + token;
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
        public String createNewFolder(FolderParam folderParam)
        {
            try
            {
                IdentityModel identity = new IdentityModel();
                IdentityManager tokenManager = new IdentityManager();

                // var identityResponse= token.getIdentityByID(enterpriseID);
                identity.ClientID = ConfigurationManager.AppSettings["ClientId"];
                identity.ClientSecret = ConfigurationManager.AppSettings["ClientSecret"];

                var qs = HttpUtility.ParseQueryString(string.Empty);
                var identityResponse = tokenManager.Authenticate_Marketo(identity);
                if (identityResponse.ContainsKey("access_token"))
                {
                    qs.Add("access_token", identityResponse["access_token"]);
                    var resourceUrl = "/rest/asset/v1/folders.json?" + qs.ToString();
                    string url = CCMTHelper.GetFullUrl(resourceUrl);
                    //Form Encode the data
                    String requestBody = bodyBuilder(folderParam);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";

                    // request.Headers.Add(HttpRequestHeader.Authorization, "");
                    StreamReader reader = ServiceManager.POST_APICall(url, requestBody, request);
                    return reader.ReadToEnd();
                }
                return string.Empty;
                //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                //var result = ServiceManager.Make_APICall(request).GetAwaiter().GetResult();
                //return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public String getFolderList1(string enterpriseID)
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

                    var resourceUrl = "/rest/asset/v1/folders.json?" + qs.ToString(); ;
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
        public String createNewFolder1(FolderParam folderParam)
        {
            try
            {
                IdentityModel identity = new IdentityModel();
                IdentityManager tokenManager = new IdentityManager();

                // var identityResponse= token.getIdentityByID(enterpriseID);
                identity.ClientID = ConfigurationManager.AppSettings["ClientId"];
                identity.ClientSecret = ConfigurationManager.AppSettings["ClientSecret"];

                var qs = HttpUtility.ParseQueryString(string.Empty);
                var identityResponse = tokenManager.Authenticate_Marketo(identity);
                if (identityResponse.ContainsKey("access_token"))
                {
                    qs.Add("access_token", identityResponse["access_token"]);
                    var resourceUrl = "/rest/asset/v1/folders.json?" + qs.ToString();
                    string url = CCMTHelper.GetFullUrl(resourceUrl);
                    //Form Encode the data
                    String requestBody = bodyBuilder(folderParam);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";

                    // request.Headers.Add(HttpRequestHeader.Authorization, "");
                    StreamReader reader = ServiceManager.POST_APICall(url, requestBody, request);
                    return reader.ReadToEnd();
                }
                return string.Empty;
                //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                //var result = ServiceManager.Make_APICall(request).GetAwaiter().GetResult();
                //return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private String bodyBuilder(FolderParam folderParam)
        {
            try
            {
                var sb = new StringBuilder();
                var foldr = (ParentFolderParam)folderParam.parent;
                sb.Append("name=" + folderParam.name);
                sb.Append("&parent={id=" + foldr.id + ",'type'=" + foldr.type + "}");
                if (folderParam.description != null)
                {
                    sb.Append("&description=" + folderParam.description);
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
