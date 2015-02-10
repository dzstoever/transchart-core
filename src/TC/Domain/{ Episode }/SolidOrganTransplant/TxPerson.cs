namespace TC.Domain.SolidOrganTransplant
{
    public class TxPerson : Person
    {
        /* NOT MAPPED - build from EpisodeSet */
        // 'Status(Type-Phase-{Laterality}Discriminator) | ...'
        public virtual string MasterShortStatus { get; set; }   // Ex.  'Active(R-Eval-LK) | Active(R-WL-L) |...'         
        public virtual string MasterStatus { get; set; }        // Ex.  'Active(Recipient-Evaluation-LeftKidney) | Active(Recipient-WaitListed-Lung) |...'

        /* NOT MAPPED - build from EpisodeSet */
        // This is the most recent/highest TxNum (0 if never Transplanted) 
        // So a previous episode for this MRN might have a lower value, 
        // but in this case the Status would be 'Inactive'
        public virtual string MasterTxNum { get; set; }
    }
}