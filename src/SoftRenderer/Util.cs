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
    }
}