namespace Winsell.YK.Ingenico
{
    partial class frmIngenicoPaymentAppForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIngenicoPaymentAppForm));
            this.btnTamam = new System.Windows.Forms.Button();
            this.lstOdemeUygulamalari = new System.Windows.Forms.ListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
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
            // lstOdemeUygulamalari
            // 
            this.lstOdemeUygulamalari.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lstOdemeUygulamalari.FormattingEnabled = true;
            this.lstOdemeUygulamalari.Location = new System.Drawing.Point(12, 12);
            this.lstOdemeUygulamalari.Name = "lstOdemeUygulamalari";
            this.lstOdemeUygulamalari.Size = new System.Drawing.Size(260, 186);
            this.lstOdemeUygulamalari.TabIndex = 129;
            this.lstOdemeUygulamalari.SelectedIndexChanged += new System.EventHandler(this.m_listPaymentApplications_SelectedIndexChanged_1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 231);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(279, 22);
            this.statusStrip1.TabIndex = 130;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // frmIngenicoPaymentAppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 253);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lstOdemeUygulamalari);
            this.Controls.Add(this.btnTamam);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmIngenicoPaymentAppForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ödeme Uygulamaları";
            this.Load += new System.EventHandler(this.PaymentAppForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTamam;
        private System.Windows.Forms.ListBox lstOdemeUygulamalari;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}