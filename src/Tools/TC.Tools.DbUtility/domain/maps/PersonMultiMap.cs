using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using TC.Doman;
using Iesi.Collections.Generic;
using Zen.Data;


namespace TC.Maps {


    public class PersonMultiMap : ClassMapping<PersonMulti>, IDbMap
    {
        
        public PersonMultiMap() {
			Table("Person_Multi");
			Schema("dbo");
			Lazy(true);
			ComposedId(compId =>
				{
					compId.Property(x => x.MRN, m => m.Column("MRN"));
					compId.Property(x => x.Tenant_ID, m => m.Column("TenantID"));
				});
			Property(x => x.SSN);
			Property(x => x.Check_Digit, map => map.Column("CheckDigit"));
			Property(x => x.SS_Nand_MRN_Same, map => map.Column("SSNandMRNSame"));
			Property(x => x.Last_Name, map => map.Column("LastName"));
			Property(x => x.First_Name, map => map.Column("FirstName"));
			Property(x => x.Middle_Name, map => map.Column("MiddleName"));
			Property(x => x.Suffix);
			Property(x => x.Title);
			Property(x => x.Address1);
			Property(x => x.Address2);
			Property(x => x.City);
			Property(x => x.State);
			Property(x => x.Zip);
			Property(x => x.Zip_Ext, map => map.Column("ZipExt"));
			Property(x => x.County);
			Property(x => x.Foreign_Country, map => map.Column("ForeignCountry"));
			Property(x => x.Cell_Phone, map => map.Column("CellPhone"));
			Property(x => x.Home_Phone, map => map.Column("HomePhone"));
			Property(x => x.Work_Phone, map => map.Column("WorkPhone"));
			Property(x => x.Work_Phone_Ext, map => map.Column("WorkPhoneExt"));
			Property(x => x.Pager);
			Property(x => x.Fax);
			Property(x => x.Alt_Phone, map => map.Column("AltPhone"));
			Property(x => x.Alt_Phone_Type, map => map.Column("AltPhoneType"));
			Property(x => x.Priority_Cell_Phone, map => map.Column("PriorityCellPhone"));
			Property(x => x.Priority_Home_Phone, map => map.Column("PriorityHomePhone"));
			Property(x => x.Priority_Work_Phone, map => map.Column("PriorityWorkPhone"));
			Property(x => x.Priority_Pager, map => map.Column("PriorityPager"));
			Property(x => x.Priority_Fax, map => map.Column("PriorityFax"));
			Property(x => x.Priority_Alt_Phone, map => map.Column("PriorityAltPhone"));
			Property(x => x.Email);
			Property(x => x.ABO);
			Property(x => x.ABO_Entered_By, map => map.Column("ABOEnteredBy"));
			Property(x => x.ABO_Entered_Date, map => map.Column("ABOEnteredDate"));
			Property(x => x.ABO2);
			Property(x => x.ABO2_Entered_By, map => map.Column("ABO2EnteredBy"));
			Property(x => x.ABO2_Entered_Date, map => map.Column("ABO2EnteredDate"));
			Property(x => x.Academic_Level, map => map.Column("AcademicLevel"));
			Property(x => x.Academic_Progress, map => map.Column("AcademicProgress"));
			Property(x => x.Autopsy);
			Property(x => x.Citizenship);
			Property(x => x.Cause_Of_Death1, map => map.Column("CauseOfDeath1"));
			Property(x => x.Cause_Of_Death_Specify1, map => map.Column("CauseOfDeathSpecify1"));
			Property(x => x.Cause_Of_Death2, map => map.Column("CauseOfDeath2"));
			Property(x => x.Cause_Of_Death_Specify2, map => map.Column("CauseOfDeathSpecify2"));
			Property(x => x.Cause_Of_Death3, map => map.Column("CauseOfDeath3"));
			Property(x => x.Cause_Of_Death_Specify3, map => map.Column("CauseOfDeathSpecify3"));
			Property(x => x.Comments);
			Property(x => x.Disabled_Date, map => map.Column("DisabledDate"));
			Property(x => x.DOB);
			Property(x => x.Education);
			Property(x => x.Employment_Status, map => map.Column("EmploymentStatus"));
			Property(x => x.Ethnicity);
			Property(x => x.Eval_Consent, map => map.Column("EvalConsent"));
			Property(x => x.Expiration_Date, map => map.Column("ExpirationDate"));
			Property(x => x.Notified_Of_Expiration_Date, map => map.Column("NotifiedOfExpirationDate"));
			Property(x => x.Functional_Status, map => map.Column("FunctionalStatus"));
			Property(x => x.Hipaa_Consent_Form_Signed, map => map.Column("HipaaConsentFormSigned"));
			Property(x => x.Interpreter_Needed, map => map.Column("InterpreterNeeded"));
			Property(x => x.Kidney_Tx, map => map.Column("KidneyTx"));
			Property(x => x.Maiden_Name, map => map.Column("MaidenName"));
			Property(x => x.Marital_Status, map => map.Column("MaritalStatus"));
			Property(x => x.Occupation);
			Property(x => x.OK_To_Share_Family, map => map.Column("OKToShareFamily"));
			Property(x => x.OK_To_Release, map => map.Column("OKToRelease"));
			Property(x => x.Physical_Capacity, map => map.Column("PhysicalCapacity"));
			Property(x => x.Primary_Payment, map => map.Column("PrimaryPayment"));
			Property(x => x.Primary_Language, map => map.Column("PrimaryLanguage"));
			Property(x => x.Processed_Date, map => map.Column("ProcessedDate"));
			Property(x => x.Race);
			Property(x => x.Referral_Date, map => map.Column("ReferralDate"));
			Property(x => x.Religion);
			Property(x => x.Retired_Date, map => map.Column("RetiredDate"));
			Property(x => x.Sex);
			Property(x => x.Spouse_Name, map => map.Column("SpouseName"));
			Property(x => x.Travel);
			Property(x => x.Travel_Time, map => map.Column("TravelTime"));
			Property(x => x.Veteran);
			Property(x => x.Working_For_Income, map => map.Column("WorkingForIncome"));
			Property(x => x.Working_For_Income_No_Status, map => map.Column("WorkingForIncomeNoStatus"));
			Property(x => x.Working_For_Income_Yes_Status, map => map.Column("WorkingForIncomeYesStatus"));
			Property(x => x.Year_Entry_US, map => map.Column("YearEntryUS"));
			Property(x => x.Emergency_Contact, map => map.Column("EmergencyContact"));
			Property(x => x.Liv_Dec, map => map.Column("LivDec"));
			Property(x => x.SW);
			Property(x => x.Feet);
			Property(x => x.Inches);
			Property(x => x.Weight);
			Property(x => x.Entered_By, map => map.Column("EnteredBy"));
			Property(x => x.Entered_Date, map => map.Column("EnteredDate"));
			Property(x => x.Entered_Time, map => map.Column("EnteredTime"));
			Property(x => x.Prior_Living_Donor, map => map.Column("PriorLivingDonor"));
        }
    }
}
