using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using TC.DataModels;
using Zen.Data.QueryModel;
using Zen.Svcs;

namespace TC.Svcs
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(Namespace = "")]
    public class DataAccessSvc : SecureSignonSvc, IDataAccess
        //RemoteFacadeSvc, 
    {
        //public DataAccessSvc(IGenericDao dao, ILogger log)
        //{}

        public IEnumerable<PersonDTO> GetPeople(Query query)
        {
            throw new NotImplementedException();
        }

        public PersonMinDTO GetPersonByMRN(string mrn)
        {
            return FakePerson;
        }

        public PersonMinDTO GetPersonBySSN(string ssn)
        {
            return FakePerson;
        }

        public PersonMinDTO GetPersonByName(string firstName, string lastName)
        {
            return FakePerson;
        }


        private PersonMinDTO FakePerson
        {
            get
            {
                return new PersonMinDTO
                {
                    FirstName = "Leroy",
                    LastName = "Brown",
                    MiddleName = "Z",
                    MRN = "12345678",
                    SSN = "111223333"
                };
            }
        }
    }

    /*
    public class DataAccessSvc : IDataAccess
    {
        protected IGenericDao Dao;
        protected ILogger Log;


        public IEnumerable<PersonDto> GetPeople(Query query)
        {
            Console.WriteLine("Configuring...");
            var cfg = new Configuration();
            cfg.DataBaseIntegration(c =>
            {
                c.ConnectionString = @"Data Source=DSTOEVERPC;Initial Catalog=TCTest5201;Integrated Security=True;Pooling=False";
                //c.ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Transchart52;Integrated Security=True;Pooling=False";
                c.Dialect<MsSql2008Dialect>();
                c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                c.LogSqlInConsole = true;
                c.LogFormattedSql = true;

                // !!!
                // be very careful with this or you could wipe out an entire database!!!
                // !!!
                //c.SchemaAction = SchemaAutoAction.Validate;// .Drop, .Update, .Export, .All                                                
            });

            var mapper = new ModelMapper();
            var mappings = from t in typeof(IDbMap).Assembly.GetTypes()
                           where t.GetInterfaces().Contains(typeof(IDbMap))
                           select t;
            Console.WriteLine("{0} mappings in domain model.", mappings.Count());
            mapper.AddMappings(mappings);
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddMapping(domainMapping);

            Dao = new NHibernateDao(cfg.BuildSessionFactory());

            IEnumerable<Person> people;
            using (Dao.StartUnitOfWork())
            {
                people = Dao.Fetch<Person>(query);
            }

            return people.Select(person => new PersonDto() { MRN = person.MRN, FirstName = person.FirstName }).ToList();
            return null;

        }

    }
    */
}
