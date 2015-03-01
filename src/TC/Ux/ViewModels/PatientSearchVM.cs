using System.Collections.Generic;
using TC.BusinessModels;

namespace TC.ViewModels
{

    public class PatientSearchVM
    {
        //public ApplicationUser CurrentAppUser { get; set; }

        public PatientBMO SelectedPatient { get; set; }
        public IList<PatientBMO> SearchPatients { get; set; }
        public IList<PatientBMO> CensusPatients { get; set; }
    }


}