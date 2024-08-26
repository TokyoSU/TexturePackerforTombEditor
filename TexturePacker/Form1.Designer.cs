namespace TexturePacker
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.loadTexDirButton = new System.Windows.Forms.Button();
            this.saveAsButton = new System.Windows.Forms.Button();
            this.StatusBar = new System.Windows.Forms.ProgressBar();
            this.FileListView = new System.Windows.Forms.ListView();
            this.FileImageList = new System.Windows.Forms.ImageList(this.components);
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.curStatusStr = new System.Windows.Forms.ToolStripStatusLabel();
            this.clearButton = new System.Windows.Forms.Button();
            this.addJsonCheckbox = new System.Windows.Forms.CheckBox();
            this.ImageDrawTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.maxAtlasXSize = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.maxAtlasYSize = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LoadImageWorker = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // loadTexDirButton
            // 
            this.loadTexDirButton.Location = new System.Drawing.Point(12, 12);
            this.loadTexDirButton.Name = "loadTexDirButton";
            this.loadTexDirButton.Size = new System.Drawing.Size(79, 23);
            this.loadTexDirButton.TabIndex = 1;
            this.loadTexDirButton.Text = "Load textures";
            this.loadTexDirButton.UseVisualStyleBackColor = true;
            this.loadTexDirButton.Click += new System.EventHandler(this.LoadTextureButtonClick);
            // 
            // saveAsButton
            // 
            this.saveAsButton.Location = new System.Drawing.Point(180, 12);
            this.saveAsButton.Name = "saveAsButton";
            this.saveAsButton.Size = new System.Drawing.Size(75, 23);
            this.saveAsButton.TabIndex = 2;
            this.saveAsButton.Text = "Save as...";
            this.saveAsButton.UseVisualStyleBackColor = true;
            this.saveAsButton.Click += new System.EventHandler(this.SaveAsButton_AsClick);
            // 
            // StatusBar
            // 
            this.StatusBar.Location = new System.Drawing.Point(261, 12);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(398, 23);
            this.StatusBar.TabIndex = 3;
            // 
            // FileListView
            // 
            this.FileListView.HideSelection = false;
            this.FileListView.Location = new System.Drawing.Point(12, 73);
            this.FileListView.Name = "FileListView";
            this.FileListView.Size = new System.Drawing.Size(776, 351);
            this.FileListView.SmallImageList = this.FileImageList;
            this.FileListView.TabIndex = 5;
            this.FileListView.UseCompatibleStateImageBehavior = false;
            // 
            // FileImageList
            // 
            this.FileImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.FileImageList.ImageSize = new System.Drawing.Size(32, 32);
            this.FileImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.curStatusStr});
            this.StatusStrip.Location = new System.Drawing.Point(0, 428);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(800, 22);
            this.StatusStrip.TabIndex = 6;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // curStatusStr
            // 
            this.curStatusStr.Name = "curStatusStr";
            this.curStatusStr.Size = new System.Drawing.Size(22, 17);
            this.curStatusStr.Text = "%s";
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(97, 12);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(77, 23);
            this.clearButton.TabIndex = 7;
            this.clearButton.Text = "Clear list";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // addJsonCheckbox
            // 
            this.addJsonCheckbox.AutoSize = true;
            this.addJsonCheckbox.Location = new System.Drawing.Point(665, 16);
            this.addJsonCheckbox.Name = "addJsonCheckbox";
            this.addJsonCheckbox.Size = new System.Drawing.Size(130, 17);
            this.addJsonCheckbox.TabIndex = 8;
            this.addJsonCheckbox.Text = "Add json when saving";
            this.addJsonCheckbox.UseVisualStyleBackColor = true;
            // 
            // ImageDrawTypeComboBox
            // 
            this.ImageDrawTypeComboBox.FormattingEnabled = true;
            this.ImageDrawTypeComboBox.Items.AddRange(new object[] {
            "LargeIcon",
            "Detail",
            "SmallIcon",
            "List",
            "Tile"});
            this.ImageDrawTypeComboBox.Location = new System.Drawing.Point(586, 46);
            this.ImageDrawTypeComboBox.Name = "ImageDrawTypeComboBox";
            this.ImageDrawTypeComboBox.Size = new System.Drawing.Size(200, 21);
            this.ImageDrawTypeComboBox.TabIndex = 9;
            this.ImageDrawTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.ImageDrawTypeComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(492, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Image draw type:";
            // 
            // maxAtlasXSize
            // 
            this.maxAtlasXSize.Location = new System.Drawing.Point(70, 46);
            this.maxAtlasXSize.Name = "maxAtlasXSize";
            this.maxAtlasXSize.Size = new System.Drawing.Size(100, 20);
            this.maxAtlasXSize.TabIndex = 11;
            this.maxAtlasXSize.Text = "-1";
            this.toolTip1.SetToolTip(this.maxAtlasXSize, "Infinite for -1, else max size in width");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Max size:";
            // 
            // maxAtlasYSize
            // 
            this.maxAtlasYSize.Location = new System.Drawing.Point(194, 46);
            this.maxAtlasYSize.Name = "maxAtlasYSize";
            this.maxAtlasYSize.Size = new System.Drawing.Size(100, 20);
            this.maxAtlasYSize.TabIndex = 13;
            this.maxAtlasYSize.Text = "-1";
            this.toolTip1.SetToolTip(this.maxAtlasYSize, "Infinite for -1, else max size in height");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(177, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "x";
            // 
            // LoadImageWorker
            // 
            this.LoadImageWorker.WorkerReportsProgress = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.maxAtlasYSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.maxAtlasXSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ImageDrawTypeComboBox);
            this.Controls.Add(this.addJsonCheckbox);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.FileListView);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.saveAsButton);
            this.Controls.Add(this.loadTexDirButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Texture Packer";
            this.Load += new System.EventHandler(this.TexturePackerStart);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button loadTexDirButton;
        private System.Windows.Forms.Button saveAsButton;
        private System.Windows.Forms.ProgressBar StatusBar;
        private System.Windows.Forms.ListView FileListView;
        private System.Windows.Forms.ImageList FileImageList;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel curStatusStr;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.CheckBox addJsonCheckbox;
        private System.Windows.Forms.ComboBox ImageDrawTypeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox maxAtlasXSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox maxAtlasYSize;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker LoadImageWorker;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

