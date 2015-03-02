using System.Collections.Generic;
using TC.BusinessModel;

namespace TC.Ux.ViewModels
{

    public class PatientSearchVM
    {
        //public ApplicationUser CurrentAppUser { get; set; }

        public PatientBMO SelectedPatient { get; set; }
        public IList<PatientBMO> SearchPatients { get; set; }
        public IList<PatientBMO> CensusPatients { get; set; }
    }


}