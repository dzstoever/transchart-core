using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Zen.Data;

namespace TC.Utility.Domain.maps 
{
    public class TtPRAMap : ClassMapping<TtPRA>, IDbMap
    {
        
        public TtPRAMap() {
            Table("TT_PRA");
			Schema("dbo");
			Lazy(true);

            ComponentAsId(x => x.Id, compId =>
            {
                compId.Property(c => c.MRN);
                compId.Property(c => c.Method);
                compId.Property(c => c.SerumId);
                compId.Property(c => c.SerumDate);
            });

			Property(x => x.WGName);//, map => map.Column("WGName"));
			Property(x => x.PRADate);//, map => map.Column("PRADate"));
			Property(x => x.Result);
			Property(x => x.Specificity);			
			Property(x => x.LabTech);//, map => map.Column("LabTech"));			
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
			//Property(x => x.UNOSCertificationComplete);//, map => map.Column("UNOSCertificationComplete"));

            //Property(x => x.Entered_By, map => map.Column("EnteredBy"));
            //Property(x => x.Entered_Date, map => map.Column("EnteredDate"));
            //Property(x => x.Entered_Time, map => map.Column("EnteredTime"));
            //Property(x => x.Tenant_ID, map => { map.Column("TenanTId"); map.NotNullable(true); });

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
