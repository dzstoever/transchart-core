using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using TC.Doman;
using Zen.Data;

namespace TC.Maps 
{
    public class TtHLAMap : ClassMapping<TtHLA>, IDbMap
    {        
        public TtHLAMap() {
            Table("TT_HLA");
			Schema("dbo");
			Lazy(true);
            
            ComponentAsId(x => x.Id, compId =>
            {
                compId.Property(c => c.MRN);
                compId.Property(c => c.LabDate);
            });

            
			Property(x => x.MRNUNOS);
			Property(x => x.Method);
			Property(x => x.A1);
			Property(x => x.A2);
			Property(x => x.B1);
			Property(x => x.B2);
			Property(x => x.C1);
			Property(x => x.C2);
			Property(x => x.DR1);
			Property(x => x.DR2);
			Property(x => x.DP1);
			Property(x => x.DP2);
			Property(x => x.DQ1);
			Property(x => x.DQ2);
			Property(x => x.BW4);
			Property(x => x.BW6);
			Property(x => x.DRW51);
			Property(x => x.DRW52);
			Property(x => x.DRW53);			
			Property(x => x.SerumID);//, map => { map.Column("SerumID"); map.NotNullable(true); });
			Property(x => x.method_11);
			Property(x => x.comment);
			Property(x => x.HaploTypeMatch);//, map => map.Column("HaploTypeMatch"));
			Property(x => x.DR52);
			Property(x => x.DQA1);
			Property(x => x.DQA2);
			Property(x => x.DR3B1);
			Property(x => x.DR3B2);
			Property(x => x.DR4B1);
			Property(x => x.DR4B2);

            //Property(x => x.Entered_Date, map => { map.Column("EnteredDate"); map.NotNullable(true); });
            //Property(x => x.Entered_By, map => { map.Column("EnteredBy"); map.NotNullable(true); });
            //Property(x => x.enteredtime);
            //Property(x => x.Tenant_ID, map => { map.Column("TenantID"); map.NotNullable(true); });
            
            //try to get a person but ignore, if not found
            ManyToOne(x => x.Person, m =>
            {
                m.Lazy(LazyRelation.NoLazy);
                m.Columns(fk1 => fk1.Name("MRN"));
                m.Cascade(Cascade.None);
                m.NotFound(NotFoundMode.Ignore);                
            });

            //ManyToOne(x => x.Person, m =>
            //{
            //    m.Columns(fk1 => fk1.Name("MRN"));
            //    m.Cascade(Cascade.All);// don't orphan entities on the other side
            //    m.Unique(true); m.Update(false);
            //});

            //Bag(s => s.People, m =>
            //{
            //    //m.Where();
            //    //m.Fetch(CollectionFetchMode.Join);//.Select, .SubSelect
            //    //m.Cascade(Cascade.None); m.Inverse(true);
            //    m.Table("Person_Multi");
            //    m.Lazy(CollectionLazy.Lazy);
            //    m.Key(k =>
            //    { k.Columns(fk1 => fk1.Name("MRN")); k.PropertyRef(x => x.MRN); });
            //}, r => //{ r.OneToMany(); });
            //r.ManyToMany(m =>
            //{

            //    //m.Where("MRN in (Select MRN from People)");
            //    m.NotFound(NotFoundMode.Ignore);
            //    m.Class(typeof(Person));
            //    //m.EntityName("entityName");
            //    //m.ForeignKey("MRN");
            //    //m.Formula("arbitrary SQL expression");
            //    //m.Lazy(LazyRelation.Proxy); // or LazyRelation.NoProxy or LazyRelation.None
            //}));


            

            //Set(s => s.People, m =>
            //{
            //    //m.Where();                
            //    //m.Key(k => k.Columns(fk1 => fk1.Name("MRN")));
            //    m.Key(k =>
            //    { k.Columns(fk1 => fk1.Name("MRN")); k.PropertyRef(x => x.Id.MRN); });
            //    m.Lazy(CollectionLazy.NoLazy);
            //    m.Cascade(Cascade.All); m.Inverse(true);
            //    m.Fetch(CollectionFetchMode.Join);//.Select, .SubSelect
            //}, r => { r.OneToMany(); });


            //ManyToOne(x => x.Candidate, m =>
            //{   m.ForeignKey("MRN");
            //    m.PropertyRef("MRN");
            //    //m.Lazy(LazyRelation.NoLazy);
            //    //m.Columns(fk1 => fk1.Name("MRN"));
            //    m.Cascade(Cascade.None);
            //    m.NotFound(NotFoundMode.Ignore);
            //    //m.Unique(true); m.Update(false);
            //});

            //Set(s => s.Candidates, m =>
            //{
            //    //m.Where();
            //    //m.Fetch(CollectionFetchMode.Join);//.Select, .SubSelect
            //    //m.Key(k => k.Columns(fk1 => fk1.Name("MRN")));                
            //    m.Lazy(CollectionLazy.NoLazy);
            //    m.Cascade(Cascade.All); m.Inverse(true);
            //}, r => //{ r.OneToMany(); });
            //{
            ////r.ManyToAny<string>(m =>
            ////{
            ////    m.Columns(id =>
            ////    {
            ////        id.Name("MRN");
            ////        //id.NotNullable(true);
            ////        // etc...
            ////    }, classRef =>
            ////    {
            ////        classRef.Name("Candidate");
            ////        //classRef.NotNullable(true);
            ////        // etc...
            ////    });
            ////});
            //r.ManyToMany(m =>
            //{
            //    m.Column("MRN");
            //    m.ForeignKey("MRN");
            //    //m.ForeignKey("MRN");
            //    //m.Formula("arbitrary SQL expression");
            //    //m.Lazy(LazyRelation.Proxy); // or LazyRelation.NoProxy or LazyRelation.None
            //    m.NotFound(NotFoundMode.Ignore);
            //    //m.Class(typeof (CustomType));
            //    //m.EntityName("entityName");
            //});                
            //}); 



        }
    }
}
