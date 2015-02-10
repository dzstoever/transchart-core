using System; 
using System.Collections.Generic; 
using System.Text; 
using TC.Domain;


namespace TC.Domain {
    
    
    [DataContract(Name="EpisodeDonor_MultiData" , Namespace="")]
    public class EpisodeDonor_MultiData {
        
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
        public virtual String Coordinator1 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Coordinator2 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String EvalPhysician1 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String EvalPhysician2 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String SocialWorker1 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String SocialWorker2 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Technician {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String TxPhysician1 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String TxPhysician2 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Boolean ArchiveDonor {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Boolean CADDonor {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String DonorType {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String NursePractitioner {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Boolean Outreach {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String OutreachLocation {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Recipient {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String RecipientMRN {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Int32 RecipientTxNum {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Relationship {
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
