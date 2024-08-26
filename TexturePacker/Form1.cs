using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TexturePacker
{

    public partial class Form1 : Form
    {
        private StringBuilder ExtensionBuilder = new StringBuilder();
        private TextureAtlasJson Json = new TextureAtlasJson();
        private const string ReadyStr = "Ready.";
        private const string SuccessStr = "Success.";
        private Size ImageSize = new Size(64, 64);

        public Form1()
        {
            InitializeComponent();
            SetupExtensionBuilder();
            Json.Initialize();
            LoadImageWorker.WorkerReportsProgress = true;
            LoadImageWorker.DoWork += LoadAllImageAsync;
            LoadImageWorker.RunWorkerCompleted += LoadAllImageCompletedAsync;
            ImageDrawTypeComboBox.SelectedIndex = 0;
            FileListView.LargeImageList = FileImageList;
            FileListView.SmallImageList = FileImageList;
            FileListView.AutoArrange = true;
        }

        private void LoadAllImageCompletedAsync(object sender, RunWorkerCompletedEventArgs e)
        {
            StatusBar.UpdateThreaded(0);
            SetStatusText(SuccessStr);
        }

        private void TexturePackerStart(object sender, EventArgs e)
        {
            SetStatusText(ReadyStr);
        }

        private void LoadTextureButtonClick(object sender, EventArgs e)
        {
            var pathList = OpenDialog();

            // Scale image based on the first image we found.
            var firstPath = pathList.ElementAtOrDefault(0);
            if (firstPath.Length > 0)
            {
                var image = Image.FromFile(firstPath);
                if (image != null)
                {
                    FileImageList.ImageSize = image.Size;
                    ImageSize = image.Size;
                    image.Dispose();
                }
            }

            StatusBar.Maximum = pathList.Length - 1;
            LoadImageWorker.RunWorkerAsync(pathList);
        }

        private void LoadAllImageAsync(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            var pathList = e.Argument as string[];
            var progress = 0;
            try
            {
                foreach (var item in pathList)
                {
                    var noExt = Path.GetFileNameWithoutExtension(item);
                    if (FileListView.ContainsThreaded(FileImageList, noExt)) // Dont add already added images.
                        continue;
                    FileListView.AddThreaded(FileImageList, noExt, Image.FromFile(item, true));
                    StatusBar.UpdateThreaded(progress++);
                }
                e.Result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception during loading.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearList();
                e.Cancel = true;
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            if (!ClearList())
                return;
            SetStatusText(ReadyStr);
        }

        private void ImageDrawTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileListView.View = (View)ImageDrawTypeComboBox.SelectedIndex;
        }

        private void SaveAsButton_AsClick(object sender, EventArgs e)
        {
            if (FileImageList.Images.Count <= 0)
            {
                SetStatusText("No image loaded.");
                return;
            }

            var destPath = SaveDialog();
            if (string.IsNullOrEmpty(destPath))
            {
                SetStatusText("Failed to get the save path, destination is null or empty !");
                return;
            }

            GetMaxAtlasSize(out var maxAtlasWidthSize, out var maxAtlasHeightSize);
            StatusBar.Maximum = FileImageList.Images.Keys.Count - 1;

            var imageToSave = new List<Image>();
            int posX = 0, width = 0, height = 0;
            bool nextLine = false;
            for (int i = 0; i < FileImageList.Images.Keys.Count; i++)
            {
                var imageList = FileImageList.Images;
                var atlasData = new AtlasData();
                var index = imageList.IndexOfKey(imageList.Keys[i]);
                var image = imageList[index] ?? throw new Exception("Failed to get image at key: " + imageList.Keys[i]);

                width += image.Width;
                if (maxAtlasWidthSize != -1 && width >= maxAtlasWidthSize)
                    width = maxAtlasWidthSize;

                posX += image.Width;
                if (maxAtlasWidthSize != -1 && posX >= maxAtlasWidthSize)
                {
                    posX = 0;
                    nextLine = true;
                }

                if (nextLine)
                {
                    height += image.Height;
                    nextLine = false;
                }

                if (maxAtlasHeightSize != -1 && height >= maxAtlasHeightSize)
                    height = maxAtlasHeightSize;

                atlasData.Name = imageList.Keys[i];
                atlasData.X = posX;
                atlasData.Y = height;
                atlasData.Width = image.Width;
                atlasData.Height = image.Height;
                Json.Add(atlasData);

                imageToSave.Add(image);
                StatusBar.Value = i;
            }

            height += ImageSize.Height; // Add a new row at the end to avoid missing last texture.
            var textureAtlas = new TextureAtlas(width, height, maxAtlasWidthSize, maxAtlasHeightSize);
            textureAtlas.SetStatusLabel(curStatusStr);

            var prevSize = ImageSize;
            int wrongImage = 0;
            foreach (var image in imageToSave)
            {
                var size = image.Size;
                if (size != prevSize)
                {
                    SetStatusText(string.Format("Failed to write an image, wrong size count: {0}, size: {1}, prevSize: {2}", wrongImage++, size.ToString(), prevSize.ToString()));
                    continue;
                }
                prevSize = size;
                textureAtlas.AddImage(new Bitmap(image));
            }

            textureAtlas.Process();
            if (textureAtlas.Save(destPath))
                SetStatusText("Success saving file to: " + destPath);
            else
                SetStatusText("Failed to save file to: " + destPath);

            if (addJsonCheckbox.Checked)
                Json.Save(destPath);
        }

        private void SetupExtensionBuilder()
        {
            ExtensionBuilder.Append("Bmp files (*.bmp)|*.bmp|"); // Filter 1
            ExtensionBuilder.Append("Png files (*.png)|*.png|"); // Filter 2
            ExtensionBuilder.Append("All files (*.*)|*.*");      // Filter 3
        }

        private string[] OpenDialog()
        {
            var ofd = new OpenFileDialog
            {
                Filter = ExtensionBuilder.ToString(),
                FilterIndex = 2,
                Multiselect = true,
                AddExtension = true,
                Title = "Select texture folder..."
            };
            if (ofd.ShowDialog() != DialogResult.OK)
                return null;
            return ofd.FileNames;
        }

        private string SaveDialog()
        {
            var sfd = new SaveFileDialog()
            {
                Filter = ExtensionBuilder.ToString(),
                FilterIndex = 2,
                AddExtension = true,
                CheckFileExists = false,
                CheckPathExists = true,
                Title = "Select destination..."
            };
            if (sfd.ShowDialog() != DialogResult.OK)
                return string.Empty;
            return sfd.FileName;
        }

        private bool ClearList()
        {
            if (FileImageList.Images.Count <= 0)
            {
                SetStatusText("No image loaded.");
                return false;
            }
            FileListView.Items.Clear();
            FileImageList.Images.Clear();
            Json.Clear();
            return true;
        }

        private void GetMaxAtlasSize(out int x, out int y)
        {
            int resultX = 256;
            if (int.TryParse(maxAtlasXSize.Text, out var xSize))
                resultX = xSize == -1 ? 256 : xSize;
            x = resultX;
            int resultY = -1;
            if (int.TryParse(maxAtlasXSize.Text, out var ySize))
                resultY = ySize;
            y = resultY;
        }

        private void SetStatusText(string text)
        {
            curStatusStr.Text = text;
        }
    }
}
