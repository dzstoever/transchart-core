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
    
    
    public class TtCrossMatchMap : ClassMapping<TtCrossMatch>, IDbMap
    {
        
        public TtCrossMatchMap() {
			Table("TT_CrossMatch");
			Schema("dbo");
			Lazy(true);
			Property(x => x.UNOSID, map => map.NotNullable(true));
			Property(x => x.MRN, map => map.NotNullable(true));
			Property(x => x.Lab_Date, map => { map.Column("LabDate"); map.NotNullable(true); });
			Property(x => x.Method, map => map.NotNullable(true));
			Property(x => x.Result);
			Property(x => x.MCS);
			Property(x => x.Entered_By, map => map.Column("EnteredBy"));
			Property(x => x.Entered_Date, map => map.Column("EnteredDate"));
			Property(x => x.Lab_Tech, map => map.Column("LabTech"));
			Property(x => x.Serum_Id, map => { map.Column("SerumId"); map.NotNullable(true); });
			Property(x => x.Entered_Time, map => map.Column("EnteredTime"));
			Property(x => x.Comments);
			Property(x => x.Test_Date, map => map.Column("TestDate"));
			Property(x => x.Cell_Type, map => { map.Column("CellType"); map.NotNullable(true); });
			Property(x => x.Target_Cell_Source, map => map.Column("TargetCellSource"));
			Property(x => x.Titer);
			Property(x => x.Channel_Shift, map => map.Column("ChannelShift"));
			Property(x => x.ID, map => map.NotNullable(true));
			//Property(x => x.Tenant_ID, map => { map.Column("TenantID"); map.NotNullable(true); });
        }
    }
}
