//using System;
//using System.Collections.Generic;
//using Zen.Core;

//namespace TC.Doman 
//{    
//    public class Candidate : DomainEntity<string>
//    {
//        public virtual Person ToPerson()
//        {
//            return new Person()
//            {
//                MRN = this.MRN,
//                SSN = this.SSN,
//                DOB = this.DOB,
//                First = this.fn,
//                Middle = this.mi,
//                Last = this.LN,
//                Sex = this.Sex
//            };
//        }

//        public virtual string MRN { get { return Id; } set { Id = value; } }
//        public virtual string SSN { get; set; }
//        public virtual DateTime? DOB { get; set; }
//        public virtual string fn { get; set; }
//        public virtual string mi { get; set; }
//        public virtual string LN { get; set; }        
//        public virtual string Sex { get; set; }

//        //public virtual ISet<TtHLA> TtHLAs { get; set; }
        
//    }
//}
