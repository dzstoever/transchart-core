using System;

namespace TC.Domain.Entities 
{
    //DbView
    public class WaitList : EnteredByEntity<string> 
    {
        //this isn't a real key/id, it's kind of a pseudo key for the view...
        public virtual string MRN { get { return Id; } set { Id = value; } }
        
        public virtual int OrganId { get; set; }        
        public virtual DateTime? WaitListDate { get; set; }
        public virtual string OrganDisplay { get; set; }
        public virtual string OrganCodes { get; set; }
        public virtual string Status { get; set; }
        public virtual string LN { get; set; }
        public virtual string FN { get; set; }        
        public virtual int? WaitListStatusId { get; set; }
        public virtual int? TxNum { get; set; }
        
        
    }
}
