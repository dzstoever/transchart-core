namespace TC.Domain.SolidOrganTransplant
{
    // NOTE: DbTableName = EpisodeDonor
    public class DonorEpisode : TxEpisode // MultiEnteredByEntity<int>
    {   // NOT NEEDED (1 to 1)
        //public virtual int ID { get; set; } //public virtual string MRN { get; set; }

        public virtual int? RecipientTxNum { get; set; }        
        public virtual string RecipientMRN { get; set; }
        public virtual string DonorType { get; set; }
        
        public DonorDetails Details { get; set; }
        public DonorCareTeam CareTeam { get; set; }
    }

    public class DonorDetails : TxEpisodeDetails
    {
        public virtual bool CadDonor { get; set; }
        public virtual bool ArchiveDonor { get; set; }

        public virtual string Recipient { get; set; }
        public virtual string Relationship { get; set; }
    }
    public class DonorCareTeam : CareTeam { }
}
