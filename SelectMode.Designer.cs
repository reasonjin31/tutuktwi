namespace TOOK
{
    partial class SelectMode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectMode));
            this.Select_Stamp = new System.Windows.Forms.Button();
            this.Select_Default = new System.Windows.Forms.Button();
            this.Select_Dot = new System.Windows.Forms.Button();
            this.Select_Ring = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Select_Stamp
            // 
            this.Select_Stamp.BackColor = System.Drawing.Color.Transparent;
            this.Select_Stamp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Select_Stamp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(50)))), ((int)(((byte)(95)))));
            this.Select_Stamp.FlatAppearance.BorderSize = 0;
            this.Select_Stamp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Select_Stamp.Image = ((System.Drawing.Image)(resources.GetObject("Select_Stamp.Image")));
            this.Select_Stamp.Location = new System.Drawing.Point(98, 1);
            this.Select_Stamp.Name = "Select_Stamp";
            this.Select_Stamp.Size = new System.Drawing.Size(75, 123);
            this.Select_Stamp.TabIndex = 0;
            this.Select_Stamp.TabStop = false;
            this.Select_Stamp.UseVisualStyleBackColor = false;
            this.Select_Stamp.Click += new System.EventHandler(this.Select_Stamp_Click);
            // 
            // Select_Default
            // 
            this.Select_Default.BackColor = System.Drawing.Color.Transparent;
            this.Select_Default.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Select_Default.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(50)))), ((int)(((byte)(95)))));
            this.Select_Default.FlatAppearance.BorderSize = 0;
            this.Select_Default.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Select_Default.Image = ((System.Drawing.Image)(resources.GetObject("Select_Default.Image")));
            this.Select_Default.Location = new System.Drawing.Point(12, 1);
            this.Select_Default.Name = "Select_Default";
            this.Select_Default.Size = new System.Drawing.Size(75, 123);
            this.Select_Default.TabIndex = 0;
            this.Select_Default.TabStop = false;
            this.Select_Default.UseVisualStyleBackColor = false;
            this.Select_Default.Click += new System.EventHandler(this.Select_Default_Click);
            // 
            // Select_Dot
            // 
            this.Select_Dot.BackColor = System.Drawing.Color.Transparent;
            this.Select_Dot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Select_Dot.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(50)))), ((int)(((byte)(95)))));
            this.Select_Dot.FlatAppearance.BorderSize = 0;
            this.Select_Dot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Select_Dot.Image = ((System.Drawing.Image)(resources.GetObject("Select_Dot.Image")));
            this.Select_Dot.Location = new System.Drawing.Point(193, 1);
            this.Select_Dot.Name = "Select_Dot";
            this.Select_Dot.Size = new System.Drawing.Size(75, 123);
            this.Select_Dot.TabIndex = 0;
            this.Select_Dot.TabStop = false;
            this.Select_Dot.UseVisualStyleBackColor = false;
            this.Select_Dot.Click += new System.EventHandler(this.Select_Dot_Click);
            // 
            // Select_Ring
            // 
            this.Select_Ring.BackColor = System.Drawing.Color.Transparent;
            this.Select_Ring.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Select_Ring.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(50)))), ((int)(((byte)(95)))));
            this.Select_Ring.FlatAppearance.BorderSize = 0;
            this.Select_Ring.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Select_Ring.Image = ((System.Drawing.Image)(resources.GetObject("Select_Ring.Image")));
            this.Select_Ring.Location = new System.Drawing.Point(286, 1);
            this.Select_Ring.Name = "Select_Ring";
            this.Select_Ring.Size = new System.Drawing.Size(75, 123);
            this.Select_Ring.TabIndex = 0;
            this.Select_Ring.TabStop = false;
            this.Select_Ring.UseVisualStyleBackColor = false;
            this.Select_Ring.Click += new System.EventHandler(this.Select_Ring_Click);
            // 
            // SelectMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(50)))), ((int)(((byte)(95)))));
            this.ClientSize = new System.Drawing.Size(376, 125);
            this.Controls.Add(this.Select_Default);
            this.Controls.Add(this.Select_Dot);
            this.Controls.Add(this.Select_Ring);
            this.Controls.Add(this.Select_Stamp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelectMode";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SelectMode";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Select_Stamp;
        private System.Windows.Forms.Button Select_Default;
        private System.Windows.Forms.Button Select_Dot;
        private System.Windows.Forms.Button Select_Ring;

    }
}