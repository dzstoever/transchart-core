using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using TC.Domain;

namespace TC.Maps 
{    
    public class AppointmentMap : MultiEnteredByMap<Appointment, AppointmenTId>, Zen.Data.IDbMap
    {        
        public AppointmentMap() 
        { 
            Schema("dbo"); Table("Appointment_Multi"); 
                        
            ComponentAsId(x => x.Id, compId =>
            {
                compId.Property(c => c.MRN);
                compId.Property(c => c.VisitDate);
                compId.Property(c => c.VisitTime);
                compId.Property(c => c.VisitTypeID);                
            });
            ;
            
            Property(x => x.LocationID, map => map.Precision(10));
			Property(x => x.VisitStatus, map => map.Length(50));
			Property(x => x.Notes, map => map.Length(255));
			Property(x => x.VisitType, map => map.Length(50));
			Property(x => x.ICD9, map => map.Length(50));
			Property(x => x.ICD9Description, map => map.Length(150));
			Property(x => x.ClinicLocation, map => map.Length(50));
			Property(x => x.Service, map => map.Length(50));
			Property(x => x.Physician, map => map.Length(50));

        }
                
    }
}
