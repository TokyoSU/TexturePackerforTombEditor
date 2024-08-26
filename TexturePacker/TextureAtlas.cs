using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
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
        private List<Bitmap> ImageList = null;
        private Bitmap Result = null;

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
            ImageList = new List<Bitmap>();
            Result = new Bitmap(width, height);
            if (width <= 0 || height <= 0 || maxWidth == 0 || maxHeight == 0)
                throw new Exception("Failed to create the atlas array, width/height is negative or 0, or maxWidth/maxHeight is 0");
            if (maxWidth != -1 && width > maxWidth)
                throw new Exception("Failed to create the atlas bitmap, width is out of bounds !");
            if (maxHeight != -1 && height > maxHeight)
                throw new Exception("Failed to create the atlas bitmap, width is out of bounds !");
        }

        public void AddImage(Bitmap image)
        {
            ImageList.Add(image);
        }

        private void AddImageAtPosition(Bitmap image)
        {
            if (Result == null)
                throw new Exception("Failed to write atlas, destination bitmap is null !");
            if (image == null)
                throw new Exception("Failed to write atlas, image bitmap is null !");

            if (CurrentPos.Y >= Size.Height || (MaxSize.Height != -1 && CurrentPos.Y >= MaxSize.Height))
            {
                if (ToolStripStatusLabel != null)
                    ToolStripStatusLabel.Text = "Failed to write image, max height was reached !";
                return;
            }

            // Write pixel at the destination bitmap !
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    int posX = CurrentPos.X + x;
                    int posY = CurrentPos.Y + y;
                    Result.SetPixel(posX, posY, image.GetPixel(x, y));
                }
            }

            CurrentPos.X += image.Width;
            if (CurrentPos.X >= Size.Width || (MaxSize.Width != -1 && CurrentPos.X >= MaxSize.Width)) // Dont go over maxWidth.
            {
                CurrentPos.X = 0;
                CurrentPos.Y += image.Height;
            }
        }

        public void Process()
        {
            // make the atlas full black
            Rectangle rect = new Rectangle(0, 0, Result.Width, Result.Height);
            BitmapData bmpData = Result.LockBits(rect, ImageLockMode.WriteOnly, Result.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * Result.Height;
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            for (int counter = 0; counter < rgbValues.Length; counter += 4)
            {
                rgbValues[counter + 0] = Color.Black.A;
                rgbValues[counter + 0] = Color.Black.R;
                rgbValues[counter + 1] = Color.Black.G;
                rgbValues[counter + 2] = Color.Black.B;
            }
            Marshal.Copy(rgbValues, 0, ptr, bytes);
            Result.UnlockBits(bmpData);

            // then write each texture to it.
            foreach (var image in ImageList)
            {
                AddImageAtPosition(image);
            }
        }

        public bool Save(string destPath)
        {
            string path = destPath;
            var ext = Path.GetExtension(path);
            if (ext.Contains(".png"))
            {
                if (path.Contains(".bmp"))
                    path.Replace(".bmp", ".png");
                Result.Save(path, ImageFormat.Png);
                DisposeAll();
                return true;
            }
            else if (ext.Contains(".bmp"))
            {
                if (path.Contains(".png"))
                    path.Replace(".png", ".bmp");
                Result.Save(path, ImageFormat.Bmp);
                DisposeAll();
                return true;
            }

            DisposeAll();
            return false;
        }

        public void DisposeAll()
        {
            if (ImageList != null)
            {
                foreach (var image in ImageList)
                    image.Dispose();
            }
            ImageList.Clear();
            Result.Dispose();
        }

        public void Dispose()
        {
            DisposeAll();
            GC.SuppressFinalize(this);
        }
    }
}
