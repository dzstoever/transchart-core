using TC.Domain.Entities;

namespace TC.Maps.Entitites 
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
