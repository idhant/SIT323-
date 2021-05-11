
namespace PT1
{
    partial class AllocationForm
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
            this.alocationDetails = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // alocationDetails
            // 
            this.alocationDetails.Location = new System.Drawing.Point(12, 12);
            this.alocationDetails.Multiline = true;
            this.alocationDetails.Name = "alocationDetails";
            this.alocationDetails.ReadOnly = true;
            this.alocationDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.alocationDetails.Size = new System.Drawing.Size(776, 426);
            this.alocationDetails.TabIndex = 0;
            // 
            // AllocationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.alocationDetails);
            this.Name = "AllocationForm";
            this.Text = "AllocationForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox alocationDetails;
    }
}