namespace Winsell.Hopi
{
    partial class frmHesapBol
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHesapBol));
            this.dgvStoklar = new System.Windows.Forms.DataGridView();
            this.colSec = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colStokAdi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMiktar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMiktarDegisim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTutar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStokKodu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colXKod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMiktarGercek = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.tsbHesabiBol = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbKapat = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.btnSil = new System.Windows.Forms.Button();
            this.btn0 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn9 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.btn7 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlAltToplam = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblToplamTutar = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStoklar)).BeginInit();
            this.tsMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlAltToplam.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvStoklar
            // 
            this.dgvStoklar.AllowUserToAddRows = false;
            this.dgvStoklar.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dgvStoklar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvStoklar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStoklar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSec,
            this.colStokAdi,
            this.colMiktar,
            this.colMiktarDegisim,
            this.colTutar,
            this.colStokKodu,
            this.colXKod,
            this.colMiktarGercek});
            this.dgvStoklar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStoklar.Location = new System.Drawing.Point(0, 54);
            this.dgvStoklar.MultiSelect = false;
            this.dgvStoklar.Name = "dgvStoklar";
            this.dgvStoklar.RowHeadersWidth = 24;
            this.dgvStoklar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStoklar.Size = new System.Drawing.Size(587, 249);
            this.dgvStoklar.TabIndex = 0;
            this.dgvStoklar.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStoklar_CellClick);
            this.dgvStoklar.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStoklar_CellValueChanged);
            // 
            // colSec
            // 
            this.colSec.DataPropertyName = "Sec";
            this.colSec.FalseValue = "0";
            this.colSec.HeaderText = "";
            this.colSec.Name = "colSec";
            this.colSec.TrueValue = "1";
            this.colSec.Width = 30;
            // 
            // colStokAdi
            // 
            this.colStokAdi.DataPropertyName = "STOKADI";
            this.colStokAdi.HeaderText = "Stok Adı";
            this.colStokAdi.Name = "colStokAdi";
            this.colStokAdi.ReadOnly = true;
            this.colStokAdi.Width = 200;
            // 
            // colMiktar
            // 
            this.colMiktar.DataPropertyName = "MIKTAR_ACK";
            this.colMiktar.HeaderText = "Miktar";
            this.colMiktar.Name = "colMiktar";
            this.colMiktar.ReadOnly = true;
            // 
            // colMiktarDegisim
            // 
            this.colMiktarDegisim.DataPropertyName = "MIKTAR_DEG";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            this.colMiktarDegisim.DefaultCellStyle = dataGridViewCellStyle2;
            this.colMiktarDegisim.HeaderText = "Adet";
            this.colMiktarDegisim.Name = "colMiktarDegisim";
            // 
            // colTutar
            // 
            this.colTutar.DataPropertyName = "Tutar";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.colTutar.DefaultCellStyle = dataGridViewCellStyle3;
            this.colTutar.HeaderText = "Tutar";
            this.colTutar.Name = "colTutar";
            this.colTutar.ReadOnly = true;
            // 
            // colStokKodu
            // 
            this.colStokKodu.DataPropertyName = "STOKKODU";
            this.colStokKodu.HeaderText = "Stok Kodu";
            this.colStokKodu.Name = "colStokKodu";
            this.colStokKodu.ReadOnly = true;
            this.colStokKodu.Visible = false;
            // 
            // colXKod
            // 
            this.colXKod.DataPropertyName = "X";
            this.colXKod.HeaderText = "X Kod";
            this.colXKod.Name = "colXKod";
            this.colXKod.ReadOnly = true;
            this.colXKod.Visible = false;
            // 
            // colMiktarGercek
            // 
            this.colMiktarGercek.DataPropertyName = "MIKTAR";
            this.colMiktarGercek.HeaderText = "Miktar";
            this.colMiktarGercek.Name = "colMiktarGercek";
            this.colMiktarGercek.ReadOnly = true;
            this.colMiktarGercek.Visible = false;
            // 
            // tsMenu
            // 
            this.tsMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbHesabiBol,
            this.toolStripSeparator1,
            this.tsbKapat});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Size = new System.Drawing.Size(831, 54);
            this.tsMenu.TabIndex = 22;
            this.tsMenu.Text = "toolStrip1";
            // 
            // tsbHesabiBol
            // 
            this.tsbHesabiBol.Image = ((System.Drawing.Image)(resources.GetObject("tsbHesabiBol.Image")));
            this.tsbHesabiBol.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHesabiBol.Name = "tsbHesabiBol";
            this.tsbHesabiBol.Size = new System.Drawing.Size(67, 51);
            this.tsbHesabiBol.Text = "Hesabı Böl";
            this.tsbHesabiBol.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbHesabiBol.Click += new System.EventHandler(this.tsbHesabiBol_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbKapat
            // 
            this.tsbKapat.Image = ((System.Drawing.Image)(resources.GetObject("tsbKapat.Image")));
            this.tsbKapat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbKapat.Name = "tsbKapat";
            this.tsbKapat.Size = new System.Drawing.Size(41, 51);
            this.tsbKapat.Text = "Kapat";
            this.tsbKapat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbKapat.Click += new System.EventHandler(this.tsbKapat_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 321);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(831, 22);
            this.statusStrip1.TabIndex = 23;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btnSil
            // 
            this.btnSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSil.Location = new System.Drawing.Point(3, 204);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(75, 61);
            this.btnSil.TabIndex = 34;
            this.btnSil.Text = "Sil";
            this.btnSil.UseVisualStyleBackColor = true;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // btn0
            // 
            this.btn0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn0.Location = new System.Drawing.Point(84, 204);
            this.btn0.Name = "btn0";
            this.btn0.Size = new System.Drawing.Size(156, 61);
            this.btn0.TabIndex = 33;
            this.btn0.Text = "0";
            this.btn0.UseVisualStyleBackColor = true;
            this.btn0.Click += new System.EventHandler(this.btn7_Click);
            // 
            // btn3
            // 
            this.btn3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn3.Location = new System.Drawing.Point(165, 137);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(75, 61);
            this.btn3.TabIndex = 32;
            this.btn3.Text = "3";
            this.btn3.UseVisualStyleBackColor = true;
            this.btn3.Click += new System.EventHandler(this.btn7_Click);
            // 
            // btn2
            // 
            this.btn2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn2.Location = new System.Drawing.Point(84, 137);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(75, 61);
            this.btn2.TabIndex = 31;
            this.btn2.Text = "2";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.btn7_Click);
            // 
            // btn1
            // 
            this.btn1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn1.Location = new System.Drawing.Point(3, 137);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(75, 61);
            this.btn1.TabIndex = 30;
            this.btn1.Text = "1";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn7_Click);
            // 
            // btn6
            // 
            this.btn6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn6.Location = new System.Drawing.Point(165, 70);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(75, 61);
            this.btn6.TabIndex = 29;
            this.btn6.Text = "6";
            this.btn6.UseVisualStyleBackColor = true;
            this.btn6.Click += new System.EventHandler(this.btn7_Click);
            // 
            // btn5
            // 
            this.btn5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn5.Location = new System.Drawing.Point(84, 70);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(75, 61);
            this.btn5.TabIndex = 28;
            this.btn5.Text = "5";
            this.btn5.UseVisualStyleBackColor = true;
            this.btn5.Click += new System.EventHandler(this.btn7_Click);
            // 
            // btn4
            // 
            this.btn4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn4.Location = new System.Drawing.Point(3, 70);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(75, 61);
            this.btn4.TabIndex = 27;
            this.btn4.Text = "4";
            this.btn4.UseVisualStyleBackColor = true;
            this.btn4.Click += new System.EventHandler(this.btn7_Click);
            // 
            // btn9
            // 
            this.btn9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn9.Location = new System.Drawing.Point(165, 3);
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(75, 61);
            this.btn9.TabIndex = 26;
            this.btn9.Text = "9";
            this.btn9.UseVisualStyleBackColor = true;
            this.btn9.Click += new System.EventHandler(this.btn7_Click);
            // 
            // btn8
            // 
            this.btn8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn8.Location = new System.Drawing.Point(84, 3);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(75, 61);
            this.btn8.TabIndex = 25;
            this.btn8.Text = "8";
            this.btn8.UseVisualStyleBackColor = true;
            this.btn8.Click += new System.EventHandler(this.btn7_Click);
            // 
            // btn7
            // 
            this.btn7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn7.Location = new System.Drawing.Point(3, 3);
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(75, 61);
            this.btn7.TabIndex = 24;
            this.btn7.Text = "7";
            this.btn7.UseVisualStyleBackColor = true;
            this.btn7.Click += new System.EventHandler(this.btn7_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn7);
            this.panel1.Controls.Add(this.btnSil);
            this.panel1.Controls.Add(this.btn8);
            this.panel1.Controls.Add(this.btn0);
            this.panel1.Controls.Add(this.btn9);
            this.panel1.Controls.Add(this.btn3);
            this.panel1.Controls.Add(this.btn4);
            this.panel1.Controls.Add(this.btn2);
            this.panel1.Controls.Add(this.btn5);
            this.panel1.Controls.Add(this.btn1);
            this.panel1.Controls.Add(this.btn6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(587, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 267);
            this.panel1.TabIndex = 35;
            // 
            // pnlAltToplam
            // 
            this.pnlAltToplam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAltToplam.Controls.Add(this.label1);
            this.pnlAltToplam.Controls.Add(this.lblToplamTutar);
            this.pnlAltToplam.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAltToplam.Location = new System.Drawing.Point(0, 303);
            this.pnlAltToplam.Name = "pnlAltToplam";
            this.pnlAltToplam.Size = new System.Drawing.Size(587, 18);
            this.pnlAltToplam.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(499, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Toplam Tutar:";
            // 
            // lblToplamTutar
            // 
            this.lblToplamTutar.AutoSize = true;
            this.lblToplamTutar.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblToplamTutar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblToplamTutar.Location = new System.Drawing.Point(585, 0);
            this.lblToplamTutar.Name = "lblToplamTutar";
            this.lblToplamTutar.Size = new System.Drawing.Size(0, 13);
            this.lblToplamTutar.TabIndex = 1;
            // 
            // frmHesapBol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(831, 343);
            this.Controls.Add(this.dgvStoklar);
            this.Controls.Add(this.pnlAltToplam);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tsMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHesapBol";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hesap Bölme";
            ((System.ComponentModel.ISupportInitialize)(this.dgvStoklar)).EndInit();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlAltToplam.ResumeLayout(false);
            this.pnlAltToplam.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvStoklar;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton tsbHesabiBol;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbKapat;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSec;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStokAdi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMiktar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMiktarDegisim;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTutar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStokKodu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colXKod;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMiktarGercek;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Button btn0;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Button btn8;
        private System.Windows.Forms.Button btn7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlAltToplam;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblToplamTutar;
    }
}