namespace Przedszkole
{
    partial class MainForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabData = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.calendar = new System.Windows.Forms.MonthCalendar();
            this.tabRejestr = new System.Windows.Forms.TabPage();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.labelSelectedDate = new System.Windows.Forms.Label();
            this.dgvRegister = new System.Windows.Forms.DataGridView();
            this.tabUczniowie = new System.Windows.Forms.TabPage();
            this.buttonDodaj = new System.Windows.Forms.Button();
            this.textBoxDodaj = new System.Windows.Forms.TextBox();
            this.dgvPupils = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabData.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabRejestr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegister)).BeginInit();
            this.tabUczniowie.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPupils)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabData);
            this.tabControl1.Controls.Add(this.tabRejestr);
            this.tabControl1.Controls.Add(this.tabUczniowie);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 491);
            this.tabControl1.TabIndex = 0;
            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.tableLayoutPanel1);
            this.tabData.Location = new System.Drawing.Point(4, 25);
            this.tabData.Name = "tabData";
            this.tabData.Padding = new System.Windows.Forms.Padding(3);
            this.tabData.Size = new System.Drawing.Size(792, 462);
            this.tabData.TabIndex = 0;
            this.tabData.Text = "Data";
            this.tabData.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.calendar, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(786, 456);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // calendar
            // 
            this.calendar.CalendarDimensions = new System.Drawing.Size(2, 2);
            this.calendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calendar.Location = new System.Drawing.Point(9, 9);
            this.calendar.MinimumSize = new System.Drawing.Size(600, 400);
            this.calendar.Name = "calendar";
            this.calendar.TabIndex = 0;
            this.calendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.onDateSelected);
            // 
            // tabRejestr
            // 
            this.tabRejestr.Controls.Add(this.buttonRefresh);
            this.tabRejestr.Controls.Add(this.labelSelectedDate);
            this.tabRejestr.Controls.Add(this.dgvRegister);
            this.tabRejestr.Location = new System.Drawing.Point(4, 25);
            this.tabRejestr.Name = "tabRejestr";
            this.tabRejestr.Padding = new System.Windows.Forms.Padding(3);
            this.tabRejestr.Size = new System.Drawing.Size(792, 462);
            this.tabRejestr.TabIndex = 3;
            this.tabRejestr.Text = "Rejestr";
            this.tabRejestr.UseVisualStyleBackColor = true;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(641, 10);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(120, 23);
            this.buttonRefresh.TabIndex = 3;
            this.buttonRefresh.Text = "Odswiez";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.reloadReg);
            // 
            // labelSelectedDate
            // 
            this.labelSelectedDate.AutoSize = true;
            this.labelSelectedDate.Location = new System.Drawing.Point(8, 17);
            this.labelSelectedDate.Name = "labelSelectedDate";
            this.labelSelectedDate.Size = new System.Drawing.Size(0, 17);
            this.labelSelectedDate.TabIndex = 2;
            // 
            // dgvRegister
            // 
            this.dgvRegister.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRegister.Location = new System.Drawing.Point(3, 54);
            this.dgvRegister.Name = "dgvRegister";
            this.dgvRegister.RowTemplate.Height = 24;
            this.dgvRegister.Size = new System.Drawing.Size(781, 405);
            this.dgvRegister.TabIndex = 1;
            this.dgvRegister.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.onCellEdit);
            // 
            // tabUczniowie
            // 
            this.tabUczniowie.Controls.Add(this.buttonDodaj);
            this.tabUczniowie.Controls.Add(this.textBoxDodaj);
            this.tabUczniowie.Controls.Add(this.dgvPupils);
            this.tabUczniowie.Location = new System.Drawing.Point(4, 25);
            this.tabUczniowie.Name = "tabUczniowie";
            this.tabUczniowie.Size = new System.Drawing.Size(792, 462);
            this.tabUczniowie.TabIndex = 2;
            this.tabUczniowie.Text = "Uczniowie";
            this.tabUczniowie.UseVisualStyleBackColor = true;
            // 
            // buttonDodaj
            // 
            this.buttonDodaj.Location = new System.Drawing.Point(113, 4);
            this.buttonDodaj.Name = "buttonDodaj";
            this.buttonDodaj.Size = new System.Drawing.Size(75, 23);
            this.buttonDodaj.TabIndex = 2;
            this.buttonDodaj.Text = "Dodaj";
            this.buttonDodaj.UseVisualStyleBackColor = true;
            this.buttonDodaj.Click += new System.EventHandler(this.addPupil);
            // 
            // textBoxDodaj
            // 
            this.textBoxDodaj.Location = new System.Drawing.Point(9, 4);
            this.textBoxDodaj.Name = "textBoxDodaj";
            this.textBoxDodaj.Size = new System.Drawing.Size(98, 22);
            this.textBoxDodaj.TabIndex = 1;
            // 
            // dgvPupils
            // 
            this.dgvPupils.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPupils.Location = new System.Drawing.Point(53, 57);
            this.dgvPupils.Name = "dgvPupils";
            this.dgvPupils.RowTemplate.Height = 24;
            this.dgvPupils.Size = new System.Drawing.Size(682, 397);
            this.dgvPupils.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 491);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "Przedszkole";
            this.Load += new System.EventHandler(this.MainFormOnLoad);
            this.tabControl1.ResumeLayout(false);
            this.tabData.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabRejestr.ResumeLayout(false);
            this.tabRejestr.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegister)).EndInit();
            this.tabUczniowie.ResumeLayout(false);
            this.tabUczniowie.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPupils)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.MonthCalendar calendar;
        private System.Windows.Forms.TabPage tabUczniowie;
        private System.Windows.Forms.DataGridView dgvPupils;
        private System.Windows.Forms.Button buttonDodaj;
        private System.Windows.Forms.TextBox textBoxDodaj;
        public System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabRejestr;
        private System.Windows.Forms.DataGridView dgvRegister;
        private System.Windows.Forms.Label labelSelectedDate;
        private System.Windows.Forms.Button buttonRefresh;
    }
}

