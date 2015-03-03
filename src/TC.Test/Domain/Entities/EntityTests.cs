using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TC.Domain;
using TC.Domain.Entities;
using Zen.Data.QueryModel;

namespace TC.Test.Domain.Entities
{
    //
    // basic CRUD tests for the whole domain
    //


    [TestClass]
    public class PersonTests : BaseEntityTest<Person, string>
    {
        [TestMethod]
        public void PersonCRUD()
        {
            var entity = Create("TESTING123");

            Update(entity);
            Delete(entity);

            //var entities = 
            Read();
        }
    }


    [TestClass]// persistent entity with a synthetic Id<Guid>
    public class TenantTests : BaseEntityTest<Tenant, Guid>
    {
        [TestMethod]
        public void TenantCRUD()
        {
            var entity = Create();            
            
            Update(entity);
            Delete(entity);

            //var entities = 
            Read();
        }
    }

    [TestClass]// persistent entity with a natural Id only
    public class AccountResourceSecurityTests : BaseEntityTest<AccountResourceSecurity, AccountResourceSecurityId>
    {
        [TestMethod]
        public void AccountResourceSecurityCRUD()
        {
            var entity = Create(new AccountResourceSecurity
            {
                Id = NaturalKeyStringStringStringString.GenForTest<AccountResourceSecurityId>(),
                CreatedOn = DateTime.Now,
                CreatedBy = "Test"
            });
                       
            Update(entity);
            Delete(entity);

            //var entities = 
            Read();
        }
    }

    [TestClass]// a parent with children, has additional tests (ReadEager + ReadLazy)
    public class AdmissionTests : BaseEntityTest<Admission, AdmissionId>
    {
        [TestMethod]
        public void AdmissionCRUD()
        {
            var entity = Create(NaturalKeyStringDateTime.GenForTest<AdmissionId>());            
            
            Update(entity);
            Delete(entity);

            //var entities = 
            Read();
        }

        /// <summary>
        /// Fetch an object graph, specifying that we want to 'eager' load.        
        /// </summary>
        [TestMethod]
        public void AdmissionReadEager()
        {
            // these cause an extra select from the child side of the relationship...
            Dao.SetFetchMode("Cancelled", FetchModes.Eager);
            Dao.SetFetchMode("DischargeCancelled", FetchModes.Eager);

            Dao.SetFetchMode("Reasons", FetchModes.Eager);
            Dao.SetFetchMode("BedHistory", FetchModes.Eager);
            Dao.SetFetchMode("Diagnosis", FetchModes.Eager);
            Dao.SetFetchMode("InvProcedures", FetchModes.Eager);
            Dao.SetFetchMode("Therapy", FetchModes.Eager);
            Dao.SetFetchMode("DischargeTherapy", FetchModes.Eager);
            Dao.SetFetchMode("DischargeTo", FetchModes.Eager);
            Dao.SetFetchMode("FollowUp", FetchModes.Eager);                        
            
            Read();
        }

        /// <summary>
        /// Fetch an object graph, specifying that we want to 'lazy' load.        
        /// </summary>
        [TestMethod]
        public void AdmissionReadLazy()
        {
            // note: associations are lazy by default            
            Read();
        }
    }

    #region Admission children

    [TestClass]// persistent entity with a synthetic & natural Id
    public class AdmissionBedHistoryTests : BaseEntityTest<AdmissionBedHistory, int>
    {
        [TestMethod]
        public void AdmissionBedHistoryCRUD()
        {
            var entity = Create(new AdmissionBedHistory { MRN = "" });
            
            Update(entity);
            Delete(entity);

            //var entities = 
            Read();
        }
    }

    [TestClass]
    public class AdmissionCancelledTests : BaseEntityTest<AdmissionCancelled, AdmissionCancelledId>
    {
        [TestMethod]
        public void AdmissionCancelledCRUD()
        {
            var entity = Create(NaturalKeyStringDateTime.GenForTest<AdmissionCancelledId>());            
            
            Update(entity);
            Delete(entity);

            //var entities = 
            Read();
        }
    }

    [TestClass]
    public class AdmissionDiagnosisTests : BaseEntityTest<AdmissionDiagnosis, AdmissionDiagnosisId>
    {
        [TestMethod]
        public void AdmissionDiagnosisCRUD()
        {
            var entity = Create(NaturalKeyStringStringDateTime.GenForTest<AdmissionDiagnosisId>());
            
            Update(entity);
            Delete(entity);

            //var entities = 
            Read();
        }
    }

