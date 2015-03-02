using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using TC.Domain.Entities;

namespace TC.Maps.Entitites 
{
        
    public class WaitListMap : ClassMapping<WaitList>, Zen.Data.IDbMap
    {

        public WaitListMap() 
        {
            Schema("dbo"); Table("View_Waitlist");

            Id(x => x.Id, map => { map.Column("MRN"); map.Generator(Generators.Assigned); map.Length(15); });
            
            Property(x => x.OrganId, map => { map.NotNullable(true); map.Precision(10); });            
            Property(x => x.TxNum, map => map.Precision(10));
			Property(x => x.Status, map => map.Length(50));
			Property(x => x.LN, map => map.Length(50));
			Property(x => x.FN, map => map.Length(50));
			Property(x => x.WaitListDate);
			Property(x => x.WaitListStatusId, map => map.Precision(10));			
			Property(x => x.OrganDisplay, map => map.Length(50));
			Property(x => x.OrganCodes, map => map.Length(50));
        }
    }
}
