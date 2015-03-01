using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using TC.Domain;

namespace TC.Maps 
{
    public class AdmissionDischargeTherapyMap : MultiEnteredByMap<AdmissionDischargeTherapy, AdmissionDischargeTherapyId>, Zen.Data.IDbMap
    {
        public AdmissionDischargeTherapyMap()
        {
            Schema("dbo"); Table("AD_DischargeTherapy_Multi");
            
            ComponentAsId(x => x.Id, compId =>
            {
                compId.Property(c => c.MRN);
                compId.Property(c => c.Therapy);
                compId.Property(c => c.AdmitDate);
            });
        }
    }
}
