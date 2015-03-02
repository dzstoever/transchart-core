using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using TC.Utility.Domain;
using Zen;
using Zen.Cmds;
using Zen.Data;
using Zen.Data.QueryModel;
using Zen.Log;
using Environment = System.Environment;

namespace TC.Utility.Controls
{
    
    public partial class UpstateControl : UserControl
    {
        const char HL7Seperator = (char)0x1C;

        // these are saved with each table set
        private static string _dateTimeFormat = "yyyy-MM-dd hh:mm:ss";
        private static string _floatFormat = "F2";
        private static string _intFormat = "D"; //Ex. D8 would be 00001234
        // these are universal
        private static string _sqlDialect;
        private static string _saveLocation;
        private static int _batchSize;

        
        public UpstateControl()
        {
            InitializeComponent();

            _logger = Aspects.GetLogger("TC");
            var rtbRegistered = 
                RtbAppender.SetRichTextBox(RtbLog, "Rtb", Color.Black, new Font("Consolas", 10.25F));

            _logger.DebugFormat("Rtb Registered: {0}", rtbRegistered);
        }


        public void BindAppSettings()
        {
            _sqlDialect = App.AppSettings.SqlDialect;
            _saveLocation = App.AppSettings.SaveLocation;
            _batchSize = Convert.ToInt32(App.AppSettings.BatchSize);
            
            uxSqlDialect.Text = _sqlDialect;
            uxSaveLocation.Text = _saveLocation;
            uxBatchSize.Value = _batchSize;
            
        }

        public object DbTablesDataSource
        {
            set { listBoxAllTables.DataSource = value; }
        }

        public string DbCnnString
        {
            set
            {
                _dbCnnString = value;
                uxCurrentDbCnn.Text = _dbCnnString;
                //ConfigureNH();             
            }
        }


        private readonly ILogger _logger;
        private string _dbCnnString;
        private Configuration _cfg;
        private ISessionFactory _sessionFactory = null;
        private readonly string _msh =
            @"MSH|^~\&|||||||ORU^R01||<UniqueMessageID>|2.2|" + Environment.NewLine +
            @"PID|||<MRN>||<LastName>^<FirstName>^<MiddleInitial>^||<DOB>|<Sex>|||||||||||<SSN>|" +
            Environment.NewLine +
            @"PV1||O|^^^Unknown Location||||000000^UNKNOWN^ORDERING PROVIDER^||||||||||||" + Environment.NewLine +
            @"ORC|RE|||" + Environment.NewLine +
            @"OBR|1||<AccessionNumber>|<ProcedureID>|||<ObservationDate>||||||||^^^|000000^UNKNOWN^ORDERING PROVIDER^|||||||||F|";        
        private readonly string _obx =
            @"OBX|<SeTId>||<ComponenTId>|<SubComponenTId>|<ObservationValue>||||";

        
        private void ButtonClearRtbClick(object sender, EventArgs e)
        {
            RtbLog.Clear();
        }

        private void UxSavedSetsEnter(object sender, EventArgs e)
        {
            uxSavedSets.Items.Clear();
            uxSavedSets.DisplayMember = "Name";
            uxSavedSets.Items.Add("New...");
            foreach (var set in App.AppSettings.DbTableSets)
                uxSavedSets.Items.Add(set);
        }

        private void UxSavedSetsSelectionChangeCommitted(object sender, EventArgs e)
        {
            var cbox = (ComboBox)sender;
            var sItem = cbox.SelectedItem;
            listBoxTableSets.Items.Clear();
            var tableSet = sItem as DbTableSet;
            if (tableSet == null) return;
            var tableList = tableSet.TableNamesCsv.CsvToList();
                //ApplicationSettings.StringToList(tableSet.TableNamesCsv);
            foreach (var table in tableList)
                listBoxTableSets.Items.Add(table);
        }
        
        private void UxSqlDialectSelectionChangeCommitted(object sender, EventArgs e)
        {
            _sqlDialect = uxSqlDialect.SelectedItem.ToString();
            App.AppSettings.SqlDialect = _sqlDialect;
        }

        private void UxBatchSizeValueChanged(object sender, EventArgs e)
        {
            _batchSize = Convert.ToInt32(uxBatchSize.Value);
            App.AppSettings.BatchSize = _batchSize.ToString();
        }

        private void UxSaveLocationTextChanged(object sender, EventArgs e)
        {
            _saveLocation = uxSaveLocation.Text;
            App.AppSettings.SaveLocation = _saveLocation;
        }

        private void ButtonAddTableToSetClick(object sender, EventArgs e)
        {
            var hashSet = new HashSet<string>();
            foreach (var i in listBoxTableSets.Items)
                hashSet.Add(i.ToString());

            var selected = listBoxAllTables.SelectedItems;
            foreach (var s in (selected))
                hashSet.Add(s.ToString());

            listBoxTableSets.Items.Clear();
            foreach (var h in hashSet)
                listBoxTableSets.Items.Add(h);
        }

