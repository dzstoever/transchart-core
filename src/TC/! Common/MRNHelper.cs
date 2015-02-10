using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TC
{
    public class MRNHelper
    {
        public static List<MrnSummary> SearchForLastName(string connectionString, string lastName)
        {
            List<MrnSummary> result = new List<MrnSummary>();
            SqlDataReader r = null;
            try
            {
                string sql = "SELECT [MRN], [LastName], [FirstName], [DOB], [SSN], [Sex]  FROM [Person] WHERE [LastName] like @lastName ";

                r = SqlHelper.ExecuteReader(connectionString, CommandType.Text, sql,
                    new SqlParameterList("@lastname", lastName).ToArray());

                // get the results
                while (r.HasRows && r.Read())
                {

                    MrnSummary item = new MrnSummary();
                    item.FirstName = TypeHelper.GetString(r["FirstName"]);
                    item.LastName = TypeHelper.GetString(r["LastName"]);
                    item.MRN = TypeHelper.GetString(r["MRN"]);
                    item.SSN = TypeHelper.GetString(r["SSN"]);
                    item.DOB = TypeHelper.GetDateTime(r["DOB"]);
                    item.Sex = TypeHelper.GetString(r["Sex"]);

                    result.Add(item);
                }

            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while searching for the specified last name.", ex);
            }
            finally
            {
                if (!r.IsClosed)
                    r.Close();
            }

            return result;
        }

        public static List<MrnSummary> GetWaitList(string connectionString, string organType)
        {
            List<MrnSummary> result = new List<MrnSummary>();
            SqlDataReader r = null;
            try
            {
                string sql = "SELECT wl.[MRN], wl.[LN], wl.[FN], c.[SSN], c.[DOB], c.[Sex], c.[Race], c.[ABO], wl.[TxNum], " +
                             "c.[Address], c.[Address2], c.[City], c.[State], c.[County], c.[Zip], c.[HPhone], c.[WPhone], " +
                             "c.[CPhone], c.[Pager], " +
                             "wl.[WaitlistDate], wl.[OrganID], wl.[OrganDisplay], wl.[OrganCodes] " +
                             "from View_Waitlist wl inner join Candidate c on wl.MRN=c.MRN and wl.TxNum=c.TxNum";

                r = SqlHelper.ExecuteReader(connectionString, CommandType.Text, sql);

                // get the results
                while (r.HasRows && r.Read())
                {

                    MrnSummary item = new MrnSummary();
                    item.FirstName = TypeHelper.GetString(r["FN"]);
                    item.LastName = TypeHelper.GetString(r["LN"]);
                    item.MRN = TypeHelper.GetString(r["MRN"]);
                    item.SSN = TypeHelper.GetString(r["SSN"]);
                    item.DOB = TypeHelper.GetDateTime(r["DOB"]);
                    item.Sex = TypeHelper.GetString(r["Sex"]);
                    item.Race = TypeHelper.GetString(r["Race"]);
                    item.ABO = TypeHelper.GetString(r["ABO"]);
                    item.TxNum = TypeHelper.GetInt(r["TxNum"]);
                    item.WaitlistDate = TypeHelper.GetDateTime(r["WaitListDate"]);
                    item.OrganID = TypeHelper.GetInt(r["OrganID"]);
                    item.OrganDisplay = TypeHelper.GetString(r["OrganDisplay"]);
                    item.OrganCodes = TypeHelper.GetString(r["OrganCodes"]);
                    item.Address = TypeHelper.GetString(r["Address"]);
                    item.Address2 = TypeHelper.GetString(r["Address2"]);
                    item.City = TypeHelper.GetString(r["City"]);
                    item.County = TypeHelper.GetString(r["County"]);
                    item.Zip = TypeHelper.GetString(r["Zip"]);
                    item.HomePhone = TypeHelper.GetString(r["HPhone"]);
                    item.WorkPhone = TypeHelper.GetString(r["WPhone"]);
                    item.CellPhone = TypeHelper.GetString(r["CPhone"]);
                    item.Pager = TypeHelper.GetString(r["Pager"]);
                    result.Add(item);
                }

            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while retrieving the wait list.", ex);
            }
            finally
            {
                if (!r.IsClosed)
                    r.Close();
            }

            return result;
        }

        public static List<Organ> GetOrganList(string connectionString)
        {
            List<Organ> result = new List<Organ>();
            SqlDataReader r = null;
            try
            {
                string sql = "SELECT distinct([OrganCodes]),[OrganDisplay],[OrganID] from View_Waitlist";

                r = SqlHelper.ExecuteReader(connectionString, CommandType.Text, sql);

                // get the results
                while (r.HasRows && r.Read())
                {

                    Organ item = new Organ();
                    item.OrganDisplay = TypeHelper.GetString(r["OrganDisplay"]);
                    item.OrganCodes = TypeHelper.GetString(r["OrganCodes"]);
                    item.OrganID = TypeHelper.GetString(r["OrganID"]);
                    result.Add(item);
                }

            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while getting the Organ List.", ex);
            }
            finally
            {
                if (!r.IsClosed)
                    r.Close();
            }

            return result;
        }

        public static MrnSummary GetMrnSummary(string connectionString, string mrn)
        {
            MrnSummary result = null;
            SqlDataReader r = null;
            try
            {
                r = SqlHelper.ExecuteReader(connectionString, CommandType.Text,
                    "Select MRN, LastName, FirstName, DOB, SSN, Sex, Race, ABO FROM Person WHERE MRN=@mrn",
                    new SqlParameterList("@mrn", mrn).ToArray()
                    );

                // get the results
                if (r.HasRows && r.Read())
                {
                    result = new MrnSummary();
                    result.FirstName = TypeHelper.GetString(r["FirstName"]);
                    result.LastName = TypeHelper.GetString(r["LastName"]);
                    result.MRN = TypeHelper.GetString(r["MRN"]);
                    result.SSN = TypeHelper.GetString(r["SSN"]);
                    result.DOB = TypeHelper.GetDateTime(r["DOB"]);
                    result.Sex = TypeHelper.GetString(r["Sex"]);
                    result.Race = TypeHelper.GetString(r["Race"]);
                    result.ABO = TypeHelper.GetString(r["ABO"]);
                }

            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while checking for a person with the specified MRN.", ex);
            }
            finally
            {
                if (!r.IsClosed)
                    r.Close();
            }

            return result;
        }

        public static List<DatabaseHelper.TableColumn> GetMrnTableColumns(string connectionString)
        {
            List<DatabaseHelper.TableColumn> results = new List<DatabaseHelper.TableColumn>();
            try
            {
                results.AddRange(DatabaseHelper.GetTablesWithColumn(connectionString, "MRN"));

                // add special cases

                var connection = new SqlConnection(connectionString);

                if (DatabaseHelper.CheckTableExistence(connection, "TT_HLA"))
                    results.Add(new DatabaseHelper.TableColumn("TT_HLA", "MRNUNOS"));

                if (DatabaseHelper.CheckTableExistence(connection, "TT_HLALINK"))
                {
                    results.Add(new DatabaseHelper.TableColumn("TT_HLALINK", "RECIPMRN"));
                    results.Add(new DatabaseHelper.TableColumn("TT_HLALINK", "DONORMRN"));
                }

                if (DatabaseHelper.CheckTableExistence(connection, "AUDIT_EPISODEDONOR"))
                    results.Add(new DatabaseHelper.TableColumn("AUDIT_EPISODEDONOR", "RECIPIENTMRN"));

                if (DatabaseHelper.CheckTableExistence(connection, "CADDONOR_DEMOGRAPHICSANDLABS"))
                    results.Add(new DatabaseHelper.TableColumn("CADDONOR_DEMOGRAPHICSANDLABS", "RECIPIENTMRN"));

                if (DatabaseHelper.CheckTableExistence(connection, "CADDONOR_KIDNEY"))
                    results.Add(new DatabaseHelper.TableColumn("CADDONOR_KIDNEY", "RECIPIENTMRN"));

                if (DatabaseHelper.CheckTableExistence(connection, "CADDONOR_LIVER"))
                    results.Add(new DatabaseHelper.TableColumn("CADDONOR_LIVER", "RECIPIENTMRN"));

                if (DatabaseHelper.CheckTableExistence(connection, "CADDONOR_PANCREAS"))
                    results.Add(new DatabaseHelper.TableColumn("CADDONOR_PANCREAS", "RECIPIENTMRN"));

                if (DatabaseHelper.CheckTableExistence(connection, "CADDONOR_HEART"))
                    results.Add(new DatabaseHelper.TableColumn("CADDONOR_HEART", "RECIPIENTMRN"));

                if (DatabaseHelper.CheckTableExistence(connection, "CADDONOR_LUNG"))
                    results.Add(new DatabaseHelper.TableColumn("CADDONOR_LUNG", "RECIPIENTMRN"));
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while getting table names.", ex);
            }

            return results;
        }

        public class Organ
        {
            public string OrganCodes;
            public string OrganDisplay;
            public string OrganID;
        }

        public class MrnSummary
        {
            public string MRN;
            public string LastName;
            public string FirstName;
            public string SSN;
            public DateTime? DOB;
            public string Sex;
            public string Race;
            public string ABO;
            public int? TxNum;
            public DateTime? WaitlistDate;
            public int? DaysWaitedAdjustment;
            public int? OrganID;
            public string OrganDisplay;
            public string OrganCodes;
            public int? Height;
            public int? Weight;
            public int? BMI;
            public string Address;
            public string Address2;
            public string City;
            public string County;
            public string Zip;
            public string HomePhone;
            public string WorkPhone;
            public string CellPhone;
            public string Pager;

            public override string ToString()
            {
                return string.Format("Name: {0}, {1} DOB: {2:d} SSN: {3}",
                    LastName, FirstName, DOB, SSN);
            }
        }

    }
}