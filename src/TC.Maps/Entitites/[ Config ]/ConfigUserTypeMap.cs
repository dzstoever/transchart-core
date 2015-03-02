using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using TC.Domain.Entities;

namespace TC.Maps.Entitites 
{
        
    public class ConfigUserTypeMap : ClassMapping<ConfigUserType>, Zen.Data.IDbMap
    {
        
        public ConfigUserTypeMap() 
        {
            Schema("dbo"); Table("Config_UserType"); //Lazy(true);
			
			Id(x => x.Id, map => map.Generator(Generators.Identity));

            Property(x => x.UserType, map => { map.NotNullable(true); map.Length(25); });
			Property(x => x.Description, map => { map.NotNullable(true); map.Length(250); });
			Property(x => x.Enabled, map => map.NotNullable(true));
			
            Set(s => s.Users, a =>
            {
                a.Key(k =>
                {
                    k.Column("UserType");           // dbcolumn name in the other table
                    k.PropertyRef(p => p.UserType); // property in the other class                                                                                
                });
                a.Cascade(Cascade.None);
                a.Inverse(true);        // specify that the collection is a mirror image
            }, r => { r.OneToMany(); });// specify the association
            
            
        }
    }
}
