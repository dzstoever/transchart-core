using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using TC.Domain;


namespace TC.Maps 
{
    public class AemcResectionsMap : MultiMap<AemcResections, AemcResectionsId>, Zen.Data.IDbMap 
    {
        public AemcResectionsMap() 
        {
            Schema("dbo"); Table("AEMC_Resections_Multi");
            
            ComponentAsId(x => x.Id, compId =>
            {
                compId.Property(c => c.MRN);
                compId.Property(c => c.Counter);                
            });

            Property(x => x.PATNum, map => map.Precision(5));
            Property(x => x.ReCandDte);
            Property(x => x.ResectNum, map => map.Length(50));
            Property(x => x.DateRef);
            Property(x => x.Race, map => map.Length(10));
            Property(x => x.Age, map => map.Precision(3));
            Property(x => x.RefMD, map => map.Length(50));
            Property(x => x.TypeT, map => map.Length(50));
            Property(x => x.Origin, map => map.Length(50));
            Property(x => x.NameT, map => map.Length(50));
            Property(x => x.Segment, map => map.Length(50));
            Property(x => x.Symptom1, map => map.Length(50));
            Property(x => x.Symptom2, map => map.Length(50));
            Property(x => x.Symptom3, map => map.Length(50));
            Property(x => x.PMH, map => map.Length(200));
            Property(x => x.PriorLS, map => map.Length(50));
            Property(x => x.TypeLS, map => map.Length(50));
            Property(x => x.DateLS);
            Property(x => x.PriorS, map => map.Length(50));
            Property(x => x.TypeS, map => map.Length(50));
            Property(x => x.DateS);
            Property(x => x.RF, map => map.Length(50));
            Property(x => x.Chemo, map => map.Length(50));
            Property(x => x.ChemAgDat, map => map.Length(100));
            Property(x => x.Radiotherapy, map => map.Length(50));
            Property(x => x.RxTxAgDat, map => map.Length(100));
            Property(x => x.CTS, map => map.Length(50));
            Property(x => x.CTRes, map => map.Length(50));
            Property(x => x.Angio, map => map.Length(50));
            Property(x => x.AngioRes, map => map.Length(50));
            Property(x => x.Sono, map => map.Length(50));
            Property(x => x.SonoRes, map => map.Length(50));
            Property(x => x.MRI, map => map.Length(50));
            Property(x => x.MRIRes, map => map.Length(50));
            Property(x => x.BoneS, map => map.Length(50));
            Property(x => x.BSRes, map => map.Length(50));
            Property(x => x.Bx, map => map.Length(50));
            Property(x => x.BxRes, map => map.Length(50));
            Property(x => x.SxCand, map => map.Length(50));
            Property(x => x.Surgeon, map => map.Length(50));
            Property(x => x.Date);
            Property(x => x.Procedure, map => map.Length(50));
            Property(x => x.Comment, map => map.Length(250));
            Property(x => x.Duration, map => map.Precision(3));
            Property(x => x.Laparoscopy, map => map.Length(50));
            Property(x => x.LapSx, map => map.Length(50));
            Property(x => x.LapComment, map => map.Length(50));
            Property(x => x.CellS, map => map.Length(50));
            Property(x => x.RIS, map => map.Length(50));
            Property(x => x.Transfusion, map => map.Length(50));
            Property(x => x.BTxReq, map => map.Length(50));
            Property(x => x.Intraopdeath, map => map.Length(50));
            Property(x => x.Extub);
            Property(x => x.ICULOS, map => map.Precision(3));
            Property(x => x.HospLOS, map => map.Precision(3));
            Property(x => x.Complications, map => map.Length(50));
            Property(x => x.Comp1, map => map.Length(50));
            Property(x => x.DateComp1);
            Property(x => x.Comp2, map => map.Length(50));
            Property(x => x.DateComp2);
            Property(x => x.Comp3, map => map.Length(50));
            Property(x => x.DateComp3);
            Property(x => x.Comp4, map => map.Length(50));
            Property(x => x.DateComp4);
            Property(x => x.FUDate);
            Property(x => x.StatusFU, map => map.Length(50));
            Property(x => x.MonthsFU, map => map.Precision(3));
            Property(x => x.Recurrence, map => map.Length(50));
            Property(x => x.RecDate);
            Property(x => x.RecSite, map => map.Length(50));
            Property(x => x.DeathOccur, map => map.Length(50));
            Property(x => x.HBsAg, map => map.Length(20));
            Property(x => x.HBsAb, map => map.Length(20));
            Property(x => x.HBcAb, map => map.Length(20));
            Property(x => x.HBcAbIgM, map => map.Length(20));
            Property(x => x.HBeAg, map => map.Length(20));
            Property(x => x.HBeAb, map => map.Length(20));
            Property(x => x.HBVDNA, map => map.Length(20));
            Property(x => x.HDV, map => map.Length(20));
            Property(x => x.HCVe, map => map.Length(20));
            Property(x => x.HCVRIBA2, map => map.Length(20));
            Property(x => x.CEA, map => map.Precision(24));
            Property(x => x.AFP, map => map.Precision(24));
            Property(x => x.Ca153, map => map.Precision(24));
            Property(x => x.Ca199, map => map.Precision(24));
            Property(x => x.Ca125, map => map.Precision(24));
            Property(x => x.LasaP, map => map.Precision(24));
            Property(x => x.HepArtInfPump, map => map.Length(50));
            Property(x => x.RFA, map => map.Length(5));
            Property(x => x.RFAepisodes, map => map.Length(100));
            Property(x => x.HAI, map => map.Length(5));
        }
    }
}
