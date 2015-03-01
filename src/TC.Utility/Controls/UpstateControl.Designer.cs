namespace TC.Utility.Controls
{
    partial class UpstateControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBoxAllTables = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.uxSqlDialect = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.uxBatchSize = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.uxSaveLocation = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonAddTableToSet = new System.Windows.Forms.Button();
            this.uxCurrentDbCnn = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listBoxTableSets = new System.Windows.Forms.ListBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.uxIntegerFormat = new System.Windows.Forms.TextBox();
            this.uxFloatFormat = new System.Windows.Forms.TextBox();
            this.uxDateFormat = new System.Windows.Forms.TextBox();
            this.uxDelimiter = new System.Windows.Forms.ComboBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonExportSetToFlatFiles = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textNewTableSetName = new System.Windows.Forms.TextBox();
            this.buttonSaveTableSet = new System.Windows.Forms.Button();
            this.uxSavedSets = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RtbLog = new System.Windows.Forms.RichTextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cbUseNH = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.buttonClearRtb = new System.Windows.Forms.Button();
            this.buttonExportTableToHL7 = new System.Windows.Forms.Button();
            this.uxHL7TableName = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbExportAllHL7 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxBatchSize)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(5, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBoxAllTables);
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(768, 614);
            this.splitContainer1.SplitterDistance = 237;
            this.splitContainer1.TabIndex = 0;
            // 
            // listBoxAllTables
            // 
            this.listBoxAllTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxAllTables.FormattingEnabled = true;
            this.listBoxAllTables.Location = new System.Drawing.Point(0, 109);
            this.listBoxAllTables.Name = "listBoxAllTables";
            this.listBoxAllTables.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxAllTables.Size = new System.Drawing.Size(237, 385);
            this.listBoxAllTables.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.uxSqlDialect);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.uxBatchSize);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.uxSaveLocation);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 494);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(237, 120);
            this.panel3.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label12.Location = new System.Drawing.Point(0, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(237, 13);
            this.label12.TabIndex = 15;
            this.label12.Text = "Sql Dialect";
            // 
            // uxSqlDialect
            // 
            this.uxSqlDialect.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uxSqlDialect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxSqlDialect.FormattingEnabled = true;
            this.uxSqlDialect.Items.AddRange(new object[] {
            "MsSql2005Dialect",
            "MsSql2008Dialect",
            "MsSql2012Dialect"});
            this.uxSqlDialect.Location = new System.Drawing.Point(0, 14);
            this.uxSqlDialect.Name = "uxSqlDialect";
            this.uxSqlDialect.Size = new System.Drawing.Size(237, 28);
            this.uxSqlDialect.TabIndex = 14;
            this.uxSqlDialect.Text = "MsSql2005Dialect";
            this.uxSqlDialect.SelectionChangeCommitted += new System.EventHandler(this.uxSqlDialect_SelectionChangeCommitted);
            // 
            // label13
            // 
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label13.Location = new System.Drawing.Point(0, 42);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(237, 13);
            this.label13.TabIndex = 13;
            this.label13.Text = "Batch Size";
            // 
            // uxBatchSize
            // 
            this.uxBatchSize.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uxBatchSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxBatchSize.Location = new System.Drawing.Point(0, 55);
            this.uxBatchSize.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.uxBatchSize.Name = "uxBatchSize";
            this.uxBatchSize.Size = new System.Drawing.Size(237, 26);
            this.uxBatchSize.TabIndex = 12;
            this.uxBatchSize.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.uxBatchSize.ValueChanged += new System.EventHandler(this.uxBatchSize_ValueChanged);
            // 
            // label14
            // 
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label14.Location = new System.Drawing.Point(0, 81);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(237, 13);
            this.label14.TabIndex = 5;
            this.label14.Text = "Save Location (All Files)";
            // 
            // uxSaveLocation
            // 
            this.uxSaveLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uxSaveLocation.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uxSaveLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxSaveLocation.Location = new System.Drawing.Point(0, 94);
            this.uxSaveLocation.Name = "uxSaveLocation";
            this.uxSaveLocation.Size = new System.Drawing.Size(237, 26);
            this.uxSaveLocation.TabIndex = 4;
            this.uxSaveLocation.Tag = "";
            this.uxSaveLocation.Text = "x:\\exported";
            this.uxSaveLocation.TextChanged += new System.EventHandler(this.uxSaveLocation_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonAddTableToSet);
            this.panel1.Controls.Add(this.uxCurrentDbCnn);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(237, 109);
            this.panel1.TabIndex = 0;
            // 
            // buttonAddTableToSet
            // 
            this.buttonAddTableToSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonAddTableToSet.Location = new System.Drawing.Point(0, 48);
            this.buttonAddTableToSet.Name = "buttonAddTableToSet";
            this.buttonAddTableToSet.Size = new System.Drawing.Size(237, 33);
            this.buttonAddTableToSet.TabIndex = 6;
            this.buttonAddTableToSet.Text = "Add To Set >";
            this.buttonAddTableToSet.UseVisualStyleBackColor = true;
            this.buttonAddTableToSet.Click += new System.EventHandler(this.buttonAddTableToSet_Click);
            // 
            // uxCurrentDbCnn
            // 
            this.uxCurrentDbCnn.BackColor = System.Drawing.Color.Black;
            this.uxCurrentDbCnn.Dock = System.Windows.Forms.DockStyle.Top;
            this.uxCurrentDbCnn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxCurrentDbCnn.ForeColor = System.Drawing.Color.White;
            this.uxCurrentDbCnn.Location = new System.Drawing.Point(0, 26);
            this.uxCurrentDbCnn.Name = "uxCurrentDbCnn";
            this.uxCurrentDbCnn.ReadOnly = true;
            this.uxCurrentDbCnn.Size = new System.Drawing.Size(237, 22);
            this.uxCurrentDbCnn.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(0, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Current Database Connection";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "All Tables/Views";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listBoxTableSets);
            this.splitContainer2.Panel1.Controls.Add(this.panel5);
            this.splitContainer2.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.RtbLog);
            this.splitContainer2.Panel2.Controls.Add(this.panel6);
            this.splitContainer2.Panel2.Controls.Add(this.panel4);
            this.splitContainer2.Size = new System.Drawing.Size(527, 614);
            this.splitContainer2.SplitterDistance = 269;
            this.splitContainer2.TabIndex = 0;
            // 
            // listBoxTableSets
            // 
            this.listBoxTableSets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxTableSets.FormattingEnabled = true;
            this.listBoxTableSets.Location = new System.Drawing.Point(0, 109);
            this.listBoxTableSets.Name = "listBoxTableSets";
            this.listBoxTableSets.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxTableSets.Size = new System.Drawing.Size(269, 385);
            this.listBoxTableSets.TabIndex = 7;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.uxIntegerFormat);
            this.panel5.Controls.Add(this.uxFloatFormat);
            this.panel5.Controls.Add(this.uxDateFormat);
            this.panel5.Controls.Add(this.uxDelimiter);
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Controls.Add(this.buttonExportSetToFlatFiles);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 494);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(269, 120);
            this.panel5.TabIndex = 2;
            // 
            // uxIntegerFormat
            // 
            this.uxIntegerFormat.Dock = System.Windows.Forms.DockStyle.Top;
            this.uxIntegerFormat.Location = new System.Drawing.Point(106, 61);
            this.uxIntegerFormat.Name = "uxIntegerFormat";
            this.uxIntegerFormat.Size = new System.Drawing.Size(163, 20);
            this.uxIntegerFormat.TabIndex = 9;
            this.uxIntegerFormat.Text = "D";
            // 
            // uxFloatFormat
            // 
            this.uxFloatFormat.Dock = System.Windows.Forms.DockStyle.Top;
            this.uxFloatFormat.Location = new System.Drawing.Point(106, 41);
            this.uxFloatFormat.Name = "uxFloatFormat";
            this.uxFloatFormat.Size = new System.Drawing.Size(163, 20);
            this.uxFloatFormat.TabIndex = 8;
            this.uxFloatFormat.Text = "F2";
            // 
            // uxDateFormat
            // 
            this.uxDateFormat.Dock = System.Windows.Forms.DockStyle.Top;
            this.uxDateFormat.Location = new System.Drawing.Point(106, 21);
            this.uxDateFormat.Name = "uxDateFormat";
            this.uxDateFormat.Size = new System.Drawing.Size(163, 20);
            this.uxDateFormat.TabIndex = 7;
            this.uxDateFormat.Text = "yyyy-MM-dd hh:mm:ss";
            // 
            // uxDelimiter
            // 
            this.uxDelimiter.Dock = System.Windows.Forms.DockStyle.Top;
            this.uxDelimiter.Enabled = false;
            this.uxDelimiter.FormattingEnabled = true;
            this.uxDelimiter.Items.AddRange(new object[] {
            "Pipe (|)",
            "Comma (,)",
            "Tab (/t)"});
            this.uxDelimiter.Location = new System.Drawing.Point(106, 0);
            this.uxDelimiter.Name = "uxDelimiter";
            this.uxDelimiter.Size = new System.Drawing.Size(163, 21);
            this.uxDelimiter.TabIndex = 4;
            this.uxDelimiter.Text = "Pipe ( | )";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label10);
            this.panel7.Controls.Add(this.label9);
            this.panel7.Controls.Add(this.label8);
            this.panel7.Controls.Add(this.label7);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel7.ForeColor = System.Drawing.SystemColors.Highlight;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(106, 87);
            this.panel7.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(106, 20);
            this.label10.TabIndex = 3;
            this.label10.Text = "Integer Format";
            // 
            // label9
            // 
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(0, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 20);
            this.label9.TabIndex = 2;
            this.label9.Text = "Float format";
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 20);
            this.label8.TabIndex = 1;
            this.label8.Text = "Date Format";
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "Delimiter";
            // 
            // buttonExportSetToFlatFiles
            // 
            this.buttonExportSetToFlatFiles.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonExportSetToFlatFiles.Location = new System.Drawing.Point(0, 87);
            this.buttonExportSetToFlatFiles.Name = "buttonExportSetToFlatFiles";
            this.buttonExportSetToFlatFiles.Size = new System.Drawing.Size(269, 33);
            this.buttonExportSetToFlatFiles.TabIndex = 2;
            this.buttonExportSetToFlatFiles.Text = "Export Set to Flat File(s)";
            this.buttonExportSetToFlatFiles.UseVisualStyleBackColor = true;
            this.buttonExportSetToFlatFiles.Click += new System.EventHandler(this.buttonExportSetToFlatFiles_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textNewTableSetName);
            this.panel2.Controls.Add(this.buttonSaveTableSet);
            this.panel2.Controls.Add(this.uxSavedSets);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(269, 109);
            this.panel2.TabIndex = 1;
            // 
            // textNewTableSetName
            // 
            this.textNewTableSetName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textNewTableSetName.Dock = System.Windows.Forms.DockStyle.Top;
            this.textNewTableSetName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNewTableSetName.Location = new System.Drawing.Point(0, 83);
            this.textNewTableSetName.Name = "textNewTableSetName";
            this.textNewTableSetName.Size = new System.Drawing.Size(269, 22);
            this.textNewTableSetName.TabIndex = 12;
            this.textNewTableSetName.Text = "NewTableSet";
            // 
            // buttonSaveTableSet
            // 
            this.buttonSaveTableSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonSaveTableSet.Location = new System.Drawing.Point(0, 50);
            this.buttonSaveTableSet.Name = "buttonSaveTableSet";
            this.buttonSaveTableSet.Size = new System.Drawing.Size(269, 33);
            this.buttonSaveTableSet.TabIndex = 11;
            this.buttonSaveTableSet.Text = "Save Set";
            this.buttonSaveTableSet.UseVisualStyleBackColor = true;
            this.buttonSaveTableSet.Click += new System.EventHandler(this.buttonSaveTableSet_Click);
            // 
            // uxSavedSets
            // 
            this.uxSavedSets.BackColor = System.Drawing.Color.Black;
            this.uxSavedSets.Dock = System.Windows.Forms.DockStyle.Top;
            this.uxSavedSets.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxSavedSets.ForeColor = System.Drawing.Color.White;
            this.uxSavedSets.FormattingEnabled = true;
            this.uxSavedSets.Items.AddRange(new object[] {
            "Set 1",
            "Set 2"});
            this.uxSavedSets.Location = new System.Drawing.Point(0, 26);
            this.uxSavedSets.Name = "uxSavedSets";
            this.uxSavedSets.Size = new System.Drawing.Size(269, 24);
            this.uxSavedSets.TabIndex = 4;
            this.uxSavedSets.Text = "New...";
            this.uxSavedSets.SelectionChangeCommitted += new System.EventHandler(this.uxSavedSets_SelectionChangeCommitted);
            this.uxSavedSets.Enter += new System.EventHandler(this.uxSavedSets_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Saved Sets";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(269, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Selected Set";
            // 
            // RtbLog
            // 
            this.RtbLog.BackColor = System.Drawing.Color.Black;
            this.RtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RtbLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.RtbLog.Location = new System.Drawing.Point(0, 109);
            this.RtbLog.Name = "RtbLog";
            this.RtbLog.Size = new System.Drawing.Size(254, 472);
            this.RtbLog.TabIndex = 7;
            this.RtbLog.Text = "";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.progressBar1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 581);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(254, 33);
            this.panel6.TabIndex = 6;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 6);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(254, 27);
            this.progressBar1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cbExportAllHL7);
            this.panel4.Controls.Add(this.cbUseNH);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.buttonClearRtb);
            this.panel4.Controls.Add(this.buttonExportTableToHL7);
            this.panel4.Controls.Add(this.uxHL7TableName);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(254, 109);
            this.panel4.TabIndex = 4;
            // 
            // cbUseNH
            // 
            this.cbUseNH.AutoSize = true;
            this.cbUseNH.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUseNH.Location = new System.Drawing.Point(138, -1);
            this.cbUseNH.Name = "cbUseNH";
            this.cbUseNH.Size = new System.Drawing.Size(55, 16);
            this.cbUseNH.TabIndex = 12;
            this.cbUseNH.Text = "use NH";
            this.cbUseNH.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 91);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "Log Messages";
            // 
            // buttonClearRtb
            // 
            this.buttonClearRtb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearRtb.Location = new System.Drawing.Point(176, 86);
            this.buttonClearRtb.Name = "buttonClearRtb";
            this.buttonClearRtb.Size = new System.Drawing.Size(75, 23);
            this.buttonClearRtb.TabIndex = 10;
            this.buttonClearRtb.Text = "Clear";
            this.buttonClearRtb.UseVisualStyleBackColor = true;
            this.buttonClearRtb.Click += new System.EventHandler(this.buttonClearRtb_Click);
            // 
            // buttonExportTableToHL7
            // 
            this.buttonExportTableToHL7.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonExportTableToHL7.Location = new System.Drawing.Point(0, 50);
            this.buttonExportTableToHL7.Name = "buttonExportTableToHL7";
            this.buttonExportTableToHL7.Size = new System.Drawing.Size(254, 33);
            this.buttonExportTableToHL7.TabIndex = 9;
            this.buttonExportTableToHL7.Text = "Export Table to HL7 File";
            this.buttonExportTableToHL7.UseVisualStyleBackColor = true;
            this.buttonExportTableToHL7.Click += new System.EventHandler(this.buttonExportTableToHL7_Click);
            // 
            // uxHL7TableName
            // 
            this.uxHL7TableName.BackColor = System.Drawing.Color.Black;
            this.uxHL7TableName.Dock = System.Windows.Forms.DockStyle.Top;
            this.uxHL7TableName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxHL7TableName.ForeColor = System.Drawing.Color.White;
            this.uxHL7TableName.FormattingEnabled = true;
            this.uxHL7TableName.Items.AddRange(new object[] {
            "TT_HLA",
            "TT_PRA",
            "TT_CrossMatch"});
            this.uxHL7TableName.Location = new System.Drawing.Point(0, 26);
            this.uxHL7TableName.Name = "uxHL7TableName";
            this.uxHL7TableName.Size = new System.Drawing.Size(254, 24);
            this.uxHL7TableName.TabIndex = 8;
            this.uxHL7TableName.Text = "TT_HLA";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(0, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 7;
            this.label5.Tag = "";
            this.label5.Text = "Table Name";
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(254, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Export HL7 from Table";
            // 
            // cbExportAllHL7
            // 
            this.cbExportAllHL7.AutoSize = true;
            this.cbExportAllHL7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbExportAllHL7.Location = new System.Drawing.Point(6, 59);
            this.cbExportAllHL7.Name = "cbExportAllHL7";
            this.cbExportAllHL7.Size = new System.Drawing.Size(35, 16);
            this.cbExportAllHL7.TabIndex = 13;
            this.cbExportAllHL7.Text = "All";
            this.cbExportAllHL7.UseVisualStyleBackColor = true;
            this.cbExportAllHL7.Visible = false;
            // 
            // UpstateControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UpstateControl";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(778, 624);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxBatchSize)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonExportSetToFlatFiles;
        private System.Windows.Forms.TextBox uxCurrentDbCnn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox uxSavedSets;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox uxIntegerFormat;
        private System.Windows.Forms.TextBox uxFloatFormat;
        private System.Windows.Forms.TextBox uxDateFormat;
        private System.Windows.Forms.ComboBox uxDelimiter;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonAddTableToSet;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxAllTables;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox uxSaveLocation;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ListBox listBoxTableSets;
        private System.Windows.Forms.TextBox textNewTableSetName;
        private System.Windows.Forms.Button buttonSaveTableSet;
        private System.Windows.Forms.Button buttonExportTableToHL7;
        private System.Windows.Forms.ComboBox uxHL7TableName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox RtbLog;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown uxBatchSize;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button buttonClearRtb;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox uxSqlDialect;
        private System.Windows.Forms.CheckBox cbUseNH;
        private System.Windows.Forms.CheckBox cbExportAllHL7;
    }
}
