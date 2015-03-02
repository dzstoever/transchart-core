using NHibernate.Mapping.ByCode;
using TC.Domain.Entities;
using Zen.Data;

namespace TC.Maps.Entitites 
{
    public class AdmissionBedHistoryMap : MultiEnteredByMap<AdmissionBedHistory, int>, IDbMap
    {
        public AdmissionBedHistoryMap()
        {
            Schema("dbo"); Table("AD_BedHistory_Multi");
						
            Id(x => x.Id, map => map.Generator(Generators.Identity));

            const string uniqueKeyName = "UniqueAdmissionBedHistory"; // a unique constraint...
            Property(x => x.MRN, map => { map.NotNullable(true); map.Length(15); map.UniqueKey(uniqueKeyName); });
            Property(x => x.AdmitDate, map => map.UniqueKey(uniqueKeyName));
			            
            Property(x => x.DischDate);
			Property(x => x.Service, map => map.Length(100));
			Property(x => x.Room, map => map.Length(50));
			Property(x => x.Action, map => map.Length(25));
			Property(x => x.EnteredDate);
			Property(x => x.EnteredTime);
			Property(x => x.EnteredBy, map => map.Length(15));
			Property(x => x.ICU);
			Property(x => x.Account, map => map.Length(20));

            ManyToOne(x => x.Admission, m =>
            {
                m.Columns(c1 => c1.Name("MRN"), c2 => c2.Name("AdmitDate"));
                m.Class(typeof(Admission));
                m.Insert(false); m.Update(false);
            });
			
        }
    }
}
