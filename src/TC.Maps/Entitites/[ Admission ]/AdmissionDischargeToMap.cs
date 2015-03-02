using TC.Domain.Entities;

namespace TC.Maps.Entitites 
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
