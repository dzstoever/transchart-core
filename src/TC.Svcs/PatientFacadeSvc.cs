using System;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using TC.DataModels;
using Zen.Data.QueryModel;

namespace TC.Svcs
{
    //[ServiceBehavior(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]    
    public class PatientFacadeSvc : IPatientFacadeSvc
    {
        private PersonMinDTO FakePerson
        {
            get
            {
                return new PersonMinDTO
                {
                    FirstName = "Bilbo",
                    LastName = "Baggins",
                    MiddleName = "A",
                    MRN = "12345678",
                    SSN = "111223333"
                };
            }
        }


        public IEnumerable<PersonDTO> GetPeople(Query query)
        {
            throw new NotImplementedException();
        }

        public PersonMinDTO GetPersonById(string mrn)
        {
            return FakePerson;
        }

        public bool UpdatePerson(PersonDTO person)
        {
            return false;
        }
    }
}
