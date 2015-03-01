using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using TC.Domain;

namespace TC.Maps 
{
    public class AdmissionTherapyMap : MultiEnteredByMap<AdmissionTherapy, AdmissionTherapyId>, Zen.Data.IDbMap 
    {
        public AdmissionTherapyMap() 
        {
            Schema("dbo"); Table("AD_Therapy_Multi");
            
            ComponentAsId(x => x.Id, compId =>
            {
                compId.Property(c => c.MRN);
                compId.Property(c => c.Therapy);
                compId.Property(c => c.AdmitDate);
            });
        }
    }
}
