
namespace DG_Engineering
{
    partial class MyobAccess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyobAccess));
            this.MYOBAccessViewer = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)(this.MYOBAccessViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // MYOBAccessViewer
            // 
            this.MYOBAccessViewer.AllowExternalDrop = true;
            this.MYOBAccessViewer.CreationProperties = null;
            this.MYOBAccessViewer.DefaultBackgroundColor = System.Drawing.Color.White;
            this.MYOBAccessViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MYOBAccessViewer.Location = new System.Drawing.Point(0, 0);
            this.MYOBAccessViewer.Name = "MYOBAccessViewer";
            this.MYOBAccessViewer.Size = new System.Drawing.Size(427, 412);
            this.MYOBAccessViewer.TabIndex = 0;
            this.MYOBAccessViewer.ZoomFactor = 1D;
            // 
            // MyobAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 412);
            this.Controls.Add(this.MYOBAccessViewer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MyobAccess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MYOB Access";
            this.Load += new System.EventHandler(this.MyobAccess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MYOBAccessViewer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 MYOBAccessViewer;
    }
}