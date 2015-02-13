using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using TC.Doman;
using Iesi.Collections.Generic;
using Zen.Data;


namespace TC.Maps {


    public class NoteMultiMap : ClassMapping<NoteMulti>, IDbMap
    {
        
        public NoteMultiMap() {
			Table("Note_Multi");
			Schema("dbo");
			Lazy(true);
			ComposedId(compId =>
				{
					compId.Property(x => x.Note_Id, m => m.Column("NoteId"));
					compId.Property(x => x.MRN, m => m.Column("MRN"));
					compId.Property(x => x.Note_Date, m => m.Column("NoteDate"));
					compId.Property(x => x.Entered_Time, m => m.Column("EnteredTime"));
					compId.Property(x => x.Tenant_ID, m => m.Column("TenantID"));
				});
			Property(x => x.Grp_Id, map => map.Column("GrpId"));
			Property(x => x.Notes);
			Property(x => x.Entered_Date, map => map.Column("EnteredDate"));
			Property(x => x.Entered_By, map => map.Column("EnteredBy"));
			Property(x => x.Note_Type, map => map.Column("NoteType"));
			Property(x => x.Urgent_Note, map => map.Column("UrgentNote"));
			Property(x => x.Urgent_Note_Receiver, map => map.Column("UrgentNoteReceiver"));
			Property(x => x.Urgent_Note_Read, map => map.Column("UrgentNoteRead"));
			Property(x => x.Processed);
			Property(x => x.Ordered_By, map => map.Column("OrderedBy"));
			Property(x => x.Esign_Date, map => map.Column("EsignDate"));
			Property(x => x.Esign_Time, map => map.Column("EsignTime"));
			Property(x => x.Struck_Out, map => map.Column("StruckOut"));
			Property(x => x.Reply_Note_Id, map => map.Column("ReplyNoteId"));
        }
    }
}
