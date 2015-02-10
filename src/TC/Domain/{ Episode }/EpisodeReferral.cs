using System;

namespace TC.Domain 
{    
    public class EpisodeReferral : MultiEnteredByEntity<int> 
    {        
        public virtual int? EpisodeId { get; set; }        
        public virtual int? ReferredFor { get; set; }
        public virtual DateTime? ReferralDate { get; set; }
        public virtual string MRN { get; set; }
        public virtual string GeneralDiagnosis { get; set; }
        public virtual string InsuranceType { get; set; }
        public virtual string PhysicianId { get; set; }
    }
}
