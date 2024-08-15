namespace FileManager
{
    partial class Form1
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
            this.OpenOriginButton = new System.Windows.Forms.Button();
            this.OpenDestButton = new System.Windows.Forms.Button();
            this.originLabel = new System.Windows.Forms.Label();
            this.destLabel = new System.Windows.Forms.Label();
            this.OriginFoldersListBox = new System.Windows.Forms.ListBox();
            this.MoveSelectedButton = new System.Windows.Forms.Button();
            this.DestFoldersListBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.originFoldersComboBox = new System.Windows.Forms.ComboBox();
            this.destFoldersComboBox = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip4 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterComboBox = new System.Windows.Forms.ComboBox();
            this.TogglePath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OpenOriginButton
            // 
            this.OpenOriginButton.Location = new System.Drawing.Point(45, 254);
            this.OpenOriginButton.Name = "OpenOriginButton";
            this.OpenOriginButton.Size = new System.Drawing.Size(116, 23);
            this.OpenOriginButton.TabIndex = 0;
            this.OpenOriginButton.Text = "Open Origin Folder";
            this.OpenOriginButton.UseVisualStyleBackColor = true;
            this.OpenOriginButton.Click += new System.EventHandler(this.SelectOriginFolderButton_Click);
            // 
            // OpenDestButton
            // 
            this.OpenDestButton.Location = new System.Drawing.Point(596, 254);
            this.OpenDestButton.Name = "OpenDestButton";
            this.OpenDestButton.Size = new System.Drawing.Size(133, 25);
            this.OpenDestButton.TabIndex = 1;
            this.OpenDestButton.Text = "Open Destination Folder";
            this.OpenDestButton.UseVisualStyleBackColor = true;
            this.OpenDestButton.Click += new System.EventHandler(this.SelectDestinationFolderButton_Click);
            // 
            // originLabel
            // 
            this.originLabel.AutoSize = true;
            this.originLabel.Location = new System.Drawing.Point(167, 259);
            this.originLabel.Name = "originLabel";
            this.originLabel.Size = new System.Drawing.Size(59, 13);
            this.originLabel.TabIndex = 2;
            this.originLabel.Text = "Origin Path";
            // 
            // destLabel
            // 
            this.destLabel.AutoSize = true;
            this.destLabel.Location = new System.Drawing.Point(735, 259);
            this.destLabel.Name = "destLabel";
            this.destLabel.Size = new System.Drawing.Size(85, 13);
            this.destLabel.TabIndex = 3;
            this.destLabel.Text = "Destination Path";
            // 
            // OriginFoldersListBox
            // 
            this.OriginFoldersListBox.FormattingEnabled = true;
            this.OriginFoldersListBox.Location = new System.Drawing.Point(45, 344);
            this.OriginFoldersListBox.Name = "OriginFoldersListBox";
            this.OriginFoldersListBox.Size = new System.Drawing.Size(346, 95);
            this.OriginFoldersListBox.TabIndex = 4;
            // 
            // MoveSelectedButton
            // 
            this.MoveSelectedButton.Location = new System.Drawing.Point(442, 358);
            this.MoveSelectedButton.Name = "MoveSelectedButton";
            this.MoveSelectedButton.Size = new System.Drawing.Size(94, 23);
            this.MoveSelectedButton.TabIndex = 5;
            this.MoveSelectedButton.Text = "Move Selected";
            this.MoveSelectedButton.UseVisualStyleBackColor = true;
            this.MoveSelectedButton.Click += new System.EventHandler(this.MoveSelectedButton_Click);
            // 
            // DestFoldersListBox
            // 
            this.DestFoldersListBox.FormattingEnabled = true;
            this.DestFoldersListBox.Location = new System.Drawing.Point(596, 344);
            this.DestFoldersListBox.Name = "DestFoldersListBox";
            this.DestFoldersListBox.Size = new System.Drawing.Size(352, 95);
            this.DestFoldersListBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(146, 324);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "List of Items in Origin Folder";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(692, 324);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "List of Items in Destination Folder";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.label5.Location = new System.Drawing.Point(410, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 19);
            this.label5.TabIndex = 9;
            this.label5.Text = "Michael\'s File Mover!";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(42, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(254, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Step 1: Select the folder you wish to move a file from";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(42, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(288, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Step 2: Select the folder you would like to move your files to";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(42, 151);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(276, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Step 3: Press Move Selected to move all the files chosen";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(42, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(342, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Note: If no files are selected it will move all the files in the displayed list. " +
    "";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(72, 204);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(283, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "By pressing CTRL + left click, you can select multiple items";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(442, 396);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(94, 23);
            this.progressBar1.TabIndex = 15;
            this.progressBar1.Visible = false;
            // 
            // originFoldersComboBox
            // 
            this.originFoldersComboBox.FormattingEnabled = true;
            this.originFoldersComboBox.Location = new System.Drawing.Point(45, 283);
            this.originFoldersComboBox.Name = "originFoldersComboBox";
            this.originFoldersComboBox.Size = new System.Drawing.Size(121, 21);
            this.originFoldersComboBox.TabIndex = 16;
            this.originFoldersComboBox.SelectedIndexChanged += new System.EventHandler(this.OriginComboBox_SelectedIndexChanged);
            // 
            // destFoldersComboBox
            // 
            this.destFoldersComboBox.FormattingEnabled = true;
            this.destFoldersComboBox.Location = new System.Drawing.Point(596, 285);
            this.destFoldersComboBox.Name = "destFoldersComboBox";
            this.destFoldersComboBox.Size = new System.Drawing.Size(121, 21);
            this.destFoldersComboBox.TabIndex = 17;
            this.destFoldersComboBox.SelectedIndexChanged += new System.EventHandler(this.DestinationComboBox_SelectedIndexChanged);
            // 
            // contextMenuStrip4
            // 
            this.contextMenuStrip4.Name = "contextMenuStrip4";
            this.contextMenuStrip4.Size = new System.Drawing.Size(61, 4);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // FilterComboBox
            // 
            this.FilterComboBox.FormattingEnabled = true;
            this.FilterComboBox.Location = new System.Drawing.Point(827, 62);
            this.FilterComboBox.Name = "FilterComboBox";
            this.FilterComboBox.Size = new System.Drawing.Size(121, 21);
            this.FilterComboBox.TabIndex = 18;
            this.FilterComboBox.SelectedIndexChanged += new System.EventHandler(this.FilterComboBox_SelectedIndexChanged);
            // 
            // TogglePath
            // 
            this.TogglePath.Location = new System.Drawing.Point(442, 259);
            this.TogglePath.Name = "TogglePath";
            this.TogglePath.Size = new System.Drawing.Size(75, 23);
            this.TogglePath.TabIndex = 19;
            this.TogglePath.Text = "Toggle Path";
            this.TogglePath.UseVisualStyleBackColor = true;
            this.TogglePath.Click += new System.EventHandler(this.TogglePath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(415, 229);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Toggle filename/filepath";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 499);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TogglePath);
            this.Controls.Add(this.FilterComboBox);
            this.Controls.Add(this.destFoldersComboBox);
            this.Controls.Add(this.originFoldersComboBox);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DestFoldersListBox);
            this.Controls.Add(this.MoveSelectedButton);
            this.Controls.Add(this.OriginFoldersListBox);
            this.Controls.Add(this.destLabel);
            this.Controls.Add(this.originLabel);
            this.Controls.Add(this.OpenDestButton);
            this.Controls.Add(this.OpenOriginButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenOriginButton;
        private System.Windows.Forms.Button OpenDestButton;
        private System.Windows.Forms.Label originLabel;
        private System.Windows.Forms.Label destLabel;
        private System.Windows.Forms.ListBox OriginFoldersListBox;
        private System.Windows.Forms.Button MoveSelectedButton;
        private System.Windows.Forms.ListBox DestFoldersListBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ComboBox originFoldersComboBox;
        private System.Windows.Forms.ComboBox destFoldersComboBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip4;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ComboBox FilterComboBox;
        private System.Windows.Forms.Button TogglePath;
        private System.Windows.Forms.Label label1;
    }
}

