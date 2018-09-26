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
}
