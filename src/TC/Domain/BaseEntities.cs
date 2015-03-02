using System;
using TC.Domain.Entities;
using Zen.Core;

namespace TC.Domain
{
    public abstract class MultiEntity<T> : DomainEntity<T>
    {
        public virtual Tenant Tenant { get; set; }
    }
 
    public abstract class EnteredByEntity<T> : DomainEntity<T>
    {
        public virtual DateTime? EnteredDate { get; set; }
        public virtual string EnteredTime { get; set; }
        public virtual string EnteredBy { get; set; }
    }
    
    public abstract class MultiEnteredByEntity<T> : MultiEntity<T>
    {
        public virtual DateTime? EnteredDate { get; set; }
        public virtual string EnteredTime { get; set; }
        public virtual string EnteredBy { get; set; }
    }

    

}
