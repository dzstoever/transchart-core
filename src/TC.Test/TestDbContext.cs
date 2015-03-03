using System.Reflection;
using TC.Maps.Entitites;
using Zen.Data;

namespace TC.Test
{
    public class TestDbContext : SqlContext
    {
        //private string _cnnString = @"Server=.\SQLEXPRESS; Integrated Security=true; Initial Catalog=master;";
        private string _cnnString = @"Server=DSTOEVERPC; Integrated Security=true; Initial Catalog=master;";
        private SqlDialects _sqlDialect = SqlDialects.MsSql2012;
        private Assembly _mappingAssembly = typeof(PersonMap).Assembly;
        
        public override string CnnString
        {
            get { return _cnnString; }
            set { _cnnString = value; }
        }
        public override SqlDialects SqlDialect
        {
            get { return _sqlDialect; }
            set { _sqlDialect = value; }
        }
        public override Assembly ByCodeMappingAssembly
        {
            get { return _mappingAssembly; }
            set { _mappingAssembly = value; }
        }
        public override Assembly HbmXmlMappingAssembly
        {
            get { return _mappingAssembly; }
            set { _mappingAssembly = value; }
        }

    }
}