using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Collections;
using Newtonsoft.Json;
using System.Web;
using RestSharp;
using CCMarketoBL.Model;
using System.Configuration;
using CCMarketoBL.CCMTManager;

namespace CCMarketoBL
{

    public class ProgramManager

    {

        public String getProgramsList(string enterpriseID,string token)
        {
            try
            {
              
                    var resourceUrl = "/rest/asset/v1/programs.json?access_token=" + token;
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
        public String createNewProgram(string enterpriseID,ProgramParam progParam, string token)
        {

            try
            {
                var resourceUrl = "/rest/asset/v1/programs.json?access_token=" + token;
                    string url = CCMTManager.CCMTHelper.GetFullUrl(resourceUrl);
                    //Form Encode the data
                    String requestBody = bodyBuilder(progParam);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";

                    // request.Headers.Add(HttpRequestHeader.Authorization, "");
                    StreamReader reader = ServiceManager.POST_APICall(url, requestBody, request);
                    return reader.ReadToEnd();
               
            }
            catch (Exception ex)
            {
                CCMTHelper.logError(ex);
                return null;
            }

        }

        private String bodyBuilder(ProgramParam progParam)
        {
            try
            {
                var sb = new StringBuilder();
                var cost = (CostParam[])progParam.costs;
                var fold = (ParentFolderParam)progParam.folder;
                sb.Append("name=" + progParam.name);
                sb.Append("&costs=[{'startDate'=" + cost[0].startDate + ",'cost'=" + cost[0].cost + "}]");
                sb.Append("&folder= {'id'=" + fold.id + ",'type'=" + fold.type + "}");
                if (progParam.description != null)
                {
                    sb.Append("&description=" + progParam.description);
                }
                sb.Append("&type=" + progParam.type);
                sb.Append("&channel=" + progParam.channel);


                return sb.ToString();
                // return "name=testi1&costs=[{'startDate'=2018-01-01,'cost'=2000}]&folder={'id'=46,'type'=Folder}&description=Sample1&type=Email&channel=Email Send";
            }
            catch (Exception ex)
            {
                CCMTHelper.logError(ex);
                return null;
            }
        }
       
    }
}