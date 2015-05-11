namespace Image_procession_and_segmentation
{
    partial class SegmentedImageWindow
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
            this.originalImagePBox = new System.Windows.Forms.PictureBox();
            this.segmentedImagePBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.originalImagePBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmentedImagePBox)).BeginInit();
            this.SuspendLayout();
            // 
            // originalImagePBox
            // 
            this.originalImagePBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.originalImagePBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.originalImagePBox.Location = new System.Drawing.Point(12, 12);
            this.originalImagePBox.Name = "originalImagePBox";
            this.originalImagePBox.Size = new System.Drawing.Size(531, 544);
            this.originalImagePBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.originalImagePBox.TabIndex = 0;
            this.originalImagePBox.TabStop = false;
            // 
            // segmentedImagePBox
            // 
            this.segmentedImagePBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.segmentedImagePBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.segmentedImagePBox.Location = new System.Drawing.Point(549, 12);
            this.segmentedImagePBox.Name = "segmentedImagePBox";
            this.segmentedImagePBox.Size = new System.Drawing.Size(536, 544);
            this.segmentedImagePBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.segmentedImagePBox.TabIndex = 1;
            this.segmentedImagePBox.TabStop = false;
            // 
            // SegmentedImageWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 568);
            this.Controls.Add(this.segmentedImagePBox);
            this.Controls.Add(this.originalImagePBox);
            this.Name = "SegmentedImageWindow";
            this.Text = "Segmented Image";
            ((System.ComponentModel.ISupportInitialize)(this.originalImagePBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmentedImagePBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.PictureBox originalImagePBox;
        internal System.Windows.Forms.PictureBox segmentedImagePBox;
    }
}