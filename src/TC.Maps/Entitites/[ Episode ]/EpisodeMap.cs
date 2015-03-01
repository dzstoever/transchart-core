using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using TC.Domain;


namespace TC.Maps {
    
    
    public class EpisodeMultiMap : ClassMapping<Episode_Multi> {
        
        public EpisodeMultiMap() {
			Schema("dbo");
			Lazy(true);
			Id(x => x.ID, map => map.Generator(Generators.Identity));
			Property(x => x.MRN, map => { map.NotNullable(true); map.Length(15); });
			Property(x => x.EpisodeTypeID, map => { map.NotNullable(true); map.Precision(10); });
			Property(x => x.EpisodeNum, map => map.Precision(10));
			Property(x => x.IsCurrent);
			Property(x => x.Organ, map => map.Precision(10));
			Property(x => x.RowID, map => { map.NotNullable(true); map.Precision(10); });
			Property(x => x.EnteredBy, map => map.Length(15));
			Property(x => x.EnteredDate);
			Property(x => x.EnteredTime);
			Property(x => x.TenantID, map => map.NotNullable(true));
        }
    }
}
