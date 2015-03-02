using System;

namespace TC.Domain.Components
{
    public abstract class Diagnosis
    {
        public virtual DateTime? PrimaryDiagnosisDate { get; set; }// note: STRING IN DB
        public virtual DateTime? PrimaryDiagnosisDate2 { get; set; }
        public virtual string PrimaryDiagnosis { get; set; }
        public virtual string PrimaryDiagnosis2 { get; set; }

        public virtual DateTime? SecondaryDiagnosisDate { get; set; }// note: STRING IN DB
        public virtual DateTime? SecondaryDiagnosisDate2 { get; set; }
        public virtual string SecondaryDiagnosis { get; set; }
        public virtual string SecondaryDiagnosis2 { get; set; }

        public virtual DateTime? OtherDiagnosisDate { get; set; }
        public virtual DateTime? OtherDiagnosisDate2 { get; set; }
        public virtual string OtherDiagnosis { get; set; }
        public virtual string OtherDiagnosis2 { get; set; }

        public virtual string DiagnosisComments { get; set; }
        public virtual string DiagnosisComments2 { get; set; }
    }
}
