using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using TC.Svcs.DataContracts;
using Zen.Data.QueryModel;

namespace TC.Svcs
{
    [ServiceContract(Namespace = "")]
    public interface IPatientFacadeSvc
    {
        [OperationContract]
        //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        IEnumerable<PersonDTO> GetPeople(Query query);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PersonMinDTO GetPersonById(string mrn);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool UpdatePerson(PersonDTO person);
    }
}
