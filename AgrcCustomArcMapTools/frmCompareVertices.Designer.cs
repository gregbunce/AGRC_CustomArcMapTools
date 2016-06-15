namespace AgrcCustomArcMapTools
{
    partial class frmCompareVertices
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnZoomOID2 = new System.Windows.Forms.Button();
            this.btnZoomOID1 = new System.Windows.Forms.Button();
            this.txtOID1 = new System.Windows.Forms.TextBox();
            this.txtOID2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboLayer2 = new System.Windows.Forms.ComboBox();
            this.cboLayer1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClearVertices = new System.Windows.Forms.Button();
            this.lstMissingVertices = new System.Windows.Forms.ListBox();
            this.chkDisplayVertices = new System.Windows.Forms.CheckBox();
            this.cmdCompare = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnZoomOID2);
            this.groupBox1.Controls.Add(this.btnZoomOID1);
            this.groupBox1.Controls.Add(this.txtOID1);
            this.groupBox1.Controls.Add(this.txtOID2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboLayer2);
            this.groupBox1.Controls.Add(this.cboLayer1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 187);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose Polygons";
            // 
            // btnZoomOID2
            // 
            this.btnZoomOID2.Location = new System.Drawing.Point(258, 152);
            this.btnZoomOID2.Name = "btnZoomOID2";
            this.btnZoomOID2.Size = new System.Drawing.Size(75, 23);
            this.btnZoomOID2.TabIndex = 6;
            this.btnZoomOID2.Text = "Zoom to";
            this.btnZoomOID2.UseVisualStyleBackColor = true;
            this.btnZoomOID2.Click += new System.EventHandler(this.btnZoomOID2_Click);
            // 
            // btnZoomOID1
            // 
            this.btnZoomOID1.Location = new System.Drawing.Point(38, 152);
            this.btnZoomOID1.Name = "btnZoomOID1";
            this.btnZoomOID1.Size = new System.Drawing.Size(75, 23);
            this.btnZoomOID1.TabIndex = 5;
            this.btnZoomOID1.Text = "Zoom to";
            this.btnZoomOID1.UseVisualStyleBackColor = true;
            this.btnZoomOID1.Click += new System.EventHandler(this.btnZoomOID1_Click);
            // 
            // txtOID1
            // 
            this.txtOID1.Location = new System.Drawing.Point(26, 125);
            this.txtOID1.Name = "txtOID1";
            this.txtOID1.Size = new System.Drawing.Size(100, 21);
            this.txtOID1.TabIndex = 3;
            // 
            // txtOID2
            // 
            this.txtOID2.Location = new System.Drawing.Point(246, 126);
            this.txtOID2.Name = "txtOID2";
            this.txtOID2.Size = new System.Drawing.Size(100, 21);
            this.txtOID2.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(243, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "OBJECTID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(23, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "OBJECTID";
            // 
            // cboLayer2
            // 
            this.cboLayer2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLayer2.FormattingEnabled = true;
            this.cboLayer2.Location = new System.Drawing.Point(241, 67);
            this.cboLayer2.Name = "cboLayer2";
            this.cboLayer2.Size = new System.Drawing.Size(183, 23);
            this.cboLayer2.TabIndex = 2;
            // 
            // cboLayer1
            // 
            this.cboLayer1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLayer1.FormattingEnabled = true;
            this.cboLayer1.Location = new System.Drawing.Point(21, 67);
            this.cboLayer1.Name = "cboLayer1";
            this.cboLayer1.Size = new System.Drawing.Size(183, 23);
            this.cboLayer1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(239, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Layer Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Layer Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(238, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Polygon 2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Polygon 1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClearVertices);
            this.groupBox2.Controls.Add(this.lstMissingVertices);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(13, 217);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(442, 169);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Missing Vertex Locations";
            // 
            // btnClearVertices
            // 
            this.btnClearVertices.Location = new System.Drawing.Point(19, 122);
            this.btnClearVertices.Name = "btnClearVertices";
            this.btnClearVertices.Size = new System.Drawing.Size(107, 23);
            this.btnClearVertices.TabIndex = 7;
            this.btnClearVertices.Text = "Clear Vertices";
            this.btnClearVertices.UseVisualStyleBackColor = true;
            this.btnClearVertices.Click += new System.EventHandler(this.btnClearVertices_Click);
            // 
            // lstMissingVertices
            // 
            this.lstMissingVertices.FormattingEnabled = true;
            this.lstMissingVertices.ItemHeight = 15;
            this.lstMissingVertices.Location = new System.Drawing.Point(19, 21);
            this.lstMissingVertices.Name = "lstMissingVertices";
            this.lstMissingVertices.Size = new System.Drawing.Size(405, 94);
            this.lstMissingVertices.TabIndex = 0;
            this.lstMissingVertices.TabStop = false;
            this.lstMissingVertices.DoubleClick += new System.EventHandler(this.lstMissingVertices_DoubleClick);
            // 
            // chkDisplayVertices
            // 
            this.chkDisplayVertices.AutoSize = true;
            this.chkDisplayVertices.Location = new System.Drawing.Point(145, 404);
            this.chkDisplayVertices.Name = "chkDisplayVertices";
            this.chkDisplayVertices.Size = new System.Drawing.Size(180, 17);
            this.chkDisplayVertices.TabIndex = 3;
            this.chkDisplayVertices.TabStop = false;
            this.chkDisplayVertices.Text = "Display Missing Vertex Locations";
            this.chkDisplayVertices.UseVisualStyleBackColor = true;
            // 
            // cmdCompare
            // 
            this.cmdCompare.Location = new System.Drawing.Point(155, 430);
            this.cmdCompare.Name = "cmdCompare";
            this.cmdCompare.Size = new System.Drawing.Size(150, 23);
            this.cmdCompare.TabIndex = 9;
            this.cmdCompare.Text = "Compare Vertices";
            this.cmdCompare.UseVisualStyleBackColor = true;
            this.cmdCompare.Click += new System.EventHandler(this.cmdCompare_Click);
            // 
            // frmCompareVertices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 471);
            this.Controls.Add(this.cmdCompare);
            this.Controls.Add(this.chkDisplayVertices);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCompareVertices";
            this.ShowIcon = false;
            this.Text = "Compare Polygon Vertices";
            this.Load += new System.EventHandler(this.frmCompareVertices_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnZoomOID2;
        private System.Windows.Forms.Button btnZoomOID1;
        private System.Windows.Forms.TextBox txtOID1;
        private System.Windows.Forms.TextBox txtOID2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboLayer2;
        private System.Windows.Forms.ComboBox cboLayer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClearVertices;
        private System.Windows.Forms.ListBox lstMissingVertices;
        private System.Windows.Forms.CheckBox chkDisplayVertices;
        private System.Windows.Forms.Button cmdCompare;
    }
}