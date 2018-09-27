

namespace CCMarketoBL.Model
{
   public class FolderParam
    {
       
        public object parent { get; set; } = new ParentFolderParam();
        public string name { get; set; }
        public string description { get; set; }
    }

    public class ParentFolderParam
    {
        public int id { get; set; }
        public string type { get; set; }
    }
}
