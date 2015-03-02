using System;
using System.Collections.Generic;
using TC.Domain.Components;

namespace TC.Domain.Entities 
{
    public class Person : MultiEnteredByEntity<string> 
    {           
        public virtual string MRN { get { return Id; } set { Id = value; } }
        public virtual string SSN { get; set; }                                                               
        public virtual DateTime? DOB { get; set; }
        public virtual DateTime? DOD { get; set; }// ExpirationDate
        public virtual DateTime? ReferralDate { get; set; }
        public virtual DateTime? ProcessedDate { get; set; }
        public virtual DateTime? DisabledDate { get; set; }
        public virtual DateTime? RetiredDate { get; set; }

        public ISet<ContactInfo> ContactInfo { get; set; }
        public Demographics Demographics { get; set; }
        public UnosData UnosData { get; set; }


        // MAP THIS
        public virtual ISet<Episode> Episodes { get; set; }


        /* 
        public virtual DateTime? NotifiedOfExpirationDate { get; set; }        
        public virtual string ABO { get; set; }
        public virtual string ABO2 { get; set; }         
        public virtual DateTime? ABOEnteredDate { get; set; }
        public virtual DateTime? ABO2EnteredDate { get; set; }
        public virtual string ABOEnteredBy { get; set; }
        public virtual string ABO2EnteredBy { get; set; }
        public virtual string CheckDigit { get; set; }
        public virtual string SSNandMRNSame { get; set; }
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string Suffix { get; set; }
        public virtual string Title { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Zip { get; set; }
        public virtual string ZipExt { get; set; }
        public virtual string County { get; set; }
        public virtual string ForeignCountry { get; set; }
        public virtual string CellPhone { get; set; }
        public virtual string HomePhone { get; set; }
        public virtual string WorkPhone { get; set; }
        public virtual string WorkPhoneExt { get; set; }
        public virtual string Pager { get; set; }
        public virtual string Fax { get; set; }
        public virtual string AltPhone { get; set; }
        public virtual string AltPhoneType { get; set; }
        public virtual string PriorityCellPhone { get; set; }
        public virtual string PriorityHomePhone { get; set; }
        public virtual string PriorityWorkPhone { get; set; }
        public virtual string PriorityPager { get; set; }
        public virtual string PriorityFax { get; set; }
        public virtual string PriorityAltPhone { get; set; }
        public virtual string Email { get; set; }
        
        public virtual string AcademicLevel { get; set; }
        public virtual string AcademicProgress { get; set; }
        public virtual string Citizenship { get; set; }
        public virtual string CauseOfDeath1 { get; set; }
        public virtual string CauseOfDeathSpecify1 { get; set; }
        public virtual string CauseOfDeath2 { get; set; }
        public virtual string CauseOfDeathSpecify2 { get; set; }
        public virtual string CauseOfDeath3 { get; set; }
        public virtual string CauseOfDeathSpecify3 { get; set; }
        public virtual string Comments { get; set; }
        public virtual string Education { get; set; }
        public virtual string EmploymentStatus { get; set; }
        public virtual string Ethnicity { get; set; }
        public virtual string FunctionalStatus { get; set; }
        public virtual string MaidenName { get; set; }
        public virtual string MaritalStatus { get; set; }
        public virtual string Occupation { get; set; }
        public virtual string OKToShareFamily { get; set; }
        public virtual string OKToRelease { get; set; }
        public virtual string PhysicalCapacity { get; set; }
        public virtual string PrimaryPayment { get; set; }
        public virtual string PrimaryLanguage { get; set; }
        public virtual string Race { get; set; }
        public virtual string Religion { get; set; }
        public virtual string Sex { get; set; }
        public virtual string SpouseName { get; set; }
        public virtual string Veteran { get; set; }
        public virtual string WorkingForIncome { get; set; }
        public virtual string WorkingForIncomeNoStatus { get; set; }
        public virtual string WorkingForIncomeYesStatus { get; set; }
        public virtual string YearEntryUS { get; set; }
        public virtual string EmergencyContact { get; set; }
        public virtual string LivDec { get; set; }
        public virtual string SW { get; set; }
        
        public virtual bool? Autopsy { get; set; }
        public virtual bool? EvalConsent { get; set; }                
        public virtual bool? HipaaConsentFormSigned { get; set; }
        public virtual bool? InterpreterNeeded { get; set; }
        public virtual bool? KidneyTx { get; set; }
        public virtual bool? PriorLivingDonor { get; set; }
        
        public virtual float? Weight { get; set; }        
        public virtual float? TravelTime { get; set; }        
        public virtual int? Travel { get; set; }
        public virtual int? Feet { get; set; }
        public virtual int? Inches { get; set; }
        */

        //TC.MRNHelper.MrnSummary MrnSummary
        //{
        //    get
        //    {
        //        return new TC.MRNHelper.MrnSummary
        //        {
        //            // note: current mrn helper only does these 8...
        //            MRN = this.MRN,
        //            LastName = this.LastName,
        //            FirstName = this.FirstName,
        //            SSN = this.SSN,
        //            DOB = this.DOB,
        //            Sex = this.Sex,
        //            Race = this.Race,
        //            ABO = this.ABO,

        //            TxNum = null,
        //            WaitlistDate = null,
        //            DaysWaitedAdjustment = null,
        //            OrganID = null,
        //            OrganDisplay = null,
        //            OrganCodes = null,
        //            Height = null,//this.Feet + this.Inches
        //            Weight = null,//this.Weight
        //            BMI = null,//

        //            Address = this.Address1,
        //            Address2 = this.Address2,
        //            City = this.City,
        //            County = this.County,
        //            Zip = this.Zip,
        //            HomePhone = this.HomePhone,
        //            WorkPhone = this.WorkPhone,
        //            CellPhone = this.CellPhone,
        //            Pager = this.Pager,
        //        };
        //    }
        //}


    }


   
}
