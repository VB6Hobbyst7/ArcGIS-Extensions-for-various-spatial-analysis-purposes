namespace ROI
{
    partial class frmGetRectangles
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
            this.cboLayers = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtNumRectWidth = new System.Windows.Forms.TextBox();
            this.txtDisHeight = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDisWidth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNumRectHeight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtBuffer = new System.Windows.Forms.TextBox();
            this.chkBuffer = new System.Windows.Forms.CheckBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnOptions = new System.Windows.Forms.Button();
            this.btnGetRectangles = new System.Windows.Forms.Button();
            this.btnClearPath = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboLayers
            // 
            this.cboLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLayers.FormattingEnabled = true;
            this.cboLayers.Location = new System.Drawing.Point(153, 11);
            this.cboLayers.Name = "cboLayers";
            this.cboLayers.Size = new System.Drawing.Size(389, 21);
            this.cboLayers.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "List of Polygon Layers:";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(455, 46);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(87, 20);
            this.txtWidth.TabIndex = 13;
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(153, 46);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(84, 20);
            this.txtHeight.TabIndex = 12;
            // 
            // txtNumRectWidth
            // 
            this.txtNumRectWidth.Location = new System.Drawing.Point(153, 95);
            this.txtNumRectWidth.Name = "txtNumRectWidth";
            this.txtNumRectWidth.Size = new System.Drawing.Size(85, 20);
            this.txtNumRectWidth.TabIndex = 11;
            // 
            // txtDisHeight
            // 
            this.txtDisHeight.Location = new System.Drawing.Point(456, 70);
            this.txtDisHeight.Name = "txtDisHeight";
            this.txtDisHeight.Size = new System.Drawing.Size(86, 20);
            this.txtDisHeight.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(244, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(201, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Distance Between Plots Across the Field:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(243, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(196, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Distance Between Plots Along the Field:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(245, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Plot\'s Width:";
            // 
            // txtDisWidth
            // 
            this.txtDisWidth.Location = new System.Drawing.Point(456, 94);
            this.txtDisWidth.Name = "txtDisWidth";
            this.txtDisWidth.Size = new System.Drawing.Size(86, 20);
            this.txtDisWidth.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Plot\'s Length:";
            // 
            // txtNumRectHeight
            // 
            this.txtNumRectHeight.Location = new System.Drawing.Point(153, 70);
            this.txtNumRectHeight.Name = "txtNumRectHeight";
            this.txtNumRectHeight.Size = new System.Drawing.Size(84, 20);
            this.txtNumRectHeight.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "No. of Plots Along the Field:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "No. of Plots Across the Field:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnAbout);
            this.groupBox1.Controls.Add(this.btnOptions);
            this.groupBox1.Controls.Add(this.btnGetRectangles);
            this.groupBox1.Controls.Add(this.cboLayers);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnClearPath);
            this.groupBox1.Controls.Add(this.txtWidth);
            this.groupBox1.Controls.Add(this.txtHeight);
            this.groupBox1.Controls.Add(this.txtNumRectWidth);
            this.groupBox1.Controls.Add(this.txtDisHeight);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtDisWidth);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNumRectHeight);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Location = new System.Drawing.Point(3, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(553, 201);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtBuffer);
            this.groupBox2.Controls.Add(this.chkBuffer);
            this.groupBox2.Location = new System.Drawing.Point(9, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(538, 41);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            // 
            // txtBuffer
            // 
            this.txtBuffer.Enabled = false;
            this.txtBuffer.Location = new System.Drawing.Point(287, 13);
            this.txtBuffer.Name = "txtBuffer";
            this.txtBuffer.Size = new System.Drawing.Size(246, 20);
            this.txtBuffer.TabIndex = 24;
            this.txtBuffer.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // chkBuffer
            // 
            this.chkBuffer.AutoSize = true;
            this.chkBuffer.Location = new System.Drawing.Point(8, 15);
            this.chkBuffer.Name = "chkBuffer";
            this.chkBuffer.Size = new System.Drawing.Size(263, 17);
            this.chkBuffer.TabIndex = 23;
            this.chkBuffer.Text = "Do want to apply a \"Negative Buffer\" to the plots?";
            this.chkBuffer.UseVisualStyleBackColor = true;
            this.chkBuffer.CheckedChanged += new System.EventHandler(this.chkBuffer_CheckedChanged);
            // 
            // btnAbout
            // 
            this.btnAbout.Image = global::ROI.Properties.Resources.info1;
            this.btnAbout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAbout.Location = new System.Drawing.Point(328, 167);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(95, 29);
            this.btnAbout.TabIndex = 21;
            this.btnAbout.Text = "About Tool";
            this.btnAbout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnOptions
            // 
            this.btnOptions.Image = global::ROI.Properties.Resources.advancedsettings;
            this.btnOptions.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOptions.Location = new System.Drawing.Point(225, 167);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(95, 29);
            this.btnOptions.TabIndex = 20;
            this.btnOptions.Text = "Options";
            this.btnOptions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // btnGetRectangles
            // 
            this.btnGetRectangles.Image = global::ROI.Properties.Resources.vertex_create;
            this.btnGetRectangles.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGetRectangles.Location = new System.Drawing.Point(17, 166);
            this.btnGetRectangles.Name = "btnGetRectangles";
            this.btnGetRectangles.Size = new System.Drawing.Size(95, 30);
            this.btnGetRectangles.TabIndex = 0;
            this.btnGetRectangles.Text = "Generate";
            this.btnGetRectangles.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGetRectangles.UseVisualStyleBackColor = true;
            this.btnGetRectangles.Click += new System.EventHandler(this.btnGetRectangles_Click);
            // 
            // btnClearPath
            // 
            this.btnClearPath.Image = global::ROI.Properties.Resources.edit_clear;
            this.btnClearPath.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearPath.Location = new System.Drawing.Point(121, 167);
            this.btnClearPath.Name = "btnClearPath";
            this.btnClearPath.Size = new System.Drawing.Size(95, 29);
            this.btnClearPath.TabIndex = 14;
            this.btnClearPath.Text = "Clear Path";
            this.btnClearPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClearPath.UseVisualStyleBackColor = true;
            this.btnClearPath.Click += new System.EventHandler(this.btnClearPath_Click);
            // 
            // btnClose
            // 
            this.btnClose.Image = global::ROI.Properties.Resources.cancel20;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(432, 167);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 29);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = false;
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(220, 17);
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 205);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(560, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // frmGetRectangles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 227);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmGetRectangles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ROI Plot Generator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGetRectangles_FormClosing);
            this.Load += new System.EventHandler(this.frmGetRectangles_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboLayers;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnClearPath;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.TextBox txtNumRectWidth;
        private System.Windows.Forms.TextBox txtDisHeight;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDisWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNumRectHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnGetRectangles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtBuffer;
        private System.Windows.Forms.CheckBox chkBuffer;
    }
}