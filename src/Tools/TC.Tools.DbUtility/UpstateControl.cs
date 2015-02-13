using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Engine;
using NHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.SqlCommand;
using TC.Doman;
using Zen.Data;

namespace TC.Tools.DbUtility
{
    public partial class UpstateControl : UserControl
    {
        private string _dbCnnString;
        public string DbCnnString {
            set
            {
                _dbCnnString = value;
                uxCurrentDbCnn.Text = _dbCnnString;
                ConfigureNH();
                AddMappingsNH();
            }
        }

        public object DbTablesDataSource
        {
            set { listBoxTables.DataSource = value; }
        } 


        public UpstateControl()
        {
            InitializeComponent();

            
        }

        




        private void buttonAddTableToSet_Click(object sender, EventArgs e)
        {
            var selected = listBoxTables.SelectedItem;

            //if(!listBoxSet.Contains(selected))
                listBoxSet.Items.Add(selected);            
        }

        private void buttonSaveTableSet_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented :(");

        }

        private void buttonExportSetToFlatFiles_Click(object sender, EventArgs e)
        {

        }

        private void buttonExportTableToHL7_Click(object sender, EventArgs e)
        {
            _sessionFactory = _cfg.BuildSessionFactory();

             var dao = new NHibernateDao(_sessionFactory);
            using (dao.StartUnitOfWork())
            {
                var count = dao.GetCount<TC.Doman.TtHLA>();
                MessageBox.Show(count.ToString());
                
                
                var entities = dao.FetchAll<TC.Doman.TtHLA>();
                                

                var sb = new StringBuilder();
                foreach (var entity in entities)
                {
                    var uniquePerson = new HashSet<Person>();
                    var personFound = false;


                    var candidate = dao.FetchById<TC.Doman.Candidate>(entity.Id.MRN);
                    var patient = dao.FetchById<TC.Doman.Patient>(entity.Id.MRN);
                    try
                    {
                        uniquePerson.Add(candidate.ToPerson());
                        personFound = true;
                    }
                    catch (Exception)
                    {
                        Debug.WriteLine("candidate not found");
                    }
                    try
                    {
                        uniquePerson.Add(patient.ToPerson());
                        personFound = true;
                    }
                    catch (Exception)
                    {
                        Debug.WriteLine("patient not found");
                    }


                    if (!personFound) return;

                    Person p = null;
                    foreach (var person in uniquePerson)
                        p = person;

                    if (p == null)
                    {
                        MessageBox.Show(string.Format("Got messages for unknown person."));
                        return;
                    }
                    else
                    {
                        //var obx = "";// there will be multiples of these per row

                        MessageBox.Show(string.Format("Got messages for {0}", p.MRN));


                        //todo: change all HLA rows into HL7 messages
                        var msh =
                            @"
                        MSH|^~\&|||||||ORU^R01||<UniqueMessageID>|2.2|
                        PID|||<MRN>||<LastName>^<FirstName>^<MiddleInitial>^||<DOB>|<Sex>|||||||||||<SSN>|
                        PV1||O|^^^Unknown Location||||000000^UNKNOWN^ORDERING PROVIDER^||||||||||||
                        ORC|RE|||
                        OBR|1||<AccessionNumber>|<ProcedureID>|||<ObservationDate>||||||||^^^|000000^UNKNOWN^ORDERING PROVIDER^|||||||||F|
                        OBX|<SetID>||<ComponentID>|<SubComponentID>|<ObservationValue>||||
                    ";

                        var mshId = "";
                        msh.Replace("<UniqueMessageID>", mshId);
                        msh.Replace("<MRN>", "");
                        //msh.Replace("<>", "");
                        //msh.Replace("<>", "");
                        //msh.Replace("<>", "");
                        //msh.Replace("<>", "");
                        //msh.Replace("<>", "");
                        //msh.Replace("<>", "");
                        //msh.Replace("<>", "");
                        //msh.Replace("<>", "");
                        //msh.Replace("<>", "");
                        //msh.Replace("<>", "");
                        //msh.Replace("<>", "");
                        //msh.Replace("<>", "");
                        //msh.Replace("<>", "");

                        //...many obx's per row
                    }
                }
            }

        }



        //private string _cnnString = @"Data Source=DSTOEVERPC;Initial Catalog=TCTest5200;Integrated Security=True;Pooling=False";
        private Configuration _cfg;
        private ISessionFactoryImplementor _sessionFactoryImpl = null;
        private ISessionFactory _sessionFactory = null;


