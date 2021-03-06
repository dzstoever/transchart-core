using NHibernate.Mapping.ByCode.Conformist;
using TC.Domain.Entities;

namespace TC.Maps.Entitites 
{        
    public class AccountResourceSecurityMap : ClassMapping<AccountResourceSecurity>, Zen.Data.IDbMap 
    {        
        public AccountResourceSecurityMap() 
        {
            Schema("dbo"); Table("AccountResourceSecurity");
            
            ComponentAsId(x => x.Id, compId =>
            {
                compId.Property(c => c.AccountType);
                compId.Property(c => c.AccounTId);
                compId.Property(c => c.ResourceType);
                compId.Property(c => c.ResourceID);
            });   
            
			Property(x => x.Authorized);
			Property(x => x.CreatedOn, map => map.NotNullable(true));
			Property(x => x.CreatedBy, map => { map.NotNullable(true); map.Length(15); });
        }
    }
}
