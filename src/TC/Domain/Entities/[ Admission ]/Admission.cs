using System;
using System.Collections.Generic;

namespace TC.Domain.Entities 
{
    [Serializable]
    public class AdmissionId : NaturalKeyStringDateTime
    {
        public string MRN { get { return Key1; } set { Key1 = value; } }
        public DateTime AdmitDate { get { return Key2; } set { Key2 = value; } }
    }

    public class Admission : MultiEnteredByEntity<AdmissionId>
    {
        public virtual DateTime? AdmitTime { get; set; }
        public virtual DateTime? DischDate { get; set; }
        public virtual DateTime? DischTime { get; set; }
        public virtual string Room { get; set; }
        public virtual string Service { get; set; }
        public virtual string CenterName { get; set; }
        public virtual string Account { get; set; }
        public virtual string AccountNumber { get; set; }
        public virtual string TxRelated { get; set; }                
        public virtual bool? ICU { get; set; }
        public virtual bool OsuAdmit { get; set; }           // should be nullable
        public virtual bool UnplannedReturnToOR { get; set; }// should be nullable
        public virtual int? AdmitNum { get; set; }           // ?
        
        // one to one associations
        public virtual AdmissionCancelled           Cancelled { get; set; }
        public virtual AdmissionDischargeCancelled  DischargeCancelled { get; set; }
        
        // one to many associations
        public virtual ISet<AdmissionReasons>          Reasons { get; set; }
        public virtual ISet<AdmissionBedHistory>       BedHistory { get; set; }
        public virtual ISet<AdmissionDiagnosis>        Diagnosis { get; set; }
        public virtual ISet<AdmissionInvProcedures>    InvProcedures { get; set; }
        public virtual ISet<AdmissionTherapy>          Therapy { get; set; }
        public virtual ISet<AdmissionDischargeTherapy> DischargeTherapy { get; set; }
        public virtual ISet<AdmissionDischargeTo>      DischargeTo { get; set; } // one to one?
        public virtual ISet<AdmissionFollowUp>         FollowUp { get; set; }

        
        public virtual void AddReason(AdmissionReasons item)
        {
            if (Reasons == null) Reasons = new HashSet<AdmissionReasons>();
            item.Admission = this;
            Reasons.Add(item);
        }
        public virtual void RemoveReason(AdmissionReasons item)
        {
            if (Reasons == null || !Reasons.Contains(item)) return;
            item.Admission = null;
            Reasons.Remove(item);
        }
        public virtual void AddBedHistory(AdmissionBedHistory item)
        {
            if (BedHistory == null) BedHistory = new HashSet<AdmissionBedHistory>();
            item.Admission = this;
            BedHistory.Add(item);
        }
        public virtual void RemoveBedHistory(AdmissionBedHistory item)
        {
            if (BedHistory == null || !BedHistory.Contains(item)) return;
            item.Admission = null;
            BedHistory.Remove(item);
        }
        public virtual void AddDiagnosis(AdmissionDiagnosis item)
        {
            if (Diagnosis == null) Diagnosis = new HashSet<AdmissionDiagnosis>();
            item.Admission = this;
            Diagnosis.Add(item);
        }
        public virtual void RemoveDiagnosis(AdmissionDiagnosis item)
        {
            if (Diagnosis == null || !Diagnosis.Contains(item)) return;
            item.Admission = null;
            Diagnosis.Remove(item);
        }
        public virtual void AddInvProcedures(AdmissionInvProcedures item)
        {
            if (InvProcedures == null) InvProcedures = new HashSet<AdmissionInvProcedures>();
            item.Admission = this;
            InvProcedures.Add(item);
        }
        public virtual void RemoveInvProcedures(AdmissionInvProcedures item)
        {
            if (InvProcedures == null || !InvProcedures.Contains(item)) return;
            item.Admission = null;
            InvProcedures.Remove(item);
        }
        public virtual void AddTherapy(AdmissionTherapy item)
        {
            if (Therapy == null) Therapy = new HashSet<AdmissionTherapy>();
            item.Admission = this;
            Therapy.Add(item);
        }
        public virtual void RemoveTherapy(AdmissionTherapy item)
        {
            if (Therapy == null || !Therapy.Contains(item)) return;
            item.Admission = null;
            Therapy.Remove(item);
        }
        public virtual void AddDischargeTherapy(AdmissionDischargeTherapy item)
        {
            if (DischargeTherapy == null) DischargeTherapy = new HashSet<AdmissionDischargeTherapy>();
            item.Admission = this;
            DischargeTherapy.Add(item);
        }
        public virtual void RemoveDischargeTherapy(AdmissionDischargeTherapy item)
        {
            if (DischargeTherapy == null || !DischargeTherapy.Contains(item)) return;
            item.Admission = null;
            DischargeTherapy.Remove(item);
        }
        public virtual void AddDischargeTo(AdmissionDischargeTo item)
        {
            if (DischargeTo == null) DischargeTo = new HashSet<AdmissionDischargeTo>();
            item.Admission = this;
            DischargeTo.Add(item);
        }
        public virtual void RemoveDischargeTo(AdmissionDischargeTo item)
        {
            if (DischargeTo == null || !DischargeTo.Contains(item)) return;
            item.Admission = null;
            DischargeTo.Remove(item);
        }
        public virtual void AddFollowUp(AdmissionFollowUp item)
        {
            if (FollowUp == null) FollowUp = new HashSet<AdmissionFollowUp>();
            item.Admission = this;
            FollowUp.Add(item);
        }
        public virtual void RemoveFollowUp(AdmissionFollowUp item)
        {
            if (FollowUp == null || !FollowUp.Contains(item)) return;
            item.Admission = null;
            FollowUp.Remove(item);
        }

    }
}
