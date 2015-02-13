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
    
    
    public class PatientMap : ClassMapping<Patient>, IDbMap
    {
        
        public PatientMap() {
			Schema("dbo");
			//Lazy(true);

            Id(x => x.MRN, map => { map.Column("MRN"); map.Generator(Generators.Assigned); map.Length(15); });
			//Property(x => x.MRN, map => map.NotNullable(true));
			Property(x => x.SSN);
            Property(x => x.DOB);
			Property(x => x.Last);
			Property(x => x.First);
			Property(x => x.Middle);			
			Property(x => x.Sex);			
        }
    }
}
