using System.Data.SqlClient;

namespace TC
{
    public class SqlParameterList : System.Collections.CollectionBase
    {
        /// <summary>
        /// Default public constructor
        /// </summary>
        public SqlParameterList()
            : base()
        {
        }

        /// <summary>
        /// Initializes list with parameters based on pairs of name and value. 
        /// <example>
        /// The following creates an SqlParameterList with two SqlParameter objects - one with name @ID and value obj.ID and one with name @Name and value obj.Name.
        /// <code>new SqlParameterList( "@ID", obj.ID, "@Name", obj.Name )</code>
        /// </example>
        /// </summary>
        /// <param name="list">Pairs of string and objects to create SqlParameterList</param>
        public SqlParameterList(params object[] list)
            : this()
        {
            SqlParameter[] prms = new SqlParameter[list.Length / 2];

            for (int i = 0; i < list.Length; i++)
            {
                if (i % 2 == 0)
                {
                    prms[i / 2] = new SqlParameter();
                    prms[i / 2].ParameterName = list[i].ToString();
                }
                else
                {
                    prms[i / 2].Value = list[i];
                }
            }
            this.Add(prms);
        }

        /// <summary>
        /// Returns an SqlParameter from the list
        /// </summary>
        /// <param name="index">index of SqlParameter to return</param>
        /// <returns>SqlParameter</returns>
        public SqlParameter this[int index]
        {
            get
            {
                return (SqlParameter)List[index];
            }
            set
            {
                List[index] = value;
            }
        }

        /// <summary>
        /// Adds an array of SqlParameters to the list
        /// </summary>
        /// <param name="list"></param>
        public void Add(SqlParameter[] list)
        {
            foreach (SqlParameter p in list)
                Add(p);
        }

        /// <summary>
        /// Adds an SqlParamter to the list
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Add(SqlParameter value)
        {
            List.Add(value);
            return List.Count;
        }

        /// <summary>
        /// Creates and adds an SqlParameter to the list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Add(string name, object value)
        {
            return Add(new SqlParameter(name, value));
        }

        /// <summary>
        /// Removes an SqlParameter from the list
        /// </summary>
        /// <param name="value"></param>
        public void Remove(SqlParameter value)
        {
            List.Remove(value);
        }

        /// <summary>
        /// Returns the index of an item in the list
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(SqlParameter value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        /// Inserts an item at a position in the list
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Insert(int index, SqlParameter value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        /// Determines if an object is in the list
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(SqlParameter value)
        {
            return List.Contains(value);
        }

        /// <summary>
        /// Converts the list to an array
        /// </summary>
        /// <returns></returns>
        public SqlParameter[] ToArray()
        {
            SqlParameter[] prms = new SqlParameter[List.Count];
            for (int i = 0; i < prms.Length; i++)
                prms[i] = (SqlParameter)List[i];
            return prms;
        }

        public static explicit operator SqlParameter[](SqlParameterList list)
        {
            return list.ToArray();
        }

    }
}