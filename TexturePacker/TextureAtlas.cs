using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace TexturePacker
{
    public struct Vector2i
    {
        public int X;
        public int Y;
        public Vector2i(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class TextureAtlas : IDisposable
    {
        private ToolStripStatusLabel ToolStripStatusLabel = null;
        private Vector2i CurrentPos = new Vector2i();
        private Size Size = Size.Empty;
        private Size MaxSize = Size.Empty;
        private MagickImage Result;

        public TextureAtlas() => InitializeAtlas(0, 0);
        public TextureAtlas(int width, int height) => InitializeAtlas(width, height);
        public TextureAtlas(int width, int height, int maxWidth) => InitializeAtlas(width, height, maxWidth);
        public TextureAtlas(int width, int height, int maxWidth, int maxHeight) => InitializeAtlas(width, height, maxWidth, maxHeight);

        public void SetStatusLabel(ToolStripStatusLabel label) => ToolStripStatusLabel = label;

        public void InitializeAtlas(int width, int height, int maxWidth = -1, int maxHeight = -1)
        {
            CurrentPos = new Vector2i(0, 0);
            Size = new Size(width, height);
            MaxSize = new Size(maxWidth, maxHeight);
            Result = new MagickImage(ColorMono.Black.ToMagickColor(), width, height)
            {
                Format = MagickFormat.Rgba
            };
            if (width <= 0 || height <= 0 || maxWidth == 0 || maxHeight == 0)
                throw new Exception("Failed to create the atlas array, width/height is negative or 0, or maxWidth/maxHeight is 0");
            if (maxWidth != -1 && width > maxWidth)
                throw new Exception("Failed to create the atlas bitmap, width is out of bounds !");
            if (maxHeight != -1 && height > maxHeight)
                throw new Exception("Failed to create the atlas bitmap, width is out of bounds !");
        }

        private void AddImageAtPosition(MagickImage image)
        {
            if (image == null)
                throw new Exception("Failed to write atlas, image bitmap is null !");

            if (CurrentPos.Y >= Size.Height || (MaxSize.Height != -1 && CurrentPos.Y >= MaxSize.Height))
            {
                if (ToolStripStatusLabel != null)
                    ToolStripStatusLabel.Text = "Failed to write image, max height was reached !";
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
                    if (pixels == null) continue;
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

        public void Process(List<MagickImage> imagelist)
        {
            // then write each texture to it.
            foreach (var image in imagelist)
            {
                AddImageAtPosition(image);
            }
        }

        public bool Save(string destPath)
        {
            string path = destPath;
            var ext = Path.GetExtension(path);
            var file = Result.ToBitmap();

            if (ext.Contains(".png"))
            {
                if (path.Contains(".bmp"))
                    path.Replace(".bmp", ".png");
                file.Save(path, ImageFormat.Png);
                file.Dispose();
                DisposeAll();
                return true;
            }
            else if (ext.Contains(".bmp"))
            {
                if (path.Contains(".png"))
                    path.Replace(".png", ".bmp");
                file.Save(path, ImageFormat.Bmp);
                file.Dispose();
                DisposeAll();
                return true;
            }

            file.Dispose();
            DisposeAll();
            return false;
        }

        public void DisposeAll()
        {
            Result.Dispose();
        }

        public void Dispose()
        {
            DisposeAll();
            GC.SuppressFinalize(this);
        }
    }
}
