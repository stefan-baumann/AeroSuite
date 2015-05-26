namespace AeroSuite.AnimationEngine.CustomAnimationTest
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
            this.menuCloseButton2 = new AeroSuite.AnimationEngine.CustomAnimationTest.MenuCloseButton();
            this.SuspendLayout();
            // 
            // menuCloseButton2
            // 
            this.menuCloseButton2.Extended = false;
            this.menuCloseButton2.Location = new System.Drawing.Point(174, 50);
            this.menuCloseButton2.Name = "menuCloseButton2";
            this.menuCloseButton2.Size = new System.Drawing.Size(25, 25);
            this.menuCloseButton2.TabIndex = 1;
            this.menuCloseButton2.Text = "menuCloseButton2";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(445, 366);
            this.Controls.Add(this.menuCloseButton2);
            this.Name = "MainForm";
            this.Text = "AeroSuite Custom Animation Test";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MenuCloseButton menuCloseButton2;





    }
}