        private void ButtonSaveTableSetClick(object sender, EventArgs e)
        {
            var tableNames = listBoxTableSets.Items;
            var tableList = (from object t in (tableNames) select t.ToString()).ToList();

            var tableSet = new DbTableSet()
            {
                Id = Guid.NewGuid(),
                Name = string.IsNullOrWhiteSpace(textNewTableSetName.Text) ? "No Name" : textNewTableSetName.Text,
                TableNamesCsv = tableList.ListToCsv(),
                Delimiter = "|",
                DateTimeFormat = uxDateFormat.Text, 
                FloatFormat = uxFloatFormat.Text,
                IntFormat = uxIntegerFormat.Text
            };

            App.AppSettings.DbTableSets.Add(tableSet);
            
            uxSavedSets.Items.Add(tableSet);
            uxSavedSets.Text = tableSet.Name; 
        }

        private void ButtonExportSetToFlatFilesClick(object sender, EventArgs e)
        {
            _dateTimeFormat = uxDateFormat.Text;
            _floatFormat = uxFloatFormat.Text;
            _intFormat = uxIntegerFormat.Text;

            Task.Factory.StartNew(ExportToFlat, new CancellationToken(),
                TaskCreationOptions.LongRunning, TaskScheduler.Default)
                .ContinueWith(TaskExceptionHandler, TaskContinuationOptions.OnlyOnFaulted);
        }

        private void ButtonExportTableToHL7Click(object sender, EventArgs e)
        {
            if (cbUseNH.Checked)
            {                
                try
                {
                    if (_cfg == null) ConfigureNH();
                    _sessionFactory = _cfg.BuildSessionFactory();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.FullMessage());
                }
            }

            //if (cbExportAllHL7.Checked)
            //{
            //}

            var tableName = uxHL7TableName.Text;
            Task.Factory.StartNew(() => ExportToHL7(tableName), new CancellationToken(),
                TaskCreationOptions.LongRunning, TaskScheduler.Default)
                .ContinueWith(TaskExceptionHandler, TaskContinuationOptions.OnlyOnFaulted);
        }

        

        private void StatusUpdate(string message)
        {
            if (RtbLog.InvokeRequired)
                Invoke(new StringDelegate(StatusUpdate), message);
            else
                RtbLog.AppendText(Environment.NewLine + message);
        }
        
        private void EnableExport(bool value)
        {
            if (buttonExportSetToFlatFiles.InvokeRequired)
                Invoke(new BoolDelegate(EnableExport), value);
            else
            {
                buttonExportSetToFlatFiles.Enabled = value;
                buttonExportTableToHL7.Enabled = value;
            }
        }        
        
        private void ProgressUpdateMax(int max)
        {
            if (progressBar1.InvokeRequired)
                Invoke(new IntDelegate(ProgressUpdateMax), max);
            else
                progressBar1.Maximum = max;
        }
        
        private void ProgressUpdateValue(int value)
        {
            if (progressBar1.InvokeRequired)
                Invoke(new IntDelegate(ProgressUpdateValue), value);
            else
                progressBar1.Value = value>progressBar1.Maximum ? progressBar1.Maximum : value;
        }        
        
        private void TaskExceptionHandler(Task task)
        {
            var aggEx = task.Exception;            
            Aspects.GetLogger().Fatal(aggEx.FullMessage());
            MessageBox.Show(aggEx.FullMessage());
        }

        
        private void ExportToFlat()
        {
            ProgressUpdateValue(0);
            EnableExport(false);

            StatusUpdate("EXPORTING SET...");
            foreach (var item in listBoxTableSets.Items)
            {
                //ProgressUpdateValue(0);
                var tableName = item.ToString();
                var path = Path.Combine(uxSaveLocation.Text, tableName + ".txt");
                StatusUpdate("Creating file: " + path);
                _logger.Info("Creating file: " + path);
                var sw = File.CreateText(path);
                sw.AutoFlush = true;
                using (sw)
                {

                    var cmd = new BuildDataTableCmd(tableName, _dbCnnString,
                        string.Format("select * from [{0}]", tableName));

                    // get the column names, and write the header line
                    cmd.SchemaOnly = true;
                    cmd.TryRun();
                    if (cmd.Exception != null)
                    {
                        MessageBox.Show(cmd.Exception.FullMessage());
                        return;
                    }
                    var dt = cmd.DataTable;
                    string[] headers = GetHeaderArrayFromTable(dt);
                    string headerLine = BuildPipeDelimitedLine(headers);
                    sw.WriteLine(headerLine);

                    // now get all the data in chunks to conserve memory
                    cmd.SchemaOnly = false;
                    var pageSize = Convert.ToInt32(uxBatchSize.Value);
                    var totalRowCount = cmd.GetCount();
                    var processedCount = 0;
                    ProgressUpdateMax(totalRowCount);
                    StatusUpdate("Row Count = " + totalRowCount);

                    do
                    {
                        // get the next X rows starting at Y
                        var page = cmd.GetPage(pageSize, processedCount, headers);

                        var dataArrayList = new ArrayList();
                        foreach (DataRow row in page.Rows)
                        {
                            var dataArray = GetDataArrayFromRow(row);
                            dataArrayList.Add(dataArray);
                        }
                        StatusUpdate(string.Format("Adding data (count = {0})", dataArrayList.Count));
                        string lines = BuildPipeDelimitedLines(dataArrayList);
                        sw.Write(lines);

                        processedCount += page.Rows.Count;
                        ProgressUpdateValue(processedCount);
                    } while (processedCount < totalRowCount);

                }//end of using(sw)
                StatusUpdate("Finishied processing " + tableName);
            }//end of foreach(item)

            StatusUpdate("FINISHED EXPORTING SET.");
            EnableExport(true);
        }

