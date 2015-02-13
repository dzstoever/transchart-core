using System;
using System.Text;
using System.Collections.Generic;
using Iesi.Collections.Generic;


namespace TC.Doman {
    
    public class NoteMulti {
        public virtual int Note_Id { get; set; }
        public virtual string MRN { get; set; }
        public virtual DateTime Note_Date { get; set; }
        public virtual string Entered_Time { get; set; }
        public virtual System.Guid Tenant_ID { get; set; }
        public virtual int? Grp_Id { get; set; }
        public virtual string Notes { get; set; }
        public virtual DateTime? Entered_Date { get; set; }
        public virtual string Entered_By { get; set; }
        public virtual string Note_Type { get; set; }
        public virtual bool? Urgent_Note { get; set; }
        public virtual string Urgent_Note_Receiver { get; set; }
        public virtual bool? Urgent_Note_Read { get; set; }
        public virtual bool? Processed { get; set; }
        public virtual string Ordered_By { get; set; }
        public virtual DateTime? Esign_Date { get; set; }
        public virtual DateTime? Esign_Time { get; set; }
        public virtual bool? Struck_Out { get; set; }
        public virtual int? Reply_Note_Id { get; set; }
        #region NHibernate Composite Key Requirements
        public override bool Equals(object obj) {
			if (obj == null) return false;
			var t = obj as NoteMulti;
			if (t == null) return false;
			if (Note_Id == t.Note_Id
			 && MRN == t.MRN
			 && Note_Date == t.Note_Date
			 && Entered_Time == t.Entered_Time
			 && Tenant_ID == t.Tenant_ID)
				return true;

			return false;
        }
        public override int GetHashCode() {
			int hash = GetType().GetHashCode();
			hash = (hash * 397) ^ Note_Id.GetHashCode();
			hash = (hash * 397) ^ MRN.GetHashCode();
			hash = (hash * 397) ^ Note_Date.GetHashCode();
			hash = (hash * 397) ^ Entered_Time.GetHashCode();
			hash = (hash * 397) ^ Tenant_ID.GetHashCode();

			return hash;
        }
        #endregion
    }
}
