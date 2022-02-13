using System;
using SFML.Graphics;

namespace SoftRenderer
{
    public static class Drawing
    {
        public static void Line(int x0, int y0, int x1, int y1, Canvas canvas, Color color)
        {
            bool steep = false;
            if (Math.Abs(x0 - x1) < Math.Abs(y0 - y1))
            {
                Util.Swap(ref x0, ref y0);
                Util.Swap(ref x1, ref y1);
                steep = true;
            }

            if (x0 > x1)
            {
                Util.Swap(ref x0, ref x1);
                Util.Swap(ref y0, ref y1);
            }

            int dx = x1 - x0;
            int dy = y1 - y0;
            int derror2 = Math.Abs(dy) * 2;
            int error2 = 0;
            int y = y0;
            for (int x = x0; x <= x1; x++)
            {
                if (steep)
                    canvas.SetPixel(y, x, color);
                else
                    canvas.SetPixel(x, y, color);

                error2 += derror2;
                
                if (error2 <= dx)
                    continue;
                
                y += (y1 > y0 ? 1 : -1);
                error2 -= dx * 2;
            }
        }
    }
}