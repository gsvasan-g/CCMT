

namespace CCMarketoBL.Model
{
   public class CCEntityModel
    {
        public string string1 { get; set; }
        public string string2 { get; set; }
    }

    public class cc_response
    {
        public int id { get; set; }
        public cc_account Account { get; set; }
    }

    public class cc_account
    {
        public cc_KeyValues[] KeyVaues { get; set; }
    }

    public class cc_KeyValues
    {
        public string Key { get; set; }
        public string Value { get; set; }

    }


}
