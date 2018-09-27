using System;

namespace CCMarketoBL.Model
{
  public  class ProgramModel
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public string ProgramDescription { get; set; }
        public string ProgramType { get; set; }
        public string ProgramChannelName { get; set; }
        public DateTime ProgramStartDate { get; set; }
        public int ProgramCost { get; set; }

    }
    public class ProgramParam
    {
        public string name { get; set; }
        public ParentFolderParam folder { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string channel { get; set; }
        public CostParam[] costs { get; set; }

    }
   public class CostParam
    {
        public string startDate { get; set; }
        public int cost { get; set; }

    }
}
