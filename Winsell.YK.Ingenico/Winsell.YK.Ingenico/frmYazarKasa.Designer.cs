namespace Winsell.YK.Ingenico
{
    partial class frmYazarKasa
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmYazarKasa));
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.tSatis = new System.Windows.Forms.Timer(this.components);
            this.btnBelgeIptal = new System.Windows.Forms.Button();
            this.btnIslem = new System.Windows.Forms.Button();
            this.niTask = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.gösterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.çıkışToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRaporlar = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.cmsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbLog
            // 
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.Location = new System.Drawing.Point(0, 0);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(546, 237);
            this.rtbLog.TabIndex = 0;
            this.rtbLog.Text = "";
            // 
            // tSatis
            // 
            this.tSatis.Interval = 1;
            this.tSatis.Tick += new System.EventHandler(this.tSatis_Tick);
            // 
            // btnBelgeIptal
            // 
            this.btnBelgeIptal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBelgeIptal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnBelgeIptal.Location = new System.Drawing.Point(0, 260);
            this.btnBelgeIptal.Name = "btnBelgeIptal";
            this.btnBelgeIptal.Size = new System.Drawing.Size(546, 23);
            this.btnBelgeIptal.TabIndex = 7;
            this.btnBelgeIptal.Text = "Belge İptal";
            this.btnBelgeIptal.UseVisualStyleBackColor = true;
            this.btnBelgeIptal.Click += new System.EventHandler(this.btnBelgeIptal_Click);
            // 
            // btnIslem
            // 
            this.btnIslem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIslem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnIslem.Location = new System.Drawing.Point(0, 237);
            this.btnIslem.Name = "btnIslem";
            this.btnIslem.Size = new System.Drawing.Size(546, 23);
            this.btnIslem.TabIndex = 8;
            this.btnIslem.Text = "İşlem Durdur";
            this.btnIslem.UseVisualStyleBackColor = true;
            this.btnIslem.Click += new System.EventHandler(this.btnIslem_Click);
            // 
            // niTask
            // 
            this.niTask.ContextMenuStrip = this.cmsMenu;
            this.niTask.Icon = ((System.Drawing.Icon)(resources.GetObject("niTask.Icon")));
            this.niTask.Text = "Yazar Kasa Kontrol";
            this.niTask.Visible = true;
            this.niTask.DoubleClick += new System.EventHandler(this.niTask_DoubleClick);
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gösterToolStripMenuItem,
            this.toolStripMenuItem1,
            this.çıkışToolStripMenuItem});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(109, 54);
            // 
            // gösterToolStripMenuItem
            // 
            this.gösterToolStripMenuItem.Name = "gösterToolStripMenuItem";
            this.gösterToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.gösterToolStripMenuItem.Text = "Göster";
            this.gösterToolStripMenuItem.Click += new System.EventHandler(this.gösterToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(105, 6);
            // 
            // çıkışToolStripMenuItem
            // 
            this.çıkışToolStripMenuItem.Name = "çıkışToolStripMenuItem";
            this.çıkışToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.çıkışToolStripMenuItem.Text = "Çıkış";
            this.çıkışToolStripMenuItem.Click += new System.EventHandler(this.çıkışToolStripMenuItem_Click);
            // 
            // btnRaporlar
            // 
            this.btnRaporlar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRaporlar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRaporlar.Location = new System.Drawing.Point(0, 283);
            this.btnRaporlar.Name = "btnRaporlar";
            this.btnRaporlar.Size = new System.Drawing.Size(546, 23);
            this.btnRaporlar.TabIndex = 9;
            this.btnRaporlar.Text = "Raporlar";
            this.btnRaporlar.UseVisualStyleBackColor = true;
            this.btnRaporlar.Click += new System.EventHandler(this.btnRaporlar_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 306);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(546, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // frmYazarKasa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 328);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.btnIslem);
            this.Controls.Add(this.btnBelgeIptal);
            this.Controls.Add(this.btnRaporlar);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmYazarKasa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Winsell Ingenico Yazar Kasa";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmYazarKasa_FormClosing);
            this.Load += new System.EventHandler(this.frmCihaz_Load);
            this.VisibleChanged += new System.EventHandler(this.frmYazarKasa_VisibleChanged);
            this.cmsMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Timer tSatis;
        private System.Windows.Forms.Button btnBelgeIptal;
        private System.Windows.Forms.Button btnIslem;
        private System.Windows.Forms.NotifyIcon niTask;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem çıkışToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gösterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.Button btnRaporlar;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}

