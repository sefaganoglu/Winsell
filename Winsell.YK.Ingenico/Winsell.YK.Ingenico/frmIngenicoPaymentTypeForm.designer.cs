namespace Winsell.YK.Ingenico
{
    partial class frmIngenicoPaymentTypeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIngenicoPaymentTypeForm));
            this.lstOdemeTipleri = new System.Windows.Forms.ListBox();
            this.btnTamam = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
            // 
            // lstOdemeTipleri
            // 
            this.lstOdemeTipleri.FormattingEnabled = true;
            this.lstOdemeTipleri.Location = new System.Drawing.Point(12, 12);
            this.lstOdemeTipleri.Name = "lstOdemeTipleri";
            this.lstOdemeTipleri.Size = new System.Drawing.Size(260, 186);
            this.lstOdemeTipleri.TabIndex = 0;
            this.lstOdemeTipleri.SelectedIndexChanged += new System.EventHandler(this.m_listPaymentTypes_SelectedIndexChanged);
            // 
            // btnTamam
            // 
            this.btnTamam.Location = new System.Drawing.Point(197, 204);
            this.btnTamam.Name = "btnTamam";
            this.btnTamam.Size = new System.Drawing.Size(75, 23);
            this.btnTamam.TabIndex = 1;
            this.btnTamam.Text = "Tamam";
            this.btnTamam.UseVisualStyleBackColor = true;
            this.btnTamam.Click += new System.EventHandler(this.button1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 231);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(279, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // frmIngenicoPaymentTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 253);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnTamam);
            this.Controls.Add(this.lstOdemeTipleri);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmIngenicoPaymentTypeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ödeme Tipleri";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstOdemeTipleri;
        private System.Windows.Forms.Button btnTamam;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}