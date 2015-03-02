using NHibernate.Mapping.ByCode;
using TC.Domain.Entities;

namespace TC.Maps.Entitites 
{
    public class PersonMap : MultiEnteredByMap<Person, string>, Zen.Data.IDbMap 
    {        
        public PersonMap() 
        {
            Schema("dbo"); Table("Person_Multi");

            Id(x => x.Id, map => { map.Column("MRN"); map.Generator(Generators.Assigned); map.Length(15); });

			Property(x => x.SSN, map => map.Length(15));
            Property(x => x.DOB);
            //Property(x => x.ExpirationDate);//DOD
            Property(x => x.ReferralDate);
            Property(x => x.ProcessedDate);
            Property(x => x.DisabledDate);            
            Property(x => x.RetiredDate);
            /*
			Property(x => x.CheckDigit, map => map.Length(1));
			Property(x => x.SSNandMRNSame, map => map.Length(50));
			Property(x => x.LastName, map => map.Length(50));
			Property(x => x.FirstName, map => map.Length(50));
			Property(x => x.MiddleName, map => map.Length(150));
			Property(x => x.Suffix, map => map.Length(10));
			Property(x => x.Title, map => map.Length(50));
			Property(x => x.Address1, map => map.Length(50));
			Property(x => x.Address2, map => map.Length(200));
			Property(x => x.City, map => map.Length(50));
			Property(x => x.State, map => map.Length(25));
			Property(x => x.Zip, map => map.Length(20));
			Property(x => x.ZipExt, map => map.Length(5));
			Property(x => x.County, map => map.Length(200));
			Property(x => x.ForeignCountry, map => map.Length(100));
			Property(x => x.CellPhone, map => map.Length(25));
			Property(x => x.HomePhone, map => map.Length(25));
			Property(x => x.WorkPhone, map => map.Length(50));
			Property(x => x.WorkPhoneExt, map => map.Length(5));
			Property(x => x.Pager, map => map.Length(25));
			Property(x => x.Fax, map => map.Length(20));
			Property(x => x.AltPhone, map => map.Length(25));
			Property(x => x.AltPhoneType, map => map.Length(50));
			Property(x => x.PriorityCellPhone, map => map.Length(2));
			Property(x => x.PriorityHomePhone, map => map.Length(2));
			Property(x => x.PriorityWorkPhone, map => map.Length(2));
			Property(x => x.PriorityPager, map => map.Length(2));
			Property(x => x.PriorityFax, map => map.Length(2));
			Property(x => x.PriorityAltPhone, map => map.Length(2));
			Property(x => x.Email, map => map.Length(50));
			Property(x => x.ABO, map => map.Length(15));
			Property(x => x.ABOEnteredBy, map => map.Length(50));
			Property(x => x.ABOEnteredDate);
			Property(x => x.ABO2, map => map.Length(7));
			Property(x => x.ABO2EnteredBy, map => map.Length(50));
			Property(x => x.ABO2EnteredDate);
			Property(x => x.AcademicLevel, map => map.Length(12));
			Property(x => x.AcademicProgress, map => map.Length(12));
			Property(x => x.Autopsy);
			Property(x => x.Citizenship, map => map.Length(100));
			Property(x => x.CauseOfDeath1, map => map.Length(12));
			Property(x => x.CauseOfDeathSpecify1, map => map.Length(50));
			Property(x => x.CauseOfDeath2, map => map.Length(12));
			Property(x => x.CauseOfDeathSpecify2, map => map.Length(50));
			Property(x => x.CauseOfDeath3, map => map.Length(12));
			Property(x => x.CauseOfDeathSpecify3, map => map.Length(50));
			Property(x => x.Comments, map => map.Length(500));
			
			
			Property(x => x.Education, map => map.Length(100));
			Property(x => x.EmploymentStatus, map => map.Length(20));
			Property(x => x.Ethnicity, map => map.Length(100));
			Property(x => x.EvalConsent);
			
			Property(x => x.NotifiedOfExpirationDate);
			Property(x => x.FunctionalStatus, map => map.Length(12));
			Property(x => x.HipaaConsentFormSigned);
			Property(x => x.InterpreterNeeded);
			Property(x => x.KidneyTx);
			Property(x => x.MaidenName, map => map.Length(50));
			Property(x => x.MaritalStatus, map => map.Length(50));
			Property(x => x.Occupation, map => map.Length(40));
			Property(x => x.OKToShareFamily, map => map.Length(1));
			Property(x => x.OKToRelease, map => map.Length(1));
			Property(x => x.PhysicalCapacity, map => map.Length(12));
			Property(x => x.PrimaryPayment, map => map.Length(12));
			Property(x => x.PrimaryLanguage, map => map.Length(100));
            
			Property(x => x.Race, map => map.Length(50));
            
			Property(x => x.Religion, map => map.Length(50));
            
			Property(x => x.Sex, map => map.Length(50));
			Property(x => x.SpouseName, map => map.Length(40));
			Property(x => x.Travel, map => map.Precision(10));
			Property(x => x.TravelTime, map => map.Precision(24));
			Property(x => x.Veteran, map => map.Length(10));
			Property(x => x.WorkingForIncome, map => map.Length(10));
			Property(x => x.WorkingForIncomeNoStatus, map => map.Length(12));
			Property(x => x.WorkingForIncomeYesStatus, map => map.Length(12));
			Property(x => x.YearEntryUS, map => map.Length(10));
			Property(x => x.EmergencyContact, map => map.Length(50));
			Property(x => x.LivDec, map => map.Length(25));
			Property(x => x.SW, map => map.Length(40));
			Property(x => x.Feet, map => map.Precision(10));
			Property(x => x.Inches, map => map.Precision(10));
			Property(x => x.Weight, map => map.Precision(53));
			Property(x => x.PriorLivingDonor);
            */
            Property(x => x.EnteredBy, map => map.Length(21));
			Property(x => x.EnteredDate);
			Property(x => x.EnteredTime);
			
        }
    }
    
}
