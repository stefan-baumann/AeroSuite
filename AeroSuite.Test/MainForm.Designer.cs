namespace AeroSuite.Test
{
    partial class MainForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ControlPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.BorderlessFormTestButton = new System.Windows.Forms.Button();
            this.HideGridButton = new System.Windows.Forms.Button();
            this.TypeComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ControlPropertyGrid);
            this.splitContainer1.Panel2.Controls.Add(this.BorderlessFormTestButton);
            this.splitContainer1.Panel2.Controls.Add(this.HideGridButton);
            this.splitContainer1.Panel2.Controls.Add(this.TypeComboBox);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(851, 530);
            this.splitContainer1.SplitterDistance = 425;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // ControlPropertyGrid
            // 
            this.ControlPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlPropertyGrid.Location = new System.Drawing.Point(0, 67);
            this.ControlPropertyGrid.Name = "ControlPropertyGrid";
            this.ControlPropertyGrid.Size = new System.Drawing.Size(421, 463);
            this.ControlPropertyGrid.TabIndex = 2;
            // 
            // BorderlessFormTestButton
            // 
            this.BorderlessFormTestButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.BorderlessFormTestButton.Location = new System.Drawing.Point(0, 44);
            this.BorderlessFormTestButton.Name = "BorderlessFormTestButton";
            this.BorderlessFormTestButton.Size = new System.Drawing.Size(421, 23);
            this.BorderlessFormTestButton.TabIndex = 4;
            this.BorderlessFormTestButton.Text = "Show Borderless Form";
            this.BorderlessFormTestButton.UseVisualStyleBackColor = true;
            this.BorderlessFormTestButton.Click += new System.EventHandler(this.BorderlessFormTestButton_Click);
            // 
            // HideGridButton
            // 
            this.HideGridButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.HideGridButton.Location = new System.Drawing.Point(0, 21);
            this.HideGridButton.Name = "HideGridButton";
            this.HideGridButton.Size = new System.Drawing.Size(421, 23);
            this.HideGridButton.TabIndex = 3;
            this.HideGridButton.Text = "Hide Property Grid";
            this.HideGridButton.UseVisualStyleBackColor = true;
            this.HideGridButton.Click += new System.EventHandler(this.HideGridButton_Click);
            // 
            // TypeComboBox
            // 
            this.TypeComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.TypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypeComboBox.FormattingEnabled = true;
            this.TypeComboBox.Location = new System.Drawing.Point(0, 0);
            this.TypeComboBox.Name = "TypeComboBox";
            this.TypeComboBox.Size = new System.Drawing.Size(421, 21);
            this.TypeComboBox.TabIndex = 1;
            this.TypeComboBox.SelectedIndexChanged += new System.EventHandler(this.TypeComboBox_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(851, 530);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Name = "MainForm";
            this.Text = "AeroSuite Control Tester";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid ControlPropertyGrid;
        private System.Windows.Forms.ComboBox TypeComboBox;
        private System.Windows.Forms.Button HideGridButton;
        private System.Windows.Forms.Button BorderlessFormTestButton;
    }
}

