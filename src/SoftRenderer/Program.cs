namespace SoftRenderer
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Renderer renderer = new Renderer(800, 800, "SoftRenderer");
            renderer.Run();
        }
    }
}