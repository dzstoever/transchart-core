using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using NHibernate.Mapping;

namespace TC.Tools.DbUtility.Commands
{
    public class BuildDataTableCmd : BaseCmd
    {
        /// <summary>
        ///   Builds a data table from any sql select query
        ///   outputs: DataTable
        /// </summary>        
        public BuildDataTableCmd(string tableName, string cnnString, string selectQry)
        {
            DataTable = new DataTable(tableName);
            SqlCnnString = cnnString;
            SqlSelectQry = selectQry;
        }

        
        /// <summary>
        /// the table created by the input query and options
        /// </summary>
        public DataTable DataTable { get; private set; }

        /// <summary>
        /// set this prior to Run() to add the table to an existing dataset
        /// </summary>
        public DataSet DataSet { private get; set; }

        /// <summary>
        /// Database connection string
        /// </summary>
        public string SqlCnnString { private get; set; }

        /// <summary>
        /// Sql query to execute
        /// </summary>
        public string SqlSelectQry { private get; set; }

        /// <summary>
        /// save the table schema (.xsd)
        /// </summary>
        public string XsdSaveAsFullName { private get; set; }

        /// <summary>
        /// set this if you only need the table schema 
        /// </summary>
        public bool SchemaOnly { private get; set; }


        public override void Run()
        {
            //validate
            if (string.IsNullOrEmpty(SqlCnnString)) throw new ArgumentException("SqlCnnString is not set.");
            if (string.IsNullOrEmpty(SqlSelectQry)) throw new ArgumentException("SqlSelectQry is not set.");

            var sqlCnn = new SqlConnection(SqlCnnString);
            using (sqlCnn)
            {
                sqlCnn.Open();
                var sqlCmd = new SqlCommand(SqlSelectQry, sqlCnn);
                using (sqlCmd)
                {
                    using (var reader = SchemaOnly
                        ? sqlCmd.ExecuteReader(CommandBehavior.SchemaOnly)
                        : sqlCmd.ExecuteReader())
                    {
                        if (DataTable == null) DataTable = new DataTable();
                        DataTable.Load(reader);
                    }
                }

            }

            //add table to dataset if set
            if (DataSet != null) DataSet.Tables.Add(DataTable);
            //save the .xsd if specified
            if (!string.IsNullOrEmpty(XsdSaveAsFullName)) DataTable.WriteXmlSchema(XsdSaveAsFullName);
        }
    

        public int GetCount()
        {
            //validate
            if (string.IsNullOrEmpty(SqlCnnString)) throw new ArgumentException("SqlCnnString is not set.");

            int count = 0;
            var sqlCnn = new SqlConnection(SqlCnnString);
            using(sqlCnn)
            { 
                sqlCnn.Open();
                var sqlCmd = new SqlCommand("select count(*) from "+ DataTable.TableName, sqlCnn);
                using (sqlCmd)
                {
                    count = (int)sqlCmd.ExecuteScalar();
                }
            }
            return count;
        }

        public DataTable GetPage(int pageSize, int startAt, string[] columnNames)
        {
            //validate
            if (string.IsNullOrEmpty(SqlCnnString)) throw new ArgumentException("SqlCnnString is not set.");

            var dt = new DataTable();
            string pageQry =
                @"
                SELECT TOP ({0}) {1}
                FROM(	select *, ROW_NUMBER() OVER( order by CURRENT_TIMESTAMP) as SortRow 
			            from [{2}]) as query 
                WHERE query.SortRow > {3} 
                ORDER BY query.SortRow;            
            ";

            var columnList = columnNames.Select(col => "[" + col + "]").ToList();
            var columnCsv = ApplicationSettings.ListToString(columnList);
            var sqlQry = string.Format(pageQry, pageSize, columnCsv, DataTable.TableName, startAt);
            var sqlCnn = new SqlConnection(SqlCnnString);
            using (sqlCnn)
            {
                sqlCnn.Open();
                var sqlCmd = new SqlCommand(sqlQry, sqlCnn);
                using (sqlCmd)
                {
                    using (var reader = sqlCmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }

            return dt;
        }

    }
}