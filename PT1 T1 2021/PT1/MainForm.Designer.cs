
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
            this.taffFileBox = new System.Windows.Forms.TextBox();
            this.cffFileBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.taffFileHeading = new System.Windows.Forms.TextBox();
            this.cffFileHeading = new System.Windows.Forms.TextBox();
            this.instructionsBox = new System.Windows.Forms.TextBox();
            this.allocationsBox = new System.Windows.Forms.TextBox();
            this.allocationsHeading = new System.Windows.Forms.TextBox();
            this.errorBox = new System.Windows.Forms.TextBox();
            this.errorsHeading = new System.Windows.Forms.TextBox();
            this.instructionBox = new System.Windows.Forms.TextBox();
            this.openTaffFile = new System.Windows.Forms.Button();
            this.validateAllocations = new System.Windows.Forms.Button();
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
            this.menuStrip1.Size = new System.Drawing.Size(1856, 28);
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
            // taffFileBox
            // 
            this.taffFileBox.Location = new System.Drawing.Point(12, 383);
            this.taffFileBox.Multiline = true;
            this.taffFileBox.Name = "taffFileBox";
            this.taffFileBox.ReadOnly = true;
            this.taffFileBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.taffFileBox.Size = new System.Drawing.Size(450, 544);
            this.taffFileBox.TabIndex = 1;
            // 
            // cffFileBox
            // 
            this.cffFileBox.Location = new System.Drawing.Point(468, 383);
            this.cffFileBox.Multiline = true;
            this.cffFileBox.Name = "cffFileBox";
            this.cffFileBox.ReadOnly = true;
            this.cffFileBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.cffFileBox.Size = new System.Drawing.Size(450, 544);
            this.cffFileBox.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // taffFileHeading
            // 
            this.taffFileHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taffFileHeading.Location = new System.Drawing.Point(12, 341);
            this.taffFileHeading.Multiline = true;
            this.taffFileHeading.Name = "taffFileHeading";
            this.taffFileHeading.ReadOnly = true;
            this.taffFileHeading.Size = new System.Drawing.Size(450, 36);
            this.taffFileHeading.TabIndex = 4;
            this.taffFileHeading.Text = "Taff File";
            this.taffFileHeading.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cffFileHeading
            // 
            this.cffFileHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cffFileHeading.Location = new System.Drawing.Point(468, 341);
            this.cffFileHeading.Name = "cffFileHeading";
            this.cffFileHeading.ReadOnly = true;
            this.cffFileHeading.Size = new System.Drawing.Size(450, 36);
            this.cffFileHeading.TabIndex = 5;
            this.cffFileHeading.Text = "Cff File";
            this.cffFileHeading.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // instructionsBox
            // 
            this.instructionsBox.Location = new System.Drawing.Point(12, 73);
            this.instructionsBox.Multiline = true;
            this.instructionsBox.Name = "instructionsBox";
            this.instructionsBox.ReadOnly = true;
            this.instructionsBox.Size = new System.Drawing.Size(1818, 220);
            this.instructionsBox.TabIndex = 6;
            this.instructionsBox.Text = resources.GetString("instructionsBox.Text");
            // 
            // allocationsBox
            // 
            this.allocationsBox.Location = new System.Drawing.Point(924, 383);
            this.allocationsBox.Multiline = true;
            this.allocationsBox.Name = "allocationsBox";
            this.allocationsBox.ReadOnly = true;
            this.allocationsBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.allocationsBox.Size = new System.Drawing.Size(450, 544);
            this.allocationsBox.TabIndex = 7;
            // 
            // allocationsHeading
            // 
            this.allocationsHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.allocationsHeading.Location = new System.Drawing.Point(924, 341);
            this.allocationsHeading.Name = "allocationsHeading";
            this.allocationsHeading.ReadOnly = true;
            this.allocationsHeading.Size = new System.Drawing.Size(450, 36);
            this.allocationsHeading.TabIndex = 8;
            this.allocationsHeading.Text = "Allocations";
            this.allocationsHeading.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // errorBox
            // 
            this.errorBox.Location = new System.Drawing.Point(1380, 383);
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
            this.errorsHeading.Location = new System.Drawing.Point(1380, 341);
            this.errorsHeading.Name = "errorsHeading";
            this.errorsHeading.ReadOnly = true;
            this.errorsHeading.Size = new System.Drawing.Size(450, 36);
            this.errorsHeading.TabIndex = 10;
            this.errorsHeading.Text = "Errors";
            this.errorsHeading.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // instructionBox
            // 
            this.instructionBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.instructionBox.Location = new System.Drawing.Point(12, 31);
            this.instructionBox.Multiline = true;
            this.instructionBox.Name = "instructionBox";
            this.instructionBox.ReadOnly = true;
            this.instructionBox.Size = new System.Drawing.Size(1818, 36);
            this.instructionBox.TabIndex = 12;
            this.instructionBox.Text = "Instructions";
            this.instructionBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // openTaffFile
            // 
            this.openTaffFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openTaffFile.Location = new System.Drawing.Point(12, 299);
            this.openTaffFile.Name = "openTaffFile";
            this.openTaffFile.Size = new System.Drawing.Size(242, 36);
            this.openTaffFile.TabIndex = 13;
            this.openTaffFile.Text = "Open .taff File";
            this.openTaffFile.UseVisualStyleBackColor = true;
            this.openTaffFile.Click += new System.EventHandler(this.openTaffFile_Click);
            // 
            // validateAllocations
            // 
            this.validateAllocations.Enabled = false;
            this.validateAllocations.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.validateAllocations.Location = new System.Drawing.Point(260, 299);
            this.validateAllocations.Name = "validateAllocations";
            this.validateAllocations.Size = new System.Drawing.Size(279, 36);
            this.validateAllocations.TabIndex = 14;
            this.validateAllocations.Text = "Validate Allocations";
            this.validateAllocations.UseVisualStyleBackColor = true;
            this.validateAllocations.Click += new System.EventHandler(this.validateAllocations_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1856, 953);
            this.Controls.Add(this.validateAllocations);
            this.Controls.Add(this.openTaffFile);
            this.Controls.Add(this.instructionBox);
            this.Controls.Add(this.errorsHeading);
            this.Controls.Add(this.errorBox);
            this.Controls.Add(this.allocationsHeading);
            this.Controls.Add(this.allocationsBox);
            this.Controls.Add(this.instructionsBox);
            this.Controls.Add(this.cffFileHeading);
            this.Controls.Add(this.taffFileHeading);
            this.Controls.Add(this.cffFileBox);
            this.Controls.Add(this.taffFileBox);
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
        private System.Windows.Forms.TextBox taffFileBox;
        private System.Windows.Forms.TextBox cffFileBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox taffFileHeading;
        private System.Windows.Forms.TextBox cffFileHeading;
        private System.Windows.Forms.TextBox instructionsBox;
        private System.Windows.Forms.TextBox allocationsBox;
        private System.Windows.Forms.TextBox allocationsHeading;
        private System.Windows.Forms.TextBox errorBox;
        private System.Windows.Forms.TextBox errorsHeading;
        private System.Windows.Forms.TextBox instructionBox;
        private System.Windows.Forms.Button openTaffFile;
        private System.Windows.Forms.Button validateAllocations;
    }
}

