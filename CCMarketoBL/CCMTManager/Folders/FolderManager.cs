
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
        public String getFolderList(string enterpriseID,string token)
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
                CCMTHelper.logError(ex);
                return null;
            }
        }
        public String createNewFolder(string enterpriseID,FolderParam folderParam, string token)
        {
            try
            {
                    var resourceUrl = "/rest/asset/v1/folders.json?access_token=" + token;
                    string url = CCMTHelper.GetFullUrl(resourceUrl);
                    String requestBody = bodyBuilder(folderParam);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    StreamReader reader = ServiceManager.POST_APICall(url, requestBody, request);
                    return reader.ReadToEnd();               
            }
            catch (Exception ex)
            {
                CCMTHelper.logError(ex);
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
