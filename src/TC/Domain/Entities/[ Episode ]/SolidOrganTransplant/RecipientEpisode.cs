using System;
using TC.Domain.Components;

namespace TC.Domain.Entities.SolidOrganTransplant
{
    // NOTE: DbTableName = EpisodeTransplant
    public class RecipientEpisode : TxEpisode // MultiEnteredByEntity<int>
    {   // NOT NEEDED (1 to 1)
        //public virtual int ID { get; set; } //public virtual string MRN { get; set; }
        // DEPRECATE
        //public virtual string PrePost { get; set; }       
        //public virtual string PatientStatus { get; set; }
        
        public virtual bool?    ReadyForTransplant { get; set; }        
        public virtual bool     Transplanted { get; set; }                
        public virtual string   ReasonForDischarge { get; set; }// ReasonForDischargeFromCare in Db
        

        public virtual RecipientDetails Details { get; set; }
        public virtual RecipientCareTeam CareTeam { get; set; }
        public virtual RecipientDiagnosis Diagnosis { get; set; }
        public virtual RecipientWaitListInfo WaitListInfo { get; set; }
        
    }

    public class RecipientDetails : TxEpisodeDetails
    {
        public virtual DateTime? DeferralDate { get; set; }
        public virtual DateTime? InsulinStartDate { get; set; }
        public virtual int? DiabetesHistoryId { get; set; }
        public virtual bool FollowedByPost { get; set; }
        public virtual bool? NeedsBackup { get; set; }
        public virtual bool? ChartValid { get; set; }
        public virtual bool? A2BConsent { get; set; }
        public virtual bool? KDPIConsent { get; set; }
        public virtual bool? SplitLiver { get; set; }
        public virtual bool? UseDialysisStartDate { get; set; }//[Use_Dialysis_StartDate] in Db
        public virtual string UNOSPts { get; set; }
        public virtual string PairedDonorExchange { get; set; }
        public virtual string DiseaseRecurrence { get; set; }
        public virtual string IsolationStatus { get; set; }
        public virtual string HepCCoreAB { get; set; }
        public virtual string HepCKidney { get; set; }
        public virtual string HepCLiver { get; set; }
        public virtual string CmvDonor { get; set; }
        public virtual string DcdDonor { get; set; }
        public virtual string ExpandedDonor { get; set; }
        public virtual string HighRiskDonor { get; set; }
        public virtual string HighRiskDonorQuestionnaireCompleted { get; set; }
    }
    public class RecipientCareTeam : CareTeam
    {
        public virtual string LocalMD { get; set; }
        public virtual string Physician { get; set; }
    }
    public class RecipientDiagnosis : Diagnosis { }
    public class RecipientWaitListInfo
    {
        public virtual DateTime? WaitListDate { get; set; }
        public virtual string WaitingZip { get; set; }
        public virtual string WaitingZipExt { get; set; }
    }
    
}
