using TC.Domain.Entities;

namespace TC.Maps.Entitites 
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
