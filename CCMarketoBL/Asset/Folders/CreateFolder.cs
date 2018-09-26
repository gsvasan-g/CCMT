
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

namespace CCMarketoBL
{

    public class CreateFolder
    {
        public String createNewFolder(FolderParameter folderParam)
        {
            var resourceUrl = "/rest/asset/v1/folders.json";
            string url =CCMTHelper.GetFullUrl(resourceUrl);
            //Form Encode the data
            String requestBody = bodyBuilder(folderParam);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            StreamReader reader = ServiceManager.POST_APICall(url, requestBody, request);
            return reader.ReadToEnd();

            //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
            //var result = ServiceManager.Make_APICall(request).GetAwaiter().GetResult();
            //return result;
        }
        
        private String bodyBuilder(FolderParameter folderParam)
        {
            var sb = new StringBuilder();
            sb.Append("&name=" + folderParam.name);           
            sb.Append("&parent=" + JsonConvert.SerializeObject(folderParam.parent));
            if (folderParam.description != null)
            {
                sb.Append("&description=" +folderParam.description);
            }
            return sb.ToString();
        }

    }
}
