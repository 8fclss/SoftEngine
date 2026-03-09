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

    public void DrawTriangle(int x0, int y0, int x1, int y1, int x2, int y2, char pixel = '#')
    {
        int minX = Math.Max(0, Math.Min(x0, Math.Min(x1, x2)));
        int maxX = Math.Min(Width - 1, Math.Max(x0, Math.Max(x1, x2)));
        int minY = Math.Max(0, Math.Min(y0, Math.Min(y1, y2)));
        int maxY = Math.Min(Height - 2, Math.Max(y0, Math.Max(y1, y2)));

        float den = (y1 - y2) * (x0 - x2) + (x2 - x1) * (y0 - y2);
        if (Math.Abs(den) < 0.0001f) return;

        for (int y = minY; y <= maxY; y++)
        {
            for (int x = minX; x <= maxX; x++)
            {
                float w0 = ((y1 - y2) * (x - x2) + (x2 - x1) * (y - y2)) / den;
                float w1 = ((y2 - y0) * (x - x2) + (x0 - x2) * (y - y2)) / den;
                float w2 = 1.0f - w0 - w1;

                if (w0 >= 0 && w1 >= 0 && w2 >= 0)
                {
                    _buffer[y * Width + x] = pixel;
                }
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
