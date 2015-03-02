using System.Collections.Generic;
using System.Web.Mvc;
using TC.BusinessModel;

namespace TC.Website.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private PatientBMO FakePerson1
        {
            get
            {
                return new PatientBMO
                {
                    FirstName = "Leroy",
                    LastName = "Brown",
                    MiddleName = "A",
                    MRN = "11111111",
                    SSN = "111111111"
                };
            }
        }
        private PatientBMO FakePerson2
        {
            get
            {
                return new PatientBMO
                {
                    FirstName = "Bilbo",
                    LastName = "Baggins",
                    MiddleName = "A",
                    MRN = "22222222",
                    SSN = "222222222"
                };
            }
        }
        private PatientBMO FakePerson3
        {
            get
            {
                return new PatientBMO
                {
                    FirstName = "Coach",
                    LastName = "Ditka",
                    MiddleName = "A",
                    MRN = "33333333",
                    SSN = "333333333"
                };
            }
        }


        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]       
        //[ValidateAntiForgeryToken]
        
        // an action method that will return a view having a PatientSearchVM
        //public ActionResult PatientSearch()
        //{
        //    var vm = new PatientSearchVM();
        //    // todo: populate the ViewModel with real patients...
        //    vm.SelectedPatient = FakePerson1;
        //    vm.CensusPatients = new List<PatientBMO>{ FakePerson1,FakePerson2 };
        //    vm.SearchPatients = new List<PatientBMO>{ FakePerson1,FakePerson2, FakePerson3 };

        //    return PartialView("_PatientSearch", vm);
        //}

        
    }

}
