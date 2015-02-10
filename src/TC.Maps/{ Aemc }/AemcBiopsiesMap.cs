using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using TC.Domain;


namespace TC.Maps 
{
    public class AemcBiopsiesMap : MultiMap<AemcBiopsies, AemcBiopsiesId>, Zen.Data.IDbMap 
    {
        public AemcBiopsiesMap() 
        {
            Schema("dbo"); Table("AEMC_Biopsies_Multi");
            
            ComponentAsId(x => x.Id, compId =>
            {
                compId.Property(c => c.MRN);
                compId.Property(c => c.Counter);                
            });

			Property(x => x.TxNum, map => map.Precision(10));			
			Property(x => x.PATNum, map => map.Precision(10));
			Property(x => x.UniqueCan, map => map.Precision(10));
			Property(x => x.SpecimenNum, map => map.Length(15));
			Property(x => x.DateBx);
			Property(x => x.Source, map => map.Length(50));
			Property(x => x.FourPortalTracts, map => map.Length(50));
			Property(x => x.OtherwiseAdequate, map => map.Length(50));
			Property(x => x.Inflammation, map => map.Length(50));
			Property(x => x.BDDamage, map => map.Length(50));
			Property(x => x.BDLoss, map => map.Length(50));
			Property(x => x.BDLDegree, map => map.Length(50));
			Property(x => x.BDProliferation, map => map.Length(50));
			Property(x => x.Arteritis, map => map.Length(50));
			Property(x => x.Endothelialitis, map => map.Length(50));
			Property(x => x.PortalFibrosis, map => map.Length(50));
			Property(x => x.CentralFibrosis, map => map.Length(50));
			Property(x => x.Balloning, map => map.Length(50));
			Property(x => x.DegreeofBallooning, map => map.Length(50));
			Property(x => x.PiecemealNecrosis, map => map.Length(50));
			Property(x => x.Bridging, map => map.Length(50));
			Property(x => x.CentralNecrosis, map => map.Length(50));
			Property(x => x.DegreeCLN, map => map.Length(50));
			Property(x => x.Cholestasis, map => map.Length(50));
			Property(x => x.Steatosis, map => map.Length(50));
			Property(x => x.SteatosisType, map => map.Length(50));
			Property(x => x.Lobularinflammation, map => map.Length(50));
			Property(x => x.Microabscesses, map => map.Length(50));
			Property(x => x.Granulomas, map => map.Length(50));
			Property(x => x.PathDiagnosis, map => map.Length(50));
			Property(x => x.OtherBiliary, map => map.Length(50));
			Property(x => x.DegreeIInjury, map => map.Length(50));
			Property(x => x.ViralDegree, map => map.Length(50));
			Property(x => x.TypeVirus, map => map.Length(50));
			Property(x => x.DegreeofRejection, map => map.Length(50));
			Property(x => x.Other, map => map.Length(50));
			Property(x => x.Reason, map => map.Length(50));
			Property(x => x.Timepoint, map => map.Length(50));
			Property(x => x.BiopsyType, map => map.Length(50));
			Property(x => x.BiopsyRoute, map => map.Length(50));
			Property(x => x.TypeVisit, map => map.Length(50));
			Property(x => x.Comment, map => map.Length(255));
			//Property(x => x.TenantID, map => map.NotNullable(true));
        }
    }
}
