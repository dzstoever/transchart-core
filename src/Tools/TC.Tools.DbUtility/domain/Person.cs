using System;

namespace TC.Doman
{
    public class Person
    {
        public virtual string MRN { get; set; }
        public virtual string SSN { get; set; }
        public virtual DateTime? DOB { get; set; }
        public virtual string Last { get; set; }
        public virtual string First { get; set; }
        public virtual string Middle { get; set; }
        public virtual string Sex { get; set; } 
    }
}