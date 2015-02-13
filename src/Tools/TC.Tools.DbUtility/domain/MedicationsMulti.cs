using System;
using System.Text;
using System.Collections.Generic;
using Iesi.Collections.Generic;


namespace TC.Doman {
    
    public class MedicationsMulti {
        public virtual string MRN { get; set; }
        public virtual DateTime S_Date { get; set; }
        public virtual int Drug_ID { get; set; }
        public virtual string Dose { get; set; }
        public virtual string Units { get; set; }
        public virtual string Entered_Time { get; set; }
        public virtual System.Guid Tenant_ID { get; set; }
        public virtual DateTime? E_Date { get; set; }
        public virtual string Frequency { get; set; }
        public virtual string Entered_By { get; set; }
        public virtual DateTime? Entered_Date { get; set; }
        public virtual string Perscribed_By { get; set; }
        public virtual string Perscriber_Reason { get; set; }
        public virtual string Mod_Del { get; set; }
        public virtual string Perscriber_Desc { get; set; }
        public virtual string Extra_Column { get; set; }
        public virtual bool? Antirejection { get; set; }
        public virtual float? Qty_Per_Dose { get; set; }
        public virtual int? numberofrefills { get; set; }
        public virtual bool? DAW { get; set; }
        public virtual int? Numberof_Days { get; set; }
        public virtual string Dose_Form { get; set; }
        public virtual string routeid { get; set; }
        public virtual float? dose_Size { get; set; }
        public virtual int id { get; set; }
        public virtual string site { get; set; }
        public virtual bool? split { get; set; }
        public virtual bool Induction { get; set; }
        public virtual DateTime? Processed_Date { get; set; }
        #region NHibernate Composite Key Requirements
        public override bool Equals(object obj) {
			if (obj == null) return false;
			var t = obj as MedicationsMulti;
			if (t == null) return false;
			if (MRN == t.MRN
			 && S_Date == t.S_Date
			 && Drug_ID == t.Drug_ID
			 && Dose == t.Dose
			 && Units == t.Units
			 && Entered_Time == t.Entered_Time
			 && Tenant_ID == t.Tenant_ID)
				return true;

			return false;
        }
        public override int GetHashCode() {
			int hash = GetType().GetHashCode();
			hash = (hash * 397) ^ MRN.GetHashCode();
			hash = (hash * 397) ^ S_Date.GetHashCode();
			hash = (hash * 397) ^ Drug_ID.GetHashCode();
			hash = (hash * 397) ^ Dose.GetHashCode();
			hash = (hash * 397) ^ Units.GetHashCode();
			hash = (hash * 397) ^ Entered_Time.GetHashCode();
			hash = (hash * 397) ^ Tenant_ID.GetHashCode();

			return hash;
        }
        #endregion
    }
}
