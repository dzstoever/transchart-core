using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TC.Domain 
{    
    [Serializable]
    public class AppointmenTId : NaturalKeyStringDateTimeDateTimeInt32 //_Guid
    {
        [StringLength(15)]
        public string MRN           { get { return Key1; } set { Key1 = value; } }
        public DateTime VisitDate   { get { return Key2; } set { Key2 = value; } }
        public DateTime VisitTime   { get { return Key3; } set { Key3 = value; } }
        public Int32 VisitTypeID    { get { return Key4; } set { Key4 = value; } }        
    }
    
    public class Appointment : MultiEnteredByEntity<AppointmenTId>
    {                
        public virtual int? LocationID { get; set; }    //FK?
        [StringLength(50)]
        public virtual string VisitStatus { get; set; }
        [StringLength(255)]
        public virtual string Notes { get; set; }
        [StringLength(50)]
        public virtual string VisitType { get; set; }
        [StringLength(50)]
        public virtual string ICD9 { get; set; }
        [StringLength(150)]
        public virtual string ICD9Description { get; set; }
        [StringLength(50)]
        public virtual string ClinicLocation { get; set; }
        [StringLength(50)]
        public virtual string Service { get; set; }
        [StringLength(50)]
        public virtual string Physician { get; set; }
    }
}
