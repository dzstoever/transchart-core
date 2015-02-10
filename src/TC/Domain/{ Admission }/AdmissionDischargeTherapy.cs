using System;
using System.Text;
using System.Collections.Generic;

namespace TC.Domain 
{
    [Serializable]
    public class AdmissionDischargeTherapyId : NaturalKeyStringStringDateTime
    {
        public virtual string MRN           { get { return Key1; } set { Key1 = value; } }
        public virtual string Therapy       { get { return Key2; } set { Key2 = value; } }
        public virtual DateTime AdmitDate   { get { return Key3; } set { Key3 = value; } }
    }

    public class AdmissionDischargeTherapy : MultiEnteredByEntity<AdmissionDischargeTherapyId>
    {
        public virtual Admission Admission { get; set; }
    }
}
