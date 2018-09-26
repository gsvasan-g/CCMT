using Newtonsoft.Json;
using System.Collections.Generic;


namespace CCMarketoModels
{
    public class Mappers
    {
        public static List<MarketoFolder> ToMarketoFolders(string content)
        {
           var results = JsonConvert.DeserializeObject<dynamic>(content).result;
            List<MarketoFolder> objLMF = new List<MarketoFolder>();
            foreach (var item in results)
            {
                var objMF = new MarketoFolder
                {
                    id = item.id,
                    name = item.name,
                    path = item.path
                };
                objLMF.Add(objMF);
            }
            return objLMF;
        }
    }
}
