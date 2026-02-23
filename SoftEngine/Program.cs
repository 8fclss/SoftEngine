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
            new Vector3(-2,  2, 10),
            new Vector3( 2,  2, 10),
            new Vector3( 2, -2, 10),
            new Vector3(-2, -2, 10)
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

            foreach (var point in points)
            {
                float projectedX = (point.X / point.Z) * fov;
                float projectedY = (point.Y / point.Z) * fov;

                int x = (int)(projectedX + (width / 2.0f));
                int y = (int)(projectedY + (height / 2.0f));

                if (x >= 0 && x < width && y >= 0 && y < height - 1)
                {
                    int index = y * width + x;
                    buffer[index] = 'O';
                }
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
}