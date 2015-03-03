using Zen.Data;

namespace TC.Test.Domain
{
    
    public class MappedEntity1 : NHMappedEntity<long> { }
    public class MappedEntity2 : NHMappedEntity<long> { }

    //public class SqlCnnFactoryWithNothing : SqlClientCnnFactory { }
    //public class SqlCnnFactoryWithAssemblyFQN : SqlClientCnnFactory
    //{
    //    protected override string ConnectionString { get { return @"Server=.\SQLExpress; Database=TransChart52; Trusted_Connection=True;"; } } 
    //    public override string GetMappingAssemblyFQN() 
    //    {
    //        return MappedDomainTests.MappingAssemblyFQN;
    //    }
    //}
    //public class SqlCnnFactoryWithAssemblyTypeName : SqlClientCnnFactory
    //{
    //    protected override string ConnectionString { get { return @"Server=.\SQLExpress; Database=TransChart52; Trusted_Connection=True;"; } } 
    //    public override string GetMappingAssemblyTypeName()
    //    {
    //        return MappedDomainTests.MappingAssemblyTypeName;
    //    }
    //}

    
    //[TestClass]
    //public class MappedDomainTests
    //{
        //internal const string MappingAssemblyFQN = "TC.Maps";// this should fail without Version=..., Culture=..., PublicKeyToken=...
        //internal const string MappingAssemblyTypeName = "TC.Maps.AdmissionMapping, TC.Maps";// that's why it's easier to use a type

        //MappedEntity1 _mappedEntity1;
        //MappedEntity2 _mappedEntity2;

        //[TestMethod]
        //public void MappedEntityConfiguratorWithAssemblyTypeName()
        //{
            // must register IDbCnnFactory with Ioc
            //Zen.Ioc.SingletonDI.Register<IDbCnnFactory>(new SqlCnnFactoryWithAssemblyTypeName());

            //_mappedEntity1 = new MappedEntity1();
            //_mappedEntity2 = new MappedEntity2();
            
        //}
    //}

    //todo: test to prove we only configure once
    //todo: test expected failures
}
