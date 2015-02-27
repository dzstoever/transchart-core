using System;

namespace TC.Doman 
{
    [Serializable]
    public abstract class NaturalKeyStringDateTime
    {
        public NaturalKeyStringDateTime() { }
        public NaturalKeyStringDateTime(string key1, DateTime key2)
        {
            Key1 = key1;
            Key2 = key2;
        }

        public string Key1 { get; set; }
        public DateTime Key2 { get; set; }

        public override bool Equals(object o)
        {
            if (o == null) return false;
            if (object.ReferenceEquals(this, o)) return true;
            var id = o as NaturalKeyStringDateTime;
            if (id == null) return false;
            if (Key1 != id.Key1) return false;
            if (Key2 != id.Key2) return false;
            return true;
        }
        public override int GetHashCode()
        {
            return 37 * Key1.GetHashCode()
                + 37 * Key2.GetHashCode();
        }

        //test helper
        public static T GenForTest<T>() where T : NaturalKeyStringDateTime, new()
        {
            return new T()
            {
                Key1 = "s1",
                Key2 = DateTime.Now
            };
        }
    }

    [Serializable]
    public class TtId : NaturalKeyStringDateTime
    {
        public string MRN { get { return Key1; } set { Key1 = value; } }
        public DateTime LabDate { get { return Key2; } set { Key2 = value; } }
    }

    public class TtHLA {

        public virtual TtId Id { get; set; }
        //{
        //    get { return MRN; }
        //    set { MRN = value; }
        //}

   

        public virtual string MRNUNOS { get; set; }
        public virtual DateTime Lab_Date { get; set; }
        public virtual string MRN { get; set; }
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
        public virtual DateTime Entered_Date { get; set; }
        public virtual string Entered_By { get; set; }
        public virtual DateTime? enteredtime { get; set; }
        public virtual string Serum_ID { get; set; }
        public virtual string method_11 { get; set; }
        public virtual string comment { get; set; }
        public virtual string Haplo_Type_Match { get; set; }
        public virtual string DR52 { get; set; }
        public virtual string DQA1 { get; set; }
        public virtual string DQA2 { get; set; }
        public virtual string DR3B1 { get; set; }
        public virtual string DR3B2 { get; set; }
        public virtual string DR4B1 { get; set; }
        public virtual string DR4B2 { get; set; }
        public virtual System.Guid Tenant_ID { get; set; }
    }
}
