using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TexturePacker
{
    public struct AtlasData
    {
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("x")]
        public int X;
        [JsonProperty("y")]
        public int Y;
        [JsonProperty("width")]
        public int Width;
        [JsonProperty("height")]
        public int Height;
    }

    public struct Atlas
    {
        [JsonProperty("atlas")]
        public List<AtlasData> AtlasList;
    }

    public partial class Form1 : Form
    {
        private StringBuilder ExtensionBuilder = new StringBuilder();
        private const string ReadyStr = "Ready.";
        private const string SuccessStr = "Success.";
        private Size ImageSize = new Size(64, 64);
        private Size AtlasSize = new Size(0, 0);
        private int MaxImageSizeWidth = 256;
        private Atlas AtlasJson = new Atlas();

        public Form1()
        {
            InitializeComponent();
            AtlasJson.AtlasList = new List<AtlasData>();
        }

        private void TexturePacker_Load(object sender, EventArgs e)
        {
            curStatusStr.Text = ReadyStr;
        }

        private void loadTexDirButton_Click(object sender, EventArgs e)
        {
            if (ExtensionBuilder.Length == 0)
            {
                ExtensionBuilder.Append("Bmp files (*.bmp)|*.bmp|"); // Filter 1
                ExtensionBuilder.Append("Png files (*.png)|*.png|"); // Filter 2
                ExtensionBuilder.Append("All files (*.*)|*.*");      // Filter 3
            }

            var ofd = new OpenFileDialog
            {
                Filter = ExtensionBuilder.ToString(),
                FilterIndex = 2,
                Multiselect = true,
                AddExtension = true,
                Title = "Select texture folder..."
            };
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            fileListView.LargeImageList = fileImageList;
            fileListView.SmallImageList = fileImageList;

            try
            {
                progressBar1.Maximum = ofd.FileNames.Length - 1;
                // Scale image based on the first image we found.
                var firstPath = ofd.FileNames.ElementAtOrDefault(0);
                if (firstPath.Length > 0)
                {
                    var image = Image.FromFile(firstPath);
                    if (image != null)
                    {
                        fileImageList.ImageSize = image.Size;
                        ImageSize = image.Size;
                        image.Dispose();
                    }
                }
                for (int i = 0; i < ofd.FileNames.Length; i++)
                {
                    var filePath = ofd.FileNames[i];
                    var filePathNoExt = Path.GetFileNameWithoutExtension(filePath);
                    if (fileImageList.Images.ContainsKey(filePathNoExt)) // Dont add already added images.
                        continue;
                    var image = Image.FromFile(filePath);
                    fileImageList.Images.Add(filePathNoExt, image);
                    var view = fileListView.Items.Add(filePathNoExt);
                    view.ImageKey = filePathNoExt;
                    progressBar1.Value = i;
                }
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "Exception during loading.", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    ClearList();
                    return;
                }
            }
            finally
            {
                // Reset progress bar.
                progressBar1.Value = 0;
                curStatusStr.Text = SuccessStr;
            }
        }

        private bool ClearList()
        {
            if (fileImageList.Images.Count <= 0)
            {
                curStatusStr.Text = "No image loaded.";
                return false;
            }
            fileListView.Items.Clear();
            fileImageList.Images.Clear();
            AtlasJson.AtlasList.Clear();
            return true;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            if (!ClearList())
                return;
            curStatusStr.Text = ReadyStr;
        }

        private void saveAsButton_Click(object sender, EventArgs e)
        {
            if (fileImageList.Images.Count <= 0)
            {
                curStatusStr.Text = "No image loaded.";
                return;
            }

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
                return;

            progressBar1.Maximum = fileImageList.Images.Keys.Count - 1;
            var destPath = sfd.FileName;
            var imageToSave = new List<Image>();

            int posX = 0, width = 0, height = 0;
            bool nextLine = false;
            for (int i = 0; i < fileImageList.Images.Keys.Count; i++)
            {
                var imageList = fileImageList.Images;
                var atlasData = new AtlasData();
                var index = imageList.IndexOfKey(imageList.Keys[i]);
                var image = imageList[index] ?? throw new Exception("Failed to get image at key: " + imageList.Keys[i]);

                width += image.Width;
                if (width >= MaxImageSizeWidth)
                    width = MaxImageSizeWidth;

                posX += image.Width;
                if (posX >= MaxImageSizeWidth)
                {
                    posX = 0;
                    nextLine = true;
                }

                if (nextLine)
                {
                    height += image.Height;
                    nextLine = false;
                }

                atlasData.Name = Path.GetFileNameWithoutExtension(imageList.Keys[i]);
                atlasData.X = posX;
                atlasData.Y = height;
                atlasData.Width = image.Width;
                atlasData.Height = image.Height;
                AtlasJson.AtlasList.Add(atlasData);

                imageToSave.Add(image);
                progressBar1.Value = i;
            }

            height += ImageSize.Height; // Add a new row at the end to avoid missing last texture.
            var textureAtlas = new TextureAtlas(width, height, MaxImageSizeWidth);
            textureAtlas.SetStatusLabel(curStatusStr);

            var prevSize = ImageSize;
            int wrongImage = 0;
            foreach (var image in imageToSave)
            {
                var size = image.Size;
                if (size != prevSize)
                {
                    curStatusStr.Text = string.Format("Failed to write an image, wrong size count: {0}, size: {1}, prevSize: {2}", wrongImage++, size.ToString(), prevSize.ToString());
                    continue;
                }
                prevSize = size;
                textureAtlas.AddImage(new Bitmap(image));
            }

            textureAtlas.Process();
            if (textureAtlas.Save(destPath))
                curStatusStr.Text = "Success saving file to: " + destPath;
            else
                curStatusStr.Text = "Failed to save file to: " + destPath;

            if (addJsonCheckbox.Checked)
                WriteJsonFile(destPath);
        }

        private void WriteJsonFile(string destPath)
        {
            string path = Path.GetFullPath(destPath);
            string fileName = Path.GetFileName(path);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(destPath);
            string pathWithoutFile = path.Replace(fileName, string.Empty);
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Formatting = Formatting.Indented;
            using (StreamWriter sw = new StreamWriter(pathWithoutFile + "\\" + fileNameWithoutExt + ".json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
                serializer.Serialize(writer, AtlasJson);
        }
    }
}