        //private static string DateTimeFormat = "yyyy-MM-dd hh:mm:ss";
        //private static string FloatFormat = "F2";
        //private static string IntFormat = "D";//Ex. D8 would be 00001234


        private void ConfigureNH()
        {
            _cfg = new Configuration();
            _cfg.DataBaseIntegration(c =>
            {
                c.ConnectionString = _dbCnnString;
                c.Dialect<NHibernate.Dialect.MsSql2008Dialect>();
                c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                
                //c.LogSqlInConsole = true;
                //c.LogFormattedSql = true;
                // !!!
                // be very careful with this or you could wipe out an entire database!!!
                // !!!
                //c.SchemaAction = SchemaAutoAction.Validate;// .Drop, .Update, .Export, .All                                                
            });
        }

        private void AddMappingsNH()
        {
            //get all the mappings from this assembly
            var mapper = new ModelMapper();
            var mappings = from t in typeof(IDataConnectionConfiguration).Assembly.GetTypes()
                           where t.GetInterfaces().Contains(typeof(IDbMap))
                           select t;

            //MessageBox.Show(string.Format("{0} mappings in domain model.", mappings.Count()));

            mapper.AddMappings(mappings);
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            _cfg.AddMapping(domainMapping);


        }


        private DataTable BuildDataTable(Type entityType)
        {
            DataTable dt = new DataTable();
            var metaData = _sessionFactoryImpl.GetClassMetadata(entityType);
            var persistentClass = _cfg.GetClassMapping(entityType);
            dt = new DataTable(persistentClass.Table.Name);

            //var fu = metaData.GetPropertyType("MRN");

            //if(metaData.IdentifierType.IsComponentType)
            //{
            //foreach (var p in persistentClass.IdentifierMapper.PropertyIterator)
            //{
            //    var t = p.Type;
            //}

            foreach (var column in persistentClass.IdentityTable.ColumnIterator)
            {
                var colName = column.Name;
                var nhThype = metaData.GetPropertyType("MRN");
                var netType = nhThype.ReturnedClass;
                dt.Columns.Add(new DataColumn(colName, netType));
            }

            //}
            //foreach (var propName in metaData.PropertyNames)
            //{
            //    var nhType = metaData.GetPropertyType(propName);
            //    if (!nhType.IsAssociationType && !nhType.IsCollectionType)
            //        dt.Columns.Add(new DataColumn(propName, nhType.ReturnedClass));
            //}

            return dt;
        }

        public string GetSqlColumnName(string propertyName, PersistentClass persistentClass)
        {
            //foreach (var dbCol in from prop in persistentClass.PropertyIterator
            //                         where prop.Name == propertyName
            //                         from Column dbCol in prop.ColumnIterator
            //                         select dbCol)
            //{
            //    return dbCol.Name;
            //}
            return "IamColumn";
            //throw new Exception("Property not found! (" + propertyName + ")");
        }

        private void AddRow(DataTable table, object entity, Type entityType)
        {
            var persistentClass = _cfg.GetClassMapping(entityType);

            var row = table.NewRow();
            var props = entityType.GetProperties();
            foreach (var prop in props)
            {
                var sqlColName = GetSqlColumnName(prop.Name, persistentClass);

                if (!table.Columns.Contains(sqlColName)) continue;
                var val = prop.GetValue(entity, null);
                row[prop.Name] = val ?? DBNull.Value;
                //else { Debug.WriteLine("Excluded value = " + colName); }
            }
            table.Rows.Add(row);
        }

        private string[] BuildHeaderRowFromTable(DataTable datatable)
        {
            var headers = new string[datatable.Columns.Count];
            var i = 0;
            foreach (DataColumn column in datatable.Columns)
            {
                headers[i] = column.ColumnName;
                i++;
            }
            return headers;
        }

        private string BuildPipeDelimitedRow(IEnumerable<string> values)
        {
            const char pipe = '|';
            var sb = new StringBuilder();
            foreach (var value in values) sb.Append(value + pipe);

            return sb.ToString();
        }

        private string BuildPipeDelimitedTable(ArrayList list)
        {
            var sb = new StringBuilder();
            foreach (var row in list) sb.AppendLine(BuildPipeDelimitedRow((string[])row));
            return sb.ToString();
        }

    }
}
