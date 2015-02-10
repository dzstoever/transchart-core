using System;
using System.Collections.Generic;
using System.Web.Http;
using TC.DataModels;
using Zen.Data.QueryModel;

namespace TC.Website.ApiControllers
{
    public class PatientController : ApiController
    {
        //Is there a way to map this to our TC.Svc library? can we add a route entry to point at the dynamic router?
        public IEnumerable<PersonDTO> Get(Query query)
        {
            throw new NotImplementedException();
        }

        public PersonMinDTO Get(string mrn)
        {
            return new PersonMinDTO();
        }

        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}