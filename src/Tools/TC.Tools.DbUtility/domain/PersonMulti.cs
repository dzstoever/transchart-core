using System;
using System.Text;
using System.Collections.Generic;
using Iesi.Collections.Generic;


namespace TC.Doman {
    
    public class PersonMulti {
        public virtual string MRN { get; set; }
        public virtual System.Guid Tenant_ID { get; set; }
        public virtual string SSN { get; set; }
        public virtual string Check_Digit { get; set; }
        public virtual string SS_Nand_MRN_Same { get; set; }
        public virtual string Last_Name { get; set; }
        public virtual string First_Name { get; set; }
        public virtual string Middle_Name { get; set; }
        public virtual string Suffix { get; set; }
        public virtual string Title { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Zip { get; set; }
        public virtual string Zip_Ext { get; set; }
        public virtual string County { get; set; }
        public virtual string Foreign_Country { get; set; }
        public virtual string Cell_Phone { get; set; }
        public virtual string Home_Phone { get; set; }
        public virtual string Work_Phone { get; set; }
        public virtual string Work_Phone_Ext { get; set; }
        public virtual string Pager { get; set; }
        public virtual string Fax { get; set; }
        public virtual string Alt_Phone { get; set; }
        public virtual string Alt_Phone_Type { get; set; }
        public virtual string Priority_Cell_Phone { get; set; }
        public virtual string Priority_Home_Phone { get; set; }
        public virtual string Priority_Work_Phone { get; set; }
        public virtual string Priority_Pager { get; set; }
        public virtual string Priority_Fax { get; set; }
        public virtual string Priority_Alt_Phone { get; set; }
        public virtual string Email { get; set; }
        public virtual string ABO { get; set; }
        public virtual string ABO_Entered_By { get; set; }
        public virtual DateTime? ABO_Entered_Date { get; set; }
        public virtual string ABO2 { get; set; }
        public virtual string ABO2_Entered_By { get; set; }
        public virtual DateTime? ABO2_Entered_Date { get; set; }
        public virtual string Academic_Level { get; set; }
        public virtual string Academic_Progress { get; set; }
        public virtual bool? Autopsy { get; set; }
        public virtual string Citizenship { get; set; }
        public virtual string Cause_Of_Death1 { get; set; }
        public virtual string Cause_Of_Death_Specify1 { get; set; }
        public virtual string Cause_Of_Death2 { get; set; }
        public virtual string Cause_Of_Death_Specify2 { get; set; }
        public virtual string Cause_Of_Death3 { get; set; }
        public virtual string Cause_Of_Death_Specify3 { get; set; }
        public virtual string Comments { get; set; }
        public virtual DateTime? Disabled_Date { get; set; }
        public virtual DateTime? DOB { get; set; }
        public virtual string Education { get; set; }
        public virtual string Employment_Status { get; set; }
        public virtual string Ethnicity { get; set; }
        public virtual bool? Eval_Consent { get; set; }
        public virtual DateTime? Expiration_Date { get; set; }
        public virtual DateTime? Notified_Of_Expiration_Date { get; set; }
        public virtual string Functional_Status { get; set; }
        public virtual bool? Hipaa_Consent_Form_Signed { get; set; }
        public virtual bool? Interpreter_Needed { get; set; }
        public virtual bool? Kidney_Tx { get; set; }
        public virtual string Maiden_Name { get; set; }
        public virtual string Marital_Status { get; set; }
        public virtual string Occupation { get; set; }
        public virtual string OK_To_Share_Family { get; set; }
        public virtual string OK_To_Release { get; set; }
        public virtual string Physical_Capacity { get; set; }
        public virtual string Primary_Payment { get; set; }
        public virtual string Primary_Language { get; set; }
        public virtual DateTime? Processed_Date { get; set; }
        public virtual string Race { get; set; }
        public virtual DateTime? Referral_Date { get; set; }
        public virtual string Religion { get; set; }
        public virtual DateTime? Retired_Date { get; set; }
        public virtual string Sex { get; set; }
        public virtual string Spouse_Name { get; set; }
        public virtual int? Travel { get; set; }
        public virtual float? Travel_Time { get; set; }
        public virtual string Veteran { get; set; }
        public virtual string Working_For_Income { get; set; }
        public virtual string Working_For_Income_No_Status { get; set; }
        public virtual string Working_For_Income_Yes_Status { get; set; }
        public virtual string Year_Entry_US { get; set; }
        public virtual string Emergency_Contact { get; set; }
        public virtual string Liv_Dec { get; set; }
        public virtual string SW { get; set; }
        public virtual int? Feet { get; set; }
        public virtual int? Inches { get; set; }
        public virtual float? Weight { get; set; }
        public virtual string Entered_By { get; set; }
        public virtual DateTime? Entered_Date { get; set; }
        public virtual DateTime? Entered_Time { get; set; }
        public virtual bool? Prior_Living_Donor { get; set; }
        #region NHibernate Composite Key Requirements
        public override bool Equals(object obj) {
			if (obj == null) return false;
			var t = obj as PersonMulti;
			if (t == null) return false;
			if (MRN == t.MRN
			 && Tenant_ID == t.Tenant_ID)
				return true;

			return false;
        }
        public override int GetHashCode() {
			int hash = GetType().GetHashCode();
			hash = (hash * 397) ^ MRN.GetHashCode();
			hash = (hash * 397) ^ Tenant_ID.GetHashCode();

			return hash;
        }
        #endregion
    }
}