    [TestClass]
    public class AdmissionDischargeCancelledTests : BaseEntityTest<AdmissionDischargeCancelled, AdmissionDischargeCancelledId>
    {
        [TestMethod]
        public void AdmissionDischargeCancelledCRUD()
        {
            var entity = Create(NaturalKeyStringDateTime.GenForTest<AdmissionDischargeCancelledId>());
            
            Update(entity);
            Delete(entity);

            //var entities = 
            Read();
        }
    }

    [TestClass]
    public class AdmissionDischargeTherapyTests : BaseEntityTest<AdmissionDischargeTherapy, AdmissionDischargeTherapyId>
    {
        [TestMethod]
        public void AdmissionDischargeTherapyCRUD()
        {
            var entity = Create(NaturalKeyStringStringDateTime.GenForTest<AdmissionDischargeTherapyId>());
            
            Update(entity);
            Delete(entity);

            //var entities = 
            Read();
        }
    }

    [TestClass]
    public class AdmissionDischargeToTests : BaseEntityTest<AdmissionDischargeTo, AdmissionDischargeToId>
    {
        [TestMethod]
        public void AdmissionDischargeToCRUD()
        {
            var entity = Create(NaturalKeyStringStringDateTime.GenForTest<AdmissionDischargeToId>());
            
            Update(entity);
            Delete(entity);

            //var entities = 
            Read();
        }
    }

    [TestClass]
    public class AdmissionFollowUpTests : BaseEntityTest<AdmissionFollowUp, AdmissionFollowUpId>
    {
        [TestMethod]
        public void AdmissionFollowUpCRUD()
        {
            var entity = Create(NaturalKeyStringStringDateTime.GenForTest<AdmissionFollowUpId>());
            
            Update(entity);
            Delete(entity);

            //var entities = 
            Read();
        }
    }

    [TestClass]
    public class AdmissionInvProceduresTests : BaseEntityTest<AdmissionInvProcedures, AdmissionInvProceduresId>
    {
        [TestMethod]
        public void AdmissionInvProceduresCRUD()
        {
            var entity = Create(NaturalKeyStringStringDateTime.GenForTest<AdmissionInvProceduresId>());
            
            Update(entity);
            Delete(entity);

            //var entities = 
            Read();
        }
    }

    [TestClass]
    public class AdmissionReasonsTests : BaseEntityTest<AdmissionReasons, AdmissionReasonsId>
    {
        [TestMethod]
        public void AdmissionReasonsCRUD()
        {
            var entity = Create(NaturalKeyStringStringDateTime.GenForTest<AdmissionReasonsId>());
            
            Update(entity);
            Delete(entity);

            //var entities = 
            Read();
        }
    }

    [TestClass]
    public class AdmissionTherapyTests : BaseEntityTest<AdmissionTherapy, AdmissionTherapyId>
    {
        [TestMethod]
        public void AdmissionTherapyCRUD()
        {
            var entity = Create(NaturalKeyStringStringDateTime.GenForTest<AdmissionTherapyId>());
            
            Update(entity);
            Delete(entity);

            //var entities = 
            Read();
        }
    }

    #endregion

    [TestClass]
    public class AemcBiopsiesTests : BaseEntityTest<AemcBiopsies, AemcBiopsiesId>
    {
        [TestMethod]
        public void AemcBiopsiesCRUD()
        {
            var entity = Create(NaturalKeyStringInt32.GenForTest<AemcBiopsiesId>());
            
            Update(entity);
            Delete(entity);

            //var entities = 
            Read();
        }
    }

    [TestClass]
    public class AemcResectionsTests : BaseEntityTest<AemcResections, AemcResectionsId>
    {
        [TestMethod]
        public void AemcResectionsCRUD()
        {
            var entity = Create(NaturalKeyStringInt32.GenForTest<AemcResectionsId>());
            
            Update(entity);
            Delete(entity);

            //var entities = 
            Read();
        }
    }


