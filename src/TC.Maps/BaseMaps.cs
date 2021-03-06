﻿using NHibernate.Mapping.ByCode.Conformist;
using TC.Domain;
using TC.Domain.Entities;

namespace TC.Maps
{
    public abstract class BaseMap<T, TId> : ClassMapping<T> where T : Zen.Core.DomainEntity<TId>
    {
        protected BaseMap()
        {
            Lazy(true); DynamicInsert(true); DynamicUpdate(true);


            #region examples for CreateDate/Version
            /*
            
            // use a CreateDate as intended
            Property(x => x.CreateDate, map => { map.NotNullable(true); map.Insert(false); map.Update(false); });
             
            // use a DateTime version (note: only using .CreateDate because of convienience for testing)
            Version(x => x.CreateDate, m =>
            {
                m.Generated(VersionGeneration.Always);
                m.Type(new NHibernate.Type.DbTimestampType());                
            }); 
            
            //use a byte[] version (preffered due to ms rounding precision in sql server)           
            Version(x => x.Version, mapper =>
            {
                mapper.Generated(VersionGeneration.Always);
                mapper.Type<BinaryTimestamp>();
            });
             
            */
            #endregion

            #region other available mapping properties
            /*
                m.Access(Accessor.Field);
                m.OptimisticLock(true);
                m.ForeignKey("column_fk");
                m.Formula("arbitrary SQL expression");
                m.Index("column_idx");
                m.NotNullable(true);
                m.Unique(true);
                m.UniqueKey("column_uniq");
                m.PropertyRef("propertyref");
                m.NotFound(NotFoundMode.Exception); // or .Ignore                
            */
            #endregion

            #region example list/set/bag
            // public interface System.Collections.Generic.IList<T> : ICollection<T>, IEnumerable<T>, IEnumerable
            // public interface System.Collections.Generic.ISet<T> : ICollection<T>, IEnumerable<T>, IEnumerable

            /* NHibernate semantics:
             * List: Ordered collection of entities, duplicate allowed. 
             *  - Use IList in code. 
             *  - The index column will need to be mapped in NHibernate.
             * Bag: Unordered list of entities, duplicates allowed. 
             *  - Use IList in code. 
             *  - The index column of the list is not mapped and not honored by NH. 
             * Set: Unordered collection of unique entities, duplicates NOT allowed. 
             *  - Use ISet in code. 
             *  - Can be sorted by defining an orderby or by defining an IComparer(binary tree).
             *  - Use GetHashCode/Equals to indicate the business definition of a duplicate. 
             *  
             * It's usually best to use a Set by default because in most use-cases 
             * uniqueness is more important and practical than ordering 
             
            List...
            Bag...
            Set(s => s.Reasons, a =>
            {
                a.Key(k => k.Columns(fk1 => fk1.Name("MRN"), fk2 => fk2.Name("AdmitDate")));
                a.Cascade(Cascade.None); 
                a.Inverse(true);        // specify that the collection is a mirror image
            }, r => { r.OneToMany(); });// specify the association
            
            
             */
            #endregion

            // for further examples and descriptions of mapping by code
            // http://notherdev.blogspot.com/2012/02/nhibernates-mapping-by-code-summary.html
        }
    }

    /// <summary>
    /// has Tenant
    /// </summary>
    public abstract class MultiMap<T, TId> : BaseMap<T, TId> where T : MultiEntity<TId>
    {        
        protected MultiMap()
        {
            ManyToOne(x => x.Tenant, m =>
            {
                m.Column("TenanTId");
                m.Class(typeof(Tenant));                
                m.Insert(false); m.Update(false);                
            });
        }
    }

    /// <summary>
    /// has Entered-By-Date-Time 
    /// </summary>
    public abstract class EnteredByMap<T, TId> : BaseMap<T, TId> where T : EnteredByEntity<TId>
    {
        protected EnteredByMap()
        {
            Property(x => x.EnteredBy, map => map.Length(15));
            Property(x => x.EnteredDate);
            Property(x => x.EnteredTime, map => map.Length(20));
        }
    }

    /// <summary>
    /// has Tenant & Entered-By-Date-Time 
    /// </summary>
    public abstract class MultiEnteredByMap<T, TId> : MultiMap<T, TId> where T : MultiEnteredByEntity<TId>
    {
        protected MultiEnteredByMap()
        {
            Property(x => x.EnteredBy, map => map.Length(15));
            Property(x => x.EnteredDate);
            Property(x => x.EnteredTime, map => map.Length(20));            
        }
    }

}
