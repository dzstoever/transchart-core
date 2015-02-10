using System;
using System.Collections.Generic;


namespace TC.Domain 
{
    [Serializable]
    public class AdmissionCancelledId : NaturalKeyStringDateTime //: AdmissionId { }
    {
        public string MRN { get { return Key1; } set { Key1 = value; } }
        public DateTime AdmitDate { get { return Key2; } set { Key2 = value; } }
    }

    public class AdmissionCancelled : MultiEnteredByEntity<AdmissionCancelledId>
    {
        public virtual Admission Admission { get; set; }

        public virtual DateTime? DischDate { get; set; }
        public virtual bool OsuAdmit { get; set; }
        public virtual string AdmitCanceled { get; set; }
    }
}
