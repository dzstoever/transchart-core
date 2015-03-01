using System.Data;
using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace TC.Maps
{
    /// <summary>
    /// BinaryTimestamp implements the NHibernate BinaryType
    /// and can be used to handle versioning.
    /// </summary>
    /// <see cref="http://thesenilecoder.blogspot.com/2012/02/nhibernate-samples-row-versioning-with.html"/>    
    public class BinaryTimestamp : IUserVersionType
    {
        #region IUserVersionType Members

        public object Next(object current, ISessionImplementor session)
        {
            return current;
        }

        public object Seed(ISessionImplementor session)
        {
            return new byte[8];
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        /// <summary>
        /// The Assemble() method is called when an instance
        /// of the type is read from the second-level cache. 
        /// </summary>
        public object Assemble(object cached, object owner)
        {
            return DeepCopy(cached);
        }

        /// <summary>
        /// The Disassemble() method is called when an instance 
        /// of the type is written to the second-level cache. 
        /// </summary>        
        public object Disassemble(object value)
        {
            return DeepCopy(value);
        }


        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public bool IsMutable
        {
            get { return false; }
        }

        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            return rs.GetValue(rs.GetOrdinal(names[0]));
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            NHibernateUtil.Binary.NullSafeSet(cmd, value, index);
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public System.Type ReturnedType
        {
            get { return typeof(byte[]); }
        }

        public SqlType[] SqlTypes
        {
            get { return new SqlType[] { new SqlType(DbType.Binary) }; }
        }

        public int Compare(object x, object y)
        {
            byte[] xbytes = (byte[])x;
            byte[] ybytes = (byte[])y;
            if (xbytes.Length < ybytes.Length)
            {
                return -1;
            }
            if (xbytes.Length > ybytes.Length)
            {
                return 1;
            }
            for (int i = 0; i < xbytes.Length; i++)
            {
                if (xbytes[i] < ybytes[i])
                {
                    return -1;
                }
                if (xbytes[i] > ybytes[i])
                {
                    return 1;
                }
            }
            return 0;
        }

        bool IUserType.Equals(object x, object y)
        {
            return (x == y);
        }

        #endregion
    }


    /* not used
    public class LongVersionType : IUserVersionType, IEnhancedUserType
    {
        public object Next(object current, NHibernate.Engine.ISessionImplementor session)
        {
            return current;
        }

        public object Seed(NHibernate.Engine.ISessionImplementor session)
        {
            return 0;
        }

        public object Assemble(object cached, object owner)
        {
            return DeepCopy(cached);
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Disassemble(object value)
        {
            return DeepCopy(value);
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public bool IsMutable
        {
            get { return false; }
        }

        public object NullSafeGet(System.Data.IDataReader rs, string[] names, object owner)
        {
            return rs.GetValue(rs.GetOrdinal(names[0]));
        }

        public void NullSafeSet(System.Data.IDbCommand cmd, object value, int index)
        {
            NHibernateUtil.Binary.NullSafeSet(cmd, value, index);
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public Type ReturnedType
        {
            get { return typeof(long?); }
        }

        public NHibernate.SqlTypes.SqlType[] SqlTypes
        {
            get { return new SqlType[] { new SqlType(DbType.Int64) }; }
        }

        public int Compare(object x, object y)
        {
            return (x == y) ? 0 : -1;
        }

        bool IUserType.Equals(object x, object y)
        {
            return (x == y);
        }



        public object FromXMLString(string xml)
        {
            throw new NotImplementedException();
        }

        public string ObjectToSQLString(object value)
        {
            throw new NotImplementedException();
        }

        public string ToXMLString(object value)
        {
            throw new NotImplementedException();
        }
    }
    */
}
