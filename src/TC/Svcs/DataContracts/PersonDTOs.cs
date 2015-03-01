using System;
using System.Runtime.Serialization;

namespace TC.DataModels 
{
    [DataContract(Namespace = "")]
    public class PersonMinDTO
    {
        [DataMember] public string MRN { get; set; }        
        [DataMember] public string SSN { get; set; }
        [DataMember] public string FirstName { get; set; }
        [DataMember] public string MiddleName { get; set; }
        [DataMember] public string LastName { get; set; }
    }
    
    public class PersonDTOs : PersonMinDTO 
    {
        public DateTime? ProcessedDate { get; set; }
        public DateTime? ReferralDate { get; set; }
        public DateTime? RetiredDate { get; set; }
    }
    
    public class PersonMaxDTO : PersonDTOs
    {
        [DataMember()] public virtual String CheckDigit {
            get;
            set;
        }
        
        [DataMember()] public virtual String SSNandMRNSame {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Suffix {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Title {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Address1 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Address2 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String City {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String State {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Zip {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String ZipExt {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String County {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String ForeignCountry {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String CellPhone {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String HomePhone {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String WorkPhone {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String WorkPhoneExt {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Pager {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Fax {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String AltPhone {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String AltPhoneType {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String PriorityCellPhone {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String PriorityHomePhone {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String PriorityWorkPhone {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String PriorityPager {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String PriorityFax {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String PriorityAltPhone {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Email {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String ABO {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String ABOEnteredBy {
            get;
            set;
        }
        
        [DataMember()]
        public virtual DateTime ABOEnteredDate {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String ABO2 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String ABO2EnteredBy {
            get;
            set;
        }
        
        [DataMember()]
        public virtual DateTime ABO2EnteredDate {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String AcademicLevel {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String AcademicProgress {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Boolean Autopsy {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Citizenship {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String CauseOfDeath1 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String CauseOfDeathSpecify1 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String CauseOfDeath2 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String CauseOfDeathSpecify2 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String CauseOfDeath3 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String CauseOfDeathSpecify3 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Comments {
            get;
            set;
        }
        
        [DataMember()]
        public virtual DateTime DisabledDate {
            get;
            set;
        }
        
        [DataMember()]
        public virtual DateTime DOB {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Education {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String EmploymentStatus {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Ethnicity {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Boolean EvalConsent {
            get;
            set;
        }
        
        [DataMember()]
        public virtual DateTime ExpirationDate {
            get;
            set;
        }
        
        [DataMember()]
        public virtual DateTime NotifiedOfExpirationDate {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String FunctionalStatus {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Boolean HipaaConsentFormSigned {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Boolean InterpreterNeeded {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Boolean KidneyTx {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String MaidenName {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String MaritalStatus {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Occupation {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String OKToShareFamily {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String OKToRelease {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String PhysicalCapacity {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String PrimaryPayment {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String PrimaryLanguage {
            get;
            set;
        }
        
        
        [DataMember()]
        public virtual String Race {
            get;
            set;
        }
        
        
        [DataMember()]
        public virtual String Religion {
            get;
            set;
        }
        
        
        [DataMember()]
        public virtual String Sex {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String SpouseName {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Int32 Travel {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Single TravelTime {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Veteran {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String WorkingForIncome {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String WorkingForIncomeNoStatus {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String WorkingForIncomeYesStatus {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String YearEntryUS {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String EmergencyContact {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String LivDec {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String SW {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Int32 Feet {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Int32 Inches {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Single Weight {
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
        public virtual Boolean PriorLivingDonor {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Guid Id {
            get;
            set;
        }
    }
}
