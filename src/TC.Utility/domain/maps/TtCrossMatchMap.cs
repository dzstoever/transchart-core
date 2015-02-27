using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using TC.Doman;
using Zen.Data;

namespace TC.Maps 
{        
    public class TtCrossMatchMap : ClassMapping<TtCrossMatch>, IDbMap
    {        
        public TtCrossMatchMap() 
        {
			Table("TT_CrossMatch");
			Schema("dbo");
			Lazy(true);

            ComponentAsId(x => x.Id, compId =>
            {
                compId.Property(c => c.UNOSID);
                compId.Property(c => c.MRN);
                compId.Property(c => c.Method);
                compId.Property(c => c.SerumId);
                compId.Property(c => c.CellType);
                compId.Property(c => c.LabDate);
            });
            
            Property(x => x.Result);
			Property(x => x.MCS);			
			Property(x => x.LabTech, map => map.Column("LabTech"));            
			Property(x => x.Comments);
			Property(x => x.TestDate, map => map.Column("TestDate"));			
			Property(x => x.TargetCellSource, map => map.Column("TargetCellSource"));
			Property(x => x.Titer);
			Property(x => x.ChannelShift, map => map.Column("ChannelShift"));
			//Property(x => x.ID, map => map.NotNullable(true));

            //Property(x => x.Entered_By, map => map.Column("EnteredBy"));
            //Property(x => x.Entered_Date, map => map.Column("EnteredDate"));
            //Property(x => x.Entered_Time, map => map.Column("EnteredTime"));
            //Property(x => x.Tenant_ID, map => { map.Column("TenantID"); map.NotNullable(true); });

            //try to get a person but ignore, if not found
            ManyToOne(x => x.Person, m =>
            {
                m.Lazy(LazyRelation.NoLazy);
                m.Columns(fk1 => fk1.Name("MRN"));
                m.Cascade(Cascade.None);
                m.NotFound(NotFoundMode.Ignore);
            });
        }
    }
}
