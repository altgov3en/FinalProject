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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageAnalysisToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToGrayscaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.erodeImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.erodeTheImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.sharpenTheImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.menuStrip1.Size = new System.Drawing.Size(432, 24);
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
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.saveImageToolStripMenuItem.Text = "Save Image";
            // 
            // imageAnalysisToolsToolStripMenuItem
            // 
            this.imageAnalysisToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertToGrayscaleToolStripMenuItem,
            this.erodeTheImageToolStripMenuItem,
            this.erodeImageToolStripMenuItem,
            this.sharpenTheImageToolStripMenuItem});
            this.imageAnalysisToolsToolStripMenuItem.Name = "imageAnalysisToolsToolStripMenuItem";
            this.imageAnalysisToolsToolStripMenuItem.Size = new System.Drawing.Size(130, 20);
            this.imageAnalysisToolsToolStripMenuItem.Text = "Image Analysis Tools";
            this.imageAnalysisToolsToolStripMenuItem.Click += new System.EventHandler(this.imageAnalysisToolsToolStripMenuItem_Click);
            // 
            // convertToGrayscaleToolStripMenuItem
            // 
            this.convertToGrayscaleToolStripMenuItem.Name = "convertToGrayscaleToolStripMenuItem";
            this.convertToGrayscaleToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.convertToGrayscaleToolStripMenuItem.Text = "Convert To Grayscale";
            // 
            // erodeImageToolStripMenuItem
            // 
            this.erodeImageToolStripMenuItem.Name = "erodeImageToolStripMenuItem";
            this.erodeImageToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.erodeImageToolStripMenuItem.Text = "Dilate the Image";
            // 
            // erodeTheImageToolStripMenuItem
            // 
            this.erodeTheImageToolStripMenuItem.Name = "erodeTheImageToolStripMenuItem";
            this.erodeTheImageToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.erodeTheImageToolStripMenuItem.Text = "Erode the Image";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(432, 435);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // sharpenTheImageToolStripMenuItem
            // 
            this.sharpenTheImageToolStripMenuItem.Name = "sharpenTheImageToolStripMenuItem";
            this.sharpenTheImageToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.sharpenTheImageToolStripMenuItem.Text = "Sharpen the Image";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 459);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Image Analysis System";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem openImageToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        internal System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem imageAnalysisToolsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem convertToGrayscaleToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem erodeImageToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem erodeTheImageToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem sharpenTheImageToolStripMenuItem;
    }
}

