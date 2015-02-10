using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Serialization;

namespace TC
{
    public class SqlHelperParameterCache
    {
        #region private methods, variables, and constructors

        //Since this class provides only static methods, make the default constructor private to prevent 
        //instances from being created with "new SqlHelperParameterCache()"
        private SqlHelperParameterCache()
        {
        }

        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// Resolve at run time the appropriate set of SqlParameters for a stored procedure
        /// </summary>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">Whether or not to include their return value parameter</param>
        /// <returns>The parameter array discovered.</returns>
        private static SqlParameter[] DiscoverSpParameterSet(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            SqlCommand cmd = new SqlCommand(spName, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlCommandBuilder.DeriveParameters(cmd);
            connection.Close();

            if (!includeReturnValueParameter)
            {
                cmd.Parameters.RemoveAt(0);
            }

            SqlParameter[] discoveredParameters = new SqlParameter[cmd.Parameters.Count];

            cmd.Parameters.CopyTo(discoveredParameters, 0);

            // Init the parameters with a DBNull value
            foreach (SqlParameter discoveredParameter in discoveredParameters)
            {
                discoveredParameter.Value = DBNull.Value;
            }
            return discoveredParameters;
        }

        /// <summary>
        /// Deep copy of cached SqlParameter array
        /// </summary>
        /// <param name="originalParameters"></param>
        /// <returns></returns>
        private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
        {
            SqlParameter[] clonedParameters = new SqlParameter[originalParameters.Length];

            for (int i = 0, j = originalParameters.Length; i < j; i++)
            {
                clonedParameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();
            }

            return clonedParameters;
        }

        #endregion private methods, variables, and constructors

        #region caching functions

        /// <summary>
        /// Add parameter array to the cache
        /// </summary>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParamters to be cached</param>
        public static void CacheParameterSet(string connectionString, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");

            string hashKey = connectionString + ":" + commandText;

            paramCache[hashKey] = commandParameters;
        }

        /// <summary>
        /// Retrieve a parameter array from the cache
        /// </summary>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An array of SqlParamters</returns>
        public static SqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");

            string hashKey = connectionString + ":" + commandText;

            SqlParameter[] cachedParameters = paramCache[hashKey] as SqlParameter[];
            if (cachedParameters == null)
            {
                return null;
            }
            else
            {
                return CloneParameters(cachedParameters);
            }
        }

        #endregion caching functions

