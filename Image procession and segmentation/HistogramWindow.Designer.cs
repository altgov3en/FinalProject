namespace Image_procession_and_segmentation
{
    partial class HistogramWindow
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

        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.histogramPicture = new System.Windows.Forms.PictureBox();
            this.numberOfClustersTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.histogramPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // histogramPicture
            // 
            this.histogramPicture.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.histogramPicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.histogramPicture.Location = new System.Drawing.Point(0, 1);
            this.histogramPicture.Name = "histogramPicture";
            this.histogramPicture.Size = new System.Drawing.Size(511, 457);
            this.histogramPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.histogramPicture.TabIndex = 0;
            this.histogramPicture.TabStop = false;
            // 
            // numberOfClustersTextBox
            // 
            this.numberOfClustersTextBox.Location = new System.Drawing.Point(294, 465);
            this.numberOfClustersTextBox.Name = "numberOfClustersTextBox";
            this.numberOfClustersTextBox.Size = new System.Drawing.Size(58, 20);
            this.numberOfClustersTextBox.TabIndex = 1;
            this.numberOfClustersTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(5, 465);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter Number Of Clusters (2, 3, 4, 5 or  6)";
            // 
            // okButton
            // 
            this.okButton.Enabled = false;
            this.okButton.Location = new System.Drawing.Point(358, 463);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(141, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "Enter Number";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // HistogramWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 493);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numberOfClustersTextBox);
            this.Controls.Add(this.histogramPicture);
            this.Name = "HistogramWindow";
            this.Text = "Choose Number Of Clusters";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HistogramWindow_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.histogramPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.PictureBox histogramPicture;
        internal System.Windows.Forms.TextBox numberOfClustersTextBox;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button okButton;
    }
}