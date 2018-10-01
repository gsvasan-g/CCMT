using System.Web.Http;
using CCMarketoBL;
using CCMarketoBL.Model;

using Newtonsoft.Json;
using System;
using System.Net.Http;
using CCMT;

namespace CCMarketoAPI.Controllers
{
    [RoutePrefix("CCMT/v1/Folder")]
    
    public class FolderController : ApiController
    {      
        [HttpGet,Route("list/{enterpriseID}")]
        [MTAuthenticate]
        public IHttpActionResult getMTFolder(string enterpriseID)
        {
            try
            {
                string accessToken = "";
                var coo = Request.Headers.GetCookies("mt_access_token");
                if (coo.Count > 0)
                {
                    var cookie = coo[0].Cookies[0];
                    if (cookie != null)
                    {
                        accessToken = cookie.Value;
                    }
                }

                FolderManager browseFldr = new FolderManager();
                var result = browseFldr.getFolderList(accessToken);
                return Ok(JsonConvert.DeserializeObject<dynamic>(result));
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPost, Route("create")]
        public IHttpActionResult createMTFolder(FolderParam folderParam)
        {
            try
            {
            
                FolderManager folder = new FolderManager();
                var result = folder.createNewFolder(folderParam);
                return Ok(JsonConvert.DeserializeObject<dynamic>(result));
                //  return Ok(new { Message = "Folder created successfully" });
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

       
    }
}
