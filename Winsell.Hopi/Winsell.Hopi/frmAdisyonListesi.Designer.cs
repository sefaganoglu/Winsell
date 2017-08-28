namespace Winsell.Hopi
{
    partial class frmAdisyonListesi
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdisyonListesi));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvAdisyonlar = new System.Windows.Forms.DataGridView();
            this.dgvStoklar = new System.Windows.Forms.DataGridView();
            this.colStokAdi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMiktar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStokKodu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdisyonNoStok = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.tsbTamam = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbKapat = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.colAdisyonNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTutar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHopiVar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdisyonlar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStoklar)).BeginInit();
            this.tsMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvAdisyonlar
            // 
            this.dgvAdisyonlar.AllowUserToAddRows = false;
            this.dgvAdisyonlar.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dgvAdisyonlar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAdisyonlar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdisyonlar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAdisyonNo,
            this.colTutar,
            this.colHopiVar});
            this.dgvAdisyonlar.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvAdisyonlar.Location = new System.Drawing.Point(0, 74);
            this.dgvAdisyonlar.MultiSelect = false;
            this.dgvAdisyonlar.Name = "dgvAdisyonlar";
            this.dgvAdisyonlar.RowHeadersWidth = 24;
            this.dgvAdisyonlar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAdisyonlar.Size = new System.Drawing.Size(458, 140);
            this.dgvAdisyonlar.TabIndex = 1;
            this.dgvAdisyonlar.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAdisyonlar_CellEnter);
            this.dgvAdisyonlar.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvAdisyonlar_CellPainting);
            // 
            // dgvStoklar
            // 
            this.dgvStoklar.AllowUserToAddRows = false;
            this.dgvStoklar.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dgvStoklar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvStoklar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStoklar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStokAdi,
            this.colMiktar,
            this.dataGridViewTextBoxColumn1,
            this.colStokKodu,
            this.colAdisyonNoStok});
            this.dgvStoklar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStoklar.Location = new System.Drawing.Point(0, 234);
            this.dgvStoklar.MultiSelect = false;
            this.dgvStoklar.Name = "dgvStoklar";
            this.dgvStoklar.RowHeadersWidth = 24;
            this.dgvStoklar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStoklar.Size = new System.Drawing.Size(458, 173);
            this.dgvStoklar.TabIndex = 2;
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
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Tutar";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn1.HeaderText = "Tutar";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // colStokKodu
            // 
            this.colStokKodu.DataPropertyName = "STOKKODU";
            this.colStokKodu.HeaderText = "Stok Kodu";
            this.colStokKodu.Name = "colStokKodu";
            this.colStokKodu.ReadOnly = true;
            this.colStokKodu.Visible = false;
            // 
            // colAdisyonNoStok
            // 
            this.colAdisyonNoStok.DataPropertyName = "CEKNO";
            this.colAdisyonNoStok.HeaderText = "Adisyon No";
            this.colAdisyonNoStok.Name = "colAdisyonNoStok";
            this.colAdisyonNoStok.ReadOnly = true;
            this.colAdisyonNoStok.Visible = false;
            // 
            // tsMenu
            // 
            this.tsMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbTamam,
            this.toolStripSeparator1,
            this.tsbKapat});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Size = new System.Drawing.Size(458, 54);
            this.tsMenu.TabIndex = 23;
            this.tsMenu.Text = "toolStrip1";
            // 
            // tsbTamam
            // 
            this.tsbTamam.Image = ((System.Drawing.Image)(resources.GetObject("tsbTamam.Image")));
            this.tsbTamam.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTamam.Name = "tsbTamam";
            this.tsbTamam.Size = new System.Drawing.Size(51, 51);
            this.tsbTamam.Text = "Tamam";
            this.tsbTamam.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbTamam.Click += new System.EventHandler(this.tsbTamam_Click);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 214);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(458, 20);
            this.panel1.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(458, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "ADİSYONDAKİ ÜRÜNLER";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(0, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(458, 20);
            this.label2.TabIndex = 25;
            this.label2.Text = "MASADAKİ ADİSYONLAR";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // colAdisyonNo
            // 
            this.colAdisyonNo.DataPropertyName = "CEKNO";
            this.colAdisyonNo.HeaderText = "Adisyon No";
            this.colAdisyonNo.Name = "colAdisyonNo";
            this.colAdisyonNo.ReadOnly = true;
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
            // 
            // colHopiVar
            // 
            this.colHopiVar.DataPropertyName = "Hopi_Var";
            this.colHopiVar.HeaderText = "Hopi Var";
            this.colHopiVar.Name = "colHopiVar";
            this.colHopiVar.ReadOnly = true;
            this.colHopiVar.Visible = false;
            // 
            // frmAdisyonListesi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(458, 407);
            this.Controls.Add(this.dgvStoklar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvAdisyonlar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tsMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdisyonListesi";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adisyon Listesi";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAdisyonListesi_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdisyonlar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStoklar)).EndInit();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAdisyonlar;
        private System.Windows.Forms.DataGridView dgvStoklar;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton tsbTamam;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbKapat;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStokAdi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMiktar;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStokKodu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdisyonNoStok;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdisyonNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTutar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHopiVar;
    }
}