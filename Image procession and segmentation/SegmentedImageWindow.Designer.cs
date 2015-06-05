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
            this.imageHistogrm = new System.Windows.Forms.PictureBox();
            this.originalImageLable = new System.Windows.Forms.Label();
            this.segmentedImageLable = new System.Windows.Forms.Label();
            this.imageHistogramLable = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.originalImagePBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmentedImagePBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageHistogrm)).BeginInit();
            this.SuspendLayout();
            // 
            // originalImagePBox
            // 
            this.originalImagePBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.originalImagePBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.originalImagePBox.Location = new System.Drawing.Point(12, 12);
            this.originalImagePBox.Name = "originalImagePBox";
            this.originalImagePBox.Size = new System.Drawing.Size(536, 544);
            this.originalImagePBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.originalImagePBox.TabIndex = 0;
            this.originalImagePBox.TabStop = false;
            // 
            // segmentedImagePBox
            // 
            this.segmentedImagePBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.segmentedImagePBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.segmentedImagePBox.Location = new System.Drawing.Point(554, 12);
            this.segmentedImagePBox.Name = "segmentedImagePBox";
            this.segmentedImagePBox.Size = new System.Drawing.Size(536, 544);
            this.segmentedImagePBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.segmentedImagePBox.TabIndex = 1;
            this.segmentedImagePBox.TabStop = false;
            // 
            // imageHistogrm
            // 
            this.imageHistogrm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.imageHistogrm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imageHistogrm.Location = new System.Drawing.Point(1096, 12);
            this.imageHistogrm.Name = "imageHistogrm";
            this.imageHistogrm.Size = new System.Drawing.Size(536, 544);
            this.imageHistogrm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageHistogrm.TabIndex = 2;
            this.imageHistogrm.TabStop = false;
            // 
            // originalImageLable
            // 
            this.originalImageLable.AutoSize = true;
            this.originalImageLable.BackColor = System.Drawing.SystemColors.ControlText;
            this.originalImageLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.originalImageLable.ForeColor = System.Drawing.Color.Yellow;
            this.originalImageLable.Location = new System.Drawing.Point(12, 12);
            this.originalImageLable.Name = "originalImageLable";
            this.originalImageLable.Size = new System.Drawing.Size(146, 24);
            this.originalImageLable.TabIndex = 3;
            this.originalImageLable.Text = "Original Image";
            // 
            // segmentedImageLable
            // 
            this.segmentedImageLable.AutoSize = true;
            this.segmentedImageLable.BackColor = System.Drawing.SystemColors.ControlText;
            this.segmentedImageLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.segmentedImageLable.ForeColor = System.Drawing.Color.Yellow;
            this.segmentedImageLable.Location = new System.Drawing.Point(554, 12);
            this.segmentedImageLable.Name = "segmentedImageLable";
            this.segmentedImageLable.Size = new System.Drawing.Size(180, 24);
            this.segmentedImageLable.TabIndex = 4;
            this.segmentedImageLable.Text = "Segmented Image";
            // 
            // imageHistogramLable
            // 
            this.imageHistogramLable.AutoSize = true;
            this.imageHistogramLable.BackColor = System.Drawing.SystemColors.ControlText;
            this.imageHistogramLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.imageHistogramLable.ForeColor = System.Drawing.Color.Yellow;
            this.imageHistogramLable.Location = new System.Drawing.Point(1096, 12);
            this.imageHistogramLable.Name = "imageHistogramLable";
            this.imageHistogramLable.Size = new System.Drawing.Size(167, 24);
            this.imageHistogramLable.TabIndex = 5;
            this.imageHistogramLable.Text = "Image Histogram";
            // 
            // SegmentedImageWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1645, 568);
            this.Controls.Add(this.imageHistogramLable);
            this.Controls.Add(this.segmentedImageLable);
            this.Controls.Add(this.originalImageLable);
            this.Controls.Add(this.imageHistogrm);
            this.Controls.Add(this.segmentedImagePBox);
            this.Controls.Add(this.originalImagePBox);
            this.Name = "SegmentedImageWindow";
            this.Text = "Segmented Image";
            ((System.ComponentModel.ISupportInitialize)(this.originalImagePBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmentedImagePBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageHistogrm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.PictureBox originalImagePBox;
        internal System.Windows.Forms.PictureBox segmentedImagePBox;
        internal System.Windows.Forms.PictureBox imageHistogrm;
        internal System.Windows.Forms.Label originalImageLable;
        internal System.Windows.Forms.Label segmentedImageLable;
        internal System.Windows.Forms.Label imageHistogramLable;
    }
}