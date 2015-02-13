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


    public class MedicationsMultiMap : ClassMapping<MedicationsMulti>, IDbMap
    {
        
        public MedicationsMultiMap() {
			Table("Medications_Multi");
			Schema("dbo");
			Lazy(true);
			ComposedId(compId =>
				{
					compId.Property(x => x.MRN, m => m.Column("MRN"));
					compId.Property(x => x.S_Date, m => m.Column("SDate"));
					compId.Property(x => x.Drug_ID, m => m.Column("DrugID"));
					compId.Property(x => x.Dose, m => m.Column("Dose"));
					compId.Property(x => x.Units, m => m.Column("Units"));
					compId.Property(x => x.Entered_Time, m => m.Column("EnteredTime"));
					compId.Property(x => x.Tenant_ID, m => m.Column("TenantID"));
				});
			Property(x => x.E_Date, map => map.Column("EDate"));
			Property(x => x.Frequency);
			Property(x => x.Entered_By, map => map.Column("EnteredBy"));
			Property(x => x.Entered_Date, map => map.Column("EnteredDate"));
			Property(x => x.Perscribed_By, map => map.Column("PerscribedBy"));
			Property(x => x.Perscriber_Reason, map => map.Column("PerscriberReason"));
			Property(x => x.Mod_Del, map => map.Column("ModDel"));
			Property(x => x.Perscriber_Desc, map => map.Column("PerscriberDesc"));
			Property(x => x.Extra_Column, map => map.Column("ExtraColumn"));
			Property(x => x.Antirejection);
			Property(x => x.Qty_Per_Dose, map => map.Column("QtyPerDose"));
			Property(x => x.numberofrefills);
			Property(x => x.DAW);
			Property(x => x.Numberof_Days, map => map.Column("NumberofDays"));
			Property(x => x.Dose_Form, map => map.Column("DoseForm"));
			Property(x => x.routeid);
			Property(x => x.dose_Size, map => map.Column("doseSize"));
			Property(x => x.id, map => map.NotNullable(true));
			Property(x => x.site);
			Property(x => x.split);
			Property(x => x.Induction, map => map.NotNullable(true));
			Property(x => x.Processed_Date, map => map.Column("ProcessedDate"));
        }
    }
}
