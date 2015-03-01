using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using TC.Domain;

namespace TC.Maps
{
    public class TenantMap : ClassMapping<Tenant>, Zen.Data.IDbMap
    {
        public TenantMap()
        { 
            Schema("dbo"); Table("Tenant");
            
            // See summary for the TC.Maps.NativeGuidGeneratorDef class
            // We could also create the guids ourselves using Generators.Guid or GuidComb
            // Id(x => x.Id, map => map.Generator(Generators.NativeGuid));<-- missing in this release
            Id(x => x.Id, map => map.Generator(new NativeGuidGeneratorDef()));

            Property(x => x.TenantName, map => map.Length(200));
            Property(x => x.DefaultState, map => map.Length(25));
            Property(x => x.DefaultCounty, map => map.Length(200));
            Property(x => x.ReportHeader, map => map.Length(255));
            Property(x => x.Location, map => map.Length(50));
            Property(x => x.Country, map => map.Length(50));
            Property(x => x.LocalOPO, map => map.Length(50));
            Property(x => x.City, map => map.Length(50));
            Property(x => x.TxCenterCode, map => map.Length(50));
            Property(x => x.MRNLength, map => map.Precision(10));
        }
    }
}
