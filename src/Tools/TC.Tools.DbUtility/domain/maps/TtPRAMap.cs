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


    public class TtPRAMap : ClassMapping<TtPRA>, IDbMap
    {
        
        public TtPRAMap() {
            Table("TT_PRA");
			Schema("dbo");
			Lazy(true);
			Property(x => x.WG_Name, map => map.Column("WGName"));
			Property(x => x.MRN, map => map.NotNullable(true));
			Property(x => x.PRA_Date, map => map.Column("PRADate"));
			Property(x => x.Method, map => map.NotNullable(true));
			Property(x => x.Result);
			Property(x => x.Specificity);
			Property(x => x.Entered_By, map => map.Column("EnteredBy"));
			Property(x => x.Entered_Date, map => map.Column("EnteredDate"));
			Property(x => x.Lab_Tech, map => map.Column("LabTech"));
			Property(x => x.Serum_Id, map => { map.Column("SerumId"); map.NotNullable(true); });
			Property(x => x.Serum_Date, map => { map.Column("SerumDate"); map.NotNullable(true); });
			Property(x => x.Entered_Time, map => map.Column("EnteredTime"));
			Property(x => x.A);
			Property(x => x.B);
			Property(x => x.BW4);
			Property(x => x.BW6);
			Property(x => x.DR);
			Property(x => x.DQ);
			Property(x => x.CW);
			Property(x => x.DR515253);
			Property(x => x.DP);
			Property(x => x.Comment);
			Property(x => x.UNOS_Certification_Complete, map => map.Column("UNOSCertificationComplete"));
			//Property(x => x.Tenant_ID, map => { map.Column("TenantID"); map.NotNullable(true); });
        }
    }
}
