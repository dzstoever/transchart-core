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


    public class CandidateMap : ClassMapping<Candidate>, IDbMap
    {
        
        public CandidateMap() {
			Schema("dbo");

            Id(x => x.MRN, map => { map.Column("MRN"); map.Generator(Generators.Assigned); map.Length(15); });
            

			//Property(x => x.MRN, map => map.NotNullable(true));
			Property(x => x.SSN);
            Property(x => x.DOB);
			Property(x => x.fn);
			Property(x => x.mi);
			Property(x => x.LN);
			Property(x => x.Sex);
			
        }
    }
}
