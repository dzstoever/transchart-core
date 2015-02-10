using System;
using System.Text;
using System.Collections.Generic;


namespace TC.Domain 
{
    /// <summary>
    /// A set of information for a given person in the context of a specific episode.
    /// The person can be a 'Recipient' or 'Donor' candidate 
    /// </summary>
    /// <remarks>
    /// An episode is unique based on MRN, TxNum, Donor, and ?OrganType
    /// </remarks>
    public class Candidate : EnteredByEntity<string> 
    {
        //this isn't a real key/id, it's kind of a pseudo key for the view...
        public virtual string MRN { get { return Id; } set { Id = value; } }
        
        public virtual int TxNum { get; set; }          // ISNULL(e.EpisodeNum, 0) AS TxNum,
        public virtual int Donor { get; set; }          // Donor=1, Recipient=0
        public virtual string Recipient { get; set; }   // LINKED to the recipient when candidate is a donor
        public virtual string RecipientMRN { get; set; }// on MRN
        public virtual int? RecipientTxNum { get; set; }// and TxNum                
        public virtual int? OrganType { get; set; }     // Multiple organ transplants?
        public virtual bool? Transplanted { get; set; }
        
        public virtual DateTime? ExpirationDate { get; set; }                
        public virtual DateTime? ReferralDate { get; set; }        
        public virtual DateTime? WaitListDate { get; set; }
        public virtual DateTime? DeferralDate { get; set; }
        public virtual DateTime? DisabledDate { get; set; }
        public virtual DateTime? RetiredDate { get; set; }
        public virtual DateTime? OtherDxDate { get; set; }
        public virtual DateTime? ProcessedDate { get; set; }
        public virtual DateTime? BTEnteredDate { get; set; }
        public virtual DateTime? BT2EnteredDate { get; set; }        
        public virtual DateTime? DOB { get; set; }

