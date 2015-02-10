using System; 
using System.Collections.Generic; 
using System.Text; 
using TC.Domain;


namespace TC.Domain {
    
    
    [DataContract(Name="Episode_MultiData" , Namespace="")]
    public class Episode_MultiData {
        
        [DataMember()]
        public virtual Int32 Id {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String MRN {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Int32 EpisodeTypeID {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Int32 EpisodeNum {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Boolean IsCurrent {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Int32 Organ {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Int32 RowID {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String EnteredBy {
            get;
            set;
        }
        
        [DataMember()]
        public virtual DateTime EnteredDate {
            get;
            set;
        }
        
        [DataMember()]
        public virtual DateTime EnteredTime {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Guid TenantID {
            get;
            set;
        }
    }
}
