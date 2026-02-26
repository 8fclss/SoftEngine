using SoftEngine.Core;
using SoftEngine.Display;
using SoftEngine.Models;

class Program
{
    static void Main()
    {
        Console.CursorVisible = false;

        var screen = new Screen(Console.WindowWidth, Console.WindowHeight);
        var cube = new Cube();
        var engine = new Engine(screen, cube);

        engine.Run();
    }
}
