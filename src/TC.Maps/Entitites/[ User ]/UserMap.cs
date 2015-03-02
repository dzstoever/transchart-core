using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using TC.Domain.Entities;

namespace TC.Maps.Entitites 
{
        
    public class UserMap : ClassMapping<User>, Zen.Data.IDbMap 
    {

        public UserMap() 
        {
            Schema("dbo"); Table("Users"); //Lazy(true);

            Id(x => x.Id,   map => { 
                            map.Generator(Generators.Assigned);
                            map.Column("UserName");
            });
            
            Property(x => x.Password, map => map.Length(100));
			Property(x => x.AccessLevel, map => map.Length(1));
			Property(x => x.FN, map => map.Length(25));
			Property(x => x.LN, map => map.Length(25));
			Property(x => x.SSN, map => map.Length(15));
			Property(x => x.GroupID, map => map.Length(30));
			Property(x => x.Email, map => map.Length(50));
			Property(x => x.GroupLevel, map => map.Length(1));
			Property(x => x.location, map => map.Length(10));
			Property(x => x.PID, map => map.Length(50));
			Property(x => x.ShowLab, map => map.Length(1));
			Property(x => x.DefaultApp, map => map.Length(4));
			Property(x => x.CadListAccess);
			Property(x => x.ttlabUser);
			Property(x => x.ApcoClient);
			Property(x => x.ExpirationDate);
			Property(x => x.PreAccessLevel, map => map.Length(1));
			Property(x => x.AccountDisable);
			Property(x => x.AccountTimeLocked);
			Property(x => x.LastFailedLogin);
			Property(x => x.FailedLogin, map => map.Precision(10));
			Property(x => x.PWDExpired);
			Property(x => x.PWDEnteredBy, map => map.Length(15));
			Property(x => x.PtAccessForOrgans, map => map.Length(50));
			Property(x => x.DialysisGroupId, map => map.Length(20));
			Property(x => x.WebAccess);
			Property(x => x.CATAccess);
			Property(x => x.CreateOrder);
			Property(x => x.WPhone, map => map.Length(50));
			Property(x => x.Pager, map => map.Length(50));
			Property(x => x.Department, map => map.Length(50));
			Property(x => x.Unit, map => map.Length(50));
			Property(x => x.CostCenter, map => map.Length(50));
			Property(x => x.Comments, map => map.Length(400));
			Property(x => x.TissueTypingAccess);
			Property(x => x.MobileAccess);
			Property(x => x.ColorTheme, map => map.Length(20));
			Property(x => x.FinancialUpdateAccess);
			Property(x => x.PIN, map => map.Length(30));
			Property(x => x.Credentials, map => map.Length(25));
			Property(x => x.LastLogin);
			Property(x => x.TenantConnectionStringID, map => { map.NotNullable(true); map.Precision(10); });

            ManyToOne(x => x.ConfigUserType, a =>
            {
                a.Class(typeof(ConfigUserType));
                a.Column("UserType");       // dbcolumn name in this table                
                a.PropertyRef("UserType");  // property name in the other class holding the FK value
                a.Fetch(FetchKind.Join);
                a.Cascade(Cascade.None);
                //Lazy(true);
                //a.Lazy(LazyRelation.Proxy);
                //a.Unique(true);
                
            });

        }
    }
}
