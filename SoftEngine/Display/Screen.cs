namespace SoftEngine.Display;

public class Screen
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    private char[] _buffer;

    public Screen(int width, int height)
    {
        Width = width;
        Height = height;
        _buffer = new char[width * (height - 1)];
    }

    public void Resize(int width, int height)
    {
        Width = width;
        Height = height;
        _buffer = new char[width * (height - 1)];
    }

    public void Clear()
    {
        Array.Fill(_buffer, ' ');
    }

    public void DrawLine(int x0, int y0, int x1, int y1, char pixel = '#')
    {
        int dx = Math.Abs(x1 - x0);
        int dy = -Math.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx + dy;

        while (true)
        {
            if (x0 >= 0 && x0 < Width && y0 >= 0 && y0 < Height - 1)
            {
                _buffer[y0 * Width + x0] = pixel;
            }

            if (x0 == x1 && y0 == y1) break;
            int e2 = 2 * err;
            if (e2 >= dy)
            {
                err += dy;
                x0 += sx;
            }
            if (e2 <= dx)
            {
                err += dx;
                y0 += sy;
            }
        }
    }

    public void Present()
    {
        if (Console.WindowWidth == Width && Console.WindowHeight == Height)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(_buffer);
        }
        else
        {
            Console.Clear();
            Resize(Console.WindowWidth, Console.WindowHeight);
        }
    }
}
