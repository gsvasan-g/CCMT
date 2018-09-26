

namespace CCMarketoBL.Model
{
   public class FolderParameter
    {
       
        public folder parent { get; set; } = new folder();
        public string name { get; set; }
        public string description { get; set; }
    }

    public class folder
    {
        public int id { get; set; }
        public string type { get; set; }
    }
}
