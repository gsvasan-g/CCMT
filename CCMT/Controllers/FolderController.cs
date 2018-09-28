using System.Web.Http;
using CCMarketoBL;
using CCMarketoBL.Model;

using Newtonsoft.Json;
using System;

namespace MarketoCanvasAPI.Controllers
{
    [RoutePrefix("CCMT/v1/Folder")]
    public class FolderController : ApiController
    {
      
        [HttpGet,Route("list/{enterpriseID}")]
        public IHttpActionResult getMTFolder(string enterpriseID)
        {
            try
            {
                FolderManager browseFldr = new FolderManager();
                var result = browseFldr.getFolderList(enterpriseID);
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
