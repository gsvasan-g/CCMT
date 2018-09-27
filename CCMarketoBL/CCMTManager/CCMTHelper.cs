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
            //IdentityManager tokenManager = new IdentityManager();
            //IdentityModel identity = new IdentityModel();
            //identity.ClientID = ConfigurationManager.AppSettings["ClientId"];
            //identity.ClientSecret = ConfigurationManager.AppSettings["ClientSecret"];
            //var token = tokenManager.authenticate(identity)["access_token"];
            String url = ConfigurationManager.AppSettings["MTBaseURL"] + resourceUrl;
            return url;
        }
    }
}
