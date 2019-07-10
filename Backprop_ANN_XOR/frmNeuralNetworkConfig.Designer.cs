namespace Backprop_ANN_XOR
{
    #region frmNeuralNetworkConfig CLASS
    /// <summary>
    /// Provides gui declaration part of frmNeuralNetworkConfig 
    /// class
    /// </summary>
    partial class frmNeuralNetworkConfig
    {
        #region Visual Studio designer code
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
        #endregion
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlBanner = new System.Windows.Forms.Panel();
            this.lblDescription = new System.Windows.Forms.Label();
            this.pnlFill = new System.Windows.Forms.Panel();
            this.pGrid = new System.Windows.Forms.PropertyGrid();
            this.pnlTop.SuspendLayout();
            this.pnlBanner.SuspendLayout();
            this.pnlFill.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlTop.Controls.Add(this.lblSubTitle);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(742, 55);
            this.pnlTop.TabIndex = 6;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.ForeColor = System.Drawing.Color.White;
            this.lblSubTitle.Location = new System.Drawing.Point(13, 25);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(343, 13);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "This example shows how to train a network to do an XOR logic problem";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(13, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(479, 22);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "A Beginners Guide To Multi Layer Neural Networks";
            // 
            // pnlBanner
            // 
            this.pnlBanner.BackColor = System.Drawing.Color.Black;
            this.pnlBanner.Controls.Add(this.lblDescription);
            this.pnlBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBanner.Location = new System.Drawing.Point(0, 55);
            this.pnlBanner.Name = "pnlBanner";
            this.pnlBanner.Size = new System.Drawing.Size(742, 26);
            this.pnlBanner.TabIndex = 8;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.ForeColor = System.Drawing.Color.White;
            this.lblDescription.Location = new System.Drawing.Point(13, 6);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(444, 13);
            this.lblDescription.TabIndex = 9;
            this.lblDescription.Text = "The property grid below, shows the latest known configuration for the trained Neu" +
                "ral Network";
            // 
            // pnlFill
            // 
            this.pnlFill.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnlFill.Controls.Add(this.pGrid);
            this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFill.Location = new System.Drawing.Point(0, 81);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Size = new System.Drawing.Size(742, 385);
            this.pnlFill.TabIndex = 9;
            // 
            // pGrid
            // 
            this.pGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pGrid.Location = new System.Drawing.Point(0, 0);
            this.pGrid.Name = "pGrid";
            this.pGrid.Size = new System.Drawing.Size(742, 385);
            this.pGrid.TabIndex = 2;
            // 
            // frmNeuralNetworkConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 466);
            this.Controls.Add(this.pnlFill);
            this.Controls.Add(this.pnlBanner);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmNeuralNetworkConfig";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Neural Network XOR";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBanner.ResumeLayout(false);
            this.pnlBanner.PerformLayout();
            this.pnlFill.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        #region Instance fields
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlBanner;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Panel pnlFill;
        private System.Windows.Forms.PropertyGrid pGrid;
        #endregion
    }
    #endregion
}