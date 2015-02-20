using System;
using System.Text;
using System.Collections.Generic;
using Iesi.Collections.Generic;


namespace TC.Doman {
    
    public class TtPRA {
        public virtual string WG_Name { get; set; }
        public virtual string MRN { get; set; }
        public virtual DateTime? PRA_Date { get; set; }
        public virtual string Method { get; set; }
        public virtual string Result { get; set; }
        public virtual string Specificity { get; set; }
        public virtual string Entered_By { get; set; }
        public virtual DateTime? Entered_Date { get; set; }
        public virtual string Lab_Tech { get; set; }
        public virtual string Serum_Id { get; set; }
        public virtual DateTime Serum_Date { get; set; }
        public virtual DateTime? Entered_Time { get; set; }
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
        public virtual bool? UNOS_Certification_Complete { get; set; }
        public virtual System.Guid Tenant_ID { get; set; }
    }
}
