using System;

namespace TC.Maps.Entitites 
{
    
    public class TtCrossMatch {
        public virtual string UNOSID { get; set; }
        public virtual string MRN { get; set; }
        public virtual DateTime Lab_Date { get; set; }
        public virtual string Method { get; set; }
        public virtual string Result { get; set; }
        public virtual string MCS { get; set; }
        public virtual string Entered_By { get; set; }
        public virtual DateTime? Entered_Date { get; set; }
        public virtual string Lab_Tech { get; set; }
        public virtual string Serum_Id { get; set; }
        public virtual DateTime? Entered_Time { get; set; }
        public virtual string Comments { get; set; }
        public virtual DateTime? Test_Date { get; set; }
        public virtual string Cell_Type { get; set; }
        public virtual string Target_Cell_Source { get; set; }
        public virtual string Titer { get; set; }
        public virtual string Channel_Shift { get; set; }
        public virtual int ID { get; set; }
        public virtual Guid Tenant_ID { get; set; }
    }
}
