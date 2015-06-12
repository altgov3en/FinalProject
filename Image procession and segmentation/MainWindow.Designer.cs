using AForge.Imaging;
namespace Image_procession_and_segmentation
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageAnalysisToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToGrayscaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.erodeTheImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sharpenTheImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.estimateNumberOfClustersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.divideImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.intoEstimatedNumberOfClustersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.intoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.runOverallDiagnosisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.showHistogramButton = new System.Windows.Forms.Button();
            this.openedImageLable = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.imageAnalysisToolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(670, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openImageToolStripMenuItem,
            this.saveImageToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.fileToolStripMenuItem.Text = "Image";
            // 
            // openImageToolStripMenuItem
            // 
            this.openImageToolStripMenuItem.Name = "openImageToolStripMenuItem";
            this.openImageToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.openImageToolStripMenuItem.Text = "Open Image";
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Enabled = false;
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.saveImageToolStripMenuItem.Text = "Save Image";
            // 
            // imageAnalysisToolsToolStripMenuItem
            // 
            this.imageAnalysisToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertToGrayscaleToolStripMenuItem,
            this.erodeTheImageToolStripMenuItem,
            this.sharpenTheImageToolStripMenuItem,
            this.estimateNumberOfClustersToolStripMenuItem,
            this.divideImageToolStripMenuItem,
            this.toolStripMenuItem2,
            this.runOverallDiagnosisToolStripMenuItem});
            this.imageAnalysisToolsToolStripMenuItem.Enabled = false;
            this.imageAnalysisToolsToolStripMenuItem.Name = "imageAnalysisToolsToolStripMenuItem";
            this.imageAnalysisToolsToolStripMenuItem.Size = new System.Drawing.Size(130, 20);
            this.imageAnalysisToolsToolStripMenuItem.Text = "Image Analysis Tools";
            // 
            // convertToGrayscaleToolStripMenuItem
            // 
            this.convertToGrayscaleToolStripMenuItem.Name = "convertToGrayscaleToolStripMenuItem";
            this.convertToGrayscaleToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.convertToGrayscaleToolStripMenuItem.Text = "Convert To Grayscale";
            // 
            // erodeTheImageToolStripMenuItem
            // 
            this.erodeTheImageToolStripMenuItem.Enabled = false;
            this.erodeTheImageToolStripMenuItem.Name = "erodeTheImageToolStripMenuItem";
            this.erodeTheImageToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.erodeTheImageToolStripMenuItem.Text = "Erode the Image";
            // 
            // sharpenTheImageToolStripMenuItem
            // 
            this.sharpenTheImageToolStripMenuItem.Enabled = false;
            this.sharpenTheImageToolStripMenuItem.Name = "sharpenTheImageToolStripMenuItem";
            this.sharpenTheImageToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.sharpenTheImageToolStripMenuItem.Text = "Sharpen the Image";
            // 
            // estimateNumberOfClustersToolStripMenuItem
            // 
            this.estimateNumberOfClustersToolStripMenuItem.Enabled = false;
            this.estimateNumberOfClustersToolStripMenuItem.Name = "estimateNumberOfClustersToolStripMenuItem";
            this.estimateNumberOfClustersToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.estimateNumberOfClustersToolStripMenuItem.Text = "Estimate Number Of Clusters";
            // 
            // divideImageToolStripMenuItem
            // 
            this.divideImageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.intoEstimatedNumberOfClustersToolStripMenuItem,
            this.intoToolStripMenuItem});
            this.divideImageToolStripMenuItem.Enabled = false;
            this.divideImageToolStripMenuItem.Name = "divideImageToolStripMenuItem";
            this.divideImageToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.divideImageToolStripMenuItem.Text = "Divide Image To...";
            // 
            // intoEstimatedNumberOfClustersToolStripMenuItem
            // 
            this.intoEstimatedNumberOfClustersToolStripMenuItem.Enabled = false;
            this.intoEstimatedNumberOfClustersToolStripMenuItem.Name = "intoEstimatedNumberOfClustersToolStripMenuItem";
            this.intoEstimatedNumberOfClustersToolStripMenuItem.Size = new System.Drawing.Size(234, 34);
            this.intoEstimatedNumberOfClustersToolStripMenuItem.Text = "Estimated Number Of Clusters\n(Never Estimated)";
            // 
            // intoToolStripMenuItem
            // 
            this.intoToolStripMenuItem.Name = "intoToolStripMenuItem";
            this.intoToolStripMenuItem.Size = new System.Drawing.Size(234, 34);
            this.intoToolStripMenuItem.Text = "Custom Number Of Clusers";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Enabled = false;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(229, 22);
            this.toolStripMenuItem2.Text = "*******************************";
            // 
            // runOverallDiagnosisToolStripMenuItem
            // 
            this.runOverallDiagnosisToolStripMenuItem.Enabled = false;
            this.runOverallDiagnosisToolStripMenuItem.Name = "runOverallDiagnosisToolStripMenuItem";
            this.runOverallDiagnosisToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.runOverallDiagnosisToolStripMenuItem.Text = "Run Overall Diagnosis";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(670, 394);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // showHistogramButton
            // 
            this.showHistogramButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.showHistogramButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.showHistogramButton.Enabled = false;
            this.showHistogramButton.Location = new System.Drawing.Point(244, 383);
            this.showHistogramButton.Name = "showHistogramButton";
            this.showHistogramButton.Size = new System.Drawing.Size(183, 23);
            this.showHistogramButton.TabIndex = 2;
            this.showHistogramButton.Text = "Show Image Histogram";
            this.showHistogramButton.UseVisualStyleBackColor = true;
            this.showHistogramButton.Visible = false;
            // 
            // openedImageLable
            // 
            this.openedImageLable.AutoSize = true;
            this.openedImageLable.BackColor = System.Drawing.SystemColors.Control;
            this.openedImageLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.openedImageLable.ForeColor = System.Drawing.Color.Black;
            this.openedImageLable.Location = new System.Drawing.Point(0, 106);
            this.openedImageLable.Name = "openedImageLable";
            this.openedImageLable.Size = new System.Drawing.Size(88, 15);
            this.openedImageLable.TabIndex = 4;
            this.openedImageLable.Text = "Original Image";
            this.openedImageLable.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.openedImageLable.Visible = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 418);
            this.Controls.Add(this.openedImageLable);
            this.Controls.Add(this.showHistogramButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Image Analysis System";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem openImageToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        internal System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.ToolStripMenuItem imageAnalysisToolsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem convertToGrayscaleToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem erodeTheImageToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem sharpenTheImageToolStripMenuItem;
        internal System.Windows.Forms.Button showHistogramButton;
        internal System.Windows.Forms.ToolStripMenuItem runOverallDiagnosisToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem estimateNumberOfClustersToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem divideImageToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem intoEstimatedNumberOfClustersToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem intoToolStripMenuItem;
        internal System.Windows.Forms.Label openedImageLable;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    }

}

