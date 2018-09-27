

namespace CCMarketoBL.Model
{
   public class IdentityModel
    {
        public string EnterpriseID { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
    }
   public class CCIdentityParam
    {
        public string grant_type { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
