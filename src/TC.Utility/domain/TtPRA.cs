using System;

namespace TC.Utility.Domain 
{
    [Serializable]
    public class TtPRAId : NaturalKeyStringStringStringDateTime
    {
        public string MRN { get { return Key1; } set { Key1 = value; } }
        public string Method { get { return Key2; } set { Key2 = value; } }
        public string SerumId { get { return Key3; } set { Key3 = value; } }
        public DateTime SerumDate { get { return Key4; } set { Key4 = value; } }
    }

    public class TtPRA 
    {
        public virtual TtPRAId Id { get; set; }

        public virtual string WGName { get; set; }
        public virtual DateTime? PRADate { get; set; }
        public virtual string Result { get; set; }
        public virtual string Specificity { get; set; }        
        public virtual string LabTech { get; set; }        
        public virtual string A { get; set; }
        public virtual string B { get; set; }
        public virtual string BW4 { get; set; }
        public virtual string BW6 { get; set; }
        public virtual string DR { get; set; }
        public virtual string DQ { get; set; }
        public virtual string CW { get; set; }
        public virtual string DR515253 { get; set; }
        public virtual string DP { get; set; }
        public virtual string Comment { get; set; }
        //public virtual bool? UNOSCertificationComplete { get; set; }

        //public virtual string Entered_By { get; set; }
        //public virtual DateTime? Entered_Date { get; set; }
        //public virtual DateTime? Entered_Time { get; set; }
        //public virtual System.Guid Tenant_ID { get; set; }

        public virtual Person Person { get; set; }
    }
}
