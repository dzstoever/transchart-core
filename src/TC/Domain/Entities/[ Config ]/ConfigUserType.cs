using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Zen.Core;


namespace TC.Domain 
{
    
    public class ConfigUserType : DomainEntity<int>// synthetic PK
    {
        [Required] 
        [StringLength(25)]  
        public virtual string UserType { get; set; }// actual PK        
        [Required] 
        [StringLength(250)] 
        public virtual string Description { get; set; }
        [Required] 
        public virtual bool Enabled { get; set; }

        // one to many associations (sets)
        public virtual ISet<User> Users { get; set; }
        public virtual void AddUser(User item)
        {
            if (Users == null) Users = new HashSet<User>();
            item.ConfigUserType = this;
            Users.Add(item);
        }
        public virtual void RemoveUser(User item)
        {
            if (Users == null || !Users.Contains(item)) return;
            item.ConfigUserType = null;
            Users.Remove(item);
        }
        
        public static ConfigUserTypeList LoadEnabledList(string cnn) { throw new NotImplementedException(); }        
        
    }
    public class ConfigUserTypeList : List<ConfigUserType> { }
}
