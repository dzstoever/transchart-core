using System;

namespace TC.Domain.Entities 
{
    [Serializable]
    public class AdmissionInvProceduresId : NaturalKeyStringStringDateTime
    {
        public virtual string MRN           { get { return Key1; } set { Key1 = value; } }
        public virtual string Proced        { get { return Key2; } set { Key2 = value; } }
        public virtual DateTime AdmitDate   { get { return Key3; } set { Key3 = value; } }
    }

    public class AdmissionInvProcedures : MultiEnteredByEntity<AdmissionInvProceduresId> 
    {
        public virtual Admission Admission { get; set; }  
    }
}
