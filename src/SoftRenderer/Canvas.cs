using System;
using SFML.Graphics;

namespace SoftRenderer
{
    public class Canvas
    {
        private readonly Texture _texture;
        private readonly Sprite _sprite;

        private byte[] _pixels;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Canvas(int width, int height)
        {
            _texture = new Texture((uint) width, (uint) height);
            _sprite = new Sprite(_texture);
            
            _pixels = new byte[width * height * 4];
            Width = width;
            Height = height;
        }

        public void SetPixel(int x, int y, Color color)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return;
            
            int index = (Width * y + x) * 4;
            _pixels[index + 0] = color.R;
            _pixels[index + 1] = color.G;
            _pixels[index + 2] = color.B;
            _pixels[index + 3] = color.A;
        }

        public void FlipPixelsVertically()
        {
            byte[] flippedData = new byte[_pixels.Length];
            for (int k = 0; k < Height; k++)
            {
                int j = Height - k - 1;
                Buffer.BlockCopy(_pixels, k * Width * 4, flippedData, j * Width * 4, Width * 4);
            }

            _pixels = flippedData;
        }

        public void Update()
        {
            _texture.Update(_pixels);
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(_sprite);
        }
    }
}