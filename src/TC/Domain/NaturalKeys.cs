using System;


namespace TC.Domain
{
    /* Natural Keys
     *     
     * As far as NHibernate is concerned, a composite/natural key may be handled as an 
     * assigned identifier of value type (the NHibernate type is a component).         
     * It’s critical that you implement Equals() and GetHashCode() correctly, because NHibernate    
     * uses these methods to do cache lookups. Furthermore, the hash code must be consistent over     
     * time. This means that if the column [XxxxXxxx] is case insensitive, it must be normalized     
     * (to uppercase/lowercase strings).
     *     
     * Composite key classes are also expected to be Serializable.
     * 
     * I have included some helper methods for testing purposes.
     */


    [Serializable]
    public abstract class NaturalKeyStringInt32
    {
        public NaturalKeyStringInt32() { }
        public NaturalKeyStringInt32(string key1, int key2)
        {
            Key1 = key1;
            Key2 = key2;
        }

        public string Key1 { get; set; }
        public int Key2 { get; set; }

        public override bool Equals(object o)
        {
            if (o == null) return false;
            if (object.ReferenceEquals(this, o)) return true;
            var id = o as NaturalKeyStringInt32;
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
        public static T GenForTest<T>() where T : NaturalKeyStringInt32, new()
        {
            return new T()
            {
                Key1 = "s1",
                Key2 = new Random(37).Next()
            };
        }
    }

    [Serializable]
    public abstract class NaturalKeyStringString
    {
        public NaturalKeyStringString() { }
        public NaturalKeyStringString(string key1, string key2)
        {
            Key1 = key1;
            Key2 = key2;
        }

        public string Key1 { get; set; }
        public string Key2 { get; set; }

        public override bool Equals(object o)
        {
            if (o == null) return false;
            if (object.ReferenceEquals(this, o)) return true;
            var id = o as NaturalKeyStringString;
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
        public static T GenForTest<T>() where T : NaturalKeyStringString, new()
        {
            return new T()
            {
                Key1 = "s1",
                Key2 = "s2"
            };
        }
    }

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
    public abstract class NaturalKeyStringStringDateTime
    {
        public NaturalKeyStringStringDateTime() { }
        public NaturalKeyStringStringDateTime(string key1, string key2, DateTime key3)
        {
            Key1 = key1;
            Key2 = key2;
            Key3 = key3;            
        }
        
        public string Key1 { get; set; }
        public string Key2 { get; set; }
        public DateTime Key3 { get; set; }        

        public override bool Equals(object o)
        {
            if (o == null) return false;
            if (object.ReferenceEquals(this, o)) return true;
            var id = o as NaturalKeyStringStringDateTime;
            if (id == null) return false;
            if (Key1 != id.Key1) return false;
            if (Key2 != id.Key2) return false;
            if (Key3 != id.Key3) return false;            

            return true;
        }
        public override int GetHashCode()
        {
            return 37 * Key1.GetHashCode()
                + 37 * Key2.GetHashCode()
                + 37 * Key3.GetHashCode();
        }

        //test helper
        public static T GenForTest<T>() where T : NaturalKeyStringStringDateTime, new()
        {
            return new T()
            {
                Key1 = "s1",
                Key2 = "s2",
                Key3 = DateTime.Now                
            };
        }                
    }

    [Serializable]
    public abstract class NaturalKeyStringStringStringString
    {
        public NaturalKeyStringStringStringString() { }
        public NaturalKeyStringStringStringString(string key1, string key2, string key3, string key4)
        {
            Key1 = key1;
            Key2 = key2;
            Key3 = key3;
            Key4 = key4;
        }

        public string Key1 { get; set; }
        public string Key2 { get; set; }
        public string Key3 { get; set; }
        public string Key4 { get; set; }


        public override bool Equals(object o)
        {
            if (o == null) return false;
            if (object.ReferenceEquals(this, o)) return true;
            var id = o as NaturalKeyStringStringStringString;
            if (id == null) return false;
            if (Key1 != id.Key1) return false;
            if (Key2 != id.Key2) return false;
            if (Key3 != id.Key3) return false;
            if (Key4 != id.Key4) return false;

            return true;
        }
        public override int GetHashCode()
        {
            return 37 * Key1.GetHashCode()
                + 37 * Key2.GetHashCode()
                + 37 * Key3.GetHashCode()
                + 37 * Key4.GetHashCode();
        }

        //test helper
        public static T GenForTest<T>() where T : NaturalKeyStringStringStringString, new()
        {
            return new T()
            {
                Key1 = "s1",
                Key2 = "s2",
                Key3 = "s3",
                Key4 = "s4"
            };
        }
    }

    [Serializable]
    public abstract class NaturalKeyStringDateTimeDateTimeInt32
    {
        public NaturalKeyStringDateTimeDateTimeInt32() { }
        public NaturalKeyStringDateTimeDateTimeInt32(string key1, DateTime key2, DateTime key3, Int32 key4)
        {
            Key1 = key1;
            Key2 = key2;
            Key3 = key3;
            Key4 = key4;
        }

