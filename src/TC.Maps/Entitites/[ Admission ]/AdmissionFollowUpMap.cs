using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using TC.Domain;

namespace TC.Maps 
{
    public class AdmissionFollowUpMap : MultiEnteredByMap<AdmissionFollowUp, AdmissionFollowUpId>, Zen.Data.IDbMap 
    {
        public AdmissionFollowUpMap() 
        {
            Schema("dbo"); Table("AD_Followup_Multi");
            
            ComponentAsId(x => x.Id, compId =>
            {
                compId.Property(c => c.MRN);
                compId.Property(c => c.FollowUp);
                compId.Property(c => c.AdmitDate);
            });
        }
    }
}
