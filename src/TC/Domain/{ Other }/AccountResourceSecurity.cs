using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TC.Domain 
{
    [Serializable]
    public class AccountResourceSecurityId : NaturalKeyStringStringStringString
    {
        public virtual string AccountType  { get { return Key1; } set { Key1 = value; } }
        public virtual string AccountID    { get { return Key2; } set { Key2 = value; } }
        public virtual string ResourceType { get { return Key3; } set { Key3 = value; } }
        public virtual string ResourceID   { get { return Key4; } set { Key4 = value; } }
    }

    
    public class AccountResourceSecurity : Zen.Core.DomainEntity<AccountResourceSecurityId> 
    {        
        public virtual bool? Authorized { get; set; }
        [Required]
        public virtual DateTime CreatedOn { get; set; }
        [Required] [StringLength(15)]
        public virtual string CreatedBy { get; set; }        
    }
}
