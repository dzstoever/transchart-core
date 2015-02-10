using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TC.DataModels;
using Zen.Data.QueryModel;
using TC.Domain;

namespace TC.Svcs
{
    [ServiceContract(Namespace = "")]
    public interface IDataAccess
    { // Can't have generics in operation contracts!!! DOHHHHH

        [OperationContract]
        IEnumerable<PersonDTO> GetPeople(Query query);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PersonMinDTO GetPersonByMRN(string mrn);
        
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PersonMinDTO GetPersonBySSN(string ssn);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PersonMinDTO GetPersonByName(string firstName, string lastName);
        
        //[OperationContract] IEnumerable<Person> GetPeopleOnWaitListByLastName(string lastName);        
        //[OperationContract] IEnumerable<Person> GetPeopleOnWaitListByOrganType(string organType);
        //[OperationContract] IEnumerable<Organ> GetDistictOrgansFromWaitList();
    }

}
