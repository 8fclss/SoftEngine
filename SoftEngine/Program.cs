using SoftEngine;

class Program
{
    static void Main()
    {
        Console.CursorVisible = false;

        int width = Console.WindowWidth;
        int height = Console.WindowHeight;

        char[] buffer = new char[width * (height - 1)];

        var points = new List<Vector3>
        {
            new Vector3(-2,  2, 20),
            new Vector3( 2,  2, 20),
            new Vector3( 2, -2, 20),
            new Vector3(-2, -2, 20)
        };

        while (true)
        {
            for (int i = 0; i < points.Count; i++)
            {
                var p = points[i];

                p.Z -= 0.1f;

                if (p.Z < 1.0f)
                {
                    p.Z = 20.0f;
                }

                points[i] = p;
            }

            Array.Fill(buffer, ' ');

            float fov = height;
            var projectedPoints = new (int x, int y)[points.Count];

            for (int i = 0; i < points.Count; i++)
            {
                var point = points[i];
                float projectedX = (point.X / point.Z) * fov;
                float projectedY = (point.Y / point.Z) * fov;

                projectedPoints[i] = (
                    (int)(projectedX + (width / 2.0f)),
                    (int)(projectedY + (height / 2.0f))
                );
            }

            for (int i = 0; i < projectedPoints.Length; i++)
            {
                var p1 = projectedPoints[i];
                var p2 = projectedPoints[(i + 1) % projectedPoints.Length];

                DrawLine(p1.x, p1.y, p2.x, p2.y, buffer, width, height);
            }

            if (Console.WindowWidth == width && Console.WindowHeight == height)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write(buffer);
            }
            else
            {
                Console.Clear();
                width = Console.WindowWidth;
                height = Console.WindowHeight;
                buffer = new char[width * (height - 1)];
            }

            Thread.Sleep(33);
        }
    }

    static void DrawLine(int x0, int y0, int x1, int y1, char[] buffer, int width, int height)
    {
        int dx = Math.Abs(x1 - x0);
        int dy = -Math.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx + dy;

        while (true)
        {
            if (x0 >= 0 && x0 < width && y0 >= 0 && y0 < height - 1)
            {
                buffer[y0 * width + x0] = '#';
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
}