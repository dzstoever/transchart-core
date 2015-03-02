using System;

namespace TC.Domain.Entities 
{    
    public class AdmissionBedHistory : MultiEnteredByEntity<int> 
    {
        public virtual Admission Admission { get; set; }

        public virtual string MRN { get; set; }
        public virtual DateTime? AdmitDate { get; set; } 
        
        public virtual DateTime? DischDate { get; set; }
        public virtual string Service { get; set; }
        public virtual string Room { get; set; }
        public virtual string Action { get; set; }
        public virtual bool? ICU { get; set; }
        public virtual string Account { get; set; }        
    }
}
