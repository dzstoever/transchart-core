using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using TC.Domain;

namespace TC.Maps 
{
    public class AdmissionInvProceduresMap : MultiEnteredByMap<AdmissionInvProcedures, AdmissionInvProceduresId>, Zen.Data.IDbMap 
    {
        public AdmissionInvProceduresMap() 
        {
            Schema("dbo"); Table("AD_InvProcedures_Multi");
            
            ComponentAsId(x => x.Id, compId =>
            {
                compId.Property(c => c.MRN);
                compId.Property(c => c.Proced);
                compId.Property(c => c.AdmitDate);
            });
        }
    }
}
