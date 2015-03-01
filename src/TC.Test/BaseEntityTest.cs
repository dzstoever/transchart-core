using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using Zen;
using Zen.Core;
using Zen.Data;

namespace TC.Tests
{
    public abstract class BaseEntityTest<T, TId> where T : class, IDomainEntity<TId>, new()
    {
        // set to run tests using explicit transactions
        private bool _usingTx = true;  
        
        // subject under test
        protected IGenericDao Dao;
                
        // configure
        protected BaseEntityTest()
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
            
            
                    
            #region NHProf

            // only initialize if the dll exists
            //var exists = new ImplChecker().CheckForDll("HibernatingRhinos.Profiler.Appender.dll");
            //if (!exists) return;

            //// set offline to true if you want a file instead of live sql
            //bool offline = false;
            //var offlineFileName = "DaoTests.nhprof";
            //if (offline) NHibernateProfiler.InitializeOfflineProfiling(offlineFileName);
            //else NHibernateProfiler.Initialize();

            #endregion


            Dao = new NHibernateDao(cfg.BuildSessionFactory());
            Dao.StartUnitOfWork(); Console.WriteLine("Starting Unit Of Work...");           
            
        }

        ~BaseEntityTest()
        {
            if (Dao == null) return;
            Dao.CloseUnitOfWork();
            Dao.Dispose();
            //NHibernateProfiler.Stop();
        }

        
        public T Create()
        {
            if (_usingTx && !Dao.IsInTx) Dao.BeginTx();

            T entity = new T();
            Dao.Insert(entity);
            
            if (Dao.IsInTx) Dao.CommitTx();
            return entity;
        }        
        public T Create(TId id)
        {
            if (_usingTx && !Dao.IsInTx) Dao.BeginTx();

            T entity = new T() { Id = id };
            Dao.Insert(entity);
            
            if (Dao.IsInTx) Dao.CommitTx();
            return entity;
        }
        public T Create(T entity)
        {
            if (_usingTx && !Dao.IsInTx) Dao.BeginTx();

            Dao.Insert(entity);
            
            if (Dao.IsInTx) Dao.CommitTx();
            return entity;
        }
        
        public IEnumerable<T> Read()
        {
            if (_usingTx && !Dao.IsInTx) Dao.BeginTx();

            var results =  Dao.FetchAll<T>(0, 10);

            if (Dao.IsInTx) Dao.CommitTx();

            return results;
        }


        public void Update(T entity)
        {
            Dao.Clear();// clear local cache to force update
            if (_usingTx && !Dao.IsInTx) Dao.BeginTx();
 
            Dao.Update(entity);
            
            if (Dao.IsInTx) Dao.CommitTx();
        }

        public void Delete(T entity)
        {
            if (_usingTx && !Dao.IsInTx) Dao.BeginTx();
 
            Dao.Delete(entity);

            if (Dao.IsInTx) Dao.CommitTx();
        }

        
        // Save(Insert) or Update based on persistence state (do I have an Id assigned? do I have a version?...)
        // transient - new object that is not saved to the db
        // persisent - object who exists in the db and ORM knows it
        // detached  - object who exists in the db but ORM doesn't know about it 
        public void Persist(T entity)
        {
            // Todo: make sure we are valid based on the validation rules defined
            // t.GetCustomAttributes...
            // get the first public property that is not Id, Version, CreateDate....and is oftype(?) and change it
            // System.Reflection.MemberInfo property = typeof(T).GetFirstPropertyOfType(typeof(System.String));

            // Todo: set the property value to something valid
            // System.Linq.Expressions.Expression.Call(
            //    entity,
            //    property,
            //    new System.Linq.Expressions.Expression[] { });

            if (_usingTx && !Dao.IsInTx) Dao.BeginTx();

            Dao.Persist(entity);

            if (Dao.IsInTx) Dao.CommitTx();
        }

    }
}
