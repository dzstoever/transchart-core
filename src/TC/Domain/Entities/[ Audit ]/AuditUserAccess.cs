using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TC.Domain.Entities 
{
    /// <summary>
    /// This is a name swap class please Use AuditUserAccess, instead
    /// </summary>
    public class UserAccessAudit : AuditUserAccess 
    {

        public static UserAccessAuditList SelectRange(string connectionString, DateTime? startDate, DateTime? endDate, int? maxRecords, string mrn, string user) { throw new NotImplementedException(); }
        //{
        //    UserAccessAuditList result = new UserAccessAuditList();
        //    SqlDataReader r = null;
        //    try
        //    {
        //        SqlParameterList prms = new SqlParameterList();
        //        prms.Add("@startdate", startDate ?? TypeHelper.MinDateTime);
        //        prms.Add("@enddate", endDate ?? TypeHelper.MaxDateTime);

        //        string sql = null;
        //        if (maxRecords != null)
        //        {
        //            sql = string.Format("SELECT TOP {0} * FROM [Audit_UserAccess] WHERE [AccessDate] BETWEEN @startDate AND @endDate", maxRecords);
        //        }
        //        else
        //        {
        //            sql = "SELECT * FROM [Audit_UserAccess] WHERE [AccessDate] BETWEEN @startDate AND @endDate";
        //        }

        //        if (user != null)
        //        {
        //            sql += " AND [username] like @user";
        //            prms.Add("@user", user + "%");
        //        }
        //        if (mrn != null)
        //        {
        //            sql += " AND [mrn] like @mrn";
        //            prms.Add("@mrn", mrn + "%");
        //        }

        //        sql += " ORDER BY AccessDate Desc, AccessTime Desc ";

        //        r = SqlHelper.ExecuteReader(connectionString, CommandType.Text, sql, prms.ToArray());

        //        result.AddRange(UserAccessAuditOMH.GetObjectList(r));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new DataAccessException("An error occurred while querying the database.", ex);
        //    }
        //    finally
        //    {
        //        if (!r.IsClosed)
        //            r.Close();
        //    }

        //    return result;
        //}

    }    
    public class UserAccessAuditList : List<UserAccessAudit> { }
    
    //internal class UserAccessAuditOMH : ObjectMappingBase<UserAccessAudit, UserAccessAuditList, UserAccessAuditOMH>, IObjectMappable<UserAccessAudit>
    //{

    //    public UserAccessAudit ReturnNextObject(SqlDataReader dr)
    //    {
    //        UserAccessAudit o = null;
    //        if (dr.HasRows && dr.Read())
    //        {
    //            o = new UserAccessAudit();

    //            o.AccessDate = TypeHelper.GetDateTime(dr["accessdate"]);
    //            o.AccessTime = TypeHelper.GetDateTime(dr["accesstime"]);
    //            o.HttpUserAgent = TypeHelper.GetString(dr["http_UserAgent"]);
    //            o.Id = (int)TypeHelper.GetInt(dr["ID"]);
    //            o.MRN = TypeHelper.GetString(dr["MRN"]);
    //            o.PageAccessed = TypeHelper.GetString(dr["pageaccessed"]);
    //            o.PageNumber = TypeHelper.GetString(dr["PAGENUMBER"]);
    //            o.RemoteMachine = TypeHelper.GetString(dr["RemoteMachine"]);
    //            o.SessionID = TypeHelper.GetString(dr["SessionID"]);
    //            o.UserName = TypeHelper.GetString(dr["username"]);
    //            o.AppName = TypeHelper.GetString(dr["AppName"]);
    //        }
    //        return o;
    //    }

    //    public SqlParameterList ReturnParameterList(UserAccessAudit o)
    //    {
    //        return new SqlParameterList(
    //        "@accessdate", o.AccessDate,
    //        "@accesstime", o.AccessTime,
    //        "@http_UserAgent", o.HttpUserAgent,
    //        "@ID", o.Id,
    //        "@MRN", o.MRN,
    //        "@pageaccessed", o.PageAccessed,
    //        "@PAGENUMBER", o.PageNumber,
    //        "@RemoteMachine", o.RemoteMachine,
    //        "@SessionID", o.SessionID,
    //        "@username", o.UserName,
    //        "@appname", o.AppName
    //        );
    //    }

    //}


    public class AuditUserAccess : MultiEntity<int>
    {
        public virtual DateTime? AccessDate { get; set; }
        public virtual DateTime? AccessTime { get; set; }
        [StringLength(10)]  public virtual string AppName { get; set; }
        [StringLength(35)]  public virtual string UserName { get; set; }
        [StringLength(200)] public virtual string ServerName { get; set; }
        [StringLength(35)]  public virtual string MRN { get; set; }        
        [StringLength(200)] public virtual string PageNumber { get; set; }
        [StringLength(200)] public virtual string PageAccessed { get; set; }
        [StringLength(200)] public virtual string RemoteMachine { get; set; }
        [StringLength(400)] public virtual string HttpUserAgent { get; set; }
        [StringLength(200)] public virtual string SessionID { get; set; }                        
                            public virtual string QueryString { get; set; }        
    }
}
