using System;
using System.Collections.Generic;
using Zen.Core;

namespace TC.Doman
{
    public class Person : DomainEntity<string>
    {        
        public virtual string MRN { get { return Id; } set { Id = value; } }
        public virtual string SSN { get; set; }
        public virtual DateTime? DOB { get; set; }
        public virtual string Last { get; set; }
        public virtual string First { get; set; }
        public virtual string Middle { get; set; }
        public virtual string Sex { get; set; }        
    }
}