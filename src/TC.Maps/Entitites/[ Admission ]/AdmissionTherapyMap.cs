using TC.Domain.Entities;

namespace TC.Maps.Entitites 
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
