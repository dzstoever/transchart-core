using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using TC.Domain.Entities;
using Zen.Data;

namespace TC.Maps.Entitites 
{
       
    public class CandidateMap : ClassMapping<Candidate>, IDbMap 
    {

        public CandidateMap() 
        {
            Schema("dbo"); Table("Candidate");

            Id(x => x.Id, map => { map.Column("MRN"); map.Generator(Generators.Assigned); map.Length(15); });
            
			Property(x => x.OrganType, map => map.Precision(10));
			Property(x => x.TxNum, map => { map.NotNullable(true); map.Precision(10); });
			Property(x => x.Cur);
			Property(x => x.SSN, map => map.Length(15));
			Property(x => x.CheckDigit, map => map.Length(1));
			Property(x => x.SSNandMRNSame, map => map.Length(50));
			Property(x => x.FN, map => map.Length(50));
			Property(x => x.MI, map => map.Length(150));
			Property(x => x.LN, map => map.Length(50));
			Property(x => x.Address, map => map.Length(50));
			Property(x => x.Address2, map => map.Length(200));
			Property(x => x.City, map => map.Length(50));
			Property(x => x.State, map => map.Length(25));
			Property(x => x.Zip, map => map.Length(20));
			Property(x => x.ZipExt, map => map.Length(5));
			Property(x => x.County, map => map.Length(200));
			Property(x => x.Fcountry, map => map.Length(100));
			Property(x => x.HPhone, map => map.Length(25));
			Property(x => x.CPhone, map => map.Length(25));
			Property(x => x.WPhone, map => map.Length(50));
			Property(x => x.WPhoneExt, map => map.Length(5));
			Property(x => x.Pager, map => map.Length(25));
			Property(x => x.Fax, map => map.Length(20));
			Property(x => x.Email, map => map.Length(50));
			Property(x => x.PriorityCellPhone, map => map.Length(2));
			Property(x => x.PriorityFax, map => map.Length(2));
			Property(x => x.PriorityHPhone, map => map.Length(2));
			Property(x => x.PriorityPager, map => map.Length(2));
			Property(x => x.PriorityWPhone, map => map.Length(2));
			Property(x => x.ABO, map => map.Length(15));
			Property(x => x.ABO2, map => map.Length(7));
			Property(x => x.BTEnteredBy, map => map.Length(50));
			Property(x => x.BTEnteredDate);
			Property(x => x.BT2EnteredBy, map => map.Length(50));
			Property(x => x.BT2EnteredDate);
            Property(x => x.AcademicLevel, map => { map.Column("academic_level"); map.Length(12); });
            Property(x => x.AcademicPrg, map => { map.Column("academic_prg"); map.Length(12); });
			Property(x => x.Citizenship, map => map.Length(100));
			Property(x => x.COD, map => map.Length(12));
			Property(x => x.Comments, map => map.Length(500));
			Property(x => x.DisabledDate, map => map.Column("DisabledDt") );
			Property(x => x.DOB);
			Property(x => x.Education, map => map.Length(100));
            Property(x => x.EmplStat, map => { map.Column("empl_stat"); map.Length(20); });
			Property(x => x.Ethnicity, map => map.Length(100));
			Property(x => x.EvalConsent);
			Property(x => x.ExpirationDate);
            Property(x => x.FuncStat, map => { map.Column("func_stat"); map.Length(12); });
			Property(x => x.HipaaConsentFormSigned);
			Property(x => x.KidneyTx);
			Property(x => x.InterpreterNeeded);
			Property(x => x.MN, map => map.Length(50));
			Property(x => x.MS, map => map.Length(50));
			Property(x => x.Occupation, map => map.Length(40));
			Property(x => x.OKToRelease, map => map.Length(1));
			Property(x => x.OKToShareFamily, map => map.Length(1));
            Property(x => x.PhysicalCapacity, map => { map.Column("physical_capacity"); map.Length(12); });
            Property(x => x.PrimaryPayment, map => { map.Column("pri_payment"); map.Length(12); });
			Property(x => x.PrimaryLanguage, map => map.Length(100));
			Property(x => x.ProcessedDate);
			Property(x => x.Race, map => map.Length(50));
			Property(x => x.ReferralDate);
			Property(x => x.Religion, map => map.Length(50));
            Property(x => x.RetiredDate, map => map.Column("RetiredDt"));
			Property(x => x.Sex, map => map.Length(50));
			Property(x => x.SN, map => map.Length(40));
			Property(x => x.Travel, map => map.Precision(10));
			Property(x => x.TravelTime, map => map.Precision(24));
			Property(x => x.Veteran, map => map.Length(10));
            Property(x => x.WorkIncome, map => { map.Column("work_income"); map.Length(10); });
            Property(x => x.WorkNoStatus, map => { map.Column("work_no_status"); map.Length(12); });
            Property(x => x.WorkYesStatus, map => { map.Column("work_yes_status"); map.Length(12); });
            Property(x => x.YrEntryUs, map => { map.Column("yr_entry_us"); map.Length(10); });
			Property(x => x.EmergencyContact, map => map.Length(50));
			Property(x => x.LivDec, map => map.Length(25));
			Property(x => x.SW, map => map.Length(40));
			Property(x => x.Feet, map => map.Precision(10));
			Property(x => x.Inches, map => map.Precision(10));
			Property(x => x.Wt, map => map.Precision(53));
			Property(x => x.EnteredBy, map => map.Length(21));
			Property(x => x.EnteredDate);
			Property(x => x.EnteredTime);
			Property(x => x.Donor, map => { map.NotNullable(true); map.Precision(10); });
			Property(x => x.ArchiveDonor, map => map.NotNullable(true));
			Property(x => x.CADDonor, map => map.NotNullable(true));
			Property(x => x.Recipient, map => map.Length(102));
			Property(x => x.RecipientMRN, map => { map.NotNullable(true); map.Length(15); });
			Property(x => x.RecipientTxNum, map => map.Precision(10));
			Property(x => x.Relationship, map => map.Length(150));
			Property(x => x.DonorType, map => map.Length(25));
			Property(x => x.PD, map => map.Length(150));
			Property(x => x.DD, map => map.Length(50));
			Property(x => x.SecDx, map => map.Length(400));
			Property(x => x.SecDxDate, map => map.Length(50));
			Property(x => x.OtherDx, map => map.Length(400));
			Property(x => x.OtherDxDate);
			Property(x => x.DxComments, map => map.Length(255));
			Property(x => x.Coord, map => map.Length(50));
			Property(x => x.Coord2, map => map.Length(50));
			Property(x => x.EvalSurgeon, map => map.Length(50));
			Property(x => x.Phys, map => map.Length(15));
			Property(x => x.SocialWorker, map => map.Length(50));
			Property(x => x.SocialWorker2, map => map.Length(50));
			Property(x => x.Technician, map => map.Length(50));
			Property(x => x.TxPhysician, map => map.Length(50));
			Property(x => x.TxPhysician2, map => map.Length(50));
            Property(x => x.WaitingZip, map => { map.Column("Waiting_zip"); map.Length(20); });
            Property(x => x.WaitingZipExt, map => { map.Column("Waiting_zipExt"); map.Length(5); });
			Property(x => x.WaitListDate);
			Property(x => x.ChartValid);
			Property(x => x.CMVDonor, map => map.Length(15));
			Property(x => x.DCDDonor, map => map.Length(15));
			Property(x => x.DeferralDate);
			Property(x => x.ExpandedDonor, map => map.Length(25));
			Property(x => x.FollowedByPost);
			Property(x => x.HEPCCoreAB, map => map.Length(25));
			Property(x => x.HepCKidney, map => map.Length(15));
			Property(x => x.HepCLiver, map => map.Length(50));
			Property(x => x.HighRiskDonorQuestionnaireCompleted, map => map.Length(15));
			Property(x => x.NeedsBackup);
			Property(x => x.NursePracticioner, map => map.Length(50));
			Property(x => x.Outreach);
			Property(x => x.OutreachLocation, map => map.Length(50));
			Property(x => x.PairedDonorExchange, map => map.Length(25));
			Property(x => x.ReadyForTransplant);
			Property(x => x.SplitLiver);
			Property(x => x.Transplanted);
			Property(x => x.UNOSPts, map => map.Length(25));
			Property(x => x.UseDialysisStartDate, map => map.Column("use_dialysis_startdate"));
        }
    }
}
