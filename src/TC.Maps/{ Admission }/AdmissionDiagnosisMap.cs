using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using TC.Domain;

namespace TC.Maps 
{
    public class AdmissionDiagnosisMap : MultiEnteredByMap<AdmissionDiagnosis, AdmissionDiagnosisId>, Zen.Data.IDbMap 
    {
        public AdmissionDiagnosisMap()
        {
            Schema("dbo"); Table("AD_Diagnosis_Multi");
            
            ComponentAsId(x => x.Id, compId =>
            {
                compId.Property(c => c.MRN);
                compId.Property(c => c.Diagnosis);
                compId.Property(c => c.AdmitDate);                                
            });

            Property(x => x.PrimaryDGNS, map => map.NotNullable(true));            
        }
    }
}
