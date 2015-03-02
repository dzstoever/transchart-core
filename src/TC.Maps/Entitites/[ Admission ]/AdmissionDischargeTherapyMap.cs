using TC.Domain.Entities;

namespace TC.Maps.Entitites 
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
