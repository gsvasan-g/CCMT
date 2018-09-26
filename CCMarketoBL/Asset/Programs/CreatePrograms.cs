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


namespace CCMarketoBL
{

    public partial class CreateProgram
    {
        private String host = Utilities.Host;//  "CHANGE ME"; //host of your marketo instance, https://AAA-BBB-CCC.mktorest.com
        private String clientId = Utilities.ClientId;// "CHANGE ME"; //clientId from admin > Launchpoint
        private String clientSecret = Utilities.ClientSecret;//"CHANGE ME"; //clientSecret from admin > Launchpoint

        public int id;//id of source program
        public string name;//name of new program
        public string channel;
        public Dictionary<string, dynamic> folder;//parent folder with id and type
        public String programDescription;

        public String type;//type of Folder to retrieve, Program or Folder
        public Dictionary<string, dynamic> root;//root folder with two members, id, and type(Folder or Program)
        public String workSpace;//optional workspace filter

        public String postData()
        {
            int FolderId = 111;// GetFolderId();
            //Assemble the URL
            String url = host + "/rest/asset/v1/programs.json"; // host + "rest/asset/v1/program/" + id + "/clone.json";  
            var postdata = new Dictionary<string, string>();
            postdata.Add("name", name);
            postdata.Add("folder", "{\"id\":" + FolderId + ",\"type\":\"Folder\"}");
            postdata.Add("type", type);
            postdata.Add("description", programDescription);
            postdata.Add("channel", channel);
            postdata.Add("costs", "[{\"startDate\":\"2015-01-01\",\"cost\":2000}]");
            return RestClientPost("/rest/asset/v1/programs.json", postdata, getToken());
        }

        private string RestClientPost(string Url, Dictionary<string, string> postdata, string token)
        {
            var client = new RestClient(host);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest($"{Url}?access_token={token}", Method.POST);
            foreach (var item in postdata)
            {
                request.AddParameter(item.Key, item.Value); // adds to POST or URL querystring based on Method

            }
            //request.AddBody(postdata);
            //request.AddUrlSegment("access_token", token); // replaces matching token in request.Resource

            // easily add HTTP Headers
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            // add files to upload (works with compatible verbs)
            //request.AddFile(path);

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            return content;
            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            //RestResponse<Person> response2 = client.Execute<Person>(request);
            //var name = response2.Data.Name;

            // easy async support
            //client.ExecuteAsync(request, response =>
            //{
            //    Console.WriteLine(response.Content);
            //});

            //// async with deserialization
            //var asyncHandle = client.ExecuteAsync<Person>(request, response =>
            //{
            //    Console.WriteLine(response.Data.Name);
            //});

            //    // abort the request on demand
            //    asyncHandle.Abort();
        }
       
     
        private String bodyBuilder()
        {
            var sb = new StringBuilder();
            //sb.Append("id=" + id);
            sb.Append("&name=" + name);
            sb.Append("&type=" + type);
            sb.Append("&channel=" + channel);
            // sb.Append("&programDescription=" + programDescription);            
            sb.Append("&folder=" + JsonConvert.SerializeObject(folder).ToString());
            if (programDescription != null)
            {
                sb.Append("&description=" + programDescription);
            }
            return sb.ToString();
        }
        private String getToken()
        {
            String url = host + "/identity/oauth/token?grant_type=client_credentials&client_id=" + clientId + "&client_secret=" + clientSecret;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            String json = reader.ReadToEnd();
            //Dictionary<String, Object> dict = JavaScriptSerializer.DeserializeObject(reader.ReadToEnd);
            Dictionary<String, String> dict = JsonConvert.DeserializeObject<Dictionary<String, String>>(json);
            return dict["access_token"];
        }
    }
}