
namespace PT1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allocationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.errorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.TaffFile = new System.Windows.Forms.TextBox();
            this.CffFile = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TaffFileName = new System.Windows.Forms.TextBox();
            this.CffFileName = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.allocationDetails = new System.Windows.Forms.TextBox();
            this.allocationsHeading = new System.Windows.Forms.TextBox();
            this.errorBox = new System.Windows.Forms.TextBox();
            this.errorsHeading = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.viewToolStripMenuItem1,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1902, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allocationsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            this.viewToolStripMenuItem.Text = "Validate";
            // 
            // allocationsToolStripMenuItem
            // 
            this.allocationsToolStripMenuItem.Enabled = false;
            this.allocationsToolStripMenuItem.Name = "allocationsToolStripMenuItem";
            this.allocationsToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.allocationsToolStripMenuItem.Text = "Allocations";
            this.allocationsToolStripMenuItem.Click += new System.EventHandler(this.allocationsToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem1
            // 
            this.viewToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.errorsToolStripMenuItem});
            this.viewToolStripMenuItem1.Name = "viewToolStripMenuItem1";
            this.viewToolStripMenuItem1.Size = new System.Drawing.Size(55, 24);
            this.viewToolStripMenuItem1.Text = "View";
            // 
            // errorsToolStripMenuItem
            // 
            this.errorsToolStripMenuItem.Enabled = false;
            this.errorsToolStripMenuItem.Name = "errorsToolStripMenuItem";
            this.errorsToolStripMenuItem.Size = new System.Drawing.Size(130, 26);
            this.errorsToolStripMenuItem.Text = "Errors";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(133, 26);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // TaffFile
            // 
            this.TaffFile.Location = new System.Drawing.Point(12, 160);
            this.TaffFile.Multiline = true;
            this.TaffFile.Name = "TaffFile";
            this.TaffFile.ReadOnly = true;
            this.TaffFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TaffFile.Size = new System.Drawing.Size(450, 544);
            this.TaffFile.TabIndex = 1;
            // 
            // CffFile
            // 
            this.CffFile.Location = new System.Drawing.Point(468, 160);
            this.CffFile.Multiline = true;
            this.CffFile.Name = "CffFile";
            this.CffFile.ReadOnly = true;
            this.CffFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CffFile.Size = new System.Drawing.Size(450, 544);
            this.CffFile.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // TaffFileName
            // 
            this.TaffFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TaffFileName.Location = new System.Drawing.Point(12, 118);
            this.TaffFileName.Multiline = true;
            this.TaffFileName.Name = "TaffFileName";
            this.TaffFileName.ReadOnly = true;
            this.TaffFileName.Size = new System.Drawing.Size(450, 36);
            this.TaffFileName.TabIndex = 4;
            this.TaffFileName.Text = "Taff File";
            this.TaffFileName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CffFileName
            // 
            this.CffFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CffFileName.Location = new System.Drawing.Point(468, 118);
            this.CffFileName.Name = "CffFileName";
            this.CffFileName.ReadOnly = true;
            this.CffFileName.Size = new System.Drawing.Size(450, 36);
            this.CffFileName.TabIndex = 5;
            this.CffFileName.Text = "Cff File";
            this.CffFileName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 31);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(1105, 81);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // allocationDetails
            // 
            this.allocationDetails.Location = new System.Drawing.Point(924, 160);
            this.allocationDetails.Multiline = true;
            this.allocationDetails.Name = "allocationDetails";
            this.allocationDetails.ReadOnly = true;
            this.allocationDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.allocationDetails.Size = new System.Drawing.Size(450, 544);
            this.allocationDetails.TabIndex = 7;
            // 
            // allocationsHeading
            // 
            this.allocationsHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.allocationsHeading.Location = new System.Drawing.Point(924, 118);
            this.allocationsHeading.Name = "allocationsHeading";
            this.allocationsHeading.ReadOnly = true;
            this.allocationsHeading.Size = new System.Drawing.Size(450, 36);
            this.allocationsHeading.TabIndex = 8;
            this.allocationsHeading.Text = "Allocations";
            this.allocationsHeading.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // errorBox
            // 
            this.errorBox.Location = new System.Drawing.Point(1380, 160);
            this.errorBox.Multiline = true;
            this.errorBox.Name = "errorBox";
            this.errorBox.ReadOnly = true;
            this.errorBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.errorBox.Size = new System.Drawing.Size(450, 544);
            this.errorBox.TabIndex = 9;
            // 
            // errorsHeading
            // 
            this.errorsHeading.BackColor = System.Drawing.SystemColors.Control;
            this.errorsHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorsHeading.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorsHeading.Location = new System.Drawing.Point(1380, 118);
            this.errorsHeading.Name = "errorsHeading";
            this.errorsHeading.ReadOnly = true;
            this.errorsHeading.Size = new System.Drawing.Size(450, 36);
            this.errorsHeading.TabIndex = 10;
            this.errorsHeading.Text = "Errors";
            this.errorsHeading.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1902, 953);
            this.Controls.Add(this.errorsHeading);
            this.Controls.Add(this.errorBox);
            this.Controls.Add(this.allocationsHeading);
            this.Controls.Add(this.allocationDetails);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.CffFileName);
            this.Controls.Add(this.TaffFileName);
            this.Controls.Add(this.CffFile);
            this.Controls.Add(this.TaffFile);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "PT1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allocationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem errorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox TaffFile;
        private System.Windows.Forms.TextBox CffFile;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox TaffFileName;
        private System.Windows.Forms.TextBox CffFileName;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox allocationDetails;
        private System.Windows.Forms.TextBox allocationsHeading;
        private System.Windows.Forms.TextBox errorBox;
        private System.Windows.Forms.TextBox errorsHeading;
    }
}

