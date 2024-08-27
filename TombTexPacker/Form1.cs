namespace TombTexPacker
{
    public partial class Form1 : Form
    {
        private TextureManager? TextureManager = null;
        private TextureAtlas? TextureAtlas = null;
        private const int DefaultMaxWidthSize = 256;
        private const int DefaultMaxHeightSize = -1;
        private const int DefaultRequiredWidthSize = 64;
        private const int DefaultRequiredHeightSize = 64;
        private const int DefaultStringArrayCount = 2;
        private const string SizeSplitCharacter = "x";

        public Form1()
        {
            InitializeComponent();
            FormClosing += Form1_FormClosing;
            TextureManager = new TextureManager();
            TextureManager.Initialize();
            ListView.View = View.LargeIcon;
            ViewType.SelectedIndex = 0;
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            ClearAll();
            TextureManager?.Dispose();
        }

        private void LoadTextureButton_Click(object sender, EventArgs e)
        {
            (int reqWidth, int reqHeight) = GetRequiredTextureSize();
            ImageListLarge.ImageSize = new Size(reqWidth, reqHeight);
            ImageListSmall.ImageSize = new Size(reqWidth / 2, reqHeight / 2);
            TextureManager?.LoadTextures(IsRequiredTextureResize(), reqWidth, reqHeight);
            TextureManager?.AddToView(ListView, ImageListLarge, ImageListSmall);
        }

        private void ClearListButton_Click(object sender, EventArgs e)
        {
            ClearAll();
            TextureManager?.Clear();
        }

        private void SaveTextureButton_Click(object sender, EventArgs e)
        {
            ArgumentNullException.ThrowIfNull(TextureManager, nameof(TextureManager));
            StringBuilder builder = new StringBuilder();
            builder.Append("Bmp files (*.bmp)|*.bmp|");
            builder.Append("Gif files (*.gif)|*.gif|");
            builder.Append("Jpg files (*.jpg)|*.jpg|");
            builder.Append("Exif files (*.exif)|*.exif|");
            builder.Append("Png files (*.png)|*.png|");
            builder.Append("Tiff files (*.tiff)|*.tiff");
            var sfd = new SaveFileDialog
            {
                Title = "Select atlas directory...",
                CheckPathExists = true,
                Filter = builder.ToString(),
                AddExtension = true,
                FilterIndex = 5
            };
            if (sfd.ShowDialog() != DialogResult.OK)
                return;
           
            (int maxWidth, int maxHeight) = GetMaxTextureSize();
            (int reqWidth, int reqHeight) = GetRequiredTextureSize();
            (int width, int height) = TextureManager.GetAtlasSize(maxWidth, maxHeight);
            height += reqHeight; // Avoid the atlas to throw an error, we add a last row at the end !
            TextureAtlas = new TextureAtlas(width, height, maxWidth, maxHeight);
            TextureAtlas.SetStatusLabel(ToolStripLabel);
            TextureAtlas.Process(TextureManager.GetImages());
            TextureAtlas.Save(sfd.FileName);
            TextureAtlas.Dispose();
        }

        private void ClearAll()
        {
            ListView?.Clear();
            ImageListLarge?.Images.Clear();
            ImageListSmall?.Images.Clear();
        }

        private bool IsRequiredTextureResize()
        {
            return RequireResizeCheckbox.Checked;
        }

        private (int, int) GetRequiredTextureSize()
        {
            string[] size = RequireSizeText.Text.Split(SizeSplitCharacter);
            int w = DefaultRequiredWidthSize, h = DefaultRequiredHeightSize;
            if (size.Length < DefaultStringArrayCount || size.Length > DefaultStringArrayCount) // Need 2 else this wont work.
                return (w, h);
            if (int.TryParse(size[0], out var width)) // Width
                w = width;
            if (int.TryParse(size[1], out var height)) // Height
                h = height;
            return (w, h);
        }

        private (int, int) GetMaxTextureSize()
        {
            string[] size = MaxSizeText.Text.Split(SizeSplitCharacter);
            int w = DefaultMaxWidthSize, h = DefaultMaxHeightSize;
            if (size.Length < DefaultStringArrayCount || size.Length > DefaultStringArrayCount) // Need 2 else this wont work.
                return (w, h);
            if (int.TryParse(size[0], out var width)) // Width
                w = width;
            if (int.TryParse(size[1], out var height)) // Height
                h = height;
            return (w, h);
        }

        private View GetViewType(int index)
        {
            return index == 0 ? View.LargeIcon : View.SmallIcon;
        }

        private void ViewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.View = GetViewType(ViewType.SelectedIndex);
        }
    }
}
