namespace Winsell.Hopi
{
    partial class frmAnaMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAnaMenu));
            this.flpMasalar = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlIslemler = new System.Windows.Forms.Panel();
            this.dgvStoklar = new System.Windows.Forms.DataGridView();
            this.colStokAdi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMiktar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTutar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.tsMasaIslemler = new System.Windows.Forms.ToolStrip();
            this.tsbHesapBol = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOdemeAl = new System.Windows.Forms.ToolStripButton();
            this.tsbHopiIade = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbKapatPanel = new System.Windows.Forms.ToolStripButton();
            this.lblMasaNo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.tsbYenile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbKullanici = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbKapat = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlKullanici = new System.Windows.Forms.Panel();
            this.lblKullanici = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.pnlIslemler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStoklar)).BeginInit();
            this.tsMasaIslemler.SuspendLayout();
            this.tsMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlKullanici.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // flpMasalar
            // 
            this.flpMasalar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpMasalar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMasalar.Location = new System.Drawing.Point(0, 120);
            this.flpMasalar.Name = "flpMasalar";
            this.flpMasalar.Size = new System.Drawing.Size(706, 583);
            this.flpMasalar.TabIndex = 17;
            // 
            // pnlIslemler
            // 
            this.pnlIslemler.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlIslemler.Controls.Add(this.dgvStoklar);
            this.pnlIslemler.Controls.Add(this.label3);
            this.pnlIslemler.Controls.Add(this.tsMasaIslemler);
            this.pnlIslemler.Controls.Add(this.lblMasaNo);
            this.pnlIslemler.Controls.Add(this.label2);
            this.pnlIslemler.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlIslemler.Location = new System.Drawing.Point(706, 120);
            this.pnlIslemler.Name = "pnlIslemler";
            this.pnlIslemler.Size = new System.Drawing.Size(298, 583);
            this.pnlIslemler.TabIndex = 19;
            this.pnlIslemler.Visible = false;
            // 
            // dgvStoklar
            // 
            this.dgvStoklar.AllowUserToAddRows = false;
            this.dgvStoklar.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dgvStoklar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvStoklar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStoklar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStokAdi,
            this.colMiktar,
            this.colTutar});
            this.dgvStoklar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStoklar.Location = new System.Drawing.Point(0, 289);
            this.dgvStoklar.MultiSelect = false;
            this.dgvStoklar.Name = "dgvStoklar";
            this.dgvStoklar.RowHeadersVisible = false;
            this.dgvStoklar.RowHeadersWidth = 24;
            this.dgvStoklar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStoklar.Size = new System.Drawing.Size(296, 292);
            this.dgvStoklar.TabIndex = 6;
            // 
            // colStokAdi
            // 
            this.colStokAdi.DataPropertyName = "STOKADI";
            this.colStokAdi.HeaderText = "Ürün Adı";
            this.colStokAdi.Name = "colStokAdi";
            this.colStokAdi.ReadOnly = true;
            this.colStokAdi.Width = 150;
            // 
            // colMiktar
            // 
            this.colMiktar.DataPropertyName = "MIKTAR_ACK";
            this.colMiktar.HeaderText = "Miktar";
            this.colMiktar.Name = "colMiktar";
            this.colMiktar.ReadOnly = true;
            this.colMiktar.Width = 60;
            // 
            // colTutar
            // 
            this.colTutar.DataPropertyName = "Tutar";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            this.colTutar.DefaultCellStyle = dataGridViewCellStyle2;
            this.colTutar.HeaderText = "Tutar";
            this.colTutar.Name = "colTutar";
            this.colTutar.ReadOnly = true;
            this.colTutar.Width = 60;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(0, 264);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(296, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "MASADAKİ ÜRÜNLER";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tsMasaIslemler
            // 
            this.tsMasaIslemler.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMasaIslemler.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsMasaIslemler.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbHesapBol,
            this.toolStripSeparator2,
            this.tsbOdemeAl,
            this.tsbHopiIade,
            this.toolStripSeparator3,
            this.tsbKapatPanel});
            this.tsMasaIslemler.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tsMasaIslemler.Location = new System.Drawing.Point(0, 75);
            this.tsMasaIslemler.Name = "tsMasaIslemler";
            this.tsMasaIslemler.Size = new System.Drawing.Size(296, 189);
            this.tsMasaIslemler.TabIndex = 5;
            this.tsMasaIslemler.Text = "toolStrip1";
            // 
            // tsbHesapBol
            // 
            this.tsbHesapBol.Image = ((System.Drawing.Image)(resources.GetObject("tsbHesapBol.Image")));
            this.tsbHesapBol.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbHesapBol.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHesapBol.Name = "tsbHesapBol";
            this.tsbHesapBol.Size = new System.Drawing.Size(294, 36);
            this.tsbHesapBol.Text = "Hesap Böl";
            this.tsbHesapBol.Click += new System.EventHandler(this.tsbHesapBol_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(294, 6);
            // 
            // tsbOdemeAl
            // 
            this.tsbOdemeAl.Image = ((System.Drawing.Image)(resources.GetObject("tsbOdemeAl.Image")));
            this.tsbOdemeAl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbOdemeAl.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOdemeAl.Name = "tsbOdemeAl";
            this.tsbOdemeAl.Size = new System.Drawing.Size(294, 36);
            this.tsbOdemeAl.Text = "Hopi Öde/Kazan";
            this.tsbOdemeAl.Click += new System.EventHandler(this.tsbOdemeAl_Click);
            // 
            // tsbHopiIade
            // 
            this.tsbHopiIade.Image = ((System.Drawing.Image)(resources.GetObject("tsbHopiIade.Image")));
            this.tsbHopiIade.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbHopiIade.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHopiIade.Name = "tsbHopiIade";
            this.tsbHopiIade.Size = new System.Drawing.Size(294, 36);
            this.tsbHopiIade.Text = "Hopi İade";
            this.tsbHopiIade.Click += new System.EventHandler(this.tsbHopiIade_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(294, 6);
            // 
            // tsbKapatPanel
            // 
            this.tsbKapatPanel.Image = ((System.Drawing.Image)(resources.GetObject("tsbKapatPanel.Image")));
            this.tsbKapatPanel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbKapatPanel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbKapatPanel.Name = "tsbKapatPanel";
            this.tsbKapatPanel.Size = new System.Drawing.Size(294, 36);
            this.tsbKapatPanel.Text = "Kapat";
            this.tsbKapatPanel.Click += new System.EventHandler(this.tsbKapatPanel_Click);
            // 
            // lblMasaNo
            // 
            this.lblMasaNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMasaNo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMasaNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblMasaNo.Location = new System.Drawing.Point(0, 25);
            this.lblMasaNo.Name = "lblMasaNo";
            this.lblMasaNo.Size = new System.Drawing.Size(296, 50);
            this.lblMasaNo.TabIndex = 4;
            this.lblMasaNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(296, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "MASA NO";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tsMenu
            // 
            this.tsMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbYenile,
            this.toolStripSeparator1,
            this.tsbKullanici,
            this.toolStripSeparator4,
            this.tsbKapat});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Size = new System.Drawing.Size(1004, 54);
            this.tsMenu.TabIndex = 21;
            this.tsMenu.Text = "toolStrip1";
            // 
            // tsbYenile
            // 
            this.tsbYenile.Image = ((System.Drawing.Image)(resources.GetObject("tsbYenile.Image")));
            this.tsbYenile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbYenile.Name = "tsbYenile";
            this.tsbYenile.Size = new System.Drawing.Size(42, 51);
            this.tsbYenile.Text = "Yenile";
            this.tsbYenile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbYenile.Click += new System.EventHandler(this.tsbYenile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbKullanici
            // 
            this.tsbKullanici.Image = ((System.Drawing.Image)(resources.GetObject("tsbKullanici.Image")));
            this.tsbKullanici.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbKullanici.Name = "tsbKullanici";
            this.tsbKullanici.Size = new System.Drawing.Size(56, 51);
            this.tsbKullanici.Text = "Kullanıcı";
            this.tsbKullanici.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbKullanici.Click += new System.EventHandler(this.tsbKullanici_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 54);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 703);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1004, 22);
            this.statusStrip1.TabIndex = 22;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pnlKullanici);
            this.panel1.Controls.Add(this.pbLogo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(2);
            this.panel1.Size = new System.Drawing.Size(1004, 66);
            this.panel1.TabIndex = 0;
            // 
            // pnlKullanici
            // 
            this.pnlKullanici.AutoSize = true;
            this.pnlKullanici.Controls.Add(this.lblKullanici);
            this.pnlKullanici.Controls.Add(this.label1);
            this.pnlKullanici.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlKullanici.Location = new System.Drawing.Point(924, 2);
            this.pnlKullanici.Name = "pnlKullanici";
            this.pnlKullanici.Padding = new System.Windows.Forms.Padding(0, 20, 0, 20);
            this.pnlKullanici.Size = new System.Drawing.Size(76, 60);
            this.pnlKullanici.TabIndex = 5;
            // 
            // lblKullanici
            // 
            this.lblKullanici.AutoSize = true;
            this.lblKullanici.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKullanici.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblKullanici.Location = new System.Drawing.Point(76, 20);
            this.lblKullanici.Name = "lblKullanici";
            this.lblKullanici.Size = new System.Drawing.Size(0, 18);
            this.lblKullanici.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(0, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Kullanıcı:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbLogo
            // 
            this.pbLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.Location = new System.Drawing.Point(2, 2);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(213, 60);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLogo.TabIndex = 2;
            this.pbLogo.TabStop = false;
            // 
            // frmAnaMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1004, 725);
            this.Controls.Add(this.flpMasalar);
            this.Controls.Add(this.pnlIslemler);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tsMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmAnaMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ana Menü";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAnaMenu_Load);
            this.pnlIslemler.ResumeLayout(false);
            this.pnlIslemler.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStoklar)).EndInit();
            this.tsMasaIslemler.ResumeLayout(false);
            this.tsMasaIslemler.PerformLayout();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlKullanici.ResumeLayout(false);
            this.pnlKullanici.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpMasalar;
        private System.Windows.Forms.Panel pnlIslemler;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton tsbYenile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbKapat;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lblKullanici;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlKullanici;
        private System.Windows.Forms.Label lblMasaNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip tsMasaIslemler;
        private System.Windows.Forms.ToolStripButton tsbHesapBol;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbOdemeAl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbKapatPanel;
        private System.Windows.Forms.ToolStripButton tsbKullanici;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.DataGridView dgvStoklar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStokAdi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMiktar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTutar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripButton tsbHopiIade;
    }
}