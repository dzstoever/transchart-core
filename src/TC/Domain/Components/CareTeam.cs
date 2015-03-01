namespace TC.Domain
{
    public abstract class CareTeam
    {
        public virtual string Coordinator1 { get; set; }
        public virtual string Coordinator2 { get; set; }
        public virtual string EvalPhysician1 { get; set; }
        public virtual string EvalPhysician2 { get; set; }
        public virtual string TxPhysician1 { get; set; }
        public virtual string TxPhysician2 { get; set; }
        public virtual string NursePractitioner { get; set; }
        public virtual string SocialWorker1 { get; set; }
        public virtual string SocialWorker2 { get; set; }
        public virtual string Technician { get; set; }
    }
}