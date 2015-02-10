using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using TC.Domain;


namespace TC.Maps {
    
    
    public class EpisodetransplantMultiMap : ClassMapping<EpisodeTransplant_Multi> {
        
        public EpisodetransplantMultiMap() {
			Schema("dbo");
			Lazy(true);
			Id(x => x.ID, map => map.Generator(Generators.Identity));
			Property(x => x.MRN, map => { map.NotNullable(true); map.Length(15); });
			Property(x => x.Coordinator1, map => map.Length(50));
			Property(x => x.Coordinator2, map => map.Length(50));
			Property(x => x.EvalPhysician1, map => map.Length(50));
			Property(x => x.EvalPhysician2, map => map.Length(50));
			Property(x => x.Physician, map => map.Length(15));
			Property(x => x.SocialWorker1, map => map.Length(50));
			Property(x => x.SocialWorker2, map => map.Length(50));
			Property(x => x.Technician, map => map.Length(50));
			Property(x => x.TxPhysician1, map => map.Length(50));
			Property(x => x.TxPhysician2, map => map.Length(50));
			Property(x => x.PrimaryDiagnosis, map => map.Length(150));
			Property(x => x.PrimaryDiagnosis2, map => map.Length(150));
			Property(x => x.PrimaryDiagnosisDate, map => map.Length(50));
			Property(x => x.PrimaryDiagnosisDate2);
			Property(x => x.SecondaryDiagnosis, map => map.Length(400));
			Property(x => x.SecondaryDiagnosis2, map => map.Length(400));
			Property(x => x.SecondaryDiagnosisDate, map => map.Length(50));
			Property(x => x.SecondaryDiagnosisDate2);
			Property(x => x.OtherDiagnosis, map => map.Length(400));
			Property(x => x.OtherDiagnosis2, map => map.Length(400));
			Property(x => x.OtherDiagnosisDate);
			Property(x => x.OtherDiagnosisDate2);
			Property(x => x.DiagnosisComments, map => map.Length(255));
			Property(x => x.DiagnosisComments2, map => map.Length(255));
			Property(x => x.WaitingZip, map => map.Length(20));
			Property(x => x.WaitingZipExt, map => map.Length(5));
			Property(x => x.WaitListDate);
			Property(x => x.ChartValid);
			Property(x => x.CMVDonor, map => map.Length(15));
			Property(x => x.DCDDonor, map => map.Length(15));
			Property(x => x.DeferralDate);
			Property(x => x.DiseaseRecurrence, map => map.Length(12));
			Property(x => x.ExpandedDonor, map => map.Length(25));
			Property(x => x.FollowedByPost, map => map.NotNullable(true));
			Property(x => x.HepCCoreAB, map => map.Length(25));
			Property(x => x.HepCKidney, map => map.Length(15));
			Property(x => x.HepCLiver, map => map.Length(50));
			Property(x => x.HighRiskDonor, map => map.Length(15));
			Property(x => x.HighRiskDonorQuestionnaireCompleted, map => map.Length(15));
			Property(x => x.IsolationStatus, map => map.Length(50));
			Property(x => x.LocalMD, map => map.Length(50));
			Property(x => x.NeedsBackup);
			Property(x => x.NursePracticioner, map => map.Length(50));
			Property(x => x.Outreach);
			Property(x => x.OutreachLocation, map => map.Length(50));
			Property(x => x.PairedDonorExchange, map => map.Length(25));
			Property(x => x.PatientStatus, map => map.Length(50));
			Property(x => x.ReasonForDischargeFromCare, map => map.Length(25));
			Property(x => x.PrePost, map => map.Length(25));
			Property(x => x.ReadyForTransplant);
			Property(x => x.SplitLiver);
			Property(x => x.Transplanted, map => map.NotNullable(true));
			Property(x => x.UNOSPts, map => map.Length(25));
			Property(x => x.Use_Dialysis_StartDate);
			Property(x => x.EnteredBy, map => map.Length(15));
			Property(x => x.EnteredDate);
			Property(x => x.EnteredTime);
			Property(x => x.TenantID, map => map.NotNullable(true));
			Property(x => x.DiabetesHistoryID, map => map.Precision(10));
			Property(x => x.InsulinStartDate);
			Property(x => x.KDPIConsent);
			Property(x => x.A2BConsent);
        }
    }
}