    [TestClass]// has some additional tests (PersistNew + PersistChanges)
    public class AppointmentTests : BaseEntityTest<Appointment, AppointmenTId>
    {
        [TestMethod]
        public void AppointmentCRUD()
        {
            var entity = Create(NaturalKeyStringDateTimeDateTimeInt32.GenForTest<AppointmenTId>());
            
            entity.Notes = "make a change";
            Update(entity);
            Delete(entity);

            //var entities = 
            Read();
        }

        
        /// <summary>
        /// 'Save' a newly instantiated object
        /// </summary>
        /// <remarks>
        /// Note: This is not recommended for entities with composite id's and no version column.
        ///       It causes a select statement to be issued prior to the insert...
        ///       The best solution would of course be to NOT have composite id's
        /// WARN:  
        /// Unable to determine if [Id: TC.Domain.AppointmenTId] [Uid:897d84d2-9bac-4596-927c-b05c14b0a20c]
        /// TC.Domain.Appointment with assigned identifier TC.Domain.AppointmenTId is transient or detached; 
        /// querying the database. Use explicit Save() or Update() in session to prevent this.
        /// </remarks>
        //[TestMethod]
        public void AppointmentPersistNew()
        {
            Persist(new Appointment
            {
                Id = NaturalKeyStringDateTimeDateTimeInt32.GenForTest<AppointmenTId>()
            });

        }

        /// <summary>
        /// 'Update' a dirty object
        /// </summary>
        /// <remarks>
        /// Note: This is not recommended for entities with composite id's and no version column.
        /// see above for more details...
        /// </remarks>
        //[TestMethod]
        public void AppointmentPersistChanges()
        {
            var entity = Create(
                NaturalKeyStringDateTimeDateTimeInt32.GenForTest<AppointmenTId>());

            entity.Notes = "some new data";
            // So NH does not know this is a already a persistent entity and whether 
            // it should INSERT or UPDATE so it issues a select prior to updating...            
            // because there is NO Id to check and NO Version to check yada-yada
            // so what can we check? we need an 'unsaved-value' set in the mapping
            Persist(entity);
        }
    }


    [TestClass]
    public class ConfigUserTypeTests : BaseEntityTest<ConfigUserType, int>
    {
        [TestMethod]
        public void ConfigUserTypeCRUD()
        {
            var entity = Create(new ConfigUserType
            {
                UserType = "SomeNewType",
                Description = "",
                Enabled = false
            });
            
            Update(entity);
            Delete(entity);

            //var entities = 
            Read();
        }

        // this creates too many selects (n+1)...
        // because we aren't assigning the property on each user?
        //[TestMethod]
        //public void ConfigUserTypeGetUsers()
        //{
        //    Dao.SetFetchMode("Users", FetchModes.Lazy);

        //    var entities = base.Read();
        //    foreach (var userType in entities)
        //    {
        //        var users = userType.Users;
        //        foreach (var u in users)
        //            Console.WriteLine(u.UserName);// read-only
        //    }
        //}
    }

    [TestClass]
    public class UserTests : BaseEntityTest<User, string>
    {
        [TestMethod]
        public void UserCRUD()
        {
            // grap a config user type to assign to the new user
            var configUserTypes = Dao.FetchAll<ConfigUserType>(0, 1) as List<ConfigUserType>;
            Debug.Assert(configUserTypes != null, "configUserTypes != null");
            var entity = Create(new User
            {
                Id = "SomeNewUser",
                ConfigUserType = configUserTypes[0]
            });

            Update(entity);
            Delete(entity);

            //var entities = 
                Read();
        }
    }

    [TestClass]
    public class AuditUserAccessTests : BaseEntityTest<AuditUserAccess, int>
    {
        [TestMethod]
        public void AuditUserAccessCRUD()
        {
            var entity = Create();

            Update(entity);
            Delete(entity);

            //var entities = 
            Read();
        }
    }


    #region Views(Read-Only)

    [TestClass]
    public class PatientViewTests : BaseEntityTest<Patient, string>
    {
        [TestMethod]
        public void PatientViewRead()
        {
            //var entities = 
            Read();
        }
    }

    [TestClass]
    public class CandidateViewTests : BaseEntityTest<Candidate, string>
    {
        [TestMethod]
        public void CandidateViewRead()
        {
            //var entities = 
            Read();
        }
    }

    [TestClass]
    public class WaitListViewTests : BaseEntityTest<WaitList, string>
    {
        [TestMethod]
        public void WaitListViewRead()
        {
            //var entities = 
            Read();
        }
    }

    #endregion
}