        public virtual string SSN { get; set; }
        public virtual string CheckDigit { get; set; }
        public virtual string SSNandMRNSame { get; set; }
        public virtual string FN { get; set; }
        public virtual string MI { get; set; }
        public virtual string LN { get; set; }
        public virtual string Address { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Zip { get; set; }
        public virtual string ZipExt { get; set; }
        public virtual string County { get; set; }
        public virtual string Fcountry { get; set; }
        public virtual string HPhone { get; set; }
        public virtual string CPhone { get; set; }
        public virtual string WPhone { get; set; }
        public virtual string WPhoneExt { get; set; }
        public virtual string Pager { get; set; }
        public virtual string Fax { get; set; }
        public virtual string Email { get; set; }
        public virtual string PriorityCellPhone { get; set; }
        public virtual string PriorityFax { get; set; }
        public virtual string PriorityHPhone { get; set; }
        public virtual string PriorityPager { get; set; }
        public virtual string PriorityWPhone { get; set; }
        public virtual string ABO { get; set; }
        public virtual string ABO2 { get; set; }
        public virtual string BTEnteredBy { get; set; }
        public virtual string BT2EnteredBy { get; set; }
        public virtual string AcademicLevel { get; set; }
        public virtual string AcademicPrg { get; set; }
        public virtual string Citizenship { get; set; }
        public virtual string COD { get; set; }
        public virtual string Comments { get; set; }
        public virtual string Education { get; set; }
        public virtual string EmplStat { get; set; }
        public virtual string Ethnicity { get; set; }
        public virtual string FuncStat { get; set; }
        public virtual string MN { get; set; }
        public virtual string MS { get; set; }
        public virtual string Occupation { get; set; }
        public virtual string OKToRelease { get; set; }
        public virtual string OKToShareFamily { get; set; }
        public virtual string PhysicalCapacity { get; set; }
        public virtual string PrimaryPayment { get; set; }
        public virtual string PrimaryLanguage { get; set; }        
        public virtual string Race { get; set; }        
        public virtual string Religion { get; set; }        
        public virtual string Sex { get; set; }
        public virtual string SN { get; set; }        
        public virtual string Veteran { get; set; }
        public virtual string WorkIncome { get; set; }
        public virtual string WorkNoStatus { get; set; }
        public virtual string WorkYesStatus { get; set; }
        public virtual string YrEntryUs { get; set; }
        public virtual string EmergencyContact { get; set; }
        public virtual string LivDec { get; set; }
        public virtual string SW { get; set; }
        public virtual string Relationship { get; set; }
        public virtual string DonorType { get; set; }
        public virtual string PD { get; set; }
        public virtual string DD { get; set; }
        public virtual string SecDx { get; set; }
        public virtual string SecDxDate { get; set; }
        public virtual string OtherDx { get; set; }
        public virtual string DxComments { get; set; }
        public virtual string Coord { get; set; }
        public virtual string Coord2 { get; set; }
        public virtual string EvalSurgeon { get; set; }
        public virtual string Phys { get; set; }
        public virtual string SocialWorker { get; set; }
        public virtual string SocialWorker2 { get; set; }
        public virtual string Technician { get; set; }
        public virtual string TxPhysician { get; set; }
        public virtual string TxPhysician2 { get; set; }
        public virtual string WaitingZip { get; set; }
        public virtual string WaitingZipExt { get; set; }
        public virtual string CMVDonor { get; set; }
        public virtual string DCDDonor { get; set; }
        public virtual string ExpandedDonor { get; set; }
        public virtual string HEPCCoreAB { get; set; }
        public virtual string HepCKidney { get; set; }
        public virtual string HepCLiver { get; set; }
        public virtual string HighRiskDonorQuestionnaireCompleted { get; set; }
        public virtual string NursePracticioner { get; set; }
        public virtual string OutreachLocation { get; set; }
        public virtual string PairedDonorExchange { get; set; }
        public virtual string UNOSPts { get; set; }
        public virtual float? TravelTime { get; set; }
        public virtual float? Wt { get; set; }        
        public virtual bool? Cur { get; set; }        
        public virtual bool? EvalConsent { get; set; }
        public virtual bool? HipaaConsentFormSigned { get; set; }
        public virtual bool? ReadyForTransplant { get; set; }
        public virtual bool? SplitLiver { get; set; }
        public virtual bool? KidneyTx { get; set; }
        public virtual bool? InterpreterNeeded { get; set; }
        public virtual bool? ChartValid { get; set; }
        public virtual bool? FollowedByPost { get; set; }
        public virtual bool? NeedsBackup { get; set; }
        public virtual bool? Outreach { get; set; }
        public virtual bool? UseDialysisStartDate { get; set; }
        public virtual bool CADDonor { get; set; }
        public virtual bool ArchiveDonor { get; set; }
        public virtual int? Travel { get; set; }
        public virtual int? Feet { get; set; }
        public virtual int? Inches { get; set; }


        //TC.MRNHelper.MrnSummary MrnSummary
        //{
        //    get 
        //    {
        //        return new TC.MRNHelper.MrnSummary 
        //        {
        //            // note: current mrn helper only does these 8...
        //            MRN = this.MRN,
        //            LastName = this.LN,
        //            FirstName = this.FN,
        //            SSN = this.SSN,
        //            DOB = this.DOB,
        //            Sex = this.Sex,
        //            Race = this.Race,
        //            ABO = this.ABO,
                                        
        //            TxNum = this.TxNum,
        //            WaitlistDate = this.WaitListDate,
                                        
        //            DaysWaitedAdjustment = null,
        //            OrganID = this.OrganType,//???
        //            OrganDisplay = null,
        //            OrganCodes = null,
        //            Height = null,//this.Feet + this.Inches
        //            Weight = null,//this.Wt
        //            BMI = null,

        //            Address = this.Address,
        //            Address2 = this.Address2,
        //            City = this.City,
        //            County = this.County,
        //            Zip = this.Zip,
        //            HomePhone = this.PriorityHPhone,
        //            WorkPhone = this.PriorityWPhone,
        //            CellPhone = this.PriorityCellPhone,
        //            Pager = this.Pager,
        //        };
        //    }        
        //}

                
    }
}
