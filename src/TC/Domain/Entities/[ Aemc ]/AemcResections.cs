using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TC.Domain 
{
    [Serializable]
    public class AemcResectionsId : NaturalKeyStringInt32
    {// There is no PK in the db, so I am guessing here but my tests pass...
        [StringLength(15)]
        public virtual string MRN { get { return Key1; } set { Key1 = value; } }
        [Required]
        public virtual int Counter { get { return Key2; } set { Key2 = value; } }        
    }

    public class AemcResections : MultiEntity<AemcResectionsId> 
    {
        public virtual short? PATNum { get; set; }
        public virtual DateTime? ReCandDte { get; set; }
        [StringLength(50)]
        public virtual string ResectNum { get; set; }
        public virtual DateTime? DateRef { get; set; }
        [StringLength(10)]
        public virtual string Race { get; set; }
        public virtual byte? Age { get; set; }
        [StringLength(50)]
        public virtual string RefMD { get; set; }
        [StringLength(50)]
        public virtual string TypeT { get; set; }
        [StringLength(50)]
        public virtual string Origin { get; set; }
        [StringLength(50)]
        public virtual string NameT { get; set; }
        [StringLength(50)]
        public virtual string Segment { get; set; }
        [StringLength(50)]
        public virtual string Symptom1 { get; set; }
        [StringLength(50)]
        public virtual string Symptom2 { get; set; }
        [StringLength(50)]
        public virtual string Symptom3 { get; set; }
        [StringLength(200)]
        public virtual string PMH { get; set; }
        [StringLength(50)]
        public virtual string PriorLS { get; set; }
        [StringLength(50)]
        public virtual string TypeLS { get; set; }
        public virtual DateTime? DateLS { get; set; }
        [StringLength(50)]
        public virtual string PriorS { get; set; }
        [StringLength(50)]
        public virtual string TypeS { get; set; }
        public virtual DateTime? DateS { get; set; }
        [StringLength(50)]
        public virtual string RF { get; set; }
        [StringLength(50)]
        public virtual string Chemo { get; set; }
        [StringLength(100)]
        public virtual string ChemAgDat { get; set; }
        [StringLength(50)]
        public virtual string Radiotherapy { get; set; }
        [StringLength(100)]
        public virtual string RxTxAgDat { get; set; }
        [StringLength(50)]
        public virtual string CTS { get; set; }
        [StringLength(50)]
        public virtual string CTRes { get; set; }
        [StringLength(50)]
        public virtual string Angio { get; set; }
        [StringLength(50)]
        public virtual string AngioRes { get; set; }
        [StringLength(50)]
        public virtual string Sono { get; set; }
        [StringLength(50)]
        public virtual string SonoRes { get; set; }
        [StringLength(50)]
        public virtual string MRI { get; set; }
        [StringLength(50)]
        public virtual string MRIRes { get; set; }
        [StringLength(50)]
        public virtual string BoneS { get; set; }
        [StringLength(50)]
        public virtual string BSRes { get; set; }
        [StringLength(50)]
        public virtual string Bx { get; set; }
        [StringLength(50)]
        public virtual string BxRes { get; set; }
        [StringLength(50)]
        public virtual string SxCand { get; set; }
        [StringLength(50)]
        public virtual string Surgeon { get; set; }
        public virtual DateTime? Date { get; set; }
        [StringLength(50)]
        public virtual string Procedure { get; set; }
        [StringLength(250)]
        public virtual string Comment { get; set; }
        public virtual byte? Duration { get; set; }
        [StringLength(50)]
        public virtual string Laparoscopy { get; set; }
        [StringLength(50)]
        public virtual string LapSx { get; set; }
        [StringLength(50)]
        public virtual string LapComment { get; set; }
        [StringLength(50)]
        public virtual string CellS { get; set; }
        [StringLength(50)]
        public virtual string RIS { get; set; }
        [StringLength(50)]
        public virtual string Transfusion { get; set; }
        [StringLength(50)]
        public virtual string BTxReq { get; set; }
        [StringLength(50)]
        public virtual string Intraopdeath { get; set; }
        public virtual DateTime? Extub { get; set; }
        public virtual byte? ICULOS { get; set; }
        public virtual byte? HospLOS { get; set; }
        [StringLength(50)]
        public virtual string Complications { get; set; }
        [StringLength(50)]
        public virtual string Comp1 { get; set; }
        public virtual DateTime? DateComp1 { get; set; }
        [StringLength(50)]
        public virtual string Comp2 { get; set; }
        public virtual DateTime? DateComp2 { get; set; }
        [StringLength(50)]
        public virtual string Comp3 { get; set; }
        public virtual DateTime? DateComp3 { get; set; }
        [StringLength(50)]
        public virtual string Comp4 { get; set; }
        public virtual DateTime? DateComp4 { get; set; }
        public virtual DateTime? FUDate { get; set; }
        [StringLength(50)]
        public virtual string StatusFU { get; set; }
        public virtual byte? MonthsFU { get; set; }
        [StringLength(50)]
        public virtual string Recurrence { get; set; }
        public virtual DateTime? RecDate { get; set; }
        [StringLength(50)]
        public virtual string RecSite { get; set; }
        [StringLength(50)]
        public virtual string DeathOccur { get; set; }
        [StringLength(20)]
        public virtual string HBsAg { get; set; }
        [StringLength(20)]
        public virtual string HBsAb { get; set; }
        [StringLength(20)]
        public virtual string HBcAb { get; set; }
        [StringLength(20)]
        public virtual string HBcAbIgM { get; set; }
        [StringLength(20)]
        public virtual string HBeAg { get; set; }
        [StringLength(20)]
        public virtual string HBeAb { get; set; }
        [StringLength(20)]
        public virtual string HBVDNA { get; set; }
        [StringLength(20)]
        public virtual string HDV { get; set; }
        [StringLength(20)]
        public virtual string HCVe { get; set; }
        [StringLength(20)]
        public virtual string HCVRIBA2 { get; set; }
        public virtual float? CEA { get; set; }
        public virtual float? AFP { get; set; }
        public virtual float? Ca153 { get; set; }
        public virtual float? Ca199 { get; set; }
        public virtual float? Ca125 { get; set; }
        public virtual float? LasaP { get; set; }
        [StringLength(50)]
        public virtual string HepArtInfPump { get; set; }
        [StringLength(5)]
        public virtual string RFA { get; set; }
        [StringLength(100)]
        public virtual string RFAepisodes { get; set; }
        [StringLength(5)]
        public virtual string HAI { get; set; }        
    }
}
