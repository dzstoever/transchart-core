using System;

namespace TC.Utility.Domain
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
    public abstract class NaturalKeyStringStringStringDateTime
    {
        public NaturalKeyStringStringStringDateTime() { }
        public NaturalKeyStringStringStringDateTime(string key1, string key2, string key3, DateTime key4)
        {
            Key1 = key1;
            Key2 = key2;
            Key3 = key3;
            Key4 = key4;            
        }

        public string Key1 { get; set; }
        public string Key2 { get; set; }
        public string Key3 { get; set; }
        public DateTime Key4 { get; set; }

        public override bool Equals(object o)
        {
            if (o == null) return false;
            if (object.ReferenceEquals(this, o)) return true;
            var id = o as NaturalKeyStringStringStringDateTime;
            if (id == null) return false;
            if (Key1 != id.Key1) return false;
            if (Key2 != id.Key2) return false;
            if (Key3 != id.Key3) return false;
            if (Key4 != id.Key4) return false;            
            return true;
        }
        public override int GetHashCode()
        {
            return 37*Key1.GetHashCode()
                   + 37*Key2.GetHashCode()
                   + 37*Key3.GetHashCode()
                   + 37*Key4.GetHashCode();
        }

        //test helper
        public static T GenForTest<T>() where T : NaturalKeyStringStringStringDateTime, new()
        {
            return new T()
            {
                Key1 = "s1",
                Key2 = "s2",
                Key3 = "s3",
                Key4 = DateTime.Now
            };
        }
    }

    [Serializable]
    public abstract class NaturalKeyStringStringStringStringStringDateTime
    {
        public NaturalKeyStringStringStringStringStringDateTime() { }
        public NaturalKeyStringStringStringStringStringDateTime(string key1, string key2, string key3, string key4, string key5, DateTime key6)
        {
            Key1 = key1;
            Key2 = key2;
            Key3 = key3;
            Key4 = key4;
            Key5 = key5;
            Key6 = key6;
        }

        public string Key1 { get; set; }
        public string Key2 { get; set; }
        public string Key3 { get; set; }
        public string Key4 { get; set; }
        public string Key5 { get; set; }
        public DateTime Key6 { get; set; }

        public override bool Equals(object o)
        {
            if (o == null) return false;
            if (object.ReferenceEquals(this, o)) return true;
            var id = o as NaturalKeyStringStringStringStringStringDateTime;
            if (id == null) return false;
            if (Key1 != id.Key1) return false;
            if (Key2 != id.Key2) return false;
            if (Key3 != id.Key3) return false;
            if (Key4 != id.Key4) return false;
            if (Key5 != id.Key5) return false;
            if (Key6 != id.Key6) return false;
            return true;
        }
        public override int GetHashCode()
        {
            return 37 * Key1.GetHashCode()
                    + 37 * Key2.GetHashCode()
                    + 37 * Key3.GetHashCode()
                    + 37 * Key4.GetHashCode()
                    + 37 * Key5.GetHashCode()
                    + 37 * Key6.GetHashCode();
        }

        //test helper
        public static T GenForTest<T>() where T : NaturalKeyStringStringStringStringStringDateTime, new()
        {
            return new T()
            {
                Key1 = "s1",
                Key2 = "s2",
                Key3 = "s3",
                Key4 = "s4",
                Key5 = "s5",
                Key6 = DateTime.Now
            };
        }
    }
}