        public string Key1 { get; set; }
        public DateTime Key2 { get; set; }
        public DateTime Key3 { get; set; }
        public Int32 Key4 { get; set; }

        public override bool Equals(object o)
        {
            if (o == null) return false;
            if (object.ReferenceEquals(this, o)) return true;
            var id = o as NaturalKeyStringDateTimeDateTimeInt32;
            if (id == null) return false;
            if (Key1 != id.Key1) return false;
            if (Key2 != id.Key2) return false;
            if (Key3 != id.Key3) return false;
            if (Key4 != id.Key4) return false;

            return true;
        }
        public override int GetHashCode()
        {
            return 37 * Key1.GetHashCode()
                + 37 * Key2.GetHashCode()
                + 37 * Key3.GetHashCode()
                + 37 * Key4.GetHashCode();
        }

        //test helper
        public static T GenForTest<T>() where T : NaturalKeyStringDateTimeDateTimeInt32, new()
        {
            return new T()
            {
                Key1 = "s1",
                Key2 = DateTime.Now,
                Key3 = DateTime.Now,
                Key4 = new Random(37).Next()
            };
        }
    }

    
    #region With TenanTId
    //[Serializable]
    //public class xNaturalKey_String_DateTime_DateTime_Int32_Guid
    //{        
    //    public xNaturalKey_String_DateTime_DateTime_Int32_Guid() { }//{ _key1 = default(String); } - can't have a db Id when instantiated - it has to be null for ORM! duh        
    //    public xNaturalKey_String_DateTime_DateTime_Int32_Guid(string key1, DateTime key2, DateTime key3, Int32 key4, Guid key5)
    //    {
    //        Key1 = key1;
    //        Key2 = key2;
    //        Key3 = key3;
    //        Key4 = key4;
    //        Key5 = key5;
    //    }
        
    //    public string   Key1 { get; set; }
    //    public DateTime Key2 { get; set; }
    //    public DateTime Key3 { get; set; }
    //    public Int32    Key4 { get; set; }
    //    public Guid?    Key5 { get; set; }


    //    public override bool Equals(object o)
    //    {
    //        if (o == null) return false;
    //        if (object.ReferenceEquals(this, o)) return true;
    //        var id = o as xNaturalKey_String_DateTime_DateTime_Int32_Guid;
    //        if (id == null) return false;
    //        if (Key1 != id.Key1) return false;
    //        if (Key2 != id.Key2) return false;
    //        if (Key3 != id.Key3) return false;
    //        if (Key4 != id.Key4) return false;
    //        if (Key5 != id.Key5) return false;

    //        return true;
    //    }

    //    public override int GetHashCode()
    //    {
    //        return 37 * Key1.GetHashCode()
    //            + 37 * Key2.GetHashCode()
    //            + 37 * Key3.GetHashCode()
    //            + 37 * Key4.GetHashCode()
    //            + 37 * Key5.GetHashCode();
    //    }

        
    //    //test helper
    //    public static T GenForTest<T>() where T : xNaturalKey_String_DateTime_DateTime_Int32_Guid, new()
    //    {
    //        return new T()
    //        {
    //            Key1 = "T123456789",
    //            Key2 = DateTime.Now,
    //            Key3 = DateTime.Now,
    //            Key4 = 0,
    //            Key5 = null
    //        };
    //    }
    //}

    //[Serializable]
    //public class xNaturalKey_String_DateTime_Guid
    //{
    //    public xNaturalKey_String_DateTime_Guid() { }
    //    public xNaturalKey_String_DateTime_Guid(string key1, DateTime key2, Guid? key3)
    //    {
    //        Key1 = key1;
    //        Key2 = key2;
    //        Key3 = key3;            
    //    }

    //    public string Key1 { get; set; }
    //    public DateTime Key2 { get; set; }
    //    public Guid? Key3 { get; set; }


    //    public override bool Equals(object o)
    //    {
    //        if (o == null) return false;
    //        if (object.ReferenceEquals(this, o)) return true;
    //        var id = o as xNaturalKey_String_DateTime_Guid;
    //        if (id == null) return false;
    //        if (Key1 != id.Key1) return false;
    //        if (Key2 != id.Key2) return false;
    //        if (Key3 != id.Key3) return false;            
    //        return true;
    //    }

    //    public override int GetHashCode()
    //    {
    //        return 37 * Key1.GetHashCode()
    //            + 37 * Key2.GetHashCode()
    //            + 37 * Key3.GetHashCode();               
    //    }


    //    //test helper
    //    public static T GenForTest<T>() where T : xNaturalKey_String_DateTime_Guid, new()
    //    {
    //        return new T()
    //        {
    //            Key1 = "T123456789",
    //            Key2 = DateTime.Now,
    //            Key3 = null
    //        };
    //    }
    //}
    #endregion
    
}
