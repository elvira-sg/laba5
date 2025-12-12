namespace BankSystem
{
    partial class BankManagerForm
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.panel = new System.Windows.Forms.FlowLayoutPanel();
            this.chkUseDatabase = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnExportDB = new System.Windows.Forms.Button();
            this.btnImportDB = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(900, 350);
            this.dataGridView.TabIndex = 0;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.chkUseDatabase);
            this.panel.Controls.Add(this.btnAdd);
            this.panel.Controls.Add(this.btnEdit);
            this.panel.Controls.Add(this.btnDelete);
            this.panel.Controls.Add(this.btnSave);
            this.panel.Controls.Add(this.btnLoad);
            this.panel.Controls.Add(this.btnSort);
            this.panel.Controls.Add(this.btnExportDB);
            this.panel.Controls.Add(this.btnImportDB);
            this.panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.panel.Location = new System.Drawing.Point(0, 520);
            this.panel.Name = "panel";
            this.panel.Padding = new System.Windows.Forms.Padding(10);
            this.panel.Size = new System.Drawing.Size(900, 80);
            this.panel.TabIndex = 1;
            // 
            // chkUseDatabase
            // 
            this.chkUseDatabase.AutoSize = true;
            this.chkUseDatabase.Checked = true;
            this.chkUseDatabase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseDatabase.Location = new System.Drawing.Point(13, 13);
            this.chkUseDatabase.Name = "chkUseDatabase";
            this.chkUseDatabase.Size = new System.Drawing.Size(110, 17);
            this.chkUseDatabase.TabIndex = 0;
            this.chkUseDatabase.Text = "Использовать БД";
            this.chkUseDatabase.UseVisualStyleBackColor = true;
            this.chkUseDatabase.CheckedChanged += new System.EventHandler(this.ChkUseDatabase_CheckedChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(129, 13);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 25);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.OnAddClick);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(235, 13);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 25);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Изменить";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.OnEditClick);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(341, 13);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 25);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.OnDeleteClick);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(447, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 25);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Экспорт в файл";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.OnSaveClick);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(573, 13);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(120, 25);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "Импорт из файла";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.OnLoadClick);
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(699, 13);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(140, 25);
            this.btnSort.TabIndex = 6;
            this.btnSort.Text = "Сортировка по балансу";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.OnSortClick);
            // 
            // btnExportDB
            // 
            this.btnExportDB.Location = new System.Drawing.Point(13, 44);
            this.btnExportDB.Name = "btnExportDB";
            this.btnExportDB.Size = new System.Drawing.Size(140, 25);
            this.btnExportDB.TabIndex = 7;
            this.btnExportDB.Text = "Экспорт БД в файл";
            this.btnExportDB.UseVisualStyleBackColor = true;
            this.btnExportDB.Click += new System.EventHandler(this.OnExportDBClick);
            // 
            // btnImportDB
            // 
            this.btnImportDB.Location = new System.Drawing.Point(159, 44);
            this.btnImportDB.Name = "btnImportDB";
            this.btnImportDB.Size = new System.Drawing.Size(160, 25);
            this.btnImportDB.TabIndex = 8;
            this.btnImportDB.Text = "Импорт в БД из файла";
            this.btnImportDB.UseVisualStyleBackColor = true;
            this.btnImportDB.Click += new System.EventHandler(this.OnImportDBClick);
            // 
            // BankManagerForm
            // 
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.dataGridView);
            this.Name = "BankManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Банковская система (C#)";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.FlowLayoutPanel panel;
        private System.Windows.Forms.CheckBox chkUseDatabase;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.Button btnExportDB;
        private System.Windows.Forms.Button btnImportDB;
    }
}