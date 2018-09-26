using CCMarketoBL;
using CCMarketoBL.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCMarketoBL.CCMTManager
{
    class CCMTHelper
    {
        public static string GetFullUrl(string resourceUrl)
        {
            IdentityManager tokenManager = new IdentityManager();
            IdentityModel identity = new IdentityModel();
            identity.ClientID = "1e4603f3-ee9e-476c-a3f9-59c5b9c4837b";
            identity.ClientSecret = "Udmc9mUFHe1J3qUsw59qdzC58H2a6Lnh";
            var token = tokenManager.getToken(identity)["access_token"];
            String url = ConfigurationManager.AppSettings["MTBaseURL"] + resourceUrl + "?access_token=" + token;
            return url;
        }
    }
}
