using System;
using SFML.System;

namespace SoftRenderer
{
    public static class Util
    {
        public static void Swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }

        public static void Swap(ref Vector2i a, ref Vector2i b)
        {
            int tmpX = a.X;
            int tmpY = a.Y;
            a.X = b.X;
            a.Y = b.Y;
            b.X = tmpX;
            b.Y = tmpY;
        }
        
        private static Vector3f Cross(Vector3f a, Vector3f b)
        {
            Vector3f cross = new Vector3f()
            {
                X = a.Y * b.Z - a.Z * b.Y,
                Y = a.Z * b.X - a.X * b.Z,
                Z = a.X * b.Y - a.Y * b.X
            };

            return cross;
        }

        public static Vector3f Pow(Vector3f a, Vector3f b)
        {
            return new Vector3f()
            {
                X = a.Y * b.Z - a.Z * b.Y,
                Y = a.Z * b.X - a.X * b.Z,
                Z = a.X * b.Y - a.Y * b.X
            };
        }

        public static void Normalize(ref Vector3f v)
        {
            float normal = MathF.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
            v.X *= 1.0f / normal;
            v.Y *= 1.0f / normal;
            v.Z *= 1.0f / normal;
        }

        public static Vector3f Sub(Vector3f a, Vector3f b)
        {
            return new Vector3f()
            {
                X = a.X - b.X,
                Y = a.Y - b.Y,
                Z = a.Z - b.Z
            };
        }

        public static Vector2i Add(Vector2i a, Vector2i b)
        {
            return new Vector2i()
            {
                X = a.X + b.X,
                Y = a.Y + b.Y
            };
        }

        public static Vector2i Mul(Vector2i a, float v)
        {
            return new Vector2i()
            {
                X = (int) (a.X * v),
                Y = (int) (a.Y * v)
            };
        }

        public static Vector2i Sub(Vector2i a, Vector2i b)
        {
            return new Vector2i()
            {
                X = a.X - b.X,
                Y = a.Y - b.Y
            };
        }

        public static float Mul(Vector3f a, Vector3f b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }
    }
}