namespace Winsell.YK.Ingenico
{
    partial class frmRaporlar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRaporlar));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnZRaporu = new System.Windows.Forms.Button();
            this.btnXRaporu = new System.Windows.Forms.Button();
            this.btnMaliRapor = new System.Windows.Forms.Button();
            this.btnMaliKumulatifRapor = new System.Windows.Forms.Button();
            this.btnKapat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtZNoBaslangic = new System.Windows.Forms.TextBox();
            this.txtZNoBitis = new System.Windows.Forms.TextBox();
            this.txtFisNoBitis = new System.Windows.Forms.TextBox();
            this.txtFisNoBaslangic = new System.Windows.Forms.TextBox();
            this.txtEkuNo = new System.Windows.Forms.TextBox();
            this.dtpTarihBaslangic = new System.Windows.Forms.DateTimePicker();
            this.dtpTarihBitis = new System.Windows.Forms.DateTimePicker();
            this.cbTarihliIslem = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 250);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(410, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnKapat);
            this.panel1.Controls.Add(this.btnMaliKumulatifRapor);
            this.panel1.Controls.Add(this.btnMaliRapor);
            this.panel1.Controls.Add(this.btnXRaporu);
            this.panel1.Controls.Add(this.btnZRaporu);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(189, 250);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbTarihliIslem);
            this.panel2.Controls.Add(this.dtpTarihBitis);
            this.panel2.Controls.Add(this.dtpTarihBaslangic);
            this.panel2.Controls.Add(this.txtEkuNo);
            this.panel2.Controls.Add(this.txtFisNoBitis);
            this.panel2.Controls.Add(this.txtFisNoBaslangic);
            this.panel2.Controls.Add(this.txtZNoBitis);
            this.panel2.Controls.Add(this.txtZNoBaslangic);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(189, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(221, 250);
            this.panel2.TabIndex = 0;
            // 
            // btnZRaporu
            // 
            this.btnZRaporu.AccessibleName = "2";
            this.btnZRaporu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZRaporu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnZRaporu.Location = new System.Drawing.Point(0, 0);
            this.btnZRaporu.Name = "btnZRaporu";
            this.btnZRaporu.Size = new System.Drawing.Size(189, 50);
            this.btnZRaporu.TabIndex = 0;
            this.btnZRaporu.Text = "Z Raporu";
            this.btnZRaporu.UseVisualStyleBackColor = true;
            this.btnZRaporu.Click += new System.EventHandler(this.btnZRaporu_Click);
            // 
            // btnXRaporu
            // 
            this.btnXRaporu.AccessibleName = "3";
            this.btnXRaporu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXRaporu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnXRaporu.Location = new System.Drawing.Point(0, 50);
            this.btnXRaporu.Name = "btnXRaporu";
            this.btnXRaporu.Size = new System.Drawing.Size(189, 50);
            this.btnXRaporu.TabIndex = 1;
            this.btnXRaporu.Text = "X Raporu";
            this.btnXRaporu.UseVisualStyleBackColor = true;
            this.btnXRaporu.Click += new System.EventHandler(this.btnZRaporu_Click);
            // 
            // btnMaliRapor
            // 
            this.btnMaliRapor.AccessibleName = "4";
            this.btnMaliRapor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaliRapor.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMaliRapor.Location = new System.Drawing.Point(0, 100);
            this.btnMaliRapor.Name = "btnMaliRapor";
            this.btnMaliRapor.Size = new System.Drawing.Size(189, 50);
            this.btnMaliRapor.TabIndex = 2;
            this.btnMaliRapor.Text = "Mali Rapor";
            this.btnMaliRapor.UseVisualStyleBackColor = true;
            this.btnMaliRapor.Click += new System.EventHandler(this.btnZRaporu_Click);
            // 
            // btnMaliKumulatifRapor
            // 
            this.btnMaliKumulatifRapor.AccessibleName = "6";
            this.btnMaliKumulatifRapor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaliKumulatifRapor.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMaliKumulatifRapor.Location = new System.Drawing.Point(0, 150);
            this.btnMaliKumulatifRapor.Name = "btnMaliKumulatifRapor";
            this.btnMaliKumulatifRapor.Size = new System.Drawing.Size(189, 50);
            this.btnMaliKumulatifRapor.TabIndex = 3;
            this.btnMaliKumulatifRapor.Text = "Mali Kümülatif Rapor";
            this.btnMaliKumulatifRapor.UseVisualStyleBackColor = true;
            this.btnMaliKumulatifRapor.Click += new System.EventHandler(this.btnZRaporu_Click);
            // 
            // btnKapat
            // 
            this.btnKapat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKapat.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnKapat.Location = new System.Drawing.Point(0, 200);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(189, 50);
            this.btnKapat.TabIndex = 4;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.UseVisualStyleBackColor = true;
            this.btnKapat.Click += new System.EventHandler(this.btnKapat_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Z No Başlangıç:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Z No Bitiş:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Fiş No Bitiş:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Fiş No Başlangıç:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Ekü No:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(42, 225);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Tarih Bitiş:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 199);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Tarih Başlangıç:";
            // 
            // txtZNoBaslangic
            // 
            this.txtZNoBaslangic.Location = new System.Drawing.Point(97, 12);
            this.txtZNoBaslangic.Name = "txtZNoBaslangic";
            this.txtZNoBaslangic.Size = new System.Drawing.Size(112, 20);
            this.txtZNoBaslangic.TabIndex = 7;
            // 
            // txtZNoBitis
            // 
            this.txtZNoBitis.Location = new System.Drawing.Point(97, 38);
            this.txtZNoBitis.Name = "txtZNoBitis";
            this.txtZNoBitis.Size = new System.Drawing.Size(112, 20);
            this.txtZNoBitis.TabIndex = 8;
            // 
            // txtFisNoBitis
            // 
            this.txtFisNoBitis.Location = new System.Drawing.Point(97, 100);
            this.txtFisNoBitis.Name = "txtFisNoBitis";
            this.txtFisNoBitis.Size = new System.Drawing.Size(112, 20);
            this.txtFisNoBitis.TabIndex = 10;
            // 
            // txtFisNoBaslangic
            // 
            this.txtFisNoBaslangic.Location = new System.Drawing.Point(97, 74);
            this.txtFisNoBaslangic.Name = "txtFisNoBaslangic";
            this.txtFisNoBaslangic.Size = new System.Drawing.Size(112, 20);
            this.txtFisNoBaslangic.TabIndex = 9;
            // 
            // txtEkuNo
            // 
            this.txtEkuNo.Location = new System.Drawing.Point(97, 136);
            this.txtEkuNo.Name = "txtEkuNo";
            this.txtEkuNo.Size = new System.Drawing.Size(112, 20);
            this.txtEkuNo.TabIndex = 11;
            // 
            // dtpTarihBaslangic
            // 
            this.dtpTarihBaslangic.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTarihBaslangic.Location = new System.Drawing.Point(97, 195);
            this.dtpTarihBaslangic.Name = "dtpTarihBaslangic";
            this.dtpTarihBaslangic.Size = new System.Drawing.Size(112, 20);
            this.dtpTarihBaslangic.TabIndex = 12;
            // 
            // dtpTarihBitis
            // 
            this.dtpTarihBitis.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTarihBitis.Location = new System.Drawing.Point(97, 221);
            this.dtpTarihBitis.Name = "dtpTarihBitis";
            this.dtpTarihBitis.Size = new System.Drawing.Size(112, 20);
            this.dtpTarihBitis.TabIndex = 13;
            // 
            // cbTarihliIslem
            // 
            this.cbTarihliIslem.AutoSize = true;
            this.cbTarihliIslem.Location = new System.Drawing.Point(97, 172);
            this.cbTarihliIslem.Name = "cbTarihliIslem";
            this.cbTarihliIslem.Size = new System.Drawing.Size(81, 17);
            this.cbTarihliIslem.TabIndex = 14;
            this.cbTarihliIslem.Text = "Tarihli İşlem";
            this.cbTarihliIslem.UseVisualStyleBackColor = true;
            // 
            // frmRaporlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 272);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRaporlar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Raporlar";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnKapat;
        private System.Windows.Forms.Button btnMaliKumulatifRapor;
        private System.Windows.Forms.Button btnMaliRapor;
        private System.Windows.Forms.Button btnXRaporu;
        private System.Windows.Forms.Button btnZRaporu;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbTarihliIslem;
        private System.Windows.Forms.DateTimePicker dtpTarihBitis;
        private System.Windows.Forms.DateTimePicker dtpTarihBaslangic;
        private System.Windows.Forms.TextBox txtEkuNo;
        private System.Windows.Forms.TextBox txtFisNoBitis;
        private System.Windows.Forms.TextBox txtFisNoBaslangic;
        private System.Windows.Forms.TextBox txtZNoBitis;
        private System.Windows.Forms.TextBox txtZNoBaslangic;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}