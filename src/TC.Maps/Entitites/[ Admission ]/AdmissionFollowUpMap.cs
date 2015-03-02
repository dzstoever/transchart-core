using TC.Domain.Entities;

namespace TC.Maps.Entitites 
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