        private string[] GetHeaderArrayFromTable(DataTable datatable)
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

        private string[] GetDataArrayFromRow(DataRow row)
        {
            var colCount = row.ItemArray.Count();
            var fitems = new string[colCount];

            // add each column as a string
            for (var k = 0; k < colCount; k++)
            {
                var field = row.ItemArray[k];
                var type = field.GetType();
                
                // take action based on the object type
                if ((type.ToString() == "System.DBNull")) 
                    fitems[k] = "";
                else if ((type.ToString() == "System.DateTime"))
                {
                    var datetime = (DateTime)field;
                    fitems[k] = datetime.ToString(_dateTimeFormat); //ExcelWrapper.
                }
                else if (type.ToString() == "System.Decimal" || type.ToString() == "System.Single" || type.ToString() == "System.Double")
                {
                    var dval = Convert.ToDecimal(field);
                    fitems[k] = dval.ToString(_floatFormat); //ExcelWrapper.
                }
                else if (type.ToString() == "System.Int16" || type.ToString() == "System.Int32" || type.ToString() == "System.Int64")
                {
                    var ival = Convert.ToInt64(field);
                    fitems[k] = ival.ToString(_intFormat); //ExcelWrapper.
                }
                else if (type.ToString() == "System.String")
                {
                    //const char tab = (char)9;
                    const char pipe = '|';                    
                    const char CR = (char)13;
                    const char LF = (char)10;

                    var sval = field.ToString();
                    //if (sval.Length > 255)
                    //    sval = "[TEXT HAS BEEN TRUNCATED!] " + sval.Substring(0, 255);
                    sval = sval.Replace(pipe.ToString(), @"/v"); //Replace embedded tabs with '/v'
                    sval = sval.Replace(CR.ToString(), @"/r");  //Replace CR with '/r'
                    sval = sval.Replace(LF.ToString(), @"/n");  //Replace LF with '/n'
                    fitems[k] = sval;
                }
                else fitems[k] = field.ToString(); // anything else...                        
            }
            return fitems;
        }

        private string BuildPipeDelimitedLine(IEnumerable<string> values)
        {
            const char pipe = '|';
            var sb = new StringBuilder();
            foreach (var value in values) sb.Append(value + pipe);

            return sb.ToString();
        }

        private string BuildPipeDelimitedLines(ArrayList list)
        {
            var sb = new StringBuilder();
            foreach (var row in list) sb.AppendLine(BuildPipeDelimitedLine((string[])row));
            return sb.ToString();
        }


