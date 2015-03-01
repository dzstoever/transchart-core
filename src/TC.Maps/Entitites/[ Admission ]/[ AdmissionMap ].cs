using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using TC.Domain;

namespace TC.Maps
{
    public class AdmissionMap : MultiEnteredByMap<Admission, AdmissionId>, Zen.Data.IDbMap
    {        
        public AdmissionMap() 
        {
            Schema("dbo"); Table("Admissions_Multi"); 
            
            ComponentAsId(x => x.Id, compId =>
            {
                compId.Property(c => c.MRN);
                compId.Property(c => c.AdmitDate);                
            });            
			
            Property(x => x.DischDate);
			Property(x => x.OsuAdmit, map => map.NotNullable(true));
            Property(x => x.AdmitNum, map => map.Precision(10));
            Property(x => x.ICU);
            Property(x => x.TxRelated, map => map.Length(50));
            Property(x => x.Room, map => map.Length(50));
            Property(x => x.Service, map => map.Length(100));
            Property(x => x.AccountNumber, map => map.Length(50));
            Property(x => x.AdmitTime);
            Property(x => x.DischTime);
            Property(x => x.Account, map => map.Length(20));
            Property(x => x.CenterName, map => map.Length(50));
            Property(x => x.UnplannedReturnToOR, map => map.NotNullable(true));

            // mapping OneToOne with a FK association 
            // the other side of this is OneToOne with a unique constraint here
            ManyToOne(x => x.Cancelled, m =>
            {
                m.Columns(fk1 => fk1.Name("MRN"), fk2 => fk2.Name("AdmitDate"));
                m.Cascade(Cascade.All);// don't orphan entities on the other side
                m.Unique(true); m.Update(false);
            });
            ManyToOne(x => x.DischargeCancelled, m =>
            {
                m.Columns(fk1 => fk1.Name("MRN"), fk2 => fk2.Name("AdmitDate"));
                m.Cascade(Cascade.All);// don't orphan entities on the other side
                m.Unique(true); m.Update(false);
            });

            // mapping OneToOne with a PK association, I cant get this to work, but it should...
            //OneToOne(o => o.Cancelled, m =>
            //{
            //    m.PropertyReference(typeof(AdmissionCancelled).GetProperty("Admission"));
            //    m.Cascade(Cascade.All);
            //});    
            //OneToOne(o => o.DischargeCancelled, m => 
            //{
            //    m.PropertyReference(typeof(AdmissionDischargeCancelled).GetProperty("Admission"));
            //    m.Cascade(Cascade.All);
            //});                


            Set(s => s.Reasons, m =>
            {
                m.Key(k => k.Columns(fk1 => fk1.Name("MRN"), fk2 => fk2.Name("AdmitDate")));
                m.Lazy(CollectionLazy.Lazy); m.Cascade(Cascade.All); m.Inverse(true);
            }, r => { r.OneToMany(); });

            Set(s => s.BedHistory, m =>
            {
                m.Key(k => k.Columns(fk1 => fk1.Name("MRN"), fk2 => fk2.Name("AdmitDate")));
                m.Lazy(CollectionLazy.Lazy); m.Cascade(Cascade.All); m.Inverse(true);
            }, r => { r.OneToMany(); });

            Set(s => s.Diagnosis, m =>
            {
                m.Key(k => k.Columns(fk1 => fk1.Name("MRN"), fk2 => fk2.Name("AdmitDate")));
                m.Lazy(CollectionLazy.Lazy); m.Cascade(Cascade.All); m.Inverse(true);
            }, r => { r.OneToMany(); });

            Set(s => s.InvProcedures, m =>
            {
                m.Key(k => k.Columns(fk1 => fk1.Name("MRN"), fk2 => fk2.Name("AdmitDate")));
                m.Lazy(CollectionLazy.Lazy); m.Cascade(Cascade.All); m.Inverse(true);
            }, r => { r.OneToMany(); });

            Set(s => s.Therapy, m =>
            {
                m.Key(k => k.Columns(fk1 => fk1.Name("MRN"), fk2 => fk2.Name("AdmitDate")));
                m.Lazy(CollectionLazy.Lazy); m.Cascade(Cascade.All); m.Inverse(true);
            }, r => { r.OneToMany(); });

            Set(s => s.DischargeTherapy, m =>
            {
                m.Key(k => k.Columns(fk1 => fk1.Name("MRN"), fk2 => fk2.Name("AdmitDate")));
                m.Lazy(CollectionLazy.Lazy); m.Cascade(Cascade.All); m.Inverse(true);
            }, r => { r.OneToMany(); });

            Set(s => s.DischargeTo, m =>
            {
                m.Key(k => k.Columns(fk1 => fk1.Name("MRN"), fk2 => fk2.Name("AdmitDate")));
                m.Lazy(CollectionLazy.Lazy); m.Cascade(Cascade.All); m.Inverse(true);
            }, r => { r.OneToMany(); });

            Set(s => s.FollowUp, m =>
            {
                m.Key(k => k.Columns(fk1 => fk1.Name("MRN"), fk2 => fk2.Name("AdmitDate")));
                m.Lazy(CollectionLazy.Lazy); m.Cascade(Cascade.All); m.Inverse(true);
            }, r => { r.OneToMany(); });

        }
    }
}
