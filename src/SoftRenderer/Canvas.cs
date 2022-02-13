using System;
using SFML.Graphics;

namespace SoftRenderer
{
    public class Canvas
    {
        private readonly Texture _texture;
        private readonly Sprite _sprite;

        private byte[] _pixels;

        private readonly int _width;
        private readonly int _height;

        public Canvas(int width, int height)
        {
            _texture = new Texture((uint) width, (uint) height);
            _sprite = new Sprite(_texture);
            
            _pixels = new byte[width * height * 4];
            _width = width;
            _height = height;
        }

        public void SetPixel(int x, int y, Color color)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height)
                return;
            
            int index = (_width * y + x) * 4;
            _pixels[index + 0] = color.R;
            _pixels[index + 1] = color.G;
            _pixels[index + 2] = color.B;
            _pixels[index + 3] = color.A;
        }

        public void FlipPixelsVertically()
        {
            byte[] flippedData = new byte[_pixels.Length];
            for (int k = 0; k < _height; k++)
            {
                int j = _height - k - 1;
                Buffer.BlockCopy(_pixels, k * _width * 4, flippedData, j * _width * 4, _width * 4);
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