namespace Viewer.RegistryViewer.RegistrySetter.View
{
    partial class frmRegisrtySetter
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtRegistryValueName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_SetTrackValues = new System.Windows.Forms.Button();
            this.txtRegistryKeyName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtRegistryValueName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btn_SetTrackValues);
            this.panel1.Controls.Add(this.txtRegistryKeyName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(702, 91);
            this.panel1.TabIndex = 2;
            // 
            // txtRegistryValueName
            // 
            this.txtRegistryValueName.Location = new System.Drawing.Point(158, 35);
            this.txtRegistryValueName.Name = "txtRegistryValueName";
            this.txtRegistryValueName.Size = new System.Drawing.Size(435, 21);
            this.txtRegistryValueName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "레지스트리 Value Name : ";
            // 
            // btn_SetTrackValues
            // 
            this.btn_SetTrackValues.Location = new System.Drawing.Point(599, 8);
            this.btn_SetTrackValues.Name = "btn_SetTrackValues";
            this.btn_SetTrackValues.Size = new System.Drawing.Size(96, 75);
            this.btn_SetTrackValues.TabIndex = 2;
            this.btn_SetTrackValues.Text = "버튼 추가";
            this.btn_SetTrackValues.UseVisualStyleBackColor = true;
            // 
            // txtRegistryKeyName
            // 
            this.txtRegistryKeyName.Location = new System.Drawing.Point(158, 8);
            this.txtRegistryKeyName.Name = "txtRegistryKeyName";
            this.txtRegistryKeyName.Size = new System.Drawing.Size(435, 21);
            this.txtRegistryKeyName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "레지스트리 Key : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "레지스트리 Value Data : ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(158, 62);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(435, 21);
            this.textBox1.TabIndex = 9;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 91);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(702, 359);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // frmRegisrtySetter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 450);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Name = "frmRegisrtySetter";
            this.Text = "frmRegisrtySetter";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtRegistryValueName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_SetTrackValues;
        private System.Windows.Forms.TextBox txtRegistryKeyName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}