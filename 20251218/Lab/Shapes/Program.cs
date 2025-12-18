namespace Shapes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Circle c1 = new Circle(10, 10, 5, FiguresColor.Red);
            Symbol[,] view = c1.Draw();
            UI.Draw(c1);
        }
    }
}
