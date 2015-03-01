using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using TC.Domain;

namespace TC.Maps 
{
    public class AdmissionDischargeCancelledMap : MultiEnteredByMap<AdmissionDischargeCancelled, AdmissionDischargeCancelledId>, Zen.Data.IDbMap
    {
        public AdmissionDischargeCancelledMap()
        {
            Schema("dbo"); Table("AD_DischargeCancelled_Multi");
            
            ComponentAsId(x => x.Id, compId =>
            {
                compId.Property(c => c.MRN);
                compId.Property(c => c.AdmitDate);
            });

            Property(x => x.DischDate);
            Property(x => x.OSUAdmit, map => map.NotNullable(true));
            Property(x => x.AdmitCanceled, map => map.Length(5));

            // map the .Admission property of this class to the .DischargeCancelled property of the other class            
            OneToOne(o => o.Admission, m => // on the other end there is a many-to-one with a unique constraint
            {
                m.PropertyReference(typeof(Admission).GetProperty("DischargeCancelled"));
                //m.Constrained(true); don't allow inserts without setting the .Admission property
            });                
        }
    }
}
