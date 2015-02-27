using System;
using System.Collections.Generic;

namespace TC.Doman 
{
    [Serializable]
    public class TtCrossMatchId : NaturalKeyStringStringStringStringStringDateTime
    {
        public string UNOSID { get { return Key1; } set { Key1 = value; } }
        public string MRN { get { return Key2; } set { Key2 = value; } }
        public string Method { get { return Key3; } set { Key3 = value; } }
        public string SerumId { get { return Key4; } set { Key4 = value; } }
        public string CellType { get { return Key5; } set { Key5 = value; } }
        public DateTime LabDate { get { return Key6; } set { Key6 = value; } }
    }
    
    public class TtCrossMatch     
    {
        public virtual TtCrossMatchId Id { get; set; }

        public virtual string Result { get; set; }
        public virtual string MCS { get; set; }        
        public virtual string LabTech { get; set; }        
        public virtual string Comments { get; set; }
        public virtual DateTime? TestDate { get; set; }
        public virtual string TargetCellSource { get; set; }
        public virtual string Titer { get; set; }
        public virtual string ChannelShift { get; set; }
        //public virtual int ID { get; set; }

        //public virtual string Entered_By { get; set; }
        //public virtual DateTime? Entered_Date { get; set; }
        //public virtual DateTime? Entered_Time { get; set; }
        //public virtual System.Guid Tenant_ID { get; set; }

        public virtual Person Person { get; set; }
    }
}
