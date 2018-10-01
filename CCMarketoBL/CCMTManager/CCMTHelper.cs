using CCMarketoBL;
using CCMarketoBL.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

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
