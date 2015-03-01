using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Zen.Data;

namespace TC.Utility.Domain.maps 
{        
    public class PersonMap : ClassMapping<Person>, IDbMap
    {        
        public PersonMap() {
			Schema("dbo");
			Table("Person_Multi");
            Lazy(true);

            Id(x => x.MRN, map => { map.Column("MRN"); map.Generator(Generators.Assigned); map.Length(15); });
			Property(x => x.SSN);
            Property(x => x.DOB);
            Property(x => x.Last, map => map.Column("LastName"));
            Property(x => x.First, map => map.Column("FirstName"));
            Property(x => x.Middle, map => map.Column("MiddleName"));			
			Property(x => x.Sex);

            //Set(s => s.TtHLAs, m =>
            //{
            //    m.Key(k => k.Columns(fk1 => fk1.Name("MRN")));
            //    m.Lazy(CollectionLazy.Lazy);
            //    m.Cascade(Cascade.None);
            //    m.Inverse(true);
            //    m.Fetch(CollectionFetchMode.Join);//.Select, .SubSelect
            //    //m.Subselect();
            //    //m.Where();   
            //}, r => { r.OneToMany(); });//todo: should be many to many...
        }
    }
}
