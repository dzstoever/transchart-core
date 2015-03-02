using TC.Domain.Entities;

namespace TC.Maps.Entitites 
{
    public class AdmissionReasonsMap : MultiEnteredByMap<AdmissionReasons, AdmissionReasonsId>, Zen.Data.IDbMap 
    {
        public AdmissionReasonsMap() 
        {
            Schema("dbo"); Table("AD_Reasons_Multi");
            
            ComponentAsId(x => x.Id, compId =>
            {
                compId.Property(c => c.MRN);
                compId.Property(c => c.Reason);
                compId.Property(c => c.AdmitDate);
            });

            ManyToOne(x => x.Admission, m =>
            {
                m.Columns(c1 => c1.Name("MRN"), c2 => c2.Name("AdmitDate"));
                m.Class(typeof(Admission));                
                m.Insert(false); m.Update(false);                                
            });
        }
    }
}
