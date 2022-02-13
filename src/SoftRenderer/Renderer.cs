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
            for (int i = 0; i < model.NumFaces; i++)
            {
                List<int> faces = model.Face(i);
                for (int j = 0; j < 3; j++)
                {
                    Vector3f v0 = model.Vert(faces[j]);
                    Vector3f v1 = model.Vert(faces[(j + 1) % 3]);

                    int x0 = (int) ((v0.X + 1.0f) * width / 2.0f);
                    int y0 = (int) ((v0.Y + 1.0f) * height / 2.0f);
                    int x1 = (int) ((v1.X + 1.0f) * width / 2.0f);
                    int y1 = (int) ((v1.Y + 1.0f) * height / 2.0f);

                    Drawing.Line(x0, y0, x1, y1, _canvas, Color.White);
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