using System.Web.Http;
using CCMarketoBL;
using CCMarketoBL.Model;

using Newtonsoft.Json;
using System;
using System.Net.Http;
using CCMT;
using CCMarketoBL.CCMTManager;

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
                if (CCMTHelper.GetCacheValue("mt_access_token") != null)
                {
                    accessToken = CCMTHelper.GetCacheValue("mt_access_token").ToString();

                }

                FolderManager browseFldr = new FolderManager();
                var result = browseFldr.getFolderList(enterpriseID,accessToken);
                return Ok(JsonConvert.DeserializeObject<dynamic>(result));
            }
            catch (Exception ex)
            {
                //log exception 
                CCMTHelper.logError(ex);
                return InternalServerError();
            }
        }

        [HttpPost, Route("create/{enterpriseID}")]
        [MTAuthenticate]
        public IHttpActionResult createMTFolder(string enterpriseID,FolderParam folderParam)
        {
            try
            {
                string accessToken = "";
                if (CCMTHelper.GetCacheValue("mt_access_token") != null)
                {
                    accessToken = CCMTHelper.GetCacheValue("mt_access_token").ToString();

                }

                FolderManager folder = new FolderManager();
                var result = folder.createNewFolder(enterpriseID,folderParam, accessToken);
                return Ok(JsonConvert.DeserializeObject<dynamic>(result));
                //  return Ok(new { Message = "Folder created successfully" });
            }
            catch (Exception ex)
            {
                //log exception 
                CCMTHelper.logError(ex);
                return InternalServerError();
            }
        }

       
    }
}
