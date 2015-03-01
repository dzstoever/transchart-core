using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using TC.Domain;

namespace TC.Maps 
{
        
    public class PatientMap : EnteredByMap<Patient, string>, Zen.Data.IDbMap 
    {

        public PatientMap() 
        {
            Schema("dbo"); Table("Patient");

            Id(x => x.Id, map => { map.Column("MRN"); map.Generator(Generators.Assigned); map.Length(15); });
			
            Property(x => x.SSN, map => map.Length(15));
			Property(x => x.CheckDigit, map => map.Length(1));
            Property(x => x.LastName, map => { map.Column("Last"); map.Length(50); });
            Property(x => x.FirstName, map => { map.Column("First"); map.Length(50); });
			Property(x => x.Middle, map => map.Length(150));
			Property(x => x.Addr1, map => map.Length(50));
			Property(x => x.Addr2, map => map.Length(200));
			Property(x => x.City, map => map.Length(50));
			Property(x => x.State, map => map.Length(25));
			Property(x => x.Zip, map => map.Length(20));
			Property(x => x.ZipExt, map => map.Length(5));
			Property(x => x.CountyCode, map => map.Length(200));
			Property(x => x.HPhone, map => map.Length(25));
			Property(x => x.CellPhone, map => map.Length(25));
			Property(x => x.WPhone, map => map.Length(50));
			Property(x => x.WPhoneExt, map => map.Length(5));
			Property(x => x.Email, map => map.Length(50));
			Property(x => x.Pager, map => map.Length(25));
			Property(x => x.Fax, map => map.Length(20));
			Property(x => x.PriorityHPhone, map => map.Length(2));
			Property(x => x.PriorityCellPhone, map => map.Length(2));
			Property(x => x.PriorityWPhone, map => map.Length(2));
			Property(x => x.PriorityFax, map => map.Length(2));
			Property(x => x.PriorityPager, map => map.Length(2));
            Property(x => x.BloodType, map => { map.Column("ABO"); map.Length(15); });
			Property(x => x.btenteredby, map => map.Length(50));
			Property(x => x.btentereddate);
            Property(x => x.AcademicActivityLevel, map => { map.Column("academic_level"); map.Length(12); });
            Property(x => x.AcademicProgress, map => { map.Column("academic_prg"); map.Length(12); });
			Property(x => x.autopsy);
			Property(x => x.Citizenship, map => map.Length(100));
            Property(x => x.CauseOfDeath, map => { map.Column("cod"); map.Length(12); });
            Property(x => x.CauseOfDeathComments, map => { map.Column("cod_ostxt"); map.Length(50); });
            Property(x => x.ContributoryCauseOfDeath1, map => { map.Column("cod2"); map.Length(12); });
            Property(x => x.ContributoryCauseOfDeath1Comments, map => { map.Column("cod2_ostxt"); map.Length(50); });
            Property(x => x.ContributoryCauseOfDeath2, map => { map.Column("cod3"); map.Length(12); });
            Property(x => x.ContributoryCauseOfDeath2Comments, map => { map.Column("cod3_ostxt"); map.Length(50); });
			Property(x => x.Comments, map => map.Length(500));
			Property(x => x.DisabledDt);
			Property(x => x.DOB);
			Property(x => x.empl_stat, map => map.Length(20));
			Property(x => x.DeceasedDate, map => { map.Column("ExpirationDate"); });
			Property(x => x.FunctionalStatus, map => { map.Column("func_stat"); map.Length(12); });
			Property(x => x.HipaaConsentFormSigned);
			Property(x => x.InterpreterNeeded);
			Property(x => x.KidneyTx);
			Property(x => x.LivDec, map => map.Length(25));
			Property(x => x.Maiden, map => map.Length(50));
			Property(x => x.MaritalStatus, map => map.Length(50));
			Property(x => x.OKTORELEASE, map => map.Length(1));
			Property(x => x.OKtoShareFamily, map => map.Length(1));
            Property(x => x.PhysicalCapacity, map => { map.Column("physical_capacity"); map.Length(12); });
			Property(x => x.PrimaryLanguage, map => map.Length(100));
			Property(x => x.ProcessedDate);
			Property(x => x.Race, map => map.Length(50));
			Property(x => x.RefDate);
			Property(x => x.Religion, map => map.Length(50));
			Property(x => x.RetiredDt);
            Property(x => x.Gender, map => { map.Column("Sex"); map.Length(50); });
			Property(x => x.Suffix, map => map.Length(10));
			Property(x => x.Title, map => map.Length(50));
			Property(x => x.Veteran, map => map.Length(10));
            Property(x => x.WorkingForIncome, map => { map.Column("work_income"); map.Length(10); });
            Property(x => x.WorkingForIncomeNoStatus, map => { map.Column("work_no_status"); map.Length(12); });
            Property(x => x.WorkingForIncomeYesStatus, map => { map.Column("work_yes_status"); map.Length(12); });
			Property(x => x.Coord, map => map.Length(50));
			Property(x => x.Coord2, map => map.Length(50));
			Property(x => x.IsolationStatus, map => map.Length(50));
			Property(x => x.LocalMD, map => map.Length(50));
			Property(x => x.NursePracticioner, map => map.Length(50));
			Property(x => x.PrePost, map => map.Length(25));
			Property(x => x.PtStatus, map => map.Length(50));
            Property(x => x.DiseaseRecurrence, map => { map.Column("recurred_orig_dgn"); map.Length(12); });
			Property(x => x.SocialWorker, map => map.Length(50));
			Property(x => x.SocialWorker2, map => map.Length(50));
			Property(x => x.Technician, map => map.Length(50));
			Property(x => x.TransPhysician, map => map.Length(50));
			Property(x => x.TransPhysician2, map => map.Length(50));
			Property(x => x.BirthPlace, map => map.Precision(10));
			Property(x => x.COC, map => map.Precision(10));
			Property(x => x.HeightCM, map => map.Precision(10));
			Property(x => x.HeightIN, map => map.Precision(10));
			Property(x => x.px_non_compl, map => map.Precision(10));
			Property(x => x.PRDX, map => map.Precision(10));
			Property(x => x.TransDate, map => map.Precision(10));
			
        }
    }
}
