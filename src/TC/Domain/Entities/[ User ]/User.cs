using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Zen.Core;


namespace TC.Domain 
{
    public class UserList : List<User> { }

    public class User : DomainEntity<string> 
    {
        // derived properties
        public virtual string UserName { get { return Id; } set { Id = value; } } 
        public virtual string UserType { get { return ConfigUserType.UserType; } }
        
        // many-to-one (there will be a select issued to get this until it is set the first time)
        public virtual ConfigUserType ConfigUserType { get; set; }


        public virtual DateTime? ExpirationDate { get; set; }
        public virtual DateTime? LastFailedLogin { get; set; }
        public virtual DateTime? PWDExpired { get; set; }
        public virtual DateTime? LastLogin { get; set; }
        [StringLength(100)] public virtual string Password { get; set; }
        [StringLength(1)]   public virtual string AccessLevel { get; set; }
        [StringLength(25)]  public virtual string FN { get; set; }
        [StringLength(25)]  public virtual string LN { get; set; }
        [StringLength(15)]  public virtual string SSN { get; set; }
        [StringLength(30)]  public virtual string GroupID { get; set; }
        [StringLength(50)]  public virtual string Email { get; set; }
        [StringLength(1)]   public virtual string GroupLevel { get; set; }
        [StringLength(10)]  public virtual string location { get; set; }
        [StringLength(50)]  public virtual string PID { get; set; }
        [StringLength(1)]   public virtual string ShowLab { get; set; }
        [StringLength(4)]   public virtual string DefaultApp { get; set; }
        [StringLength(1)]   public virtual string PreAccessLevel { get; set; }
        [StringLength(15)]  public virtual string PWDEnteredBy { get; set; }
        [StringLength(50)]  public virtual string PtAccessForOrgans { get; set; }
        [StringLength(20)]  public virtual string DialysisGroupId { get; set; }        
        [StringLength(50)]  public virtual string WPhone { get; set; }
        [StringLength(50)]  public virtual string Pager { get; set; }
        [StringLength(50)]  public virtual string Department { get; set; }
        [StringLength(50)]  public virtual string Unit { get; set; }
        [StringLength(50)]  public virtual string CostCenter { get; set; }
        [StringLength(400)] public virtual string Comments { get; set; }        
        [StringLength(20)]  public virtual string ColorTheme { get; set; }        
        [StringLength(30)]  public virtual string PIN { get; set; }
        [StringLength(25)]  public virtual string Credentials { get; set; }
        public virtual bool? CadListAccess { get; set; }
        public virtual bool? ttlabUser { get; set; }
        public virtual bool? ApcoClient { get; set; }
        public virtual bool? AccountDisable { get; set; }
        public virtual bool? AccountTimeLocked { get; set; }
        public virtual bool? WebAccess { get; set; }
        public virtual bool? CATAccess { get; set; }
        public virtual bool? CreateOrder { get; set; }
        public virtual bool? TissueTypingAccess { get; set; }
        public virtual bool? MobileAccess { get; set; }
        public virtual bool? FinancialUpdateAccess { get; set; }
        public virtual int? FailedLogin { get; set; }
        [Required]
        public virtual int TenantConnectionStringID { get; set; }

        /*
        public object Insert(string cnn) { throw new NotImplementedException(); }
        public object Update(string cnn) { throw new NotImplementedException(); }
        public object Delete(string cnn) { throw new NotImplementedException(); }

        public static List<string> GetDialysisCenterGroupNames(string cnn) { throw new NotImplementedException(); }
        public static List<string> GetReferringPhysicianGroupNames(string cnn) { throw new NotImplementedException(); }

        public static UserList LoadUsersForUserSecurity(string cnn, int functionID) { throw new NotImplementedException(); }
        public static UserList LoadAllOrderByUserName(string cnn) { throw new NotImplementedException(); }
        public static UserList LoadAllOrderByLN(string cnn) { throw new NotImplementedException(); }
        public static UserList LoadAllForTenant(string cnn, Guid tenanTId) { throw new NotImplementedException(); }

        public static User LoadByUserName(string cnn, string userName) { throw new NotImplementedException(); }

        */
        
    }

}
