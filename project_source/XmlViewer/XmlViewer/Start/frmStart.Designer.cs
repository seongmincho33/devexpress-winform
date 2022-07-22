namespace Viewer
{
    partial class frmStart
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnStartXMLViewer = new System.Windows.Forms.Button();
            this.btnPDMSRegistry = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnStartXMLViewer);
            this.flowLayoutPanel1.Controls.Add(this.btnPDMSRegistry);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(344, 107);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnStartXMLViewer
            // 
            this.btnStartXMLViewer.Location = new System.Drawing.Point(3, 3);
            this.btnStartXMLViewer.Name = "btnStartXMLViewer";
            this.btnStartXMLViewer.Size = new System.Drawing.Size(164, 94);
            this.btnStartXMLViewer.TabIndex = 1;
            this.btnStartXMLViewer.Text = "XML Viewer";
            this.btnStartXMLViewer.UseVisualStyleBackColor = true;
            // 
            // btnPDMSRegistry
            // 
            this.btnPDMSRegistry.Location = new System.Drawing.Point(173, 3);
            this.btnPDMSRegistry.Name = "btnPDMSRegistry";
            this.btnPDMSRegistry.Size = new System.Drawing.Size(164, 94);
            this.btnPDMSRegistry.TabIndex = 2;
            this.btnPDMSRegistry.Text = "PDMS & PEDAS-Converter Registry Viewer";
            this.btnPDMSRegistry.UseVisualStyleBackColor = true;
            // 
            // frmStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 131);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "frmStart";
            this.Text = "StartMenu";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnStartXMLViewer;
        private System.Windows.Forms.Button btnPDMSRegistry;
    }
}