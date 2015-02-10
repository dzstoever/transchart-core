using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using TC.Domain;


namespace TC.Maps 
{
        
    public class AuditUserAccessMap : MultiMap<AuditUserAccess, int>, Zen.Data.IDbMap 
    {
        
        public AuditUserAccessMap() 
        {
            Schema("dbo"); Table("Audit_UserAccess");

            Id(x => x.Id, map => map.Generator(Generators.Identity));
            
			Property(x => x.UserName, map => map.Length(35));
			Property(x => x.MRN, map => map.Length(35));
			Property(x => x.AccessDate);
			Property(x => x.AccessTime);
			Property(x => x.PageAccessed, map => map.Length(200));
			Property(x => x.PageNumber, map => map.Length(200));
			Property(x => x.RemoteMachine, map => map.Length(200));
            Property(x => x.HttpUserAgent, map => { map.Column("http_UserAgent"); map.Length(400); });
			Property(x => x.SessionID, map => map.Length(200));
			Property(x => x.AppName, map => map.Length(10));
			Property(x => x.ServerName, map => map.Length(200));
			Property(x => x.QueryString);
			
        }
    }
}
