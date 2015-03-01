using System;
using NHibernate.Mapping.ByCode;

namespace TC.Maps
{
    /// <summary>
    /// The following class is a temporary fix for a known bug in the version 
    /// of NHibernate we are referencing.    
    /// </summary>
    /// <remarks>
    /// The NHibernate.Mapping.ByCode.NativeGuidGeneratorDef returns "native" 
    /// instead of "guid.native" for the Class thus NHibernate issues a 
    /// SCOPE_INDENTITY() and thinks the database Id is null.
    /// 
    /// There is also no static member named NativeGuid in the Generators class.
    /// I assume because this was a known bug at the time of release.
    /// This bug has been fixed in the the latest source code for NH.
    /// We should be able to safely use Generators.NativeGuid in the future.
    /// </remarks>
    public class NativeGuidGeneratorDef : IGeneratorDef
    {
        public string Class
        {
            get { return "guid.native"; }
        }

        public object Params
        {
            get { return null; }
        }

        public Type DefaultReturnType
        {
            get { return typeof(Guid); }
        }

        public bool SupportedAsCollectionElementId
        {
            get { return true; }
        }
        
    }
}