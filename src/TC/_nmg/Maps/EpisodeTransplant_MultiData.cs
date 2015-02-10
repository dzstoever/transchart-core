using System; 
using System.Collections.Generic; 
using System.Text; 
using TC.Domain;


namespace TC.Domain {
    
    
    [DataContract(Name="EpisodeTransplant_MultiData" , Namespace="")]
    public class EpisodeTransplant_MultiData {
        
        [DataMember()]
        public virtual Int32 Id {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String MRN {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Coordinator1 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Coordinator2 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String EvalPhysician1 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String EvalPhysician2 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Physician {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String SocialWorker1 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String SocialWorker2 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Technician {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String TxPhysician1 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String TxPhysician2 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String PrimaryDiagnosis {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String PrimaryDiagnosis2 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String PrimaryDiagnosisDate {
            get;
            set;
        }
        
        [DataMember()]
        public virtual DateTime PrimaryDiagnosisDate2 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String SecondaryDiagnosis {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String SecondaryDiagnosis2 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String SecondaryDiagnosisDate {
            get;
            set;
        }
        
        [DataMember()]
        public virtual DateTime SecondaryDiagnosisDate2 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String OtherDiagnosis {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String OtherDiagnosis2 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual DateTime OtherDiagnosisDate {
            get;
            set;
        }
        
        [DataMember()]
        public virtual DateTime OtherDiagnosisDate2 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String DiagnosisComments {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String DiagnosisComments2 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String WaitingZip {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String WaitingZipExt {
            get;
            set;
        }
        
        [DataMember()]
        public virtual DateTime WaitListDate {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Boolean ChartValid {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String CMVDonor {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String DCDDonor {
            get;
            set;
        }
        
        [DataMember()]
        public virtual DateTime DeferralDate {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String DiseaseRecurrence {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String ExpandedDonor {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Boolean FollowedByPost {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String HepCCoreAB {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String HepCKidney {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String HepCLiver {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String HighRiskDonor {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String HighRiskDonorQuestionnaireCompleted {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String IsolationStatus {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String LocalMD {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Boolean NeedsBackup {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String NursePracticioner {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Boolean Outreach {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String OutreachLocation {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String PairedDonorExchange {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String PatientStatus {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String ReasonForDischargeFromCare {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String PrePost {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Boolean ReadyForTransplant {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Boolean SplitLiver {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Boolean Transplanted {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String UNOSPts {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Boolean Use_Dialysis_StartDate {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String EnteredBy {
            get;
            set;
        }
        
        [DataMember()]
        public virtual DateTime EnteredDate {
            get;
            set;
        }
        
        [DataMember()]
        public virtual DateTime EnteredTime {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Guid TenantID {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Int32 DiabetesHistoryID {
            get;
            set;
        }
        
        [DataMember()]
        public virtual DateTime InsulinStartDate {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Boolean KDPIConsent {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Boolean A2BConsent {
            get;
            set;
        }
    }
}