        #region Parameter Discovery Functions

        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <returns>An array of SqlParameters</returns>
        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName)
        {
            return GetSpParameterSet(connectionString, spName, false);
        }

        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">A bool value indicating whether the return value parameter should be included in the results</param>
        /// <returns>An array of SqlParameters</returns>
        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return GetSpParameterSetInternal(connection, spName, includeReturnValueParameter);
            }
        }

        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <returns>An array of SqlParameters</returns>
        internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName)
        {
            return GetSpParameterSet(connection, spName, false);
        }

        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">A bool value indicating whether the return value parameter should be included in the results</param>
        /// <returns>An array of SqlParameters</returns>
        internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            using (SqlConnection clonedConnection = (SqlConnection)((ICloneable)connection).Clone())
            {
                return GetSpParameterSetInternal(clonedConnection, spName, includeReturnValueParameter);
            }
        }

        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">A bool value indicating whether the return value parameter should be included in the results</param>
        /// <returns>An array of SqlParameters</returns>
        private static SqlParameter[] GetSpParameterSetInternal(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            string hashKey = connection.ConnectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");

            SqlParameter[] cachedParameters;

            cachedParameters = paramCache[hashKey] as SqlParameter[];
            if (cachedParameters == null)
            {
                SqlParameter[] spParameters = DiscoverSpParameterSet(connection, spName, includeReturnValueParameter);
                paramCache[hashKey] = spParameters;
                cachedParameters = spParameters;
            }

            return CloneParameters(cachedParameters);
        }

        #endregion Parameter Discovery Functions

    }

    public class SqlHelper
    {
        public static SqlTransaction CreateTransaction(SqlConnection connection)
        {
            SqlTransaction transaction = connection.BeginTransaction();
            return transaction;
        }

        public static SqlTransaction CreateTransaction(string connectionString)
        {
            return CreateTransaction(CreateConnection(connectionString));
        }

        public static SqlConnection CreateConnection(string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        #region private utility methods & constructors

        // Since this class provides only static methods, make the default constructor private to prevent 
        // instances from being created with "new SqlHelper()"
        private SqlHelper()
        {
        }

        /// <summary>
        /// This method is used to attach array of SqlParameters to a SqlCommand.
        /// 
        /// This method will assign a value of DbNull to any parameter with a direction of
        /// InputOutput and a value of null.  
        /// 
        /// This behavior will prevent default values from being used, but
        /// this will be the less common case than an intended pure output parameter (derived as InputOutput)
        /// where the user provided no input value.
        /// </summary>
        /// <param name="command">The command to which the parameters will be added</param>
        /// <param name="commandParameters">An array of SqlParameters to be added to command</param>
        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null)
                throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        /// <summary>
        /// This method assigns dataRow column values to an array of SqlParameters
        /// </summary>
        /// <param name="commandParameters">Array of SqlParameters to be assigned values</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values</param>
        private static void AssignParameterValues(SqlParameter[] commandParameters, DataRow dataRow)
        {
            if ((commandParameters == null) || (dataRow == null))
            {
                // Do nothing if we get no data
                return;
            }

            int i = 0;
            // Set the parameters values
            foreach (SqlParameter commandParameter in commandParameters)
            {
                // Check the parameter name
                if (commandParameter.ParameterName == null ||
                    commandParameter.ParameterName.Length <= 1)
                    throw new Exception(
                        string.Format(
                            "Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: '{1}'.",
                            i, commandParameter.ParameterName));
                if (dataRow.Table.Columns.IndexOf(commandParameter.ParameterName.Substring(1)) != -1)
                    commandParameter.Value = dataRow[commandParameter.ParameterName.Substring(1)];
                i++;
            }
        }

        /// <summary>
        /// This method assigns an array of values to an array of SqlParameters
        /// </summary>
        /// <param name="commandParameters">Array of SqlParameters to be assigned values</param>
        /// <param name="parameterValues">Array of objects holding the values to be assigned</param>
        private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters == null) || (parameterValues == null))
            {
                // Do nothing if we get no data
                return;
            }

            // We must have the same number of values as we pave parameters to put them in
            if (commandParameters.Length != parameterValues.Length)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            }

            // Iterate through the SqlParameters, assigning the values from the corresponding position in the 
            // value array
            for (int i = 0, j = commandParameters.Length; i < j; i++)
            {
                // If the current array value derives from IDbDataParameter, then assign its Value property
                if (parameterValues[i] is IDbDataParameter)
                {
                    IDbDataParameter paramInstance = (IDbDataParameter)parameterValues[i];
                    if (paramInstance.Value == null)
                    {
                        commandParameters[i].Value = DBNull.Value;
                    }
                    else
                    {
                        commandParameters[i].Value = paramInstance.Value;
                    }
                }
                else if (parameterValues[i] == null)
                {
                    commandParameters[i].Value = DBNull.Value;
                }
                else
                {
                    commandParameters[i].Value = parameterValues[i];
                }
            }
        }

        /// <summary>
        /// This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
        /// to the provided command
        /// </summary>
        /// <param name="command">The SqlCommand to be prepared</param>
        /// <param name="connection">A valid SqlConnection, on which to execute this command</param>
        /// <param name="transaction">A valid SqlTransaction, or 'null'</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters to be associated with the command or 'null' if no parameters are required</param>
        /// <param name="mustCloseConnection"><c>true</c> if the connection was opened by the method, otherwose is false.</param>
        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null)
                throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            // Associate the connection with the command
            command.Connection = connection;

            // Set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            // If we were provided a transaction, assign it
            if (transaction != null)
            {
                if (transaction.Connection == null)
                    throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }

            // Set the command type
            command.CommandType = commandType;

            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }

        #endregion private utility methods & constructors

        #region ExecuteNonQuery

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset and takes no parameters) against the database specified in 
        /// the connection string
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteNonQuery(connectionString, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");

            // Create & open a SqlConnection, and dispose of it after we are done
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                try
                {
                    // Call the overload that takes a connection in place of the connection string
                    return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns no resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="spName">The name of the stored prcedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided SqlConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteNonQuery(connection, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the specified SqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 90;
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

            // Finally, execute the command
            int retval = cmd.ExecuteNonQuery();

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return retval;
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified SqlConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided SqlTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteNonQuery(transaction, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the specified SqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 90;
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

            // Finally, execute the command
            int retval = cmd.ExecuteNonQuery();

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified 
        /// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, trans, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteNonQuery

        #region ExecuteDataset

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteDataset(connectionString, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");

            // Create & open a SqlConnection, and dispose of it after we are done
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                return ExecuteDataset(connection, commandType, commandText, commandParameters);
            }
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(connString, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(string connectionString, string spName, params object[] parameterValues)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteDataset(connection, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 90;
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

            // Create the DataAdapter & DataSet
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();

                // Fill the DataSet using default values for DataTable names, etc
                da.Fill(ds);

                // Detach the SqlParameters from the command object, so they can be used again
                cmd.Parameters.Clear();

                if (mustCloseConnection)
                    connection.Close();

                // Return the dataset
                return ds;
            }
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteDataset(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteDataset(transaction, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 90;
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

            // Create the DataAdapter & DataSet
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();

                // Fill the DataSet using default values for DataTable names, etc
                da.Fill(ds);

                // Detach the SqlParameters from the command object, so they can be used again
                cmd.Parameters.Clear();

                // Return the dataset
                return ds;
            }
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified 
        /// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteDataset

        #region ExecuteReader

        /// <summary>
        /// This enum is used to indicate whether the connection was provided by the caller, or created by SqlHelper, so that
        /// we can set the appropriate CommandBehavior when calling ExecuteReader()
        /// </summary>
        private enum SqlConnectionOwnership
        {
            /// <summary>Connection is owned and managed by SqlHelper</summary>
            Internal,
            /// <summary>Connection is owned and managed by the caller</summary>
            External
        }

        /// <summary>
        /// Create and prepare a SqlCommand, and call ExecuteReader with the appropriate CommandBehavior.
        /// </summary>
        /// <remarks>
        /// If we created and opened the connection, we want the connection to be closed when the DataReader is closed.
        /// 
        /// If the caller provided the connection, we want to leave it to them to manage.
        /// </remarks>
        /// <param name="connection">A valid SqlConnection, on which to execute this command</param>
        /// <param name="transaction">A valid SqlTransaction, or 'null'</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters to be associated with the command or 'null' if no parameters are required</param>
        /// <param name="connectionOwnership">Indicates whether the connection parameter was provided by the caller, or created by SqlHelper</param>
        /// <returns>SqlDataReader containing the results of the command</returns>
        private static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, SqlConnectionOwnership connectionOwnership)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            bool mustCloseConnection = false;
            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 90;
            try
            {
                PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

                // Create a reader
                SqlDataReader dataReader;

                // Call ExecuteReader with the appropriate CommandBehavior
                if (connectionOwnership == SqlConnectionOwnership.External)
                {
                    dataReader = cmd.ExecuteReader();
                }
                else
                {
                    dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }

                // Detach the SqlParameters from the command object, so they can be used again.
                // HACK: There is a problem here, the output parameter values are fletched 
                // when the reader is closed, so if the parameters are detached from the command
                // then the SqlReader can´t set its values. 
                // When this happen, the parameters can´t be used again in other command.
                bool canClear = true;
                foreach (SqlParameter commandParameter in cmd.Parameters)
                {
                    if (commandParameter.Direction != ParameterDirection.Input)
                        canClear = false;
                }

                if (canClear)
                {
                    cmd.Parameters.Clear();
                }

                return dataReader;
            }
            catch
            {
                if (mustCloseConnection)
                    connection.Close();
                throw;
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteReader(connectionString, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();

                // Call the private overload that takes an internally owned connection in place of the connection string
                return ExecuteReader(connection, null, commandType, commandText, commandParameters, SqlConnectionOwnership.Internal);
            }
            catch
            {
                // If we fail to return the SqlDatReader, we need to close the connection ourselves
                if (connection != null)
                    connection.Close();
                throw;
            }

        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(connString, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteReader(connection, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            // Pass through the call to the private overload using a null transaction value and an externally owned connection
            return ExecuteReader(connection, (SqlTransaction)null, commandType, commandText, commandParameters, SqlConnectionOwnership.External);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);

                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteReader(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteReader(transaction, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///   SqlDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // Pass through to private overload, indicating that the connection is owned by the caller
            return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, SqlConnectionOwnership.External);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified
        /// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);

                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteReader

        #region ExecuteScalar

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteScalar(connectionString, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            // Create & open a SqlConnection, and dispose of it after we are done
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                return ExecuteScalar(connection, commandType, commandText, commandParameters);
            }
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(connString, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(string connectionString, string spName, params object[] parameterValues)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided SqlConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteScalar(connection, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 90;

            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

            // Execute the command & return the results
            object retval = cmd.ExecuteScalar();

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();

            if (mustCloseConnection)
                connection.Close();

            return retval;
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided SqlTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteScalar(transaction, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 90;
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

            // Execute the command & return the results
            object retval = cmd.ExecuteScalar();

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified
        /// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(trans, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // PPull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteScalar

        #region ExecuteXmlReader

        public static XmlReader ExecuteXmlReader(string connectionString, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteXmlReader(connectionString, commandType, commandText, (SqlParameter[])null);
        }

        public static XmlReader ExecuteXmlReader(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            // Create & open a SqlConnection, and dispose of it after we are done
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                return ExecuteXmlReader(connection, commandType, commandText, commandParameters);
            }
        }

        public static XmlReader ExecuteXmlReader(string connectionString, string spName, params object[] parameterValues)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteXmlReader(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteXmlReader(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteXmlReader(connection, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            bool mustCloseConnection = false;
            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 90;
            try
            {
                PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

                // Create the DataAdapter & DataSet
                XmlReader retval = cmd.ExecuteXmlReader();

                // Detach the SqlParameters from the command object, so they can be used again
                cmd.Parameters.Clear();

                return retval;
            }
            catch
            {
                if (mustCloseConnection)
                    connection.Close();
                throw;
            }
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="spName">The name of the stored procedure using "FOR XML AUTO"</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        public static XmlReader ExecuteXmlReader(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteXmlReader(transaction, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 90;
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

            // Create the DataAdapter & DataSet
            XmlReader retval = cmd.ExecuteXmlReader();

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified 
        /// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteXmlReader

        #region FillDataset
        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(connString, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"});
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)</param>
        public static void FillDataset(string connectionString, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (dataSet == null)
                throw new ArgumentNullException("dataSet");

            // Create & open a SqlConnection, and dispose of it after we are done
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                FillDataset(connection, commandType, commandText, dataSet, tableNames);
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(connString, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        public static void FillDataset(string connectionString, CommandType commandType,
            string commandText, DataSet dataSet, string[] tableNames,
            params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (dataSet == null)
                throw new ArgumentNullException("dataSet");
            // Create & open a SqlConnection, and dispose of it after we are done
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                FillDataset(connection, commandType, commandText, dataSet, tableNames, commandParameters);
            }
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  FillDataset(connString, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, 24);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>    
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        public static void FillDataset(string connectionString, string spName,
            DataSet dataSet, string[] tableNames,
            params object[] parameterValues)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (dataSet == null)
                throw new ArgumentNullException("dataSet");
            // Create & open a SqlConnection, and dispose of it after we are done
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                FillDataset(connection, spName, dataSet, tableNames, parameterValues);
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(conn, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"});
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>    
        public static void FillDataset(SqlConnection connection, CommandType commandType,
            string commandText, DataSet dataSet, string[] tableNames)
        {
            FillDataset(connection, commandType, commandText, dataSet, tableNames, null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(conn, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        public static void FillDataset(SqlConnection connection, CommandType commandType,
            string commandText, DataSet dataSet, string[] tableNames,
            params SqlParameter[] commandParameters)
        {
            FillDataset(connection, null, commandType, commandText, dataSet, tableNames, commandParameters);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  FillDataset(conn, "GetOrders", ds, new string[] {"orders"}, 24, 36);
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        public static void FillDataset(SqlConnection connection, string spName,
            DataSet dataSet, string[] tableNames,
            params object[] parameterValues)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (dataSet == null)
                throw new ArgumentNullException("dataSet");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames);
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(trans, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"});
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        public static void FillDataset(SqlTransaction transaction, CommandType commandType,
            string commandText,
            DataSet dataSet, string[] tableNames)
        {
            FillDataset(transaction, commandType, commandText, dataSet, tableNames, null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(trans, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        public static void FillDataset(SqlTransaction transaction, CommandType commandType,
            string commandText, DataSet dataSet, string[] tableNames,
            params SqlParameter[] commandParameters)
        {
            FillDataset(transaction.Connection, transaction, commandType, commandText, dataSet, tableNames, commandParameters);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified 
        /// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  FillDataset(trans, "GetOrders", ds, new string[]{"orders"}, 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        public static void FillDataset(SqlTransaction transaction, string spName,
            DataSet dataSet, string[] tableNames,
            params object[] parameterValues)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (dataSet == null)
                throw new ArgumentNullException("dataSet");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames);
            }
        }

        /// <summary>
        /// Private helper method that execute a SqlCommand (that returns a resultset) against the specified SqlTransaction and SqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(conn, trans, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        private static void FillDataset(SqlConnection connection, SqlTransaction transaction, CommandType commandType,
            string commandText, DataSet dataSet, string[] tableNames,
            params SqlParameter[] commandParameters)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (dataSet == null)
                throw new ArgumentNullException("dataSet");

            // Create a command and prepare it for execution
            SqlCommand command = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

            // Create the DataAdapter & DataSet
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
            {

                // Add the table mappings specified by the user
                if (tableNames != null && tableNames.Length > 0)
                {
                    string tableBase = "Table";
                    string tableName = tableBase;
                    for (int index = 0; index < tableNames.Length; index++)
                    {
                        if (tableNames[index] == null || tableNames[index].Length == 0)
                            throw new ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames");
                        dataAdapter.TableMappings.Add(tableName, tableNames[index]);
                        tableName = tableBase + (index + 1).ToString();
                    }
                }

                // Fill the DataSet using default values for DataTable names, etc
                dataAdapter.Fill(dataSet);

                // Detach the SqlParameters from the command object, so they can be used again
                command.Parameters.Clear();
            }

            if (mustCloseConnection)
                connection.Close();
        }
        #endregion

        #region UpdateDataset
        /// <summary>
        /// Executes the respective command for each inserted, updated, or deleted row in the DataSet.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  UpdateDataset(conn, insertCommand, deleteCommand, updateCommand, dataSet, "Order");
        /// </remarks>
        /// <param name="insertCommand">A valid transact-SQL statement or stored procedure to insert new records into the data source</param>
        /// <param name="deleteCommand">A valid transact-SQL statement or stored procedure to delete records from the data source</param>
        /// <param name="updateCommand">A valid transact-SQL statement or stored procedure used to update records in the data source</param>
        /// <param name="dataSet">The DataSet used to update the data source</param>
        /// <param name="tableName">The DataTable used to update the data source.</param>
        public static void UpdateDataset(SqlCommand insertCommand, SqlCommand deleteCommand, SqlCommand updateCommand, DataSet dataSet, string tableName)
        {
            if (insertCommand == null)
                throw new ArgumentNullException("insertCommand");
            if (deleteCommand == null)
                throw new ArgumentNullException("deleteCommand");
            if (updateCommand == null)
                throw new ArgumentNullException("updateCommand");
            if (tableName == null || tableName.Length == 0)
                throw new ArgumentNullException("tableName");

            // Create a SqlDataAdapter, and dispose of it after we are done
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                // Set the data adapter commands
                dataAdapter.UpdateCommand = updateCommand;
                dataAdapter.InsertCommand = insertCommand;
                dataAdapter.DeleteCommand = deleteCommand;

                // Update the dataset changes in the data source
                dataAdapter.Update(dataSet, tableName);

                // Commit all the changes made to the DataSet
                dataSet.AcceptChanges();
            }
        }
        #endregion

        #region CreateCommand
        /// <summary>
        /// Simplify the creation of a Sql command object by allowing
        /// a stored procedure and optional parameters to be provided
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlCommand command = CreateCommand(conn, "AddCustomer", "CustomerID", "CustomerName");
        /// </remarks>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="sourceColumns">An array of string to be assigned as the source columns of the stored procedure parameters</param>
        /// <returns>A valid SqlCommand object</returns>
        public static SqlCommand CreateCommand(SqlConnection connection, string spName, params string[] sourceColumns)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // Create a SqlCommand
            SqlCommand cmd = new SqlCommand(spName, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            // If we receive parameter values, we need to figure out where they go
            if ((sourceColumns != null) && (sourceColumns.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);

                // Assign the provided source columns to these parameters based on parameter order
                for (int index = 0; index < sourceColumns.Length; index++)
                    commandParameters[index].SourceColumn = sourceColumns[index];

                // Attach the discovered parameters to the SqlCommand object
                AttachParameters(cmd, commandParameters);
            }

            return cmd;
        }
        public static SqlCommand CreateCommand(string connectionString, string spName, params string[] sourceColumns)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");

            // Create & open a SqlConnection, and dispose of it after we are done
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                return CreateCommand(connection, spName, sourceColumns);
            }
        }
        #endregion

        #region ExecuteNonQueryTypedParams
        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns no resultset) against the database specified in 
        /// the connection string using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
        /// </summary>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQueryTypedParams(String connectionString, String spName, DataRow dataRow)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified SqlConnection 
        /// using the dataRow column values as the stored procedure's parameters values.  
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
        /// </summary>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQueryTypedParams(SqlConnection connection, String spName, DataRow dataRow)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified
        /// SqlTransaction using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
        /// </summary>
        /// <param name="transaction">A valid SqlTransaction object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQueryTypedParams(SqlTransaction transaction, String spName, DataRow dataRow)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // Sf the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
            }
        }
        #endregion

        #region ExecuteDatasetTypedParams
        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
        /// the connection string using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
        /// </summary>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDatasetTypedParams(string connectionString, String spName, DataRow dataRow)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            //If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
        /// using the dataRow column values as the store procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
        /// </summary>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDatasetTypedParams(SqlConnection connection, String spName, DataRow dataRow)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlTransaction 
        /// using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
        /// </summary>
        /// <param name="transaction">A valid SqlTransaction object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDatasetTypedParams(SqlTransaction transaction, String spName, DataRow dataRow)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
            }
        }

        #endregion

        #region ExecuteReaderTypedParams
        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
        /// the connection string using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReaderTypedParams(String connectionString, String spName, DataRow dataRow)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return SqlHelper.ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return SqlHelper.ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
        /// using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReaderTypedParams(SqlConnection connection, String spName, DataRow dataRow)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlTransaction 
        /// using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <param name="transaction">A valid SqlTransaction object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReaderTypedParams(SqlTransaction transaction, String spName, DataRow dataRow)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return SqlHelper.ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return SqlHelper.ExecuteReader(transaction, CommandType.StoredProcedure, spName);
            }
        }
        #endregion

        #region ExecuteScalarTypedParams
        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the database specified in 
        /// the connection string using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalarTypedParams(String connectionString, String spName, DataRow dataRow)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return SqlHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return SqlHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection 
        /// using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalarTypedParams(SqlConnection connection, String spName, DataRow dataRow)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified SqlTransaction
        /// using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <param name="transaction">A valid SqlTransaction object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalarTypedParams(SqlTransaction transaction, String spName, DataRow dataRow)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return SqlHelper.ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return SqlHelper.ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
            }
        }
        #endregion

        #region ExecuteXmlReaderTypedParams

        public static XmlReader ExecuteXmlReaderTypedParams(String connectionString, String spName, DataRow dataRow)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return SqlHelper.ExecuteXmlReader(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return SqlHelper.ExecuteXmlReader(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
        /// using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        public static XmlReader ExecuteXmlReaderTypedParams(SqlConnection connection, String spName, DataRow dataRow)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return SqlHelper.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return SqlHelper.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlTransaction 
        /// using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <param name="transaction">A valid SqlTransaction object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        public static XmlReader ExecuteXmlReaderTypedParams(SqlTransaction transaction, String spName, DataRow dataRow)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return SqlHelper.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return SqlHelper.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName);
            }
        }
        #endregion

    }

    public class GroupAccessHelper
    {
        public static GroupAccessSummaryList GetAccessSummaryList(string connectionString, string GroupType, string GroupName)
        {
            GroupAccessSummaryList summaryList = new GroupAccessSummaryList();
            SqlDataReader r = null;

            try
            {
                string sql = "SELECT a.ScreenName, CASE "
                            + "WHEN b.Screen IS NULL THEN 0 "
                            + "ELSE 1 END AS 'AccessRights' "
                            + "FROM Lookup_WebScreens a "
                            + "LEFT JOIN Config_Group_ScreenAccess b "
                            + "ON a.ScreenName = b.Screen "
                            + "AND b.grouptype = @GroupType "
                            + "AND b.groupName = @GroupName "
                            + "order by b.groupname desc, a.screenname asc ";

                r = SqlHelper.ExecuteReader(connectionString, CommandType.Text, sql, new SqlParameterList("@GroupType", GroupType, "@GroupName", GroupName).ToArray());

                while (r.HasRows && r.Read())
                {
                    GroupAccessSummary summary = new GroupAccessSummary();
                    summary.ScreenName = TypeHelper.GetString(r["ScreenName"]);
                    summary.AccessRights = TypeHelper.GetBool(r["AccessRights"]);
                    summaryList.Add(summary);
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while retrieving the Access Summary List.", ex);
            }
            finally
            {
                if (!r.IsClosed)
                    r.Close();
            }

            return summaryList;
        }

        public static void DeleteGroupAccess(string connectionString, string GroupType, string GroupName)
        {
            try
            {
                using (SqlTransaction transaction = SqlHelper.CreateTransaction(connectionString))
                {
                    try
                    {
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text,
                            "DELETE From Config_Group_ScreenAccess WHERE GroupType=@GroupType AND GroupName=@GroupName",
                            new SqlParameterList("@GroupType", GroupType, "@GroupName", GroupName).ToArray());

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch { }
                        throw new DataAccessException("Delete failed because of one or more errors. No changes have been made.", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while deleting the Group Access records.", ex);
            }
        }

        public static void InsertGroupAccessBatch(string connectionString, GroupAccessInsertBatch Batch)
        {
            try
            {
                using (SqlTransaction transaction = SqlHelper.CreateTransaction(connectionString))
                {
                    try
                    {
                        foreach (GroupAccessInsert Record in Batch)
                        {
                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text,
                                "INSERT INTO Config_Group_ScreenAccess(Screen, GroupName, GroupType) VALUES (@Screen, @GroupName, @GroupType)",
                                new SqlParameterList("@Screen", Record.Screen, "@GroupName", Record.GroupName, "@GroupType", Record.GroupType).ToArray());
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch { }
                        throw new DataAccessException("Batch Insert failed because of one or more errors. No changes have been made.", ex);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while inserting the Group Access Batch.", ex);
            }
        }

        public class GroupAccessSummary
        {
            public string ScreenName;
            public bool? AccessRights;
        }
        public class GroupAccessSummaryList : List<GroupAccessSummary> { }

        public class GroupAccessInsert
        {
            public string Screen;
            public string GroupName;
            public string GroupType;
        }
        public class GroupAccessInsertBatch : List<GroupAccessInsert> { }
    }

    public class DatabaseHelper : TypeHelper
    {

        public static void CreateTable(SqlTransaction Transaction, string TableName, params string[] ColumnDefinitonArr)
        {
            try
            {
                StringBuilder sql = new StringBuilder("CREATE TABLE dbo.[");
                sql.Append(TableName);
                sql.Append("] (");

                foreach (string ColumnDefinition in ColumnDefinitonArr)
                {
                    sql.Append(ColumnDefinition);
                    sql.Append(", ");
                }

                string DDLSql = sql.ToString().TrimEnd(',', ' ');
                DDLSql += ")";

                SqlHelper.ExecuteNonQuery(Transaction, CommandType.Text, DDLSql);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while creating the specified table.", ex);
            }
        }

        public static bool CheckTableExistence(string connectionString, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return CheckTableExistence(connection, tableName);
            }

        }

        public static bool CheckTableExistence(SqlConnection Connection, string TableName)
        {
            if (Connection == null)
            {
                throw new ArgumentNullException("Connection");
            }

            bool exists = false;
            try
            {
                string sql = "SELECT Count(*) FROM SYSOBJECTS WHERE xtype='U' AND Name=@TableName";
                object obj = null;

                obj = SqlHelper.ExecuteScalar(Connection, CommandType.Text, sql, new SqlParameterList("@TableName", TableName).ToArray());

                int count = (int)GetInt(obj);

                if (count > 0)
                    exists = true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while checking whether the table existed or not.", ex);
            }

            return exists;
        }

        public static List<DataType> GetDataTypes()
        {
            List<DataType> dataTypes = new List<DataType>();

            DataType bit = new DataType();
            bit.Name = "bit";
            bit.Length = 1;
            bit.Status = 0;
            dataTypes.Add(bit);

            DataType datetime = new DataType();
            datetime.Name = "datetime";
            datetime.Length = 8;
            datetime.Status = 0;
            dataTypes.Add(datetime);

            DataType typeInt = new DataType();
            typeInt.Name = "int";
            typeInt.Length = 4;
            typeInt.Status = 0;
            dataTypes.Add(typeInt);

            DataType varchar = new DataType();
            varchar.Name = "varchar";
            varchar.Length = 8000;
            varchar.Status = 2;
            dataTypes.Add(varchar);

            DataType typeFloat = new DataType();
            typeFloat.Name = "float";
            typeFloat.Length = 8;
            typeFloat.Status = 0;
            dataTypes.Add(typeFloat);

            DataType real = new DataType();
            real.Name = "real";
            real.Length = 4;
            real.Status = 0;
            dataTypes.Add(real);


            /*
            SqlDataReader r = null;

            try
            {
                string sql = "SELECT Name, Length, Status FROM Systypes WHERE Name != 'text' ORDER BY Name";
                r = SqlHelper.ExecuteReader(CommandType.Text, sql);

                while (r.HasRows && r.Read())
                {
                    DataType dType = new DataType();
                    dType.Name = GetString(r[0]);
                    if (dType.Name != "text" && dType.Name != "varbinary"
                        && dType.Name != "sysname" && dType.Name != "sql_variant"
                        && dType.Name != "ntext" && dType.Name != "image"
                        && dType.Name != "binary" && dType.Name != "uniqueidentifier")
                    {
                        dType.Length = (int)GetInt(r[1]);
                        dType.Status = (int)GetInt(r[2]);
                        dataTypes.Add(dType);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while retrieving the datatypes from the database.", ex);
            }
            finally
            {
                if (r != null && !r.IsClosed)
                    r.Close();
            }
            */

            return dataTypes;
        }

        public static void UpdateColumnValue(SqlTransaction transaction, TableColumn tc, object oldValue, object newValue)
        {
            try
            {
                string sql = string.Format("UPDATE [{0}] SET [{1}]=@newValue WHERE [{1}]=@oldValue",
                    tc.TableName, tc.ColumnName);
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text,
                    sql, new SqlParameterList("@oldValue", oldValue, "@newValue", newValue).ToArray()
                );
            }
            catch (Exception ex)
            {
                throw new DataAccessException(
                    string.Format(
                        "An error occurred while updating column {0} in table {1}.",
                             tc.ColumnName, tc.TableName), ex
                );
            }
        }

        public static void DeleteRowsWithColumnValue(SqlTransaction transaction, TableColumn tc, string value)
        {
            try
            {
                string sql = string.Format("DELETE FROM [{0}] WHERE [{1}]=@value", tc.TableName, tc.ColumnName);
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text,
                    sql, new SqlParameterList("@value", value).ToArray()
                );
            }
            catch (Exception ex)
            {
                throw new DataAccessException(
                    string.Format(
                        "An error occurred while deleting rows with matching column {0} in table {1}.",
                            tc.ColumnName, tc.TableName), ex
                );
            }
        }

        public static List<TableColumn> GetTableColumns(string connectionString, string tableName)
        {
            List<DatabaseHelper.TableColumn> results = new List<DatabaseHelper.TableColumn>();
            try
            {
                SqlDataReader r = null;
                try
                {
                    r = SqlHelper.ExecuteReader(connectionString, CommandType.Text,
                        @"select distinct b.name from sysobjects a, syscolumns b 
                            where a.id = b.id and (a.xtype = 'U' or a.xtype='V') and a.name = @name",
                        new SqlParameterList("@name", tableName).ToArray()
                    );
                    while (r.Read())
                    {
                        results.Add(
                            new DatabaseHelper.TableColumn(tableName.ToUpper(),
                                TypeHelper.GetString(r["name"]).ToUpper()));
                    }
                }
                finally
                {
                    if (!r.IsClosed)
                        r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while getting column names.", ex);
            }

            return results;
        }

        public static List<TableColumn> GetTablesWithColumn(string connectionString, string columnName)
        {
            List<DatabaseHelper.TableColumn> results = new List<DatabaseHelper.TableColumn>();
            try
            {
                SqlDataReader r = null;
                try
                {
                    r = SqlHelper.ExecuteReader(connectionString, CommandType.Text,
                        @"select distinct o.name from sysobjects o left join syscolumns c on o.id = c.id where  o.xtype = 'U' and c.name = @name and o.name NOT LIKE 'HL7_%' and o.name NOT LIKE 'audit_useraccess' AND USER_NAME(o.uid)='dbo'",
                        new SqlParameterList("@name", columnName).ToArray()
                    );
                    while (r.Read())
                    {
                        results.Add(new DatabaseHelper.TableColumn(TypeHelper.GetString(r["name"]).ToUpper(), columnName.ToUpper()));
                    }
                }
                finally
                {
                    if (!r.IsClosed)
                        r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while getting table names.", ex);
            }

            return results;
        }

        public class TableColumn
        {
            public string TableName;
            public string ColumnName;

            public TableColumn() { }
            public TableColumn(string tableName, string columnName)
            {
                this.TableName = tableName;
                this.ColumnName = columnName;
            }
        }
        public class DataType
        {
            public string Name;
            public int Length;
            public int Status;

            public DataType() { }
            public DataType(string name, int length, int status)
            {
                this.Name = name;
                this.Length = length;
                this.Status = status;
            }
        }
    }

    public class TypeHelper
    {

        #region Utility functions for type conversion from SQL

        // min and max dates for MSSQL
        public static DateTime MinDateTime = DateTime.Parse("1/1/1753");
        public static DateTime MaxDateTime = DateTime.Parse("12/31/9999 11:59:59.99 PM");

        public static byte[] GetBinary(object o)
        {
            if (o != null && !Convert.IsDBNull(o))
            {
                return (byte[])o;
            }
            else
            {
                return null;
            }
        }

        public static string GetString(object o)
        {
            if (o != null && !Convert.IsDBNull(o))
            {
                return Convert.ToString(o);
            }
            else
            {
                return null;
            }
        }

        public static int? GetInt(object o)
        {
            if (o != null && !Convert.IsDBNull(o))
            {
                return Convert.ToInt32(o);
            }
            else
            {
                return null;
            }
        }

        public static long? GetLong(object o)
        {
            if (o != null && !Convert.IsDBNull(o))
            {
                return Convert.ToInt64(o);
            }
            else
            {
                return null;
            }
        }

        public static short? GetShort(object o)
        {
            if (o != null && !Convert.IsDBNull(o))
            {
                return Convert.ToInt16(o);
            }
            else
            {
                return null;
            }
        }

        public static char? GetChar(object o)
        {
            if (o != null && !Convert.IsDBNull(o))
            {
                return Convert.ToChar(o);
            }
            else
            {
                return null;
            }
        }

        public static bool? GetBool(object o)
        {
            if (o != null && !Convert.IsDBNull(o))
            {
                return Convert.ToBoolean(o);
            }
            else
            {
                return null;
            }
        }

        public static double? GetDouble(object o)
        {
            if (o != null && !Convert.IsDBNull(o))
            {
                return Convert.ToDouble(o);
            }
            else
            {
                return null;
            }
        }

        public static decimal? GetDecimal(object o)
        {
            if (o != null && !Convert.IsDBNull(o))
            {
                return Convert.ToDecimal(o);
            }
            else
            {
                return null;
            }
        }

        public static float? GetFloat(object o)
        {
            if (o != null && !Convert.IsDBNull(o))
            {
                return Convert.ToSingle(o);
            }
            else
            {
                return null;
            }
        }

        public static Guid? GetGuid(object o)
        {
            if (o != null && !Convert.IsDBNull(o))
            {
                return new Guid(o.ToString());
            }
            else
            {
                return null;
            }
        }

        public static DateTime? GetDateTime(object o)
        {
            if (o != null && !Convert.IsDBNull(o))
            {
                return Convert.ToDateTime(o);
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Utility functions for type conversion for SQL Parameters

        public static object DbBox(bool value)
        {
            return (object)value;
        }

        public static object DbBox(string value)
        {
            return DbBox(value, true);
        }

        public static object DbBox(string value, bool zeroLengthNull)
        {
            if (value == null || zeroLengthNull && (value.Length == 0))
                return null;
            else
                return (object)value;
        }

        public static object DbBox(int value)
        {
            return DbBox(value, false, 0);
        }

        public static object DbBox(int value, bool valueNull, int nullValue)
        {
            if (valueNull && value == nullValue)
                return null;
            else
                return (object)value;
        }

        public static object DbBox(DateTime value)
        {
            return DbBox(value, true, new DateTime(0));
        }

        public static object DbBox(DateTime value, bool valueNull, DateTime nullValue)
        {
            if (valueNull && value == nullValue)
                return null;
            else
                return (object)value;
        }

        public static object DbBox(float value)
        {
            return (object)value;
        }

        public static object DbBox(double value)
        {
            return (object)value;
        }

        public static object DbBox(Guid value)
        {
            return (object)value;
        }

        public static object DbBox(short value)
        {
            return (object)value;
        }

        public static object DbBox(long value)
        {
            return (object)value;
        }

        #endregion

    }

    public class HL7IncomingHelper
    {
        public static List<HL7IncomingMessage> DynamicallyGetMessages(string connectionString, int NumberOfRecords, DateTime StartDate, string[] MessageSearchFilters)
        {
            List<HL7IncomingMessage> Messages = new List<HL7IncomingMessage>();

            SqlParameterList spList = new SqlParameterList();
            string sql = null;

            try
            {
                string dynamicSQL = string.Format("SELECT TOP {0} ID, MESSAGE, Imported FROM HL7Incoming WHERE EnteredDate >= @StartDate ", NumberOfRecords);
                spList.Add("@StartDate", StartDate);

                StringBuilder builder = new StringBuilder(dynamicSQL);

                for (int i = 0; i < MessageSearchFilters.Length; i++)
                {
                    string paramName = "@Message" + i;
                    StringBuilder b = new StringBuilder("%");
                    b.Append(MessageSearchFilters[i]);
                    b.Append("%");
                    string value = b.ToString();
                    builder.Append(" AND MESSAGE LIKE ");
                    builder.Append(paramName);

                    spList.Add(paramName, value);
                }

                builder.Append(" ORDER BY ENTEREDDATE DESC, ENTEREDTIME DESC ");

                sql = builder.ToString();
            }
            catch (Exception ex)
            {
                throw new TcException("An error occurred while building the dynamic SQL to execute the specified query.", ex);
            }

            SqlDataReader r = null;

            try
            {
                r = SqlHelper.ExecuteReader(connectionString, CommandType.Text, sql, spList.ToArray());

                while (r.HasRows && r.Read())
                {
                    HL7IncomingMessage msg = new HL7IncomingMessage();
                    msg.ID = TypeHelper.GetInt(r[0]);
                    msg.Message = ConvertStringToByteArray(TypeHelper.GetString(r[1]));
                    msg.Imported = TypeHelper.GetBool(r[2]);
                    Messages.Add(msg);
                }

            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while retrieving the specified HL7 Incoming records from the database.", ex);
            }
            finally
            {
                if (r != null && !r.IsClosed)
                    r.Close();
            }

            return Messages;
        }

        public static void UpdateMessagesForReprocessing(string connectionString, List<int> MessagesIDs)
        {
            using (SqlTransaction transaction = SqlHelper.CreateTransaction(connectionString))
            {

                try
                {
                    foreach (int messageID in MessagesIDs)
                    {
                        string updateSql = "UPDATE HL7Incoming SET Imported = 0 Where ID = @ID";

                        SqlHelper.ExecuteNonQuery(transaction,
                            CommandType.Text,
                            updateSql,
                            new SqlParameterList("@ID", messageID).ToArray());
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch { }
                    throw new DataAccessException("An error occurred while flagging the records for reprocessing. No changes were made.", ex);
                }
            }
        }

        private static byte[] ConvertStringToByteArray(string stringToConvert)
        {
            return (new UnicodeEncoding()).GetBytes(stringToConvert);
        }

        public class HL7IncomingMessage
        {
            public int? ID;
            public byte[] Message;
            public bool? Imported;
        }
    }

    public class HL7OutgoingHelper
    {
        public static DataSet GetOutgoingHL7CompatableTableNames(string connectionString)
        {
            DataSet ds = null;

            try
            {
                ds = SqlHelper.ExecuteDataset(connectionString, CommandType.Text,
                    " select o.name tableName, " +
                    " case when (select 1 where exists ( select null from syscolumns where id=o.id and name='HL7OutID' )) = 1 then 'True' else 'False' end configured," +
                    " case when (select 1 where exists ( select null from HL7OutboundTables where send = 1 and [table] = o.name) ) = 1 then 'True' else 'False' end enabled" +
                    " from sysobjects o" +
                    " where o.xtype='u' and o.name not like 'audit_%' and o.name not like 'hl7%'" +
                    " and exists ( select null from syscolumns where id=o.id and name='mrn' )" +
                    " and exists ( select null from syscolumns where id=o.id and name='entereddate' )" +
                    " and exists ( select null from syscolumns where id=o.id and name='enteredtime' )" +
                    " and exists ( select null from syscolumns where id=o.id and name='enteredby' )" +
                    " order by o.name");
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while retrieve the Table names from the database.", ex);
            }

            return ds;
        }

        public static void SetupOutgoingTables(string connectionString, List<string> tablesToBeEnabled)
        {

            DataSet tableInfo = GetOutgoingHL7CompatableTableNames(connectionString);

            List<string> tables = new List<string>();
            List<string> configuredTables = new List<string>();
            List<string> enabledTables = new List<string>();

            foreach (DataRow row in tableInfo.Tables[0].Rows)
            {
                tables.Add((string)row["tableName"]);
                if ((string)row["configured"] == "True")
                    configuredTables.Add((string)row["tableName"]);
                if ((string)row["enabled"] == "True")
                    enabledTables.Add((string)row["tableName"]);
            }


            SqlConnection connection = SqlHelper.CreateConnection(connectionString);
            SqlTransaction t = SqlHelper.CreateTransaction(connection);
            SqlCommand c;

            try
            {
                foreach (string table in tables)
                {

                    // Configure table if not already configured
                    Predicate<string> tableMatch = delegate(string s) { return s == table; };

                    bool configured = configuredTables.Find(tableMatch) != null;
                    bool enabled = enabledTables.Find(tableMatch) != null;
                    bool toBeEnabled = tablesToBeEnabled.Find(tableMatch) != null;

                    if (toBeEnabled)
                    {
                        if (!enabled)
                        {
                            if (!configured)
                            {
                                // Add HL7 columns to table
                                c = new SqlCommand(
                                        "ALTER TABLE [" + table + "] ADD " +
                                        "HL7OutID varchar(25) NULL," +
                                        "HL7OutDate datetime NULL," +
                                        "HL7OutTime datetime NULL"
                                        , connection, t);
                                c.ExecuteNonQuery();

                                // Add HL7 columns to audit table if exists
                                c = new SqlCommand(
                                        " select top 1 o.name" +
                                        " from sysobjects o" +
                                        " where o.xtype='u' and o.name like @tablename" +
                                        " and exists ( select null from syscolumns where id=o.id and name='mrn' )" +
                                        " and exists ( select null from syscolumns where id=o.id and name='entereddate' )" +
                                        " and exists ( select null from syscolumns where id=o.id and name='enteredtime' )" +
                                        " order by o.name", connection, t);

                                c.Parameters.AddWithValue("tableName", "audit_" + table);

                                string auditTableName = (string)c.ExecuteScalar();

                                if (auditTableName != null)
                                {
                                    c = new SqlCommand(
                                            "ALTER TABLE [" + auditTableName + "] ADD " +
                                            "HL7OutID varchar(25) NULL," +
                                            "HL7OutDate datetime NULL," +
                                            "HL7OutTime datetime NULL"
                                            , connection, t);
                                    c.ExecuteNonQuery();
                                }

                            }

                            // Update outbound dates and times to ensure existing records are not sent
                            c = new SqlCommand(
                                "update [" + table + "]" +
                                " set HL7OutDate = @date, HL7OutTime = @time"
                                , connection, t);
                            c.Parameters.AddWithValue("tableName", table);
                            c.Parameters.AddWithValue("date", DateTime.Now.Date);
                            c.Parameters.AddWithValue("time", DateTime.Parse("1900-01-01 " + DateTime.Now.ToLongTimeString()));
                            c.ExecuteNonQuery();

                            // Enable outbound messages by updating hl7outgoing table or inserting if it doesnt exit
                            c = new SqlCommand("select case when exists (select null from HL7OutboundTables where [table] = @tableName) then 1 else 0 end", connection, t);
                            c.Parameters.AddWithValue("tableName", table);

                            if ((int)c.ExecuteScalar() > 0)
                            {
                                c = new SqlCommand(
                                    "update HL7OutboundTables" +
                                    " set send = 1" +
                                    " where [table] = @tableName"
                                    , connection, t);
                                c.Parameters.AddWithValue("tableName", table);
                                c.ExecuteNonQuery();
                            }
                            else
                            {
                                c = new SqlCommand(
                                    "insert into HL7OutboundTables" +
                                    " ([table],send) values" +
                                    " (@tableName, 1)"
                                    , connection, t);
                                c.Parameters.AddWithValue("tableName", table);
                                c.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        // disable outbound messages from newly disabled table
                        if (enabledTables.Find(tableMatch) != null)
                        {
                            c = new SqlCommand(
                                "update HL7OutboundTables" +
                                " set send = 0" +
                                " where [table] = @tableName"
                                , connection, t);
                            c.Parameters.AddWithValue("tableName", table);
                            c.ExecuteNonQuery();
                        }
                    }
                }

                t.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    t.Rollback();
                }
                catch (Exception ex1)
                {
                    throw new DataAccessException("An error occurred while rolling back after failing to configure outgoing tables.", ex1);
                }
                throw new DataAccessException("An error occurred while configuring outgoing tables.", ex);
            }
        }
    }

    public class LookupTableHelper
    {
        public static void CreateLookupTable(string connectionString, string TableName, List<LookupTableColumn> ColumnDefinitionArr)
        {
            try
            {
                string IDColumn = "ID int IDENTITY (1, 1) PRIMARY KEY";
                string[] newArr = new string[ColumnDefinitionArr.Count + 1];
                StringBuilder modifiableFieldsBuilder = new StringBuilder();
                newArr[0] = IDColumn;

                int i = 1;
                foreach (LookupTableColumn col in ColumnDefinitionArr)
                {
                    if (col.DataType.Name != "binary" &&
                        col.DataType.Name != "char" &&
                        col.DataType.Name != "nchar" &&
                        col.DataType.Name != "nvarchar" &&
                        col.DataType.Name != "sysname" &&
                        col.DataType.Name != "timestamp" &&
                        col.DataType.Name != "varbinary" &&
                        col.DataType.Name != "varchar")
                    {
                        newArr[i] = string.Format("[{0}] {1}", col.ColumnName, col.DataType.Name);
                    }
                    else
                    {
                        newArr[i] = string.Format("[{0}] {1}({2})", col.ColumnName, col.DataType.Name, col.DataType.Length);
                    }
                    if (col.Editable)
                    {
                        modifiableFieldsBuilder.Append(col.ColumnName);
                        modifiableFieldsBuilder.Append(",");
                    }
                    i++;
                }

                string modifiableFields = modifiableFieldsBuilder.ToString();

                if (modifiableFields != null && modifiableFields != string.Empty)
                {
                    modifiableFields = modifiableFields.Remove(modifiableFields.Length - 1);
                }

                using (SqlTransaction transaction = SqlHelper.CreateTransaction(connectionString))
                {

                    try
                    {
                        DatabaseHelper.CreateTable(transaction, TableName, newArr);
                        UpdateModifyTable(transaction, TableName, modifiableFields);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch { }
                        throw new DataAccessException("Insert failed due to one or more errors. No changes have been made.", ex);

                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while creating the lookup table.", ex);
            }
        }

        public static List<string> GetLookupTableFields(string connectionString, string tablename)
        {
            List<string> lookupTableFieldList = new List<string>();
            SqlDataReader r = null;

            try
            {
                SqlCommand cmd = new SqlCommand(
                    "select name from syscolumns where id = (SELECT id name FROM SYSOBJECTS where xtype='u' AND name = @tablename) order by name", SqlHelper.CreateConnection(connectionString));

                cmd.Parameters.AddWithValue("@tablename", tablename);

                r = cmd.ExecuteReader();

                while (r.HasRows && r.Read())
                {
                    lookupTableFieldList.Add(TypeHelper.GetString(r["name"]));
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while retrieve the Lookup Table names from the database.", ex);
            }
            finally
            {
                if (!r.IsClosed)
                    r.Close();
            }

            return lookupTableFieldList;
        }

        public static List<string> GetLookupValues(string connectionString, string table, string field)
        {
            List<string> result = new List<string>();
            SqlDataReader dr = null;
            try
            {
                string sql = string.Format("SELECT [{0}] FROM [{1}]", field, table);
                dr = SqlHelper.ExecuteReader(connectionString, CommandType.Text, sql);
                while (dr.HasRows && dr.Read())
                {
                    result.Add(TypeHelper.GetString(dr[0]));
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while querying the lookup table.", ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }

            return result;

        }

        public static List<string> GetLookupValues(string connectionString, string table, string field, List<QueryFilter> filter, string order)
        {
            List<string> result = new List<string>();
            SqlDataReader dr = null;
            try
            {
                string sql = string.Format("SELECT [{0}] FROM [{1}]", field, table);
                SqlParameterList prms = new SqlParameterList();
                if (filter != null && filter.Count > 0)
                {
                    sql += " WHERE ";
                    StringBuilder sb = new StringBuilder();
                    foreach (QueryFilter qf in filter)
                    {
                        if (sb.Length > 0)
                            sb.Append(" AND");
                        sb.AppendFormat(" {0}", qf.FilterString);
                        prms.Add(qf.Param);
                    }
                    sql += sb.ToString();
                }
                if (order != null)
                {
                    sql += " ORDER BY " + order;
                }
                dr = SqlHelper.ExecuteReader(connectionString, CommandType.Text, sql, prms.ToArray());
                while (dr.HasRows && dr.Read())
                {
                    result.Add(TypeHelper.GetString(dr[0]));
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while querying the lookup table.", ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }

            return result;

        }

        public static TableSummary GetLookupTableSummaryByName(string connectionString, string TableName)
        {
            return GetLookupTableSummaryByName(connectionString, TableName, null);
        }

        public static TableSummary GetLookupTableSummaryByName(string connectionString, string TableName, string sortExpression)
        {
            TableSummary summary = new TableSummary();

            try
            {
                using (SqlConnection connection = SqlHelper.CreateConnection(connectionString))
                {
                    if (sortExpression != null && sortExpression.Length > 0)
                        SetLookupTableSummaryDS(summary, connection, TableName, null, new string[] { sortExpression });
                    else
                        SetLookupTableSummaryDS(summary, connection, TableName);

                    SetLookupTableEditableInfo(summary, connection, TableName);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while retrieving the summary for the specified table.", ex);
            }

            return summary;
        }

        public static void SaveLookupTablePermissions(string connectionString, string TableName, List<string> EditableFields)
        {
            try
            {
                StringBuilder eFields = new StringBuilder();

                foreach (string fieldName in EditableFields)
                {
                    eFields.Append(fieldName);
                    eFields.Append(",");
                }

                string eFieldsStr = eFields.ToString();
                if (eFieldsStr != null && eFieldsStr != string.Empty)
                {
                    eFieldsStr = eFieldsStr.Remove(eFieldsStr.Length - 1);
                }

                using (SqlTransaction transaction = SqlHelper.CreateTransaction(connectionString))
                {
                    try
                    {
                        DeleteModifyTableData(transaction, TableName);
                        UpdateModifyTable(transaction, TableName, eFieldsStr);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch { }
                        throw new DataAccessException("Update failed due to one or more errors. No changes have been made.", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while saving the table's modifiable status information.", ex);
            }
        }

        public static void SetLookupTableSummaryDS(TableSummary summary, SqlConnection conn, string DataTableName)
        {
            SetLookupTableSummaryDS(summary, conn, DataTableName, null, null);
        }

        public static void SetLookupTableSummaryDS(TableSummary summary, SqlConnection conn, string DataTableName, List<QueryFilter> QueryFilterList, string[] OrderByFields)
        {
            DynamicSQL dSql = DynamicSQL.CreateDynamicSelectSQL(DataTableName, QueryFilterList, OrderByFields);

            SqlDataReader r = null;

            DataSet summaryDS = new DataSet();
            DataTable LookupDataTableSchema = null;
            DataTable LookupDataTable = null;

            try
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = dSql.Sql;

                    foreach (SqlParameter param in dSql.SqlParameters)
                    {
                        command.Parameters.Add(param);
                    }

                    r = command.ExecuteReader(CommandBehavior.KeyInfo);
                    LookupDataTableSchema = r.GetSchemaTable();
                }
                catch (Exception ex)
                {
                    throw new DataAccessException("An error occurred while retrieving the lookup table's schema.", ex);
                }

                if (LookupDataTableSchema != null)
                {
                    try
                    {
                        RemoveDbUserIDColumn(LookupDataTableSchema);

                        LookupDataTable = new DataTable();
                        foreach (DataRow row in LookupDataTableSchema.Rows)
                        {
                            if (string.Equals(row["IsHidden"].ToString(), "true", StringComparison.InvariantCultureIgnoreCase))
                            {
                                continue;
                            }

                            string columnName = row["ColumnName"].ToString();
                            DataColumn column = new DataColumn(columnName, (Type)row["DataType"]);
                            LookupDataTable.Columns.Add(column);
                        }

                        while (r.HasRows && r.Read())
                        {
                            DataRow newRow = LookupDataTable.NewRow();

                            foreach (DataColumn column in LookupDataTable.Columns)
                            {
                                newRow[column.ColumnName] = r[column.ColumnName];
                            }

                            LookupDataTable.Rows.Add(newRow);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new DataAccessException("An error occurred while retrieving the lookup data from the database.", ex);
                    }
                }
            }
            catch { throw; }
            finally
            {
                if (LookupDataTableSchema != null)
                    summaryDS.Tables.Add(LookupDataTableSchema);
                if (LookupDataTable != null)
                    summaryDS.Tables.Add(LookupDataTable);
                summary.TableDS = summaryDS;
                if (r != null && !r.IsClosed)
                    r.Close();
            }

        }

        public static void SetLookupTableSummaryDS(TableSummary summary, SqlConnection conn, int TopRecordsToSelect, string DataTableName, List<QueryFilter> QueryFilterList, string[] OrderByFields)
        {
            DynamicSQL dSql = DynamicSQL.CreateDynamicSelectSQL(TopRecordsToSelect, DataTableName, QueryFilterList, OrderByFields);

            SqlDataReader r = null;

            DataSet summaryDS = new DataSet();
            DataTable LookupDataTableSchema = null;
            DataTable LookupDataTable = null;

            try
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = dSql.Sql;

                    foreach (SqlParameter param in dSql.SqlParameters)
                    {
                        command.Parameters.Add(param);
                    }

                    r = command.ExecuteReader(CommandBehavior.KeyInfo);
                    LookupDataTableSchema = r.GetSchemaTable();
                }
                catch (Exception ex)
                {
                    throw new DataAccessException("An error occurred while retrieving the lookup table's schema.", ex);
                }

                if (LookupDataTableSchema != null)
                {
                    try
                    {
                        RemoveDbUserIDColumn(LookupDataTableSchema);

                        LookupDataTable = new DataTable();
                        foreach (DataRow row in LookupDataTableSchema.Rows)
                        {
                            if (string.Equals(row["IsHidden"].ToString(), "true", StringComparison.InvariantCultureIgnoreCase))
                            {
                                continue;
                            }

                            switch (row["DataTypeName"].ToString())
                            {
                                case "text":
                                case "image":
                                case "binary":
                                case "varbinary":
                                    continue;
                            }

                            string columnName = row["ColumnName"].ToString();
                            DataColumn column = new DataColumn(columnName, (Type)row["DataType"]);
                            LookupDataTable.Columns.Add(column);
                        }

                        while (r.HasRows && r.Read())
                        {
                            DataRow newRow = LookupDataTable.NewRow();

                            foreach (DataColumn column in LookupDataTable.Columns)
                            {
                                newRow[column.ColumnName] = r[column.ColumnName];
                            }
                            LookupDataTable.Rows.Add(newRow);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new DataAccessException("An error occurred while retrieving the lookup data from the database.", ex);
                    }
                }
            }
            catch { throw; }
            finally
            {
                if (LookupDataTableSchema != null)
                    summaryDS.Tables.Add(LookupDataTableSchema);
                if (LookupDataTable != null)
                    summaryDS.Tables.Add(LookupDataTable);
                summary.TableDS = summaryDS;
                if (r != null && !r.IsClosed)
                    r.Close();
            }
        }

        public static void SetLookupTableEditableInfo(TableSummary summary, SqlConnection conn, string DataTableName)
        {
            SqlDataReader r = null;

            bool Modifiable = false;
            List<string> EditableFields = new List<string>();

            try
            {
                string sql = "SELECT CASE WHEN TableName IS NULL THEN 0 ELSE 1 END AS 'Modifiable' , IsNull(Editablefields,'') Editablefields FROM Lookup_ModifiableTables Where TableName=@TableName";
                r = SqlHelper.ExecuteReader(conn, CommandType.Text, sql, new SqlParameterList("@TableName", DataTableName).ToArray());

                if (r.HasRows && r.Read())
                {
                    Modifiable = (bool)TypeHelper.GetBool(r[0]);

                    string commaDelimEditFields = TypeHelper.GetString(r[1]);

                    foreach (string EditableField in commaDelimEditFields.Split(','))
                    {
                        EditableFields.Add(EditableField);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while retrieving information from the database whether the table is editable.", ex);
            }
            finally
            {
                if (!r.IsClosed)
                    r.Close();
            }

            summary.ModifiableTable = Modifiable;
            summary.EditableFields = EditableFields;

        }

        public static void InsertLookupTableRecord(string connectionString, string DataTableName, List<InsertUpdateParam> ParamList)
        {
            try
            {
                DynamicSQL dSql = DynamicSQL.CreateDynamicInsertSQL(DataTableName, ParamList);

                SqlHelper.ExecuteNonQuery(connectionString, CommandType.Text,
                    dSql.Sql,
                    dSql.SqlParameters.ToArray());
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while inserting into the specified Lookup Table.", ex);
            }
        }

        public static void UpdateLookupTableRecord(string connectionString, string DataTableName, List<InsertUpdateParam> ParamList, List<QueryFilter> QueryFilterList)
        {
            try
            {
                DynamicSQL dSql = DynamicSQL.CreateDynamicUpdateSQL(DataTableName, ParamList, QueryFilterList);

                SqlHelper.ExecuteNonQuery(connectionString, CommandType.Text,
                    dSql.Sql,
                    dSql.SqlParameters.ToArray());
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while updating the Lookup Table record.", ex);
            }
        }

        private static void UpdateModifyTable(SqlTransaction Transaction, string TableName, string CommaDelimEditableFields)
        {
            try
            {
                object obj = SqlHelper.ExecuteScalar(Transaction, CommandType.Text,
                    "SELECT count(*) FROM Lookup_ModifiableTables WHERE Tablename=@TableName",
                    new SqlParameterList("@TableName", TableName).ToArray());
                int? cnt = TypeHelper.GetInt(obj);

                if (CommaDelimEditableFields != string.Empty && CommaDelimEditableFields != null)
                {
                    string sql = null;
                    if (cnt != 0)
                    {
                        sql = "UPDATE Lookup_ModifiableTables SET EditableFields=@EditableFields WHERE TableName=@TableName";
                    }
                    else
                    {
                        sql = "INSERT INTO Lookup_ModifiableTables(Tablename, EditableFields) VALUES (@TableName, @EditableFields)";
                    }

                    SqlHelper.ExecuteNonQuery(Transaction, CommandType.Text, sql,
                        new SqlParameterList("@TableName", TableName, "@EditableFields", CommaDelimEditableFields).ToArray());
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while updating the Modifiable Tables.", ex);
            }
        }

        private static void DeleteModifyTableData(SqlTransaction Transaction, string TableName)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(Transaction, CommandType.Text, "DELETE FROM Lookup_ModifiableTables WHERE TableName=@TableName",
                    new SqlParameterList("@TableName", TableName).ToArray());
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while deleting the modifiable table data.", ex);
            }
        }

        private static void RemoveDbUserIDColumn(DataTable tableSchema)
        {
            // Remove the DbUserID column if it exists so it doesn't show in the UI
            foreach (DataRow row in tableSchema.Rows)
            {
                string columnName = row["ColumnName"].ToString();
                if (string.Equals("TenantId", columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    tableSchema.Rows.Remove(row);
                    break;
                }
            }
        }

        public class LookupTableColumn
        {
            public string ColumnName;
            public DatabaseHelper.DataType DataType;
            public bool Editable;
        }

    }

    public class MultiTenantHelper
    {
        private const string suffixMultiTenant = "_Multi";

        public static string GetDisplayTableName(string tableName)
        {
            if (IsMultiTenantTable(tableName))
            {
                return tableName.Substring(0, tableName.Length - 6);
            }
            else
            {
                return tableName;
            }
        }

        public static bool IsMultiTenantTable(string tableName)
        {
            return (tableName != null && tableName.EndsWith(suffixMultiTenant, StringComparison.OrdinalIgnoreCase));
        }
    }

    public class UnosFormHelper
    {
        #region SQL Statements
        
        private const string SearchUnosFormDataSql = @" SELECT  usf.MRN, usf.EnteredDate, usf.EnteredBy, usf.FormID, usf.ID, 
                                                                p.[Last], p.[First], p.DOB, uf.Name, usf.ImportedFromUnos 
                                                        FROM    UnosSavedForm usf 
                                                            JOIN UnosForm uf ON uf.ID = usf.FormID 
                                                            LEFT JOIN patient p ON usf.MRN = p.MRN 
                                                        WHERE   usf.MRN like case when @mrn is null then usf.MRN else @MRN end and
                                                                usf.ImportedFromUnos = case when @ImportedFromUnos is null then usf.ImportedFromUnos else @ImportedFromUnos end and
                                                                usf.FormID = case when @FormID is null then usf.FormID else @FormID end and
                                                                p.[First] like case when @First is null then p.[First] else @First end and
                                                                p.[Last] like case when @Last is null then p.[Last] else @Last end and
                                                                p.[DOB] = case when @DOB is null then p.[DOB] else @DOB end and
                                                                usf.EnteredBy like case when @EnteredBy is null then usf.EnteredBy else @EnteredBy end and   
                                                                usf.EnteredDate >=  case when @StartDate is null then usf.EnteredDate else @StartDate end and
                                                                usf.EnteredDate <=  case when @EndDate is null then usf.EnteredDate else @EndDate end
                                                        UNION
                                                        SELECT  usf.MRN, usf.EnteredDate, usf.EnteredBy, usf.FormID, usf.ID, 
                                                                c.[LN] ,c.[fn], c.DOB, uf.Name, usf.ImportedFromUnos 
                                                        FROM    Candidate  c 
                                                            JOIN UnosSavedForm usf on usf.MRN = c.MRN 
                                                            JOIN UnosForm uf on uf.ID = usf.FormID 
                                                        WHERE   usf.MRN like case when @mrn is null then usf.MRN else @MRN end and
                                                                usf.ImportedFromUnos = case when @ImportedFromUnos is null then usf.ImportedFromUnos else @ImportedFromUnos end and
                                                                usf.FormID = case when @FormID is null then usf.FormID else @FormID end and
                                                                c.[fn] like case when @First is null then c.[fn] else @First end and 
                                                                c.[LN] like case when @Last is null then c.[LN] else @First end and
                                                                usf.EnteredBy like case when @EnteredBy is null then usf.EnteredBy else @EnteredBy end and                                                                
                                                                usf.EnteredDate >=  case when @StartDate is null then usf.EnteredDate else @StartDate end and
                                                                usf.EnteredDate <=  case when @EndDate is null then usf.EnteredDate else @EndDate end";

        private const string DeleteUnosFormDataSql = @" Begin Try
                                                        Begin Transaction DeleteUnosFormData
                                                            Delete Audit_UnosSavedForm where UnosSavedFormId = @id
                                                            Delete UnosWorkListToSavedForm where SavedFormID = @id
                                                            Delete UnosValue where SavedFormID = @id
                                                            Delete UnosSavedForm where ID = @id 
                                                            Commit Transaction DeleteUnosFormData
                                                    End Try
                                                    Begin Catch
                                                        Rollback Transaction DeleteUnosFormData
                                                        DECLARE @ErrorMessage NVARCHAR(4000);
                                                        DECLARE @ErrorSeverity INT;
                                                        DECLARE @ErrorState INT;
                                                        SELECT 
                                                            @ErrorMessage = ERROR_MESSAGE(),
                                                            @ErrorSeverity = ERROR_SEVERITY(),
                                                            @ErrorState = ERROR_STATE();
                                                        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
                                                    End Catch";

        private const string DeleteUnosWorkListSql = @"Begin Try
                                                        Begin Transaction DeleteUnosFormData
                                                            Delete Audit_UnosSavedForm where UnosSavedFormId in (select SavedFormID from UnosWorkListToSavedform where WorkListID = @id)
                                                            Delete UnosWorkListToSavedForm where WorkListID = @id
                                                            Delete UnosWorkList where ID = @id
                                                            Delete UnosValue where SavedFormID in (select SavedFormID from UnosWorkListToSavedform where WorkListID = @id)
                                                            Delete UnosSavedForm where ID in (select SavedFormID from UnosWorkListToSavedform where WorkListID = @id)
                                                            Commit Transaction DeleteUnosFormData
                                                    End Try
                                                    Begin Catch
                                                        Rollback Transaction DeleteUnosFormData
                                                        DECLARE @ErrorMessage NVARCHAR(4000);
                                                        DECLARE @ErrorSeverity INT;
                                                        DECLARE @ErrorState INT;
                                                        SELECT 
                                                            @ErrorMessage = ERROR_MESSAGE(),
                                                            @ErrorSeverity = ERROR_SEVERITY(),
                                                            @ErrorState = ERROR_STATE();
                                                        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
                                                    End Catch";

        #endregion



    }


    public class TableSummary
    {
        public DataSet TableDS;
        public bool ModifiableTable;
        public List<string> EditableFields;
    }

    public class DynamicSQL
    {
        private string sql;
        private List<SqlParameter> sqlParameters;

        public string Sql
        {
            get
            {
                return sql;
            }
        }
        public List<SqlParameter> SqlParameters
        {
            get
            {
                return sqlParameters;
            }
        }

        public DynamicSQL()
        {
            sql = null;
            sqlParameters = new List<SqlParameter>();
        }

        public static DynamicSQL CreateDynamicInsertSQL(string DataTableName, List<InsertUpdateParam> ParamList)
        {
            StringBuilder sb = new StringBuilder(string.Format("INSERT INTO [{0}] ", DataTableName));
            DynamicSQL dSql = new DynamicSQL();

            StringBuilder fieldSpecs = new StringBuilder();
            StringBuilder valuesSpecs = new StringBuilder();

            if (ParamList != null && ParamList.Count > 0)
            {
                foreach (InsertUpdateParam param in ParamList)
                {
                    fieldSpecs.Append("[");
                    fieldSpecs.Append(param.FieldName);
                    fieldSpecs.Append("]");
                    fieldSpecs.Append(",");
                    valuesSpecs.Append(param.Param.ParameterName);
                    valuesSpecs.Append(",");
                    dSql.sqlParameters.Add(param.Param);
                }

                string fieldSpecsStr = fieldSpecs.ToString();
                string valuesSpecsStr = valuesSpecs.ToString();

                fieldSpecsStr = fieldSpecsStr.Remove(fieldSpecsStr.LastIndexOf(","));
                valuesSpecsStr = valuesSpecsStr.Remove(valuesSpecsStr.LastIndexOf(","));

                sb.Append(" (");
                sb.Append(fieldSpecsStr);
                sb.Append(" ) values (");
                sb.Append(valuesSpecsStr);
                sb.Append(" )");

                dSql.sql = sb.ToString();
            }

            return dSql;
        }
        public static DynamicSQL CreateDynamicUpdateSQL(string DataTableName, List<InsertUpdateParam> ParamList, List<QueryFilter> QueryFilterList)
        {
            StringBuilder sb = new StringBuilder(string.Format("UPDATE {0} ", DataTableName));
            DynamicSQL dSql = new DynamicSQL();

            if (ParamList != null && ParamList.Count > 0)
            {
                sb.Append(" set ");
                sb.Append(ParamList[0].FieldName);
                sb.Append(" = ");
                sb.Append(ParamList[0].Param.ParameterName);
                dSql.sqlParameters.Add(ParamList[0].Param);
                sb.Append(",");

                for (int i = 1; i < ParamList.Count; i++)
                {
                    sb.Append(ParamList[i].FieldName);
                    sb.Append(" = ");
                    sb.Append(ParamList[i].Param.ParameterName);
                    dSql.sqlParameters.Add(ParamList[i].Param);
                    sb.Append(",");
                }
            }

            dSql.sql = sb.ToString();

            if (dSql.sql.EndsWith(","))
                dSql.sql = dSql.sql.Remove(dSql.sql.LastIndexOf(","));

            DynamicSQL tempDSQL = CreateWhereClauseDynamically(QueryFilterList, null);
            dSql.sql += tempDSQL.sql;
            dSql.sqlParameters.AddRange(tempDSQL.sqlParameters);

            return dSql;
        }
        public static DynamicSQL CreateDynamicSelectSQL(string DataTableName, List<QueryFilter> QueryFilterList, string[] OrderByFields)
        {
            StringBuilder sb = new StringBuilder(string.Format(" SELECT * FROM [{0}] WHERE ", DataTableName));

            if (MultiTenantHelper.IsMultiTenantTable(DataTableName))
            {
                sb.Append("TenantID = dbo.GetTenantID()");
            }
            else
            {
                sb.Append("1=1");
            }

            DynamicSQL dSql = new DynamicSQL();

            if (QueryFilterList != null && QueryFilterList.Count > 0)
            {
                sb.Append(" and ");
                sb.Append(QueryFilterList[0].FilterString);
                dSql.sqlParameters.Add(QueryFilterList[0].Param);

                for (int i = 1; i < QueryFilterList.Count; i++)
                {
                    sb.Append(" and ");
                    sb.Append(QueryFilterList[i].FilterString);
                    dSql.sqlParameters.Add(QueryFilterList[i].Param);
                }
            }

            if (OrderByFields != null && OrderByFields.Length > 0)
            {
                sb.Append(" order by ");

                foreach (string OrderByField in OrderByFields)
                {
                    sb.Append(OrderByField);
                    sb.Append(",");
                }
            }

            dSql.sql = sb.ToString();

            if (dSql.sql.EndsWith(","))
                dSql.sql = dSql.sql.Remove(dSql.sql.LastIndexOf(","));

            return dSql;
        }

        public static DynamicSQL CreateDynamicSelectSQL(int TopRecordsToSelect, string DataTableName, List<QueryFilter> QueryFilterList, string[] OrderByFields)
        {
            StringBuilder sb = new StringBuilder(string.Format(" SELECT TOP {0} * FROM [{1}] WHERE ", TopRecordsToSelect, DataTableName));

            if (MultiTenantHelper.IsMultiTenantTable(DataTableName))
            {
                sb.Append("TenantID = dbo.GetTenantID()");
            }
            else
            {
                sb.Append("1=1");
            }

            DynamicSQL dSql = new DynamicSQL();

            if (QueryFilterList != null && QueryFilterList.Count > 0)
            {
                sb.Append(" and ");
                sb.Append(QueryFilterList[0].FilterString);
                dSql.sqlParameters.Add(QueryFilterList[0].Param);

                for (int i = 1; i < QueryFilterList.Count; i++)
                {
                    sb.Append(" and ");
                    sb.Append(QueryFilterList[i].FilterString);
                    dSql.sqlParameters.Add(QueryFilterList[i].Param);
                }
            }

            if (OrderByFields != null && OrderByFields.Length > 0)
            {
                sb.Append(" order by ");

                foreach (string OrderByField in OrderByFields)
                {
                    sb.Append(OrderByField);
                    sb.Append(",");
                }
            }

            dSql.sql = sb.ToString();

            if (dSql.sql.EndsWith(","))
                dSql.sql = dSql.sql.Remove(dSql.sql.LastIndexOf(","));

            return dSql;
        }

        public static DynamicSQL CreateWhereClauseDynamically(List<QueryFilter> QueryFilterList, string[] OrderByFields)
        {
            StringBuilder sb = new StringBuilder();
            DynamicSQL dSql = new DynamicSQL();

            if (QueryFilterList != null && QueryFilterList.Count > 0)
            {
                sb.Append(" where ");
                sb.Append(QueryFilterList[0].FilterString);
                dSql.sqlParameters.Add(QueryFilterList[0].Param);

                for (int i = 1; i < QueryFilterList.Count; i++)
                {
                    sb.Append(" and ");
                    sb.Append(QueryFilterList[i].FilterString);
                    dSql.sqlParameters.Add(QueryFilterList[i].Param);
                }
            }

            if (OrderByFields != null && OrderByFields.Length > 0)
            {
                sb.Append(" order by ");

                foreach (string OrderByField in OrderByFields)
                {
                    sb.Append(OrderByField);
                    sb.Append(",");
                }
            }

            dSql.sql = sb.ToString();

            if (dSql.sql.EndsWith(","))
                dSql.sql = dSql.sql.Remove(dSql.sql.LastIndexOf(","));

            return dSql;
        }

    }

    public class QueryFilter
    {
        public string FilterString;
        public SqlParameter Param;
    }

    public class InsertUpdateParam
    {
        public string FieldName;
        public SqlParameter Param;
    }

    



    [Serializable]
    [XmlRoot("ExceptionInfo")]
    public class TcExceptionSummary
    {
        public static bool Debug;

        private TcException exception;

        public TcExceptionSummary() { }

        public TcExceptionSummary(TcException ex)
        {
            exception = ex;
        }

        public string ExceptionType
        {
            get { return exception.GetType().AssemblyQualifiedName; }
            set { }
        }


        public string Message
        {
            get { return exception.Message; }
            set { }
        }

        public string Source
        {
            get { return exception.Source; }
            set { }
        }

        public string StackTrace
        {
            get { return exception.StackTrace; }
            set { }
        }

        public string UserMessage
        {
            get
            {
                if (Debug)
                    return exception.UserMessage + "\r\n<br />\r\n" + exception.ToString();
                else
                    return exception.UserMessage;
            }
            set { }
        }


        [XmlIgnore()]
        public TcException OriginalException
        {
            get { return exception; }
            set { exception = value; }
        }

    }

    public class TcException : Exception
    {

        public TcException()
            : base() { }

        public TcException(string userMessage)
            : base(userMessage) { this.userMessage = userMessage; }

        public TcException(string userMessage, Exception innerException)
            : base(userMessage, innerException) { this.userMessage = userMessage; }

        public TcException(string userMessage, string exceptionMessage, Exception innerException)
            : base(exceptionMessage, innerException) { this.userMessage = userMessage; }


        private string userMessage;
        public string UserMessage
        {
            get { return userMessage; }
        }

    }

    public class TcNotAuthenticatedException : TcException
    {
        public TcNotAuthenticatedException()
            : base("WebService authentication failed.")
        {
        }
    }

    public class DataAccessException : TcException
    {
        public DataAccessException(string message, Exception innerException)
            : base(message, message, innerException)
        { }
    }

    public class UserInputException : TcException
    {
        public UserInputException(string userMessage) : base(userMessage) { }

    }



    public interface ITcWebMethodResult
    {
        bool Success { get; set; }
        TcExceptionSummary ExceptionSummary { get; set; }

    }

    public abstract class TcWebMethodResultBase : ITcWebMethodResult
    {

        protected bool success;
        protected TcExceptionSummary exceptionSummary;

        protected TcWebMethodResultBase()
        {
            success = true;
        }

        #region ITcWebMethodResult Members

        public bool Success
        {
            get { return this.success; }
            set { this.success = value; }
        }

        public TcExceptionSummary ExceptionSummary
        {
            get { return this.exceptionSummary; }
            set
            {
                this.exceptionSummary = value;
                this.success = false;
            }
        }

        #endregion
    }

    public class TcWebMethodResult<ResultType> : TcWebMethodResultBase
    {

        public TcWebMethodResult() : base() { }
        public TcWebMethodResult(ResultType value) : base() { this.value = value; this.Success = true; }
        public TcWebMethodResult(TcException ex) : base() { this.ExceptionSummary = new TcExceptionSummary(ex); this.Success = false; }

        private ResultType value;
        public ResultType Value
        {
            get { return this.value; }
            set { this.value = value; base.Success = true; }
        }

        [XmlIgnore]
        public TcException Exception
        {
            set
            {
                base.ExceptionSummary = new TcExceptionSummary(value);
            }
        }

    }

    public class TcWebMethodResultVoid : TcWebMethodResultBase
    {
        public TcWebMethodResultVoid() : base() { }
        public TcWebMethodResultVoid(TcException ex) : base() { this.ExceptionSummary = new TcExceptionSummary(ex); this.Success = false; }

        [XmlIgnore]
        public TcException Exception
        {
            set
            {
                base.ExceptionSummary = new TcExceptionSummary(value);
            }
        }
    }
}


namespace TC.Domain
{

    //public interface IAdminUserManager
    //{
    //    TC.Domain.AdminUser GetAdminUserByUsername(string userName);
    //    IEnumerable<TC.Domain.AdminUser> GetAllAdminUsers();

    //    void AddAdminUser(TC.Domain.AdminUser adminUser);

    //    void UpdateAdminUser(TC.Domain.AdminUser adminUser);

    //    void DeleteAdminUser(string userName);
    //}

    //public interface ITenantManager
    //{
    //    Tenant GetTenant();
    //}

    /*

    public class CommonEntity : MultiEnteredByEntity<int> 
    {
        public bool Exists(string cnn) { throw new NotImplementedException(); }
        
        public void Insert(string cnn) { }
        public void Update(string cnn) { }
        public void Delete(string cnn) { }
    }


    public class AdminUser : User 
    { } 
    public class AdminUserList : List<AdminUser> { }

    
    public class BlockedPatient : CommonEntity 
    {
        public virtual string UserName { get; set; }
        public virtual string MRN { get; set; }
        public virtual string Comments { get; set; }
        

        public static BlockedPatientsList LoadAll(string cnn) { throw new NotImplementedException(); }
    } 
    public class BlockedPatientsList : List<BlockedPatient> { }

    public class Patient : CommonEntity
    {
        public static void UpdateCurrentTx(string connectionString, string mrn)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();

                try
                {
                    SqlParameterList parms = new SqlParameterList("@MRN", mrn);
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text,
                        "update candidate set cur = 0 where mrn = @MRN;",
                        parms.ToArray());
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text,
                        "update candidate set cur = 1 where mrn = @MRN and txnum = (select max(txnum) from candidate where mrn = @MRN);",
                        parms.ToArray());
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new DataAccessException("An error occurred while marking current tx num.", ex);
                }
            }
        }

    }
    public class PatientsList : List<Patient> { }

    public class PatientImage : CommonEntity
    {
        public static PatientImageList SelectForMrn(string connectionString, string mrn)
        { throw new NotImplementedException(); }

        public static PatientImage Select(string connectionString, string mrn, int seqNo)
        { throw new NotImplementedException(); }

        public static void Insert(string connectionString, PatientImage obj)
        { }

        public static void Delete(string connectionString, PatientImage obj)
        { }

    }
    public class PatientImageList : List<PatientImage> { }


    #region Audit

    public class ClientServerAudit
    { } 
    public class ClientServerAuditList : List<ClientServerAudit> { }

    public class LdapAudit
    { }
    public class LdapAuditList : List<LdapAudit> { }

    public class PatientAccessAudit
    { }
    public class PatientAccessAuditList : List<PatientAccessAudit> { }

    #endregion


    #region Config

    public class ConfigField : CommonEntity
    {
        public static ConfigField LoadByTableIDAndFieldName(string connectionString, int tableID, string fieldName) { throw new NotImplementedException(); }
        public static ConfigFieldList LoadByTableIDAndNoneComputedAndCheckOffColumnNone(string connectionString, int tableID) { throw new NotImplementedException(); }
        public static ConfigFieldList LoadByTableIDWhereFieldDBNameLikeDTA(string connectionString, int tableID) { throw new NotImplementedException(); }

    } 
    public class ConfigFieldList : List<ConfigField> { }


    public class ConfigForm : CommonEntity 
    {
        public virtual int FormID { get; set; }
        public virtual int GroupID { get; set; }
        public virtual string FormName { get; set; }
        public virtual int DisplayOrder { get; set; }
        public virtual bool? LoadPreviousValue { get; set; }
        public virtual int? MaxTextBoxWidth { get; set; }
        public virtual bool? EnableSetToNormal { get; set; }


        public static ConfigFormList LoadByGroupID(string connectionString, int GroupID) { throw new NotImplementedException(); }
        public static ConfigFormList SelectAll(string connectionString){ throw new NotImplementedException(); }
        public static ConfigForm Select(string connectionString, int id) { throw new NotImplementedException(); }
        public static void Update(string connectionString, ConfigForm obj){ throw new NotImplementedException(); }
        public static void DeletePatientForm(string connectionString, int formID, string MRN, DateTime date) { throw new NotImplementedException(); }
        
    } 
    public class ConfigFormList : List<ConfigForm> { }


    public class ConfigFormHeader : CommonEntity
    {
        public virtual int HeaderID { get; set; }
        public virtual int FormID { get; set; }
        public virtual string Header { get; set; }

        public static ConfigFormHeaderList LoadByFormID(string connectionString, int FormID) { throw new NotImplementedException(); }

    } 
    public class ConfigFormHeaderList : List<ConfigFormHeader> { }


    public class ConfigFormLookupObjectType : CommonEntity 
    { } 
    public class ConfigFormLookupObjectTypeList : List<ConfigFormLookupObjectType> { }


    public class ConfigFormQuestion : CommonEntity
    { } 
    public class ConfigFormQuestionList : List<ConfigFormQuestion> { }


    public class ConfigFormQuestionResponse : CommonEntity
    { } 
    public class ConfigFormQuestionResponseList : List<ConfigFormQuestionResponse> { }


    public class ConfigFormsGroup : CommonEntity
    {
        public virtual int GroupID { get; set; }
        public virtual string GroupName { get; set; }
        public virtual string TableName { get; set; }
        public virtual int? DisplayOrder { get; set; }
        public virtual string AuditTable { get; set; }
    } 
    public class ConfigFormsGroupList : List<ConfigFormsGroup> { }


    public class ConfigLogonMessage : CommonEntity
    { }
    public class ConfigLogonMessageList : List<ConfigLogonMessage> { }


    public class ConfigMenu : CommonEntity
    { }
    public class ConfigMenuList : List<ConfigMenu> { }


    public class ConfigMultipleEntry : CommonEntity
    {
        public virtual int MultipleEntryId { get; set; }
        public virtual string TableName { get; set; }
        public virtual string FieldName { get; set; }

        public static ConfigMultipleEntryList LoadByMultipleEntryID(string connectionString, int MultipleEntryID) { throw new NotImplementedException(); }

    }
    public class ConfigMultipleEntryList : List<ConfigMultipleEntry> { }


    public class ConfigSelectCandidate : CommonEntity
    { }
    public class ConfigSelectCandidateList : List<ConfigSelectCandidate> { }


    public class ConfigTable : CommonEntity
    {
        public static ConfigTableList LoadAllOrderByUserScreenName(string connectionString) { throw new NotImplementedException(); }
 
    }
    public class ConfigTableList : List<ConfigTable> { }


    public class ConfigTableDisplayOrder : CommonEntity
    { }
    public class ConfigTableDisplayOrderList : List<ConfigTableDisplayOrder> { }

    
    public class ConfigSelectPatient : CommonEntity
    { }
    public class ConfigSelectPatientList : List<ConfigSelectPatient> { }


    public class ConfigWallChart : CommonEntity
    { }
    public class ConfigWallChartList : List<ConfigSelectPatient> { }


    #endregion


    public class FailedLogin : CommonEntity
    { }
    public class FailedLoginList : List<FailedLogin> { }

    public class GroupLookup : CommonEntity
    {
        public static List<GroupLookup> GetGroupIDList(string connectionString, int functionID) { throw new NotImplementedException(); }
        public static List<GroupLookup> GetDialysisGroupIDList(string connectionString, int functionID) { throw new NotImplementedException(); }

    }
    public class GroupLookupList : List<GroupLookup> { }

    public class GroupSecurity : CommonEntity
    {
        //public virtual int ID { get; set; }
        public virtual int FunctionID { get; set; }
        public virtual string GroupID { get; set; }
        public virtual string DialysisGroupID { get; set; }
        public virtual bool? Deny { get; set; }
        //public virtual System.Guid TenantID { get; set; }

        public static GroupSecurityCollection GetGroupSecurity(string connectionString, int functionID) { throw new NotImplementedException(); }
    }
    public class GroupSecurityCollection : List<GroupSecurity> { }

    
    public class HL7Lookup : CommonEntity
    { }
    public class HL7LookupList : List<HL7Lookup> { }

    public class HL7LookupLabInterface : CommonEntity
    { }
    public class HL7LookupLabInterfaceList : List<HL7LookupLabInterface> { }

    public class HL7ProcessedData : CommonEntity
    { }
    public class HL7ProcessedDataList : List<HL7ProcessedData> { }

    public class HL7ProcessedError : CommonEntity
    {
        public enum RecordsToRetrieve
        {
            All,
            Imported,
            NonImported
        }
    
    }
    public class HL7ProcessedErrorList : List<HL7ProcessedError> { }

    public class InterfaceReportData : CommonEntity
    { }


    public class LookupLabFaceSheet : CommonEntity
    { }
    public class LookupLabFaceSheetList : List<LookupLabFaceSheet> { }


    public class LookupMedication : CommonEntity
    { }
    public class LookupMedicationList : List<LookupMedication> { }

    public class LookupMedicationChildren : CommonEntity
    { }
    public class LookupMedicationChildrenList : List<LookupMedicationChildren> { }

    public class LookupMedicationClass : CommonEntity
    { }
    public class LookupMedicationClassList : List<LookupMedicationClass> { }

    public class LookupMedicationForm : CommonEntity
    { }
    public class LookupMedicationFormList : List<LookupMedicationForm> { }

    public class LookupMedicationFrequency : CommonEntity
    { }
    public class LookupMedicationFrequencyList : List<LookupMedicationFrequency> { }

    public class LookupMedFrequency : CommonEntity
    { }
    public class LookupMedFrequencyList : List<LookupMedFrequency> { }

    public class LookupMedRoute : CommonEntity
    { }
    public class LookupMedRouteList : List<LookupMedRoute> { }

    public class LookupMedUnit : CommonEntity
    { }
    public class LookupMedUnitList : List<LookupMedUnit> { }


    public class LookupMenuLink : CommonEntity
    { }
    public class LookupMenuLinkList : List<LookupMenuLink> { }

    public class LookupScreen : CommonEntity
    { }
    public class LookupScreenList : List<LookupScreen> { }

    public class LookupSubClass : CommonEntity
    { }
    public class LookupSubClassList : List<LookupSubClass> { }


    public class LookupTransplantType : CommonEntity
    { }
    public class LookupTransplantTypeCollection : List<LookupTransplantType> { }

    //public class LookupTransplantTypeCollection : CommonEntity
    //{ }
    //public class LookupTransplantTypeCollectionList : List<LookupTransplantTypeCollection> { }

    public class LookupUNOSLabField : CommonEntity
    { }
    public class LookupUNOSLabFieldList : List<LookupUNOSLabField> { }

    public class LookupUNOSMedicationField : CommonEntity
    { }
    public class LookupUNOSMedicationFieldList : List<LookupUNOSMedicationField> { }

    public class LookupUNOSMedClass : CommonEntity
    { }
    public class LookupUNOSMedClassList : List<LookupUNOSMedClass> { }
    
    public class MenuItem : CommonEntity
    { }
    public class MenuItemCollection : List<MenuItem> { }

    
    public class MRNMergeData : CommonEntity
    {
        public static MRNMergeData GetMRNMergeData(string connectionString, string oldMRN, string newMRN)
        { throw new NotImplementedException(); }

        public static DataSet GetTableDetails(string connectionString, string mrn, string tableName)
        { throw new NotImplementedException(); }

        public static bool DeleteRecords(string connectionString, string mrn, List<string> tableNames)
        { throw new NotImplementedException(); }

        public static bool MergeRecords(string connectionString, string oldMrn, string newMrn, List<string> tableNames, string enteredBy)
        { throw new NotImplementedException(); }
    }
    //public class MRNMergeDataList : List<MRNMergeData> { }


    public class Note : CommonEntity
    { }
    public class NoteList : List<Note> { }

    public class NoteGroupUser : CommonEntity
    {
        public static NoteGroupUserList LoadByNoteGroupName(string connectionString, string NoteGroupName, Guid tenantID) { throw new NotImplementedException(); }
        public static NoteGroupUserList LoadByNoteGroupName(string connectionString, string NoteGroupName) { throw new NotImplementedException(); }
        public static NoteGroupUserList LoadAll(string connectionString, Guid tenantID) { throw new NotImplementedException(); }
    }
    public class NoteGroupUserList : List<NoteGroupUser> { }
    
    public class NoteUserGroupDetails : CommonEntity
    {
        public virtual string NoteGroupName { get; set; }
        public virtual string UserName { get; set; }

        public static List<string> GetDistinctNoteGroupNames(string connectionString) { throw new NotImplementedException(); }
        public static void DeleteByNoteGroupName(string connectionString, string NoteGroupName) { throw new NotImplementedException(); }
        public static bool NoteGroupNameExists(string connectionString, string NoteGroupName) { throw new NotImplementedException(); }
    }
    public class NoteUserGroupDetailsList : List<NoteUserGroupDetails> { }


    public class RxNormDrugSearch : CommonEntity
    { }
    public class RxNormDrugSearchList : List<RxNormDrugSearch> { }
    public class RxNormDrugSearchResult
    {
        public int TotalCount
        {
            get;
            set;
        }

        public RxNormDrugSearchList Results
        {
            get;
            set;
        }
    }


    public class UserSecurity : CommonEntity
    {
        public static UserSecurityCollection GetUsersSecurity(string connectionString, int functionID) { throw new NotImplementedException(); }
    }
    public class UserSecurityCollection : List<UserSecurity> {}

    public class SecurityFunction : CommonEntity
    {// Note: table='Functions'

        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        //public virtual System.Guid TenantID { get; set; }

        public static SecurityFunctionCollection GetSecurityFunctions(string connectionString) { throw new NotImplementedException(); }

        public static SecurityFunction GetSecurityFunction(string connectionString, int id) { throw new NotImplementedException(); }
 
    }
    public class SecurityFunctionCollection : List<SecurityFunction> { }

    public class SecurityPageFunction : CommonEntity
    {
        public static SecurityPageFunctionCollection GetPageFunctions(string connectionString, int functionID) { throw new NotImplementedException(); }
    
    }
    public class SecurityPageFunctionCollection : List<SecurityPageFunction> { }


    public class UnosFormData : CommonEntity
    { }
    public class UnosFormDataList : List<UnosFormData> { }

    public class UnosForm : CommonEntity
    { }
    public class UnosFormList : List<UnosForm> { }

    
    
    //public class  : CommonEntity
    //{ }
    //public class List : List<> { }
    */
    
}
