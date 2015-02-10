using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Zen.Core;

namespace TC.Domain
{
    public class Tenant : DomainEntity<Guid>
    {
        [StringLength(200)]
        public virtual string TenantName { get; set; }
        [StringLength(25)]
        public virtual string DefaultState { get; set; }
        [StringLength(200)]
        public virtual string DefaultCounty { get; set; }
        [StringLength(255)]
        public virtual string ReportHeader { get; set; }
        [StringLength(50)]
        public virtual string Location { get; set; }
        [StringLength(50)]
        public virtual string Country { get; set; }
        [StringLength(50)]
        public virtual string LocalOPO { get; set; }
        [StringLength(50)]
        public virtual string City { get; set; }
        [StringLength(50)]
        public virtual string TxCenterCode { get; set; }

        public virtual int? MRNLength { get; set; }
    }
}
