using System; 
using System.Collections.Generic; 
using System.Text; 
using TC.Domain;


namespace TC.Domain {
    
    
    [DataContract(Name="EpisodeReferral_MultiData" , Namespace="")]
    public class EpisodeReferral_MultiData {
        
        [DataMember()]
        public virtual Int32 ID {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Int32 EpisodeID {
            get;
            set;
        }
        
        [DataMember()]
        public virtual DateTime ReferralDate {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String PhysicianID {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Int32 ReferredFor {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String MRN {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String GeneralDiagnosis {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String InsuranceType {
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
        public virtual Guid TenantID {
            get;
            set;
        }
    }
}
