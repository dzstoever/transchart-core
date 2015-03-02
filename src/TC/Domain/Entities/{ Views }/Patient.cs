using System;

namespace TC.Domain.Entities 
{
    
    public class Patient : EnteredByEntity<string> 
    {
        //this isn't a real key/id, it's kind of a pseudo key for the view...
        public virtual string MRN { get { return Id; } set { Id = value; } }        
        public virtual string SSN { get; set; }                 
        public virtual string State { get; set; }
        public virtual string Zip { get; set; }
        public virtual DateTime? DOB { get; set; }
        
        #region /* names from NewCore.Data */
        //public virtual string Last { get; set; }                //LastName
        //public virtual string First { get; set; }               //FirstName     
        //public virtual string Sex { get; set; }                 //Gender
        //public virtual string ABO { get; set; }                 //BloodType        
        //public virtual string cod { get; set; }                 //CauseOfDeath
        //public virtual string cod_ostxt { get; set; }           //CauseOfDeathComments
        //public virtual string cod2 { get; set; }                //ContributoryCauseOfDeath1
        //public virtual string cod2_ostxt { get; set; }          //ContributoryCauseOfDeath1Comments
        //public virtual string cod3 { get; set; }                //ContributoryCauseOfDeath2
        //public virtual string cod3_ostxt { get; set; }          //ContributoryCauseOfDeath2Comments
        //public virtual string func_stat { get; set; }           //FunctionalStatus
        //public virtual string physical_capacity { get; set; }   //PhysicalCapacity
        //public virtual string work_income { get; set; }         //WorkingForIncome
        //public virtual string work_no_status { get; set; }      //WorkingForIncomeNoStatus
        //public virtual string work_yes_status { get; set; }     //WorkingForIncomeYesStatus
        //public virtual string academic_prg { get; set; }        //AcademicProgress
        //public virtual string academic_level { get; set; }      //AcademicActivityLevel
        //public virtual string recurred_orig_dgn { get; set; }   //DiseaseRecurrence
        //public virtual DateTime? ExpirationDate { get; set; }   //DeceasedDate
        #endregion
        public virtual string LastName { get; set; } 
        public virtual string FirstName { get; set; }
        public virtual string Gender { get; set; }
        public virtual string BloodType { get; set; }        
        public virtual string CauseOfDeath { get; set; }
        public virtual string CauseOfDeathComments { get; set; }
        public virtual string ContributoryCauseOfDeath1 { get; set; }
        public virtual string ContributoryCauseOfDeath1Comments { get; set; }
        public virtual string ContributoryCauseOfDeath2 { get; set; }
        public virtual string ContributoryCauseOfDeath2Comments { get; set; }
        public virtual string FunctionalStatus { get; set; }
        public virtual string PhysicalCapacity { get; set; }
        public virtual string WorkingForIncome { get; set; }
        public virtual string WorkingForIncomeNoStatus { get; set; }
        public virtual string WorkingForIncomeYesStatus { get; set; }
        public virtual string AcademicProgress { get; set; }
        public virtual string AcademicActivityLevel { get; set; }
        public virtual string DiseaseRecurrence { get; set; }
        public virtual DateTime? DeceasedDate { get; set; }


        public virtual DateTime? ProcessedDate { get; set; }
        public virtual DateTime? RefDate { get; set; }
        public virtual DateTime? RetiredDt { get; set; }
        public virtual DateTime? DisabledDt { get; set; }
        public virtual DateTime? btentereddate { get; set; }
        public virtual string btenteredby { get; set; }
        public virtual string CheckDigit { get; set; }        
        public virtual string Middle { get; set; }
        public virtual string Addr1 { get; set; }
        public virtual string Addr2 { get; set; }
        public virtual string City { get; set; }
        public virtual string ZipExt { get; set; }
        public virtual string CountyCode { get; set; }
        public virtual string HPhone { get; set; }
        public virtual string CellPhone { get; set; }
        public virtual string WPhone { get; set; }
        public virtual string WPhoneExt { get; set; }
        public virtual string Email { get; set; }
        public virtual string Pager { get; set; }
        public virtual string Fax { get; set; }
        public virtual string PriorityHPhone { get; set; }
        public virtual string PriorityCellPhone { get; set; }
        public virtual string PriorityWPhone { get; set; }
        public virtual string PriorityFax { get; set; }
        public virtual string PriorityPager { get; set; }        
        public virtual string LivDec { get; set; }
        public virtual string Maiden { get; set; }
        public virtual string MaritalStatus { get; set; }
        public virtual string OKTORELEASE { get; set; }
        public virtual string OKtoShareFamily { get; set; }
        public virtual string Citizenship { get; set; }
        public virtual string Comments { get; set; }
        public virtual string empl_stat { get; set; }
        public virtual string PrimaryLanguage { get; set; }
        public virtual string Race { get; set; }
        public virtual string Religion { get; set; }
        public virtual string Suffix { get; set; }
        public virtual string Title { get; set; }
        public virtual string Veteran { get; set; }        
        public virtual string Coord { get; set; }
        public virtual string Coord2 { get; set; }
        public virtual string IsolationStatus { get; set; }
        public virtual string LocalMD { get; set; }
        public virtual string NursePracticioner { get; set; }
        public virtual string PrePost { get; set; }
        public virtual string PtStatus { get; set; }        
        public virtual string SocialWorker { get; set; }
        public virtual string SocialWorker2 { get; set; }
        public virtual string Technician { get; set; }
        public virtual string TransPhysician { get; set; }
        public virtual string TransPhysician2 { get; set; }

        public virtual bool? autopsy { get; set; }
        public virtual bool? HipaaConsentFormSigned { get; set; }
        public virtual bool? InterpreterNeeded { get; set; }
        public virtual bool? KidneyTx { get; set; }

        public virtual int? BirthPlace { get; set; }
        public virtual int? COC { get; set; }
        public virtual int? HeightCM { get; set; }
        public virtual int? HeightIN { get; set; }
        public virtual int? px_non_compl { get; set; }
        public virtual int? PRDX { get; set; }
        public virtual int? TransDate { get; set; }
        
    }
}
