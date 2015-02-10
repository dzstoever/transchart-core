using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using TC.Domain;


namespace TC.Maps {
    
    
    public class EpisodedonorMultiMap : ClassMapping<EpisodeDonor_Multi> {
        
        public EpisodedonorMultiMap() {
			Schema("dbo");
			Lazy(true);
			Id(x => x.ID, map => map.Generator(Generators.Identity));
			Property(x => x.MRN, map => { map.NotNullable(true); map.Length(15); });
			Property(x => x.Coordinator1, map => map.Length(50));
			Property(x => x.Coordinator2, map => map.Length(50));
			Property(x => x.EvalPhysician1, map => map.Length(50));
			Property(x => x.EvalPhysician2, map => map.Length(50));
			Property(x => x.SocialWorker1, map => map.Length(50));
			Property(x => x.SocialWorker2, map => map.Length(50));
			Property(x => x.Technician, map => map.Length(50));
			Property(x => x.TxPhysician1, map => map.Length(50));
			Property(x => x.TxPhysician2, map => map.Length(50));
			Property(x => x.ArchiveDonor, map => map.NotNullable(true));
			Property(x => x.CADDonor, map => map.NotNullable(true));
			Property(x => x.DonorType, map => map.Length(25));
			Property(x => x.NursePractitioner, map => map.Length(50));
			Property(x => x.Outreach);
			Property(x => x.OutreachLocation, map => map.Length(50));
			Property(x => x.Recipient, map => map.Length(50));
			Property(x => x.RecipientMRN, map => map.Length(15));
			Property(x => x.RecipientTxNum, map => map.Precision(10));
			Property(x => x.Relationship, map => map.Length(50));
			Property(x => x.EnteredBy, map => map.Length(15));
			Property(x => x.EnteredDate);
			Property(x => x.EnteredTime);
			Property(x => x.TenantID, map => map.NotNullable(true));
        }
    }
}
