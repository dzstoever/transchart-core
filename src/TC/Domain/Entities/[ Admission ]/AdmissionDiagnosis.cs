using System;

namespace TC.Domain.Entities 
{
    [Serializable]
    public class AdmissionDiagnosisId : NaturalKeyStringStringDateTime
    {
        public virtual string MRN           { get { return Key1; } set { Key1 = value; } }
        public virtual string Diagnosis     { get { return Key2; } set { Key2 = value; } }
        public virtual DateTime AdmitDate   { get { return Key3; } set { Key3 = value; } }        
    }

    public class AdmissionDiagnosis : MultiEnteredByEntity<AdmissionDiagnosisId> 
    {
        public virtual Admission Admission { get; set; }

        public virtual bool PrimaryDGNS { get; set; } 
    }
}
