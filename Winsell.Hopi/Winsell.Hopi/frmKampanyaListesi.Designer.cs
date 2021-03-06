﻿namespace Winsell.Hopi
{
    partial class frmKampanyaListesi
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKampanyaListesi));
            this.dgvKampanyalar = new System.Windows.Forms.DataGridView();
            this.colSec = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colKampanya = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAciklama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKazancOrani = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKazancMiktari = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKatMiktari = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIndirimOrani = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaxHopiKat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaxKazancPara = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMiktar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFiyatsalSinir = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.tsbTamam = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbKapat = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtParacikKusurat = new System.Windows.Forms.TextBox();
            this.lblAdisyonTutari = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblKullanilabilirLimit = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtParacik = new System.Windows.Forms.TextBox();
            this.btn7 = new System.Windows.Forms.Button();
            this.btnSil = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.btn0 = new System.Windows.Forms.Button();
            this.btn9 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKampanyalar)).BeginInit();
            this.tsMenu.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvKampanyalar
            // 
            this.dgvKampanyalar.AllowUserToAddRows = false;
            this.dgvKampanyalar.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dgvKampanyalar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvKampanyalar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKampanyalar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSec,
            this.colKampanya,
            this.colAciklama,
            this.colAdet,
            this.colKazancOrani,
            this.colKazancMiktari,
            this.colKatMiktari,
            this.colIndirimOrani,
            this.colMaxHopiKat,
            this.colMaxKazancPara,
            this.colMiktar,
            this.colFiyatsalSinir});
            this.dgvKampanyalar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKampanyalar.Location = new System.Drawing.Point(0, 54);
            this.dgvKampanyalar.MultiSelect = false;
            this.dgvKampanyalar.Name = "dgvKampanyalar";
            this.dgvKampanyalar.RowHeadersWidth = 24;
            this.dgvKampanyalar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKampanyalar.Size = new System.Drawing.Size(463, 382);
            this.dgvKampanyalar.TabIndex = 24;
            this.dgvKampanyalar.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKampanyalar_CellClick);
            this.dgvKampanyalar.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvKampanyalar_CellPainting);
            // 
            // colSec
            // 
            this.colSec.DataPropertyName = "Sec";
            this.colSec.FalseValue = "0";
            this.colSec.HeaderText = "";
            this.colSec.Name = "colSec";
            this.colSec.ReadOnly = true;
            this.colSec.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSec.TrueValue = "1";
            this.colSec.Width = 30;
            // 
            // colKampanya
            // 
            this.colKampanya.DataPropertyName = "KOD";
            this.colKampanya.HeaderText = "Kampanya";
            this.colKampanya.Name = "colKampanya";
            this.colKampanya.ReadOnly = true;
            this.colKampanya.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colKampanya.Width = 75;
            // 
            // colAciklama
            // 
            this.colAciklama.DataPropertyName = "ACIKLAMA";
            this.colAciklama.HeaderText = "Açıklama";
            this.colAciklama.Name = "colAciklama";
            this.colAciklama.ReadOnly = true;
            this.colAciklama.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAciklama.Width = 300;
            // 
            // colAdet
            // 
            this.colAdet.DataPropertyName = "ADET";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            this.colAdet.DefaultCellStyle = dataGridViewCellStyle5;
            this.colAdet.HeaderText = "Adet";
            this.colAdet.Name = "colAdet";
            this.colAdet.ReadOnly = true;
            this.colAdet.Visible = false;
            this.colAdet.Width = 75;
            // 
            // colKazancOrani
            // 
            this.colKazancOrani.DataPropertyName = "HOPI_KAZAN_ORANI";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            this.colKazancOrani.DefaultCellStyle = dataGridViewCellStyle6;
            this.colKazancOrani.HeaderText = "Kazanç Oranı";
            this.colKazancOrani.Name = "colKazancOrani";
            this.colKazancOrani.ReadOnly = true;
            this.colKazancOrani.Visible = false;
            this.colKazancOrani.Width = 75;
            // 
            // colKazancMiktari
            // 
            this.colKazancMiktari.DataPropertyName = "HOPI_KAZAN_PARA";
            this.colKazancMiktari.HeaderText = "Kazanç Miktarı";
            this.colKazancMiktari.Name = "colKazancMiktari";
            this.colKazancMiktari.ReadOnly = true;
            this.colKazancMiktari.Visible = false;
            this.colKazancMiktari.Width = 200;
            // 
            // colKatMiktari
            // 
            this.colKatMiktari.DataPropertyName = "HOPI_KAT";
            this.colKatMiktari.HeaderText = "Kat Miktarı";
            this.colKatMiktari.Name = "colKatMiktari";
            this.colKatMiktari.ReadOnly = true;
            this.colKatMiktari.Visible = false;
            this.colKatMiktari.Width = 75;
            // 
            // colIndirimOrani
            // 
            this.colIndirimOrani.DataPropertyName = "INDIRIM_ORANI";
            this.colIndirimOrani.HeaderText = "İndirim Oranı";
            this.colIndirimOrani.Name = "colIndirimOrani";
            this.colIndirimOrani.ReadOnly = true;
            this.colIndirimOrani.Visible = false;
            // 
            // colMaxHopiKat
            // 
            this.colMaxHopiKat.DataPropertyName = "MAX_HOPI_KAT";
            this.colMaxHopiKat.HeaderText = "Max Hopi Kat";
            this.colMaxHopiKat.Name = "colMaxHopiKat";
            this.colMaxHopiKat.Visible = false;
            // 
            // colMaxKazancPara
            // 
            this.colMaxKazancPara.DataPropertyName = "MAX_KAZAN_PARA";
            this.colMaxKazancPara.HeaderText = "Max Kazanç Para";
            this.colMaxKazancPara.Name = "colMaxKazancPara";
            this.colMaxKazancPara.Visible = false;
            // 
            // colMiktar
            // 
            this.colMiktar.DataPropertyName = "Miktar";
            this.colMiktar.HeaderText = "Miktar";
            this.colMiktar.Name = "colMiktar";
            this.colMiktar.Visible = false;
            // 
            // colFiyatsalSinir
            // 
            this.colFiyatsalSinir.DataPropertyName = "FIYATSAL_SINIR";
            this.colFiyatsalSinir.HeaderText = "Fiyatsal Sınır";
            this.colFiyatsalSinir.Name = "colFiyatsalSinir";
            this.colFiyatsalSinir.Visible = false;
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
            this.tsMenu.Size = new System.Drawing.Size(713, 54);
            this.tsMenu.TabIndex = 25;
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
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 436);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(713, 22);
            this.statusStrip1.TabIndex = 26;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(0, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ödenecek Paracık:";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblAdisyonTutari);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lblKullanilabilirLimit);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btn7);
            this.panel2.Controls.Add(this.btnSil);
            this.panel2.Controls.Add(this.btn8);
            this.panel2.Controls.Add(this.btn0);
            this.panel2.Controls.Add(this.btn9);
            this.panel2.Controls.Add(this.btn3);
            this.panel2.Controls.Add(this.btn4);
            this.panel2.Controls.Add(this.btn2);
            this.panel2.Controls.Add(this.btn5);
            this.panel2.Controls.Add(this.btn1);
            this.panel2.Controls.Add(this.btn6);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(463, 54);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(250, 382);
            this.panel2.TabIndex = 36;
            // 
            // txtParacikKusurat
            // 
            this.txtParacikKusurat.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtParacikKusurat.Location = new System.Drawing.Point(162, 0);
            this.txtParacikKusurat.Name = "txtParacikKusurat";
            this.txtParacikKusurat.Size = new System.Drawing.Size(75, 38);
            this.txtParacikKusurat.TabIndex = 40;
            // 
            // lblAdisyonTutari
            // 
            this.lblAdisyonTutari.AutoSize = true;
            this.lblAdisyonTutari.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAdisyonTutari.ForeColor = System.Drawing.Color.Maroon;
            this.lblAdisyonTutari.Location = new System.Drawing.Point(131, 360);
            this.lblAdisyonTutari.Name = "lblAdisyonTutari";
            this.lblAdisyonTutari.Size = new System.Drawing.Size(0, 13);
            this.lblAdisyonTutari.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(39, 360);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Adisyon Tutarı:";
            // 
            // lblKullanilabilirLimit
            // 
            this.lblKullanilabilirLimit.AutoSize = true;
            this.lblKullanilabilirLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblKullanilabilirLimit.ForeColor = System.Drawing.Color.Green;
            this.lblKullanilabilirLimit.Location = new System.Drawing.Point(131, 343);
            this.lblKullanilabilirLimit.Name = "lblKullanilabilirLimit";
            this.lblKullanilabilirLimit.Size = new System.Drawing.Size(0, 13);
            this.lblKullanilabilirLimit.TabIndex = 37;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(5, 343);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Kullanılabilir Paracık:";
            // 
            // txtParacik
            // 
            this.txtParacik.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtParacik.Location = new System.Drawing.Point(0, 0);
            this.txtParacik.Name = "txtParacik";
            this.txtParacik.Size = new System.Drawing.Size(156, 38);
            this.txtParacik.TabIndex = 1;
            this.txtParacik.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn7
            // 
            this.btn7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn7.Location = new System.Drawing.Point(5, 78);
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(75, 61);
            this.btn7.TabIndex = 24;
            this.btn7.Text = "7";
            this.btn7.UseVisualStyleBackColor = true;
            this.btn7.Click += new System.EventHandler(this.btn7_Click);
            // 
            // btnSil
            // 
            this.btnSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSil.Location = new System.Drawing.Point(5, 279);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(75, 61);
            this.btnSil.TabIndex = 34;
            this.btnSil.Text = "Sil";
            this.btnSil.UseVisualStyleBackColor = true;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // btn8
            // 
            this.btn8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn8.Location = new System.Drawing.Point(86, 78);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(75, 61);
            this.btn8.TabIndex = 25;
            this.btn8.Text = "8";
            this.btn8.UseVisualStyleBackColor = true;
            this.btn8.Click += new System.EventHandler(this.btn7_Click);
            // 
            // btn0
            // 
            this.btn0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn0.Location = new System.Drawing.Point(86, 279);
            this.btn0.Name = "btn0";
            this.btn0.Size = new System.Drawing.Size(156, 61);
            this.btn0.TabIndex = 33;
            this.btn0.Text = "0";
            this.btn0.UseVisualStyleBackColor = true;
            this.btn0.Click += new System.EventHandler(this.btn7_Click);
            // 
            // btn9
            // 
            this.btn9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn9.Location = new System.Drawing.Point(167, 78);
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(75, 61);
            this.btn9.TabIndex = 26;
            this.btn9.Text = "9";
            this.btn9.UseVisualStyleBackColor = true;
            this.btn9.Click += new System.EventHandler(this.btn7_Click);
            // 
            // btn3
            // 
            this.btn3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn3.Location = new System.Drawing.Point(167, 212);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(75, 61);
            this.btn3.TabIndex = 32;
            this.btn3.Text = "3";
            this.btn3.UseVisualStyleBackColor = true;
            this.btn3.Click += new System.EventHandler(this.btn7_Click);
            // 
            // btn4
            // 
            this.btn4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn4.Location = new System.Drawing.Point(5, 145);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(75, 61);
            this.btn4.TabIndex = 27;
            this.btn4.Text = "4";
            this.btn4.UseVisualStyleBackColor = true;
            this.btn4.Click += new System.EventHandler(this.btn7_Click);
            // 
            // btn2
            // 
            this.btn2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn2.Location = new System.Drawing.Point(86, 212);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(75, 61);
            this.btn2.TabIndex = 31;
            this.btn2.Text = "2";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.btn7_Click);
            // 
            // btn5
            // 
            this.btn5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn5.Location = new System.Drawing.Point(86, 145);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(75, 61);
            this.btn5.TabIndex = 28;
            this.btn5.Text = "5";
            this.btn5.UseVisualStyleBackColor = true;
            this.btn5.Click += new System.EventHandler(this.btn7_Click);
            // 
            // btn1
            // 
            this.btn1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn1.Location = new System.Drawing.Point(5, 212);
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
            this.btn6.Location = new System.Drawing.Point(167, 145);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(75, 61);
            this.btn6.TabIndex = 29;
            this.btn6.Text = "6";
            this.btn6.UseVisualStyleBackColor = true;
            this.btn6.Click += new System.EventHandler(this.btn7_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtParacikKusurat);
            this.panel1.Controls.Add(this.txtParacik);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(5, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(237, 38);
            this.panel1.TabIndex = 41;
            // 
            // frmKampanyaListesi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(713, 458);
            this.Controls.Add(this.dgvKampanyalar);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tsMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmKampanyaListesi";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kampanya Listesi";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmKampanyaListesi_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKampanyalar)).EndInit();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvKampanyalar;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton tsbTamam;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbKapat;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn7;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Button btn8;
        private System.Windows.Forms.Button btn0;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.TextBox txtParacik;
        private System.Windows.Forms.Label lblKullanilabilirLimit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAdisyonTutari;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSec;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKampanya;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAciklama;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKazancOrani;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKazancMiktari;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKatMiktari;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIndirimOrani;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaxHopiKat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaxKazancPara;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMiktar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFiyatsalSinir;
        private System.Windows.Forms.TextBox txtParacikKusurat;
        private System.Windows.Forms.Panel panel1;
    }
}