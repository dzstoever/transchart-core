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


    public class TtHLAMap : ClassMapping<TtHLA>, IDbMap
    {
        
        public TtHLAMap() {
            Table("TT_HLA");
			Schema("dbo");
			Lazy(true);
            //Property(x => x.Id, map => { map.Column("MRN"); map.NotNullable(true); });

            ComponentAsId(x => x.Id, compId =>
            {
                compId.Property(c => c.MRN);
                compId.Property(c => c.LabDate);
            });

			Property(x => x.MRNUNOS);
			//Property(x => x.Lab_Date, map => { map.Column("LabDate"); map.NotNullable(true); });
			//Property(x => x.MRN, map => map.NotNullable(true));
			Property(x => x.Method);
			Property(x => x.A1);
			Property(x => x.A2);
			Property(x => x.B1);
			Property(x => x.B2);
			Property(x => x.C1);
			Property(x => x.C2);
			Property(x => x.DR1);
			Property(x => x.DR2);
			Property(x => x.DP1);
			Property(x => x.DP2);
			Property(x => x.DQ1);
			Property(x => x.DQ2);
			Property(x => x.BW4);
			Property(x => x.BW6);
			Property(x => x.DRW51);
			Property(x => x.DRW52);
			Property(x => x.DRW53);
			Property(x => x.Entered_Date, map => { map.Column("EnteredDate"); map.NotNullable(true); });
			Property(x => x.Entered_By, map => { map.Column("EnteredBy"); map.NotNullable(true); });
			Property(x => x.enteredtime);
			Property(x => x.Serum_ID, map => { map.Column("SerumID"); map.NotNullable(true); });
			Property(x => x.method_11);
			Property(x => x.comment);
			Property(x => x.Haplo_Type_Match, map => map.Column("HaploTypeMatch"));
			Property(x => x.DR52);
			Property(x => x.DQA1);
			Property(x => x.DQA2);
			Property(x => x.DR3B1);
			Property(x => x.DR3B2);
			Property(x => x.DR4B1);
			Property(x => x.DR4B2);
			//Property(x => x.Tenant_ID, map => { map.Column("TenantID"); map.NotNullable(true); });
        }
    }
}
