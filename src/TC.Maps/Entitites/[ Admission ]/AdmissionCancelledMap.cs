using TC.Domain.Entities;

namespace TC.Maps.Entitites
{
    public class AdmissionCancelledMap : MultiEnteredByMap<AdmissionCancelled, AdmissionCancelledId>, Zen.Data.IDbMap
    {        
        public AdmissionCancelledMap() 
        { 
            Schema("dbo"); Table("AD_AdmissionCancelled_Multi"); 
            
            ComponentAsId(x => x.Id, compId =>
            {
                compId.Property(c => c.MRN);
                compId.Property(c => c.AdmitDate);                
            });            
			
            Property(x => x.DischDate);
			Property(x => x.OsuAdmit, map => map.NotNullable(true));
            Property(x => x.AdmitCanceled, map => map.Length(5));

            // map the .Admission property of this class
            OneToOne(o => o.Admission, m => // to the .Cancelled property of the other class
            {
                m.PropertyReference(typeof(Admission).GetProperty("Cancelled"));
                //m.Constrained(true); don't allow inserts without setting the .Admission property
            });
                
        }
    }
}
