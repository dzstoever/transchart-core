using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using TC.Doman;
using Zen.Data;


namespace TC.Maps {
    
    
    public class PhysicianMultiMap : ClassMapping<PhysicianMulti>, IDbMap 
    {
        
        public PhysicianMultiMap() {
			Table("Physician_Multi");
			Schema("dbo");
			Lazy(true);
            ComposedId(compId =>
                {
                    compId.Property(x => x.MRN, m => m.Column("MRN"));
                    compId.Property(x => x.PID, m => m.Column("PID"));
                    compId.Property(x => x.Entered_Time, m => m.Column("EnteredTime"));
                    compId.Property(x => x.Tenant_ID, m => m.Column("TenantID"));
                });

            //Property(x => x.MRN, m => m.Column("MRN"));
            //Property(x => x.PID, m => m.Column("PID"));
            //Property(x => x.Entered_Time, m => m.Column("EnteredTime"));
            //Property(x => x.Tenant_ID, m => m.Column("TenantID"));

			Property(x => x.Cur, map => map.NotNullable(true));
			Property(x => x.Mod_Del, map => map.Column("ModDel"));
			Property(x => x.Phy_Type, map => map.Column("PhyType"));
			Property(x => x.Entered_By, map => map.Column("EnteredBy"));
			Property(x => x.Entered_Date, map => map.Column("EnteredDate"));
        }
    }
}