        private void ConfigureNH()
        {
            try
            {
                _cfg = new Configuration();
                _cfg.DataBaseIntegration(c =>
                {
                    c.ConnectionString = _dbCnnString;
                    if (_sqlDialect == "MsSql2012Dialect")
                        c.Dialect<MsSql2012Dialect>();
                    else if (_sqlDialect == "MsSql2008Dialect")
                        c.Dialect<MsSql2008Dialect>();
                    else
                        c.Dialect<MsSql2005Dialect>();
                    c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                    c.LogSqlInConsole = true;
                    c.LogFormattedSql = true;
                });

                //get all the mappings from this assembly
                var mapper = new ModelMapper();
                var mappings = from t in typeof (IDataConnectionConfiguration).Assembly.GetTypes()
                    where t.GetInterfaces().Contains(typeof (IDbMap))
                    select t;

                var emappings = mappings as Type[] ?? mappings.ToArray();
                //StatusUpdate(emappings.Length + " mappings");
                mapper.AddMappings(emappings);
                //mapper.AddMapping();.hbm.xml
                HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
                _cfg.AddMapping(domainMapping);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.FullMessage());
            }
        }

        private void ExportToHL7(string tableName)
        {
            ProgressUpdateValue(0);
            EnableExport(false);
            
            var path = Path.Combine(uxSaveLocation.Text, tableName + ".txt");
            StatusUpdate("Creating file: " + path);
            _logger.Info("Creating file: " + path);
            var sw = File.CreateText(path);
            sw.AutoFlush = true;

            using (sw)
            {//using (dao.StartUnitOfWork())
                NHibernateDao dao = null;                                
                try
                {
                    if (cbUseNH.Checked)
                    {
                        dao = new NHibernateDao(_sessionFactory);
                        dao.StartUnitOfWork();
                    }
                    _logger.DebugFormat("DbCnn = {0}", _dbCnnString);
                    _logger.DebugFormat("Processing [" + tableName + "] table...");
                   
                    var totalCount = 0;
                    switch (tableName)
                    {
                        case "TT_HLA":
                            if(cbUseNH.Checked)
                                totalCount = dao.GetCount<TtHLA>();
                            else
                            {
                                var cmd = new BuildDataTableCmd(tableName, _dbCnnString,
                                    string.Format("select * from [{0}]", tableName));
                                totalCount = cmd.GetCount();
                            }
                            break;
                        case "TT_PRA":
                            if (cbUseNH.Checked)
                                totalCount = dao.GetCount<TtPRA>();
                            else
                            {
                                var cmd = new BuildDataTableCmd(tableName, _dbCnnString,
                                    string.Format("select * from [{0}]", tableName));
                                totalCount = cmd.GetCount();
                            }
                            break;
                        case "TT_CrossMatch":
                            if (cbUseNH.Checked)
                                totalCount = dao.GetCount<TtCrossMatch>();
                            else
                            {
                                var cmd = new BuildDataTableCmd(tableName, _dbCnnString,
                                    string.Format("select * from [{0}]", tableName));
                                totalCount = cmd.GetCount();
                            }
                            break;
                    }
                    StatusUpdate("Processing [" + tableName + "] table. (Count = " + totalCount + ")");
                    ProgressUpdateMax(totalCount);
                    ProgressUpdateValue(totalCount/100);

                    var recordsProcessed = 0;
                    var recordCounter = 0;
                    var batchPage = 0;
                    var batchCount = 0;
                    do
                    {
                        var recordsExpected = (totalCount - recordsProcessed) < _batchSize ? (totalCount - recordsProcessed) : _batchSize;
                        StringBuilder sb = null;
                        switch (tableName)
                        {
                            case "TT_HLA": 
                                sb = FetchBatchHLA(dao, batchPage, recordsExpected, ref recordCounter, out batchCount);
                                break;
                            case "TT_PRA":
                                sb = FetchBatchPRA(dao, batchPage, recordsExpected, ref recordCounter, out batchCount);
                                break;
                            case "TT_CrossMatch":
                                sb = FetchBatchCrossMatch(dao, batchPage, recordsExpected, ref recordCounter, out batchCount);
                                break;
                        }
                        StatusUpdate(string.Format("Writing {0} records...", batchCount));
                        if (sb != null) sw.Write(sb.ToString());
                        batchPage++;
                        if (batchCount < recordsExpected) recordCounter += (recordsExpected - batchCount);
                        recordsProcessed += recordsExpected;

                    } while (recordsProcessed < totalCount);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.FullMessage());
                }
                finally
                {
                    if(dao != null) dao.Dispose();
                }
            }//end using(sw)

            StatusUpdate("Processing " + tableName + " Complete.");
            EnableExport(true);
        }

        private StringBuilder FetchBatchHLA(IGenericDao dao, int batchPage, int recordsExpected, ref int recordCounter, out int batchCount)
        {                          
            StatusUpdate("Fetching data...");
            
            IEnumerable<TtHLA> tts = null;
            if (cbUseNH.Checked)// NH Fails in Upstate environment? can't determine cause... 
                tts = dao.Fetch<TtHLA>(new Query(QueryTypes.Hql, "from TtHLA t join fetch t.Person"), batchPage, _batchSize);
            else
                tts = ReadBatchHLA(ref recordCounter);
            
            var ttSet = tts as TtHLA[] ?? tts.ToArray();
            batchCount = ttSet.Count();
            StatusUpdate(string.Format("{0} of {1} records with a Person", batchCount, recordsExpected));            
            
            var sb = new StringBuilder(); // hold the text for this page of results            
            foreach (var entity in ttSet)
            {
                recordCounter++;
                ProgressUpdateValue(recordCounter);
                _logger.DebugFormat("Processing TT_HLA row {0}...", recordCounter);

                Person p = entity.Person;
                if (p == null) continue;// should never happen                              
                
                // change all rows into HL7 messages
                string msh = _msh;
                var mshId = DateTime.Now.ToString("yyyyMMddhhmmssfffff");
                int seTId = 0;
                msh = msh.Replace("<UniqueMessageID>", mshId);
                msh = msh.Replace("<MRN>", p.MRN);
                msh = msh.Replace("<LastName>", p.Last);
                msh = msh.Replace("<FirstName>", p.First);
                msh = msh.Replace("<MiddleInitial>", p.Middle);
                msh = msh.Replace("<DOB>", p.DOB.HasValue ? p.DOB.Value.ToString("yyyyMMdd") : "");
                msh = msh.Replace("<Sex>", p.Sex ?? "");
                msh = msh.Replace("<SSN>", p.SSN ?? "");
                msh = msh.Replace("<AccessionNumber>", entity.SerumID ?? Guid.NewGuid().ToString());
                msh = msh.Replace("<ProcedureID>", "HLA: " + entity.Method ?? "");
                msh = msh.Replace("<ObservationDate>", entity.Id.LabDate.ToString("yyyyMMdd"));
                msh = msh.Replace("<SeTId>", seTId.ToString());
                //...many obx's per row

                #region add obxs

                if (entity.A1 != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "A1")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.A1);
                if (entity.A2 != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "A2")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.A2);
                if (entity.B1 != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "B1")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.B1);
                if (entity.B2 != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "B2")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.B2);
                if (entity.C1 != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "C1")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.C1);
                if (entity.C2 != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "C2")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.C2);
                if (entity.DR1 != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "DR1")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.DR1);
                if (entity.DR2 != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "DR2")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.DR2);
                if (entity.DP1 != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "DP1")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.DP1);
                if (entity.DP2 != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "DP2")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.DP2);
                if (entity.DQ1 != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "DQ1")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.DQ1);
                if (entity.DQ2 != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "DQ2")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.DQ2);
                if (entity.BW4 != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "BW4")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.BW4);
                if (entity.BW6 != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "BW6")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.BW6);
                if (entity.DRW51 != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "DRW51")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.DRW51);
                if (entity.DRW52 != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "DRW52")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.DRW52);
                if (entity.DRW53 != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "DRW53")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.DRW53);

                #endregion

                seTId++;
                sb.Append(msh);
                sb.Append(Environment.NewLine);
                sb.Append(HL7Seperator);
                sb.Append(Environment.NewLine);
                _logger.Info("Added HL7 for " + p.MRN);                
            }

            return sb;
        }

        private StringBuilder FetchBatchPRA(IGenericDao dao, int batchPage, int recordsExpected, ref int recordCounter, out int batchCount)
        {
            StatusUpdate("Fetching data...");

            IEnumerable<TtPRA> tts = null;
            if (cbUseNH.Checked)// NH Fails in Upstate environment? can't determine cause... 
                tts = dao.Fetch<TtPRA>(new Query(QueryTypes.Hql, "from TtPRA t join fetch t.Person"), batchPage, _batchSize);
            else
                tts = ReadBatchPRA(ref recordCounter);
            
            var ttSet = tts as TtPRA[] ?? tts.ToArray();
            batchCount = ttSet.Count();
            StatusUpdate(string.Format("{0} of {1} records with a Person", batchCount, recordsExpected));

            var sb = new StringBuilder(); // hold the text for this page of results
            foreach (var entity in ttSet)
            {
                recordCounter++;
                ProgressUpdateValue(recordCounter);
                _logger.DebugFormat("Processing TT_PRA row {0}...", recordCounter);

                Person p = entity.Person;
                if (p == null) continue;// should never happen  
                
                // change all rows into HL7 messages
                string msh = _msh;
                var mshId = DateTime.Now.ToString("yyyyMMddhhmmssfffff");
                int seTId = 0;
                msh = msh.Replace("<UniqueMessageID>", mshId);
                msh = msh.Replace("<MRN>", p.MRN);
                msh = msh.Replace("<LastName>", p.Last);
                msh = msh.Replace("<FirstName>", p.First);
                msh = msh.Replace("<MiddleInitial>", p.Middle);
                msh = msh.Replace("<DOB>", p.DOB.HasValue ? p.DOB.Value.ToString("yyyyMMdd") : "");
                msh = msh.Replace("<Sex>", p.Sex ?? "");
                msh = msh.Replace("<SSN>", p.SSN ?? "");
                msh = msh.Replace("<AccessionNumber>", entity.Id.SerumId ?? Guid.NewGuid().ToString());
                msh = msh.Replace("<ProcedureID>", "PRA: " + entity.Id.Method ?? "");
                msh = msh.Replace("<ObservationDate>", entity.Id.SerumDate.ToString("yyyyMMdd"));
                msh = msh.Replace("<SeTId>", seTId.ToString());
                //...many obx's per row

                #region add obxs

                if (entity.Result != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "Result")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.Result);
                if (entity.Specificity != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "Result")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.Specificity);
                if (entity.A != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "A")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.A);
                if (entity.B != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "B")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.B);
                if (entity.BW4 != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "BW4")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.BW4);
                if (entity.BW6 != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "BW6")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.BW6);
                if (entity.DR != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "DR")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.DR);
                if (entity.DQ != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "DQ")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.DQ);
                if (entity.CW != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "CW")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.CW);

                if (entity.DR515253 != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "DR515253")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.DR515253);
                if (entity.DP != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "DP")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.DP);
                //comments
                if (entity.Comment != null)
                {
                    const char CR = (char)13;
                    const char LF = (char)10;
                    var lines = entity.Comment.Split(CR);
                    foreach (var line in lines)
                    {
                        var comments = line.Split(LF);
                        for (int i = 0; i < comments.Length; i++)
                        {
                            msh = msh + Environment.NewLine +
                                  _obx.Replace("<SeTId>", (seTId + 1).ToString())
                                      .Replace("<ComponenTId>", "Comment")
                                      .Replace("<SubComponenTId>", i.ToString())
                                      .Replace("<ObservationValue>", comments[i]);
                        }

                    }
                }

                #endregion

                seTId++;
                sb.Append(msh);
                sb.Append(Environment.NewLine);
                sb.Append(HL7Seperator);
                sb.Append(Environment.NewLine);
                _logger.Info("Added HL7 for " + p.MRN);
            }

            return sb;
        }

        private StringBuilder FetchBatchCrossMatch(IGenericDao dao, int batchPage, int recordsExpected, ref int recordCounter, out int batchCount)
        {
            StatusUpdate("Fetching data...");

            IEnumerable<TtCrossMatch> tts = null;
            if (cbUseNH.Checked)// NH Fails in Upstate environment? can't determine cause... 
                tts = dao.Fetch<TtCrossMatch>(new Query(QueryTypes.Hql, "from TtCrossMatch t join fetch t.Person"), batchPage, _batchSize);
            else
                tts = ReadBatchCrossMatch(ref recordCounter);

            var ttSet = tts as TtCrossMatch[] ?? tts.ToArray();
            batchCount = ttSet.Count();
            StatusUpdate(string.Format("{0} of {1} records with a Person", batchCount, recordsExpected));

            var sb = new StringBuilder(); // hold the text for this page of results
            foreach (var entity in ttSet)
            {
                recordCounter++;
                ProgressUpdateValue(recordCounter);
                _logger.DebugFormat("Processing TT_CrossMatch row {0}...", recordCounter);

                Person p = entity.Person;
                if (p == null) continue;// should never happen 

                // change all rows into HL7 messages
                string msh = _msh;
                var mshId = DateTime.Now.ToString("yyyyMMddhhmmssfffff");
                int seTId = 0;
                msh = msh.Replace("<UniqueMessageID>", mshId);
                msh = msh.Replace("<MRN>", p.MRN);
                msh = msh.Replace("<LastName>", p.Last);
                msh = msh.Replace("<FirstName>", p.First);
                msh = msh.Replace("<MiddleInitial>", p.Middle);
                msh = msh.Replace("<DOB>", p.DOB.HasValue ? p.DOB.Value.ToString("yyyyMMdd") : "");
                msh = msh.Replace("<Sex>", p.Sex ?? "");
                msh = msh.Replace("<SSN>", p.SSN ?? "");
                msh = msh.Replace("<AccessionNumber>", entity.Id.SerumId ?? Guid.NewGuid().ToString());
                msh = msh.Replace("<ProcedureID>", "CROSSMATCH: " + entity.Id.Method ?? "");
                msh = msh.Replace("<ObservationDate>", entity.Id.LabDate.ToString("yyyyMMdd"));
                msh = msh.Replace("<SeTId>", seTId.ToString());
                //...many obx's per row

                #region add obxs

                if (entity.Result != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "Result")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.Result);
                if (entity.Id.CellType != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "CellType")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.Id.CellType);
                if (entity.TargetCellSource != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "TargetCellSource")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.TargetCellSource);
                if (entity.Titer != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "Titer")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.Titer);
                if (entity.ChannelShift != null)
                    msh = msh + Environment.NewLine +
                          _obx.Replace("<SeTId>", (seTId + 1).ToString())
                              .Replace("<ComponenTId>", "ChannelShift")
                              .Replace("<SubComponenTId>", "")
                              .Replace("<ObservationValue>", entity.ChannelShift);

                //comments
                if (entity.Comments != null)
                {
                    const char CR = (char)13;
                    const char LF = (char)10;
                    var lines = entity.Comments.Split(CR);
                    foreach (var line in lines)
                    {
                        var comments = line.Split(LF);
                        for (int i = 0; i < comments.Length; i++)
                        {
                            msh = msh + Environment.NewLine +
                                  _obx.Replace("<SeTId>", (seTId + 1).ToString())
                                      .Replace("<ComponenTId>", "Comments")
                                      .Replace("<SubComponenTId>", i.ToString())
                                      .Replace("<ObservationValue>", comments[i]);
                        }

                    }
                }

                #endregion

                seTId++;
                sb.Append(msh);
                sb.Append(Environment.NewLine);
                sb.Append(HL7Seperator);
                sb.Append(Environment.NewLine);
                _logger.Info("Added HL7 for " + p.MRN);
            }

            return sb;
        }


        private IEnumerable<TtHLA> ReadBatchHLA(ref int recordCounter)
        {
            const string tableName = "TT_HLA";
            var cmd = new BuildDataTableCmd(tableName, _dbCnnString,
                        string.Format("select * from [{0}]", tableName));            
            // get the column names
            cmd.SchemaOnly = true;
            cmd.TryRun();
            if (cmd.Exception != null)
            {
                MessageBox.Show(cmd.Exception.FullMessage());
                return new List<TtHLA>();
            }
            var dt = cmd.DataTable;
            var columnNames = GetHeaderArrayFromTable(dt);
            var startAt = recordCounter;
            var page = cmd.GetPage(_batchSize, startAt, columnNames);
            var ttList = new List<TtHLA>();
            foreach (DataRow row in page.Rows)
            {
                //map data into entity...
                var tt = new TtHLA
                {
                    Id = new TtHLAId
                    {
                        MRN = row["MRN"] as string,
                        LabDate = (DateTime)row["LabDate"]
                    },
                    MRNUNOS = row["MRNUNOS"] as string,
                    Method = row["Method"] as string,
                    A1 = row["A1"] as string,
                    A2 = row["A2"] as string,
                    B1 = row["B1"] as string,
                    B2 = row["B2"] as string,
                    C1 = row["C1"] as string,
                    C2 = row["C2"] as string,
                    DR1 = row["DR1"] as string,
                    DR2 = row["DR2"] as string,
                    DP1 = row["DP1"] as string,
                    DP2 = row["DP2"] as string,
                    DQ1 = row["DQ1"] as string,
                    DQ2 = row["DQ2"] as string,
                    BW4 = row["BW4"] as string,
                    BW6 = row["BW6"] as string,
                    DRW51 = row["DRW51"] as string,
                    DRW52 = row["DRW52"] as string,
                    DRW53 = row["DRW53"] as string,
                    SerumID = row["SerumID"] as string,
                    method_11 = row["method_11"] as string,
                    comment = row["comment"] as string,
                    HaploTypeMatch = row["HaploTypeMatch"] as string,
                    DR52 = row["DQA1"] as string,
                    DQA1 = row["DQA1"] as string,
                    DQA2 = row["DQA2"] as string,
                    DR3B1 = row["DR3B1"] as string,
                    DR3B2 = row["DR3B2"] as string,
                    DR4B1 = row["DR4B1"] as string,
                    DR4B2 = row["DR4B2"] as string,
                };

                Person p = GetPerson(tt.Id.MRN);
                if (p == null) continue; //nobody found

                tt.Person = p;
                ttList.Add(tt);
            }
            return ttList;
        }

        private IEnumerable<TtPRA> ReadBatchPRA(ref int recordCounter)
        {
            const string tableName = "TT_PRA";
            var cmd = new BuildDataTableCmd(tableName, _dbCnnString,
                        string.Format("select * from [{0}]", tableName));
            // get the column names
            cmd.SchemaOnly = true;
            cmd.TryRun();
            if (cmd.Exception != null)
            {
                MessageBox.Show(cmd.Exception.FullMessage());
                return new List<TtPRA>();
            }
            var dt = cmd.DataTable;
            var columnNames = GetHeaderArrayFromTable(dt);
            var startAt = recordCounter;
            var page = cmd.GetPage(_batchSize, startAt, columnNames);
            var ttList = new List<TtPRA>();
            foreach (DataRow row in page.Rows)
            {
                //map data into entity...
                var tt = new TtPRA
                {
                    Id = new TtPRAId
                    {
                        MRN = row["MRN"] as string,
                        Method = row["Method"] as string,
                        SerumId = row["SerumId"] as string, 
                        SerumDate = (DateTime)row["SerumDate"]
                    },
                    WGName = row["WGName"] as string,
                    PRADate = row["PRADate"] as DateTime?,
                    Result = row["Result"] as string,
                    Specificity = row["Specificity"] as string,
                    LabTech = row["LabTech"] as string,
                    A = row["A"] as string,
                    B = row["B"] as string,
                    BW4 = row["BW4"] as string,
                    BW6 = row["BW6"] as string,
                    DR = row["DR"] as string,
                    DQ = row["DQ"] as string,
                    CW = row["CW"] as string,
                    DR515253 = row["DR515253"] as string,
                    DP = row["DP"] as string,
                    Comment = row["Comment"] as string                    
                };

                Person p = GetPerson(tt.Id.MRN);
                if (p == null) continue; //nobody found

                tt.Person = p;
                ttList.Add(tt);
            }
            return ttList;
        }

        private IEnumerable<TtCrossMatch> ReadBatchCrossMatch(ref int recordCounter)
        {
            const string tableName = "TT_CrossMatch";
            var cmd = new BuildDataTableCmd(tableName, _dbCnnString,
                        string.Format("select * from [{0}]", tableName));
            // get the column names
            cmd.SchemaOnly = true;
            cmd.TryRun();
            if (cmd.Exception != null)
            {
                MessageBox.Show(cmd.Exception.FullMessage());
                return new List<TtCrossMatch>();
            }
            var dt = cmd.DataTable;
            var columnNames = GetHeaderArrayFromTable(dt);
            var startAt = recordCounter;
            var page = cmd.GetPage(_batchSize, startAt, columnNames);
            var ttList = new List<TtCrossMatch>();
            foreach (DataRow row in page.Rows)
            {
                //map data into entity...
                var tt = new TtCrossMatch()
                {
                    Id = new TtCrossMatchId
                    {
                        MRN = row["MRN"] as string,
                        UNOSID = row["UNOSID"] as string,
                        Method = row["Method"] as string,
                        SerumId = row["SerumId"] as string,
                        CellType = row["CellType"] as string,
                        LabDate = (DateTime)row["LabDate"]
                    },
                    Result = row["Result"] as string,
                    MCS = row["MCS"] as string,
                    LabTech = row["LabTech"] as string,
                    Comments = row["Comments"] as string,
                    TestDate = row["TestDate"] as DateTime?,
                    TargetCellSource = row["TargetCellSource"] as string,
                    Titer = row["Titer"] as string,
                    ChannelShift = row["ChannelShift"] as string,                    
                };

                Person p = GetPerson(tt.Id.MRN);
                if (p == null) continue; //nobody found

                tt.Person = p;
                ttList.Add(tt);
            }
            return ttList;
        }

        private Person GetPerson(string mrn)
        {
            const string tableName = "Person";
            var cmd = new BuildDataTableCmd(tableName, _dbCnnString,
                        string.Format("select MRN, SSN, DOB, LastName, FirstName, MiddleName, Sex from [{0}] where MRN = '{1}'", tableName, mrn));
            cmd.TryRun();
            if (cmd.Exception == null)
                //return the first person found
                return (from DataRow row in cmd.DataTable.Rows
                    select new Person()
                    {
                        Id = row["MRN"] as string,
                        SSN = row["SSN"] as string,
                        DOB = row["DOB"] as DateTime?,
                        Last = row["LastName"] as string,
                        First = row["FirstName"] as string,
                        Middle = row["MiddleName"] as string,
                        Sex = row["Sex"] as string
                    }).FirstOrDefault();
            MessageBox.Show(cmd.Exception.FullMessage());
            return null;            
        }


        //not used
        //private Person FindPerson(IGenericDao dao, string mrn)
        //{
        //    if (string.IsNullOrEmpty(mrn)) return null;
        //    try
        //    {
        //        //var personHql = string.Format("from Person where MRN = '{0}'", mrn); 
        //        //var people = dao.Find<Person>(new Query(QueryTypes.Hql, personHql)).ToArray();
        //        //if (people.ToArray().Length > 0)
        //        //    return people[0];//.ToPerson();

        //        var patientHql = string.Format("from Patient where MRN = '{0}'", mrn); 
        //        var patients = dao.Fetch<Patient>(new Query(QueryTypes.Hql, patientHql)).ToArray();
        //        if (patients.ToArray().Length > 0)
        //            return patients[0].ToPerson();

        //        var candidateHql = string.Format("from Candidate where MRN = '{0}'", mrn); //order by TxNum desc
        //        var candidates = dao.Fetch<Candidate>(new Query(QueryTypes.Hql, candidateHql)).ToArray();
        //        if (candidates.ToArray().Length > 0)
        //            return candidates[0].ToPerson();
        //    }
        //    catch (Exception)
        //    { return null; }            
        //    return null;

        //    #region another way
        //    //var candidateHql = string.Format("from Candidate where MRN = '{0}'", mrn); //order by TxNum desc
        //    //var patientHql = string.Format("from Patient where MRN = '{0}'", mrn);

        //    //var uniquePerson = new HashSet<Person>();
        //    //var cCnt = dao.GetCount<TtHLA>(new Query(QueryTypes.Hql, candidateHql));
        //    //var pCnt = dao.GetCount<TtHLA>(new Query(QueryTypes.Hql, patientHql));


        //    //if (cCnt >= 1)
        //    //{
        //    //    var candidates = dao.Fetch<Candidate>(new Query(QueryTypes.Hql, candidateHql));
        //    //    foreach (var candidate in candidates)
        //    //    { uniquePerson.Add(candidate.ToPerson()); break; }
        //    //}
        //    //if (pCnt >= 1)
        //    //{
        //    //    var patients = dao.Fetch<Patient>(new Query(QueryTypes.Hql, patientHql));
        //    //    foreach (var patient in patients)
        //    //    { uniquePerson.Add(patient.ToPerson()); break; }
        //    //}

        //    //return uniquePerson.FirstOrDefault();
        //    #endregion
        //}

    }


    public interface IUtilityUserControl
    {
    }

}
