//using System;
//using Zen.Core;

//namespace TC.Doman 
//{    
//    public class Patient : DomainEntity<string>
//    {
//        public virtual Person ToPerson()
//        {
//            return new Person()
//            {
//                MRN = this.MRN,
//                SSN = this.SSN,
//                DOB = this.DOB,
//                First = this.First,
//                Middle = this.Middle,
//                Last = this.Last,
//                Sex = this.Sex
//            };
//        }

//        public virtual string MRN { get { return Id; } set { Id = value; } }
//        public virtual string SSN { get; set; }
//        public virtual DateTime? DOB { get; set; }
//        public virtual string Last { get; set; }
//        public virtual string First { get; set; }
//        public virtual string Middle { get; set; }        
//        public virtual string Sex { get; set; }        

//    }
//}
