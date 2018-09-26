using System.Web.Http;
using CCMarketoBL;
using CCMarketoBL.Model;

using Newtonsoft.Json;

namespace MarketoCanvasAPI.Controllers
{
    [RoutePrefix("CCMT/v1/Folder")]
    public class FolderController : ApiController
    {
      
        [HttpGet,Route("list/{enterpriseID}")]
        public IHttpActionResult getMTFolder(string enterpriseID)
        {
            BrowseFolders browseFldr = new BrowseFolders();
          var result =  browseFldr.getData(enterpriseID);
            return Ok(JsonConvert.DeserializeObject<dynamic>(result));
        }

        [HttpPost, Route("create")]
        public IHttpActionResult createMTFolder(FolderParameter folderParam)
        {
            CreateFolder folder = new CreateFolder();
            var result=folder.createNewFolder(folderParam);
            return Ok(JsonConvert.DeserializeObject<dynamic>(result));
          //  return Ok(new { Message = "Folder created successfully" });
        }

       
    }
}
