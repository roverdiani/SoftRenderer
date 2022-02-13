using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SoftRenderer
{
    public class Renderer
    {
        private readonly RenderWindow _window;
        private readonly Canvas _canvas;
        
        public Renderer(uint width, uint height, string title)
        {
            _window = new RenderWindow(new VideoMode(width, height), title);
            _canvas = new Canvas((int) width, (int) height);

            _window.Closed += OnWindowClosed;

            Model model = new Model("obj/african_head.obj");

            Vector3f lightDir = new Vector3f(0, 0, -1);

            for (int i = 0; i < model.NumFaces; i++)
            {
                List<int> faces = model.Face(i);
                Vector2i[] screenCoords = new Vector2i[3];
                Vector3f[] worldCoords = new Vector3f[3];
                for (int j = 0; j < 3; j++)
                {
                    Vector3f v = model.Vert(faces[j]);
                    screenCoords[j] = new Vector2i((int) ((v.X + 1.0f) * width / 2.0f), (int) ((v.Y + 1.0f) * height / 2.0f));
                    worldCoords[j] = v;
                }

                Vector3f n = Util.Pow(Util.Sub(worldCoords[2], worldCoords[0]),
                    Util.Sub(worldCoords[1], worldCoords[0]));
                Util.Normalize(ref n);

                float intensity = Util.Mul(n, lightDir);

                if (intensity > 0)
                {
                    Color color = new Color((byte) (intensity * 255), (byte) (intensity * 255), (byte) (intensity * 255), 255);
                    Drawing.Triangle(ref screenCoords[0], ref screenCoords[1], ref screenCoords[2], _canvas, color);
                }
            }

            _canvas.FlipPixelsVertically();
            _canvas.Update();
        }

        public void Run()
        {
            while (_window.IsOpen)
            {
                _window.DispatchEvents();
                Render();
            }
        }

        private void Render()
        {
            _window.Clear(Color.Black);
            _canvas.Draw(_window);
            _window.Display();
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            _window.Close();
        }
    }
}