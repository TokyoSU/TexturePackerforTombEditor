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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.fileListView = new System.Windows.Forms.ListView();
            this.fileImageList = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.curStatusStr = new System.Windows.Forms.ToolStripStatusLabel();
            this.clearButton = new System.Windows.Forms.Button();
            this.addJsonCheckbox = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
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
            this.loadTexDirButton.Click += new System.EventHandler(this.loadTexDirButton_Click);
            // 
            // saveAsButton
            // 
            this.saveAsButton.Location = new System.Drawing.Point(180, 12);
            this.saveAsButton.Name = "saveAsButton";
            this.saveAsButton.Size = new System.Drawing.Size(75, 23);
            this.saveAsButton.TabIndex = 2;
            this.saveAsButton.Text = "Save as...";
            this.saveAsButton.UseVisualStyleBackColor = true;
            this.saveAsButton.Click += new System.EventHandler(this.saveAsButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(261, 12);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(398, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // fileListView
            // 
            this.fileListView.HideSelection = false;
            this.fileListView.Location = new System.Drawing.Point(12, 41);
            this.fileListView.Name = "fileListView";
            this.fileListView.Size = new System.Drawing.Size(776, 383);
            this.fileListView.SmallImageList = this.fileImageList;
            this.fileListView.TabIndex = 5;
            this.fileListView.UseCompatibleStateImageBehavior = false;
            // 
            // fileImageList
            // 
            this.fileImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.fileImageList.ImageSize = new System.Drawing.Size(32, 32);
            this.fileImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.curStatusStr});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
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
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.addJsonCheckbox);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.fileListView);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.saveAsButton);
            this.Controls.Add(this.loadTexDirButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Texture Packer";
            this.Load += new System.EventHandler(this.TexturePacker_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button loadTexDirButton;
        private System.Windows.Forms.Button saveAsButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ListView fileListView;
        private System.Windows.Forms.ImageList fileImageList;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel curStatusStr;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.CheckBox addJsonCheckbox;
    }
}

