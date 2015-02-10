using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using TC.Domain;

namespace TC.Maps 
{        
    public class AccountResourceSecurityMap : ClassMapping<AccountResourceSecurity>, Zen.Data.IDbMap 
    {        
        public AccountResourceSecurityMap() 
        {
            Schema("dbo"); Table("AccountResourceSecurity");
            
            ComponentAsId(x => x.Id, compId =>
            {
                compId.Property(c => c.AccountType);
                compId.Property(c => c.AccountID);
                compId.Property(c => c.ResourceType);
                compId.Property(c => c.ResourceID);
            });   
            
			Property(x => x.Authorized);
			Property(x => x.CreatedOn, map => map.NotNullable(true));
			Property(x => x.CreatedBy, map => { map.NotNullable(true); map.Length(15); });
        }
    }
}
