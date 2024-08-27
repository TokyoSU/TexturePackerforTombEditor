namespace TombTexPacker
{
    public class TextureManager : IDisposable
    {
        private static readonly Logger TexLog = LogManager.GetCurrentClassLogger();
        private Dictionary<string, MagickImage>? ListOfImages = null;
        private Dictionary<string, Bitmap>? ListOfImagesBitmap = null;
        private StringBuilder? ExtensionBuilder = null;

        public void Initialize()
        {
            ListOfImages = [];
            ListOfImagesBitmap = [];
            GenerateExtension();
        }

        public Dictionary<string, MagickImage> GetImages()
        {
            ArgumentNullException.ThrowIfNull(ListOfImages, nameof(ListOfImages));
            return ListOfImages;
        }

        public StringBuilder GetExtensionBuilder()
        {
            ArgumentNullException.ThrowIfNull(ExtensionBuilder, nameof(ExtensionBuilder));
            return ExtensionBuilder;
        }

        public void LoadTextures(bool isReqSizeRequired, int reqWidth, int reqHeight)
        {
            ArgumentNullException.ThrowIfNull(ExtensionBuilder, nameof(ExtensionBuilder));
            ArgumentNullException.ThrowIfNull(ListOfImages, nameof(ListOfImages));
            ArgumentNullException.ThrowIfNull(ListOfImagesBitmap, nameof(ListOfImagesBitmap));

            var ofd = new OpenFileDialog
            {
                Filter = ExtensionBuilder.ToString(),
                Multiselect = true,
                Title = "Select texture... (multiple allowed)",
                CheckFileExists = true,
                CheckPathExists = true
            };
            var result = ofd.ShowDialog();
            if (result != DialogResult.OK)
                return;

            var list = ofd.FileNames;
            foreach (var file in list)
            {
                var image = new MagickImage(file);
                if (image is null)
                {
                    TexLog.Error("Failed to load image: " + file + ", returned null.");
                    continue;
                }

                try
                {
                    if (isReqSizeRequired)
                        image.Resize(reqWidth, reqHeight);
                    ListOfImages.Add(file, image);
                    ListOfImagesBitmap.Add(file, image.ToBitmap());
                }
                catch (Exception ex)
                {
                    TexLog.Error($"Error: {ex}");
                    image.Dispose();
                }
            }
        }

        public void AddToView(ListView view, ImageList imageListLarge, ImageList imageListSmall)
        {
            ArgumentNullException.ThrowIfNull(ListOfImages, nameof(ListOfImages));
            ArgumentNullException.ThrowIfNull(ListOfImagesBitmap, nameof(ListOfImagesBitmap));
            ArgumentNullException.ThrowIfNull(imageListLarge, nameof(imageListLarge));
            ArgumentNullException.ThrowIfNull(imageListSmall, nameof(imageListSmall));

            foreach (var image in ListOfImages)
            {
                imageListLarge.Images.Add(image.Key, ListOfImagesBitmap[image.Key]);
                imageListSmall.Images.Add(image.Key, ListOfImagesBitmap[image.Key]);

                var nameNoExt = Path.GetFileNameWithoutExtension(image.Key);
                var item = view.Items.Add(nameNoExt);
                item.ImageKey = image.Key;
            }
        }

        private void GenerateExtension()
        {
            ExtensionBuilder = new();
            ExtensionBuilder.Append("Image files(");
            var enumList = Enum.GetNames<MagickFormat>();

            int index = 0;
            foreach (var enumType in enumList)
            {
                if (enumType.Contains("unknown", StringComparison.CurrentCultureIgnoreCase))
                    continue;
                ExtensionBuilder.AppendFormat("*.{0}", enumType.ToLower());
                if (index + 1 >= (enumList.Length - 1))
                    break;
                ExtensionBuilder.Append(';');
                index++;
            }
            ExtensionBuilder.Append(")|");

            index = 0;
            foreach (var enumType in enumList)
            {
                if (enumType.Contains("unknown", StringComparison.CurrentCultureIgnoreCase))
                    continue;
                ExtensionBuilder.AppendFormat("*.{0}", enumType.ToLower());
                if (index + 1 >= (enumList.Length - 1))
                    break;
                ExtensionBuilder.Append(';');
                index++;
            }
            ExtensionBuilder.AppendFormat("|All files (*.*)|*.*");
        }

        public (int, int) GetAtlasSize(int maxWidth, int maxHeight)
        {
            ArgumentNullException.ThrowIfNull(ListOfImages, nameof(ListOfImages));
            int posX = 0, width = 0, height = 0;
            bool nextLine = false;
            foreach (var image in ListOfImages)
            {
                width += image.Value.Width;
                if (maxWidth != -1 && width >= maxWidth)
                    width = maxWidth;
                posX += image.Value.Width;
                if (maxWidth != -1 && posX >= maxWidth)
                {
                    posX = 0;
                    nextLine = true;
                }
                if (nextLine)
                {
                    height += image.Value.Height;
                    nextLine = false;
                }
                if (maxHeight != -1 && height >= maxHeight)
                    height = maxHeight;
            }
            return (width, height);
        }

        public void Clear()
        {
            if (ListOfImages is not null)
            {
                foreach (var image in ListOfImages)
                    image.Value?.Dispose();
            }
            ListOfImages?.Clear();

            if (ListOfImagesBitmap is not null)
            {
                foreach (var image in ListOfImagesBitmap)
                    image.Value?.Dispose();
            }
            ListOfImagesBitmap?.Clear();
        }

        public void Dispose()
        {
            Clear();
            GC.SuppressFinalize(this);
        }
    }
}
