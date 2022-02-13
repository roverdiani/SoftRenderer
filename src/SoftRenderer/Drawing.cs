using System;
using SFML.Graphics;
using SFML.System;

namespace SoftRenderer
{
    public static class Drawing
    {
        public static void Line(Vector2i p0, Vector2i p1, Canvas canvas, Color color)
        {
            bool steep = false;
            if (Math.Abs(p0.X - p1.X) < Math.Abs(p0.Y - p1.Y))
            {
                Util.Swap(ref p0.X, ref p0.Y);
                Util.Swap(ref p1.X, ref p1.Y);
                steep = true;
            }

            if (p0.X > p1.X)
            {
                Util.Swap(ref p0.X, ref p1.X);
                Util.Swap(ref p0.Y, ref p1.Y);
            }

            int dx = p1.X - p0.X;
            int dy = p1.Y - p0.Y;
            int derror2 = Math.Abs(dy) * 2;
            int error2 = 0;
            int y = p0.Y;
            for (int x = p0.X; x <= p1.X; x++)
            {
                if (steep)
                    canvas.SetPixel(y, x, color);
                else
                    canvas.SetPixel(x, y, color);

                error2 += derror2;
                
                if (error2 <= dx)
                    continue;
                
                y += (p1.Y > p0.Y ? 1 : -1);
                error2 -= dx * 2;
            }
        }

        public static void Triangle(ref Vector2i t0, ref Vector2i t1, ref Vector2i t2, Canvas canvas, Color color)
        {
            if (t0.Y == t1.Y && t0.Y == t2.Y)
                return;

            if (t0.Y > t1.Y)
                Util.Swap(ref t0, ref t1);
            if (t0.Y > t2.Y)
                Util.Swap(ref t0, ref t2);
            if (t1.Y > t2.Y)
                Util.Swap(ref t1, ref t2);
            
            int totalHeight = t2.Y - t0.Y;

            for (int i = 0; i < totalHeight; i++)
            {
                bool secondHalf = i > t1.Y - t0.Y || t1.Y == t0.Y;
                int segmentHeight = secondHalf ? t2.Y - t1.Y : t1.Y - t0.Y;
                float alpha = (float) i / totalHeight;
                float beta = (float) (i - (secondHalf ? t1.Y - t0.Y : 0)) / segmentHeight;
                Vector2i a = new Vector2i(Util.Add(t0, Util.Mul(Util.Sub(t2, t0),alpha)).X, 0);
                Vector2i b = secondHalf
                    ? new Vector2i(Util.Add(t1, Util.Mul(Util.Sub(t2, t1), beta)).X, 0)
                    : new Vector2i(Util.Add(t0, Util.Mul(Util.Sub(t1, t0), beta)).X, 0);
                
                if (a.X > b.X)
                    Util.Swap(ref a, ref b);

                for (int j = a.X; j < b.X; j++)
                    canvas.SetPixel(j, t0.Y + i, color);
            }
        }
    }
}