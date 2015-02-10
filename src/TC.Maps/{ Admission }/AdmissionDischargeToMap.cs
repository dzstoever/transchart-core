using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using TC.Domain;

namespace TC.Maps 
{
    public class AdmissionDischargeToMap : MultiEnteredByMap<AdmissionDischargeTo, AdmissionDischargeToId>, Zen.Data.IDbMap 
    {
        public AdmissionDischargeToMap()
        {
            Schema("dbo"); Table("AD_DischargeTo_Multi");
            
            ComponentAsId(x => x.Id, compId =>
            {
                compId.Property(c => c.MRN);
                compId.Property(c => c.DischargeTo);
                compId.Property(c => c.AdmitDate);
            });
			
        }
    }
}
