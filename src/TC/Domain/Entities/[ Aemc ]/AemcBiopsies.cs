using System;
using System.ComponentModel.DataAnnotations;

namespace TC.Domain.Entities 
{
    [Serializable]
    public class AemcBiopsiesId : NaturalKeyStringInt32
    {// There is no PK in the db, also Counter is nullable, so I am guessing here but my tests pass...
        [StringLength(15)]
        public virtual string MRN { get { return Key1; } set { Key1 = value; } }
        public virtual int Counter { get { return Key2; } set { Key2 = value; } }        
    }

    public class AemcBiopsies : MultiEntity<AemcBiopsiesId> 
    {
        public virtual int? TxNum { get; set; }        
        public virtual int? PATNum { get; set; }
        public virtual int? UniqueCan { get; set; }
        [StringLength(15)]
        public virtual string SpecimenNum { get; set; }
        public virtual DateTime? DateBx { get; set; }
        [StringLength(50)]
        public virtual string Source { get; set; }
        [StringLength(50)]
        public virtual string FourPortalTracts { get; set; }
        [StringLength(50)]
        public virtual string OtherwiseAdequate { get; set; }
        [StringLength(50)]
        public virtual string Inflammation { get; set; }
        [StringLength(50)]
        public virtual string BDDamage { get; set; }
        [StringLength(50)]
        public virtual string BDLoss { get; set; }
        [StringLength(50)]
        public virtual string BDLDegree { get; set; }
        [StringLength(50)]
        public virtual string BDProliferation { get; set; }
        [StringLength(50)]
        public virtual string Arteritis { get; set; }
        [StringLength(50)]
        public virtual string Endothelialitis { get; set; }
        [StringLength(50)]
        public virtual string PortalFibrosis { get; set; }
        [StringLength(50)]
        public virtual string CentralFibrosis { get; set; }
        [StringLength(50)]
        public virtual string Balloning { get; set; }
        [StringLength(50)]
        public virtual string DegreeofBallooning { get; set; }
        [StringLength(50)]
        public virtual string PiecemealNecrosis { get; set; }
        [StringLength(50)]
        public virtual string Bridging { get; set; }
        [StringLength(50)]
        public virtual string CentralNecrosis { get; set; }
        [StringLength(50)]
        public virtual string DegreeCLN { get; set; }
        [StringLength(50)]
        public virtual string Cholestasis { get; set; }
        [StringLength(50)]
        public virtual string Steatosis { get; set; }
        [StringLength(50)]
        public virtual string SteatosisType { get; set; }
        [StringLength(50)]
        public virtual string Lobularinflammation { get; set; }
        [StringLength(50)]
        public virtual string Microabscesses { get; set; }
        [StringLength(50)]
        public virtual string Granulomas { get; set; }
        [StringLength(50)]
        public virtual string PathDiagnosis { get; set; }
        [StringLength(50)]
        public virtual string OtherBiliary { get; set; }
        [StringLength(50)]
        public virtual string DegreeIInjury { get; set; }
        [StringLength(50)]
        public virtual string ViralDegree { get; set; }
        [StringLength(50)]
        public virtual string TypeVirus { get; set; }
        [StringLength(50)]
        public virtual string DegreeofRejection { get; set; }
        [StringLength(50)]
        public virtual string Other { get; set; }
        [StringLength(50)]
        public virtual string Reason { get; set; }
        [StringLength(50)]
        public virtual string Timepoint { get; set; }
        [StringLength(50)]
        public virtual string BiopsyType { get; set; }
        [StringLength(50)]
        public virtual string BiopsyRoute { get; set; }
        [StringLength(50)]
        public virtual string TypeVisit { get; set; }
        [StringLength(255)]
        public virtual string Comment { get; set; }
    }
}
