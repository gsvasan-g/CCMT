

namespace CCMarketoBL.Model
{
   
    public class CCMTMappingModel
    {
        public string id { get; set; }
        public string user { get; set; }
        public string key { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public QuestionIdMapping questionIdMappings { get; set; }
    }

    public class QuestionIdMapping
    {
        public string id { get; set; }
    }
}
