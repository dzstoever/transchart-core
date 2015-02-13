using System;
using System.Text;
using System.Collections.Generic;


namespace TC.Doman {
    
    public class PhysicianMulti {
        public virtual string MRN { get; set; }
        public virtual string PID { get; set; }
        public virtual string Entered_Time { get; set; }
        public virtual System.Guid Tenant_ID { get; set; }
        public virtual bool Cur { get; set; }
        public virtual string Mod_Del { get; set; }
        public virtual string Phy_Type { get; set; }
        public virtual string Entered_By { get; set; }
        public virtual DateTime? Entered_Date { get; set; }
        #region NHibernate Composite Key Requirements
        public override bool Equals(object obj) {
			if (obj == null) return false;
			var t = obj as PhysicianMulti;
			if (t == null) return false;
			if (MRN == t.MRN
			 && PID == t.PID
			 && Entered_Time == t.Entered_Time
			 && Tenant_ID == t.Tenant_ID)
				return true;

			return false;
        }
        public override int GetHashCode() {
			int hash = GetType().GetHashCode();
			hash = (hash * 397) ^ MRN.GetHashCode();
			hash = (hash * 397) ^ PID.GetHashCode();
			hash = (hash * 397) ^ Entered_Time.GetHashCode();
			hash = (hash * 397) ^ Tenant_ID.GetHashCode();

			return hash;
        }
        #endregion
    }
}
