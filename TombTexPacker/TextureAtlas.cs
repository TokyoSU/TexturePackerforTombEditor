namespace TombTexPacker
{
    public struct Vector2i(int x, int y)
    {
        public int X = x;
        public int Y = y;
    }

    public class TextureAtlas : IDisposable
    {
        private ToolStripLabel? ToolStripLabel = null;
        private MagickImage? Result = null;
        private Vector2i CurrentPos = new();
        private Size Size = Size.Empty;
        private Size MaxSize = Size.Empty;

        public TextureAtlas() => InitializeAtlas(0, 0);
        public TextureAtlas(int width, int height) => InitializeAtlas(width, height);
        public TextureAtlas(int width, int height, int maxWidth) => InitializeAtlas(width, height, maxWidth);
        public TextureAtlas(int width, int height, int maxWidth, int maxHeight) => InitializeAtlas(width, height, maxWidth, maxHeight);

        public void SetStatusLabel(ToolStripLabel label) => ToolStripLabel = label;

        public void InitializeAtlas(int width, int height, int maxWidth = -1, int maxHeight = -1)
        {
            if (width <= 0 || height <= 0 || maxWidth == 0 || maxHeight == 0)
                throw new Exception("Failed to create the atlas array, width/height is negative or 0, or maxWidth/maxHeight is 0");
            if (maxWidth != -1 && width > maxWidth)
                throw new Exception("Failed to create the atlas bitmap, width is out of bounds !");
            if (maxHeight != -1 && height > maxHeight)
                throw new Exception("Failed to create the atlas bitmap, width is out of bounds !");
            CurrentPos = new Vector2i(0, 0);
            Size = new Size(width, height);
            MaxSize = new Size(maxWidth, maxHeight);
            Result = new MagickImage(ColorMono.Black.ToMagickColor(), width, height)
            {
                Format = MagickFormat.Rgba
            };
        }

        private void AddImageAtPosition(MagickImage image)
        {
            ArgumentNullException.ThrowIfNull(image, nameof(image));
            ArgumentNullException.ThrowIfNull(Result, nameof(Result));

            if (CurrentPos.Y >= Size.Height || (MaxSize.Height != -1 && CurrentPos.Y >= MaxSize.Height))
            {
                ArgumentNullException.ThrowIfNull(ToolStripLabel, nameof(ToolStripLabel));
                if (ToolStripLabel != null)
                    ToolStripLabel.Text = "Failed to write image, max height was reached !";
                return;
            }

            var pixelCollection = Result.GetPixels();
            // Write pixel at the destination bitmap !
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    int posX = CurrentPos.X + x;
                    int posY = CurrentPos.Y + y;
                    var pixels = image.GetPixels().GetValue(x, y);
                    if (pixels is null) continue;
                    pixelCollection.SetPixel(posX, posY, pixels);
                }
            }

            CurrentPos.X += image.Width;
            if (CurrentPos.X >= Size.Width || (MaxSize.Width != -1 && CurrentPos.X >= MaxSize.Width)) // Dont go over maxWidth.
            {
                CurrentPos.X = 0;
                CurrentPos.Y += image.Height;
            }
        }

        public void Process(Dictionary<string, MagickImage> imagelist)
        {
            // then write each texture to it.
            foreach (var image in imagelist)
            {
                AddImageAtPosition(image.Value);
            }
        }

        private ImageFormat GetFormatByString(string path)
        {
            var ext = Path.GetExtension(path);
            return ext switch
            {
                ".bmp" => ImageFormat.Bmp,
                ".gif" => ImageFormat.Gif,
                ".jpg" => ImageFormat.Jpeg,
                ".exif" => ImageFormat.Exif,
                ".png" => ImageFormat.Png,
                ".tiff" => ImageFormat.Tiff,
                _ => ImageFormat.Png
            };
        }

        public void Save(string destPath)
        {
            ArgumentNullException.ThrowIfNull(Result, nameof(Result));
            using var file = Result.ToBitmap();
            file.Save(destPath, GetFormatByString(destPath));
            DisposeAll();
        }

        public void DisposeAll()
        {
            Result?.Dispose();
        }

        public void Dispose()
        {
            DisposeAll();
            GC.SuppressFinalize(this);
        }
    }
}
