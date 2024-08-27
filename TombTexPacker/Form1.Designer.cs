namespace TombTexPacker
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ListView = new ListView();
            ImageListLarge = new ImageList(components);
            ImageListSmall = new ImageList(components);
            ToolStrip = new ToolStrip();
            ToolStripLabel = new ToolStripLabel();
            LoadTextureButton = new Button();
            SaveTextureButton = new Button();
            ClearListButton = new Button();
            ViewType = new ComboBox();
            ViewTypeLabel = new Label();
            ProgressBar = new ProgressBar();
            SaveJsonCheckbox = new CheckBox();
            RequireResizeCheckbox = new CheckBox();
            label1 = new Label();
            MaxSizeText = new TextBox();
            RequireSizeText = new TextBox();
            label2 = new Label();
            ToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // ListView
            // 
            ListView.LargeImageList = ImageListLarge;
            ListView.Location = new Point(12, 73);
            ListView.Name = "ListView";
            ListView.Size = new Size(776, 349);
            ListView.SmallImageList = ImageListSmall;
            ListView.TabIndex = 0;
            ListView.UseCompatibleStateImageBehavior = false;
            // 
            // ImageListLarge
            // 
            ImageListLarge.ColorDepth = ColorDepth.Depth32Bit;
            ImageListLarge.ImageSize = new Size(16, 16);
            ImageListLarge.TransparentColor = Color.Transparent;
            // 
            // ImageListSmall
            // 
            ImageListSmall.ColorDepth = ColorDepth.Depth32Bit;
            ImageListSmall.ImageSize = new Size(16, 16);
            ImageListSmall.TransparentColor = Color.Transparent;
            // 
            // ToolStrip
            // 
            ToolStrip.Dock = DockStyle.Bottom;
            ToolStrip.Items.AddRange(new ToolStripItem[] { ToolStripLabel });
            ToolStrip.Location = new Point(0, 425);
            ToolStrip.Name = "ToolStrip";
            ToolStrip.Size = new Size(800, 25);
            ToolStrip.TabIndex = 1;
            ToolStrip.Text = "toolStrip1";
            // 
            // ToolStripLabel
            // 
            ToolStripLabel.Name = "ToolStripLabel";
            ToolStripLabel.Size = new Size(22, 22);
            ToolStripLabel.Text = "%s";
            // 
            // LoadTextureButton
            // 
            LoadTextureButton.Location = new Point(12, 12);
            LoadTextureButton.Name = "LoadTextureButton";
            LoadTextureButton.Size = new Size(94, 23);
            LoadTextureButton.TabIndex = 2;
            LoadTextureButton.Text = "Load textures";
            LoadTextureButton.UseVisualStyleBackColor = true;
            LoadTextureButton.Click += LoadTextureButton_Click;
            // 
            // SaveTextureButton
            // 
            SaveTextureButton.Location = new Point(211, 12);
            SaveTextureButton.Name = "SaveTextureButton";
            SaveTextureButton.Size = new Size(94, 23);
            SaveTextureButton.TabIndex = 3;
            SaveTextureButton.Text = "Save textures";
            SaveTextureButton.UseVisualStyleBackColor = true;
            SaveTextureButton.Click += SaveTextureButton_Click;
            // 
            // ClearListButton
            // 
            ClearListButton.Location = new Point(111, 12);
            ClearListButton.Name = "ClearListButton";
            ClearListButton.Size = new Size(94, 23);
            ClearListButton.TabIndex = 4;
            ClearListButton.Text = "Clear list";
            ClearListButton.UseVisualStyleBackColor = true;
            ClearListButton.Click += ClearListButton_Click;
            // 
            // ViewType
            // 
            ViewType.FormattingEnabled = true;
            ViewType.Items.AddRange(new object[] { "LargeIcon", "SmallIcon" });
            ViewType.Location = new Point(656, 44);
            ViewType.Name = "ViewType";
            ViewType.Size = new Size(132, 23);
            ViewType.TabIndex = 0;
            ViewType.SelectedIndexChanged += ViewType_SelectedIndexChanged;
            // 
            // ViewTypeLabel
            // 
            ViewTypeLabel.AutoSize = true;
            ViewTypeLabel.Location = new Point(589, 47);
            ViewTypeLabel.Name = "ViewTypeLabel";
            ViewTypeLabel.Size = new Size(61, 15);
            ViewTypeLabel.TabIndex = 6;
            ViewTypeLabel.Text = "View type:";
            // 
            // ProgressBar
            // 
            ProgressBar.Location = new Point(486, 12);
            ProgressBar.Name = "ProgressBar";
            ProgressBar.Size = new Size(302, 23);
            ProgressBar.TabIndex = 7;
            // 
            // SaveJsonCheckbox
            // 
            SaveJsonCheckbox.AutoSize = true;
            SaveJsonCheckbox.Location = new Point(311, 15);
            SaveJsonCheckbox.Name = "SaveJsonCheckbox";
            SaveJsonCheckbox.Size = new Size(83, 19);
            SaveJsonCheckbox.TabIndex = 8;
            SaveJsonCheckbox.Text = "Save json ?";
            SaveJsonCheckbox.UseVisualStyleBackColor = true;
            // 
            // RequireResizeCheckbox
            // 
            RequireResizeCheckbox.AutoSize = true;
            RequireResizeCheckbox.Location = new Point(408, 45);
            RequireResizeCheckbox.Name = "RequireResizeCheckbox";
            RequireResizeCheckbox.Size = new Size(164, 19);
            RequireResizeCheckbox.TabIndex = 9;
            RequireResizeCheckbox.Text = "Require resize when load ?";
            RequireResizeCheckbox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 46);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 10;
            label1.Text = "Max size:";
            // 
            // MaxSizeText
            // 
            MaxSizeText.Location = new Point(72, 42);
            MaxSizeText.Name = "MaxSizeText";
            MaxSizeText.Size = new Size(115, 23);
            MaxSizeText.TabIndex = 11;
            MaxSizeText.Text = "256x-1";
            MaxSizeText.TextAlign = HorizontalAlignment.Center;
            // 
            // RequireSizeText
            // 
            RequireSizeText.Location = new Point(276, 43);
            RequireSizeText.Name = "RequireSizeText";
            RequireSizeText.Size = new Size(115, 23);
            RequireSizeText.TabIndex = 13;
            RequireSizeText.Text = "64x64";
            RequireSizeText.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(198, 46);
            label2.Name = "label2";
            label2.Size = new Size(72, 15);
            label2.TabIndex = 12;
            label2.Text = "Require size:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(RequireSizeText);
            Controls.Add(label2);
            Controls.Add(MaxSizeText);
            Controls.Add(label1);
            Controls.Add(RequireResizeCheckbox);
            Controls.Add(SaveJsonCheckbox);
            Controls.Add(ProgressBar);
            Controls.Add(ViewTypeLabel);
            Controls.Add(ViewType);
            Controls.Add(ClearListButton);
            Controls.Add(SaveTextureButton);
            Controls.Add(LoadTextureButton);
            Controls.Add(ToolStrip);
            Controls.Add(ListView);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            Text = "Tomb Texture Packer";
            ToolStrip.ResumeLayout(false);
            ToolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView ListView;
        private ToolStrip ToolStrip;
        private Button LoadTextureButton;
        private Button SaveTextureButton;
        private Button ClearListButton;
        private ImageList ImageListLarge;
        private ComboBox ViewType;
        private Label ViewTypeLabel;
        private ProgressBar ProgressBar;
        private CheckBox SaveJsonCheckbox;
        private CheckBox RequireResizeCheckbox;
        private Label label1;
        private TextBox MaxSizeText;
        private TextBox RequireSizeText;
        private Label label2;
        private ToolStripLabel ToolStripLabel;
        private ImageList ImageListSmall;
    }
}
