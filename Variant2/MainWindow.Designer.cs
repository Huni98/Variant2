namespace Variant2
{
    partial class MainWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabWorkOrders = new System.Windows.Forms.TabPage();
            this.tabClientsCars = new System.Windows.Forms.TabPage();
            this.tabDiagnostics = new System.Windows.Forms.TabPage();
            this.btnResetImport = new System.Windows.Forms.Button();
            this.dgvWorkOrders = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExportSummary = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbClients = new System.Windows.Forms.ListBox();
            this.dgvCars = new System.Windows.Forms.DataGridView();
            this.cmbMakeFilter = new System.Windows.Forms.ComboBox();
            this.txtYearFilter = new System.Windows.Forms.TextBox();
            this.btnFilterCars = new System.Windows.Forms.Button();
            this.cmbCarForDiagnostics = new System.Windows.Forms.ComboBox();
            this.dgvTestResults = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.splitDiagnostics = new System.Windows.Forms.SplitContainer();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvObdCodes = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabControlMain.SuspendLayout();
            this.tabWorkOrders.SuspendLayout();
            this.tabClientsCars.SuspendLayout();
            this.tabDiagnostics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCars)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitDiagnostics)).BeginInit();
            this.splitDiagnostics.Panel1.SuspendLayout();
            this.splitDiagnostics.Panel2.SuspendLayout();
            this.splitDiagnostics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObdCodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabWorkOrders);
            this.tabControlMain.Controls.Add(this.tabClientsCars);
            this.tabControlMain.Controls.Add(this.tabDiagnostics);
            this.tabControlMain.Location = new System.Drawing.Point(12, 12);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(884, 456);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabWorkOrders
            // 
            this.tabWorkOrders.Controls.Add(this.btnExportSummary);
            this.tabWorkOrders.Controls.Add(this.btnDelete);
            this.tabWorkOrders.Controls.Add(this.btnEdit);
            this.tabWorkOrders.Controls.Add(this.btnAdd);
            this.tabWorkOrders.Controls.Add(this.dgvWorkOrders);
            this.tabWorkOrders.Location = new System.Drawing.Point(4, 33);
            this.tabWorkOrders.Name = "tabWorkOrders";
            this.tabWorkOrders.Padding = new System.Windows.Forms.Padding(3);
            this.tabWorkOrders.Size = new System.Drawing.Size(768, 331);
            this.tabWorkOrders.TabIndex = 0;
            this.tabWorkOrders.Text = "Work Orders";
            this.tabWorkOrders.UseVisualStyleBackColor = true;
            // 
            // tabClientsCars
            // 
            this.tabClientsCars.Controls.Add(this.splitContainer1);
            this.tabClientsCars.Location = new System.Drawing.Point(4, 33);
            this.tabClientsCars.Name = "tabClientsCars";
            this.tabClientsCars.Padding = new System.Windows.Forms.Padding(3);
            this.tabClientsCars.Size = new System.Drawing.Size(876, 419);
            this.tabClientsCars.TabIndex = 1;
            this.tabClientsCars.Text = "Clients & Cars";
            this.tabClientsCars.UseVisualStyleBackColor = true;
            // 
            // tabDiagnostics
            // 
            this.tabDiagnostics.Controls.Add(this.splitDiagnostics);
            this.tabDiagnostics.Controls.Add(this.label4);
            this.tabDiagnostics.Controls.Add(this.dgvTestResults);
            this.tabDiagnostics.Controls.Add(this.cmbCarForDiagnostics);
            this.tabDiagnostics.Location = new System.Drawing.Point(4, 33);
            this.tabDiagnostics.Name = "tabDiagnostics";
            this.tabDiagnostics.Size = new System.Drawing.Size(876, 419);
            this.tabDiagnostics.TabIndex = 2;
            this.tabDiagnostics.Text = "Diagnostics";
            this.tabDiagnostics.UseVisualStyleBackColor = true;
            // 
            // btnResetImport
            // 
            this.btnResetImport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnResetImport.Location = new System.Drawing.Point(0, 533);
            this.btnResetImport.Name = "btnResetImport";
            this.btnResetImport.Size = new System.Drawing.Size(924, 40);
            this.btnResetImport.TabIndex = 1;
            this.btnResetImport.Text = "Reset DB + Re-import JSON";
            this.btnResetImport.UseVisualStyleBackColor = true;
            this.btnResetImport.Click += new System.EventHandler(this.btnResetImport_Click);
            // 
            // dgvWorkOrders
            // 
            this.dgvWorkOrders.AllowUserToAddRows = false;
            this.dgvWorkOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWorkOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWorkOrders.Location = new System.Drawing.Point(3, 0);
            this.dgvWorkOrders.Name = "dgvWorkOrders";
            this.dgvWorkOrders.ReadOnly = true;
            this.dgvWorkOrders.RowHeadersWidth = 72;
            this.dgvWorkOrders.RowTemplate.Height = 31;
            this.dgvWorkOrders.Size = new System.Drawing.Size(762, 246);
            this.dgvWorkOrders.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(6, 285);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 40);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(112, 285);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 40);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(218, 285);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 40);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnExportSummary
            // 
            this.btnExportSummary.Location = new System.Drawing.Point(324, 285);
            this.btnExportSummary.Name = "btnExportSummary";
            this.btnExportSummary.Size = new System.Drawing.Size(200, 40);
            this.btnExportSummary.TabIndex = 4;
            this.btnExportSummary.Text = "Export Summary";
            this.btnExportSummary.UseVisualStyleBackColor = true;
            this.btnExportSummary.Click += new System.EventHandler(this.btnExportSummary_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.lbClients);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.btnFilterCars);
            this.splitContainer1.Panel2.Controls.Add(this.txtYearFilter);
            this.splitContainer1.Panel2.Controls.Add(this.cmbMakeFilter);
            this.splitContainer1.Panel2.Controls.Add(this.dgvCars);
            this.splitContainer1.Size = new System.Drawing.Size(870, 413);
            this.splitContainer1.SplitterDistance = 290;
            this.splitContainer1.TabIndex = 0;
            // 
            // lbClients
            // 
            this.lbClients.FormattingEnabled = true;
            this.lbClients.ItemHeight = 24;
            this.lbClients.Location = new System.Drawing.Point(3, 48);
            this.lbClients.Name = "lbClients";
            this.lbClients.Size = new System.Drawing.Size(248, 268);
            this.lbClients.TabIndex = 0;
            // 
            // dgvCars
            // 
            this.dgvCars.AllowUserToAddRows = false;
            this.dgvCars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCars.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvCars.Location = new System.Drawing.Point(0, 0);
            this.dgvCars.Name = "dgvCars";
            this.dgvCars.ReadOnly = true;
            this.dgvCars.RowHeadersWidth = 72;
            this.dgvCars.RowTemplate.Height = 31;
            this.dgvCars.Size = new System.Drawing.Size(576, 232);
            this.dgvCars.TabIndex = 0;
            // 
            // cmbMakeFilter
            // 
            this.cmbMakeFilter.FormattingEnabled = true;
            this.cmbMakeFilter.Location = new System.Drawing.Point(178, 252);
            this.cmbMakeFilter.Name = "cmbMakeFilter";
            this.cmbMakeFilter.Size = new System.Drawing.Size(300, 32);
            this.cmbMakeFilter.TabIndex = 1;
            // 
            // txtYearFilter
            // 
            this.txtYearFilter.Location = new System.Drawing.Point(178, 290);
            this.txtYearFilter.Name = "txtYearFilter";
            this.txtYearFilter.Size = new System.Drawing.Size(300, 29);
            this.txtYearFilter.TabIndex = 2;
            // 
            // btnFilterCars
            // 
            this.btnFilterCars.Location = new System.Drawing.Point(228, 347);
            this.btnFilterCars.Name = "btnFilterCars";
            this.btnFilterCars.Size = new System.Drawing.Size(150, 40);
            this.btnFilterCars.TabIndex = 3;
            this.btnFilterCars.Text = "Filter Cars";
            this.btnFilterCars.UseVisualStyleBackColor = true;
            // 
            // cmbCarForDiagnostics
            // 
            this.cmbCarForDiagnostics.FormattingEnabled = true;
            this.cmbCarForDiagnostics.Location = new System.Drawing.Point(327, 240);
            this.cmbCarForDiagnostics.Name = "cmbCarForDiagnostics";
            this.cmbCarForDiagnostics.Size = new System.Drawing.Size(300, 32);
            this.cmbCarForDiagnostics.TabIndex = 0;
            // 
            // dgvTestResults
            // 
            this.dgvTestResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTestResults.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvTestResults.Location = new System.Drawing.Point(0, 0);
            this.dgvTestResults.Name = "dgvTestResults";
            this.dgvTestResults.RowHeadersWidth = 72;
            this.dgvTestResults.RowTemplate.Height = 31;
            this.dgvTestResults.Size = new System.Drawing.Size(876, 197);
            this.dgvTestResults.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Clients";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 252);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Make:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(113, 290);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Year:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(211, 240);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 25);
            this.label4.TabIndex = 2;
            this.label4.Text = "Select Car:";
            // 
            // splitDiagnostics
            // 
            this.splitDiagnostics.Location = new System.Drawing.Point(47, 294);
            this.splitDiagnostics.Name = "splitDiagnostics";
            // 
            // splitDiagnostics.Panel1
            // 
            this.splitDiagnostics.Panel1.Controls.Add(this.dgvObdCodes);
            this.splitDiagnostics.Panel1.Controls.Add(this.label5);
            // 
            // splitDiagnostics.Panel2
            // 
            this.splitDiagnostics.Panel2.Controls.Add(this.dataGridView1);
            this.splitDiagnostics.Panel2.Controls.Add(this.label6);
            this.splitDiagnostics.Size = new System.Drawing.Size(788, 100);
            this.splitDiagnostics.SplitterDistance = 262;
            this.splitDiagnostics.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 25);
            this.label5.TabIndex = 0;
            this.label5.Text = "OBD Codes";
            // 
            // dgvObdCodes
            // 
            this.dgvObdCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvObdCodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvObdCodes.Location = new System.Drawing.Point(0, 25);
            this.dgvObdCodes.Name = "dgvObdCodes";
            this.dgvObdCodes.RowHeadersWidth = 72;
            this.dgvObdCodes.RowTemplate.Height = 31;
            this.dgvObdCodes.Size = new System.Drawing.Size(262, 75);
            this.dgvObdCodes.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(277, 25);
            this.label6.TabIndex = 0;
            this.label6.Text = "Test Results (Highlight on Fail)";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(142, 50);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 72;
            this.dataGridView1.RowTemplate.Height = 31;
            this.dataGridView1.Size = new System.Drawing.Size(240, 150);
            this.dataGridView1.TabIndex = 1;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 573);
            this.Controls.Add(this.btnResetImport);
            this.Controls.Add(this.tabControlMain);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.tabControlMain.ResumeLayout(false);
            this.tabWorkOrders.ResumeLayout(false);
            this.tabClientsCars.ResumeLayout(false);
            this.tabDiagnostics.ResumeLayout(false);
            this.tabDiagnostics.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkOrders)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCars)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestResults)).EndInit();
            this.splitDiagnostics.Panel1.ResumeLayout(false);
            this.splitDiagnostics.Panel1.PerformLayout();
            this.splitDiagnostics.Panel2.ResumeLayout(false);
            this.splitDiagnostics.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitDiagnostics)).EndInit();
            this.splitDiagnostics.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvObdCodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabWorkOrders;
        private System.Windows.Forms.TabPage tabClientsCars;
        private System.Windows.Forms.TabPage tabDiagnostics;
        private System.Windows.Forms.Button btnResetImport;
        private System.Windows.Forms.DataGridView dgvWorkOrders;
        private System.Windows.Forms.Button btnExportSummary;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lbClients;
        private System.Windows.Forms.Button btnFilterCars;
        private System.Windows.Forms.TextBox txtYearFilter;
        private System.Windows.Forms.ComboBox cmbMakeFilter;
        private System.Windows.Forms.DataGridView dgvCars;
        private System.Windows.Forms.DataGridView dgvTestResults;
        private System.Windows.Forms.ComboBox cmbCarForDiagnostics;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitDiagnostics;
        private System.Windows.Forms.DataGridView dgvObdCodes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
    }
}

