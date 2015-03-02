namespace TC.Domain.Entities
{
    public class Episode : MultiEnteredByEntity<int>
    {
        //public virtual int ID { get { return Id; } set { Id = value; } }

        // DEPRECATE
        //public virtual int? EpisodeNum { get; set; }
        //public virtual bool? IsCurrent { get; set; }

        public virtual string MRN { get; set; }
        public virtual int EpisodeTypeId { get; set; }
        public virtual int EpisodeDetailId { get; set; } //RowId
        
        public virtual EpisodeReferral Referral { get; set; }

    }
}