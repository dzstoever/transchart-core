namespace TC.Domain.Entities.SolidOrganTransplant
{
    public class TxEpisode : Episode //MultiEnteredByEntity<int>
    {        
        public virtual int? Organ { get; set; }                
    }
}
