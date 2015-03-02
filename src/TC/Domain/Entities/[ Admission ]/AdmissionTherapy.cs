using System;

namespace TC.Domain.Entities 
{
    [Serializable]
    public class AdmissionTherapyId : NaturalKeyStringStringDateTime
    {
        public virtual string MRN           { get { return Key1; } set { Key1 = value; } }
        public virtual string Therapy       { get { return Key2; } set { Key2 = value; } }
        public virtual DateTime AdmitDate   { get { return Key3; } set { Key3 = value; } }
    }

    public class AdmissionTherapy : MultiEnteredByEntity<AdmissionTherapyId> 
    {
        public virtual Admission Admission { get; set; }      
    }
}
