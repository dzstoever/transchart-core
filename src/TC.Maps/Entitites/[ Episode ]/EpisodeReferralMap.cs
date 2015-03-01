using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using TC.Domain;


namespace TC.Maps {
    
    
    public class EpisodereferralMultiMap : ClassMapping<EpisodeReferral_Multi> {
        
        public EpisodereferralMultiMap() {
			Schema("dbo");
			Lazy(true);
			Property(x => x.ID, map => { map.NotNullable(true); map.Precision(10); });
			Property(x => x.EpisodeID, map => map.Precision(10));
			Property(x => x.ReferralDate);
			Property(x => x.PhysicianID, map => map.Length(35));
			Property(x => x.ReferredFor, map => map.Precision(10));
			Property(x => x.MRN, map => { map.NotNullable(true); map.Length(15); });
			Property(x => x.GeneralDiagnosis, map => map.Length(50));
			Property(x => x.InsuranceType, map => map.Length(25));
			Property(x => x.EnteredBy, map => map.Length(50));
			Property(x => x.EnteredDate);
			Property(x => x.TenantID, map => map.NotNullable(true));
        }
    }
}
