using System;

namespace TC.Utility.Domain 
{
    [Serializable]
    public class TtHLAId : NaturalKeyStringDateTime
    {
        public string MRN { get { return Key1; } set { Key1 = value; } }
        public DateTime LabDate { get { return Key2; } set { Key2 = value; } }
    }

    public class TtHLA 
    {
        //public virtual string MRN
        //{
        //    get { return Id.MRN; }
        //    set { Id.MRN = value; }
        //}

        public virtual TtHLAId Id { get; set; }

        public virtual string MRNUNOS { get; set; }
        public virtual string Method { get; set; }
        public virtual string A1 { get; set; }
        public virtual string A2 { get; set; }
        public virtual string B1 { get; set; }
        public virtual string B2 { get; set; }
        public virtual string C1 { get; set; }
        public virtual string C2 { get; set; }
        public virtual string DR1 { get; set; }
        public virtual string DR2 { get; set; }
        public virtual string DP1 { get; set; }
        public virtual string DP2 { get; set; }
        public virtual string DQ1 { get; set; }
        public virtual string DQ2 { get; set; }
        public virtual string BW4 { get; set; }
        public virtual string BW6 { get; set; }
        public virtual string DRW51 { get; set; }
        public virtual string DRW52 { get; set; }
        public virtual string DRW53 { get; set; }        
        public virtual string SerumID { get; set; }
        public virtual string method_11 { get; set; }
        public virtual string comment { get; set; }
        public virtual string HaploTypeMatch { get; set; }
        public virtual string DR52 { get; set; }
        public virtual string DQA1 { get; set; }
        public virtual string DQA2 { get; set; }
        public virtual string DR3B1 { get; set; }
        public virtual string DR3B2 { get; set; }
        public virtual string DR4B1 { get; set; }
        public virtual string DR4B2 { get; set; }

        //public virtual DateTime Entered_Date { get; set; }
        //public virtual string Entered_By { get; set; }
        //public virtual DateTime? enteredtime { get; set; }
        //public virtual System.Guid Tenant_ID { get; set; }

        //public virtual Patient Patient { get; set; }//0 -> 1        
        //public virtual Candidate Candidate { get; set; }//load manually...
        //public virtual ISet<Candidate> Candidates { get; set; }//0 -> many? 

        public virtual Person Person { get; set; }
        
    }
}
