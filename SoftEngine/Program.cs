using SoftEngine;


class Program
{
    const float MovementSpeed = 0.1f;
    const float ResetDepth = 20f;
    const float MinDepth = 1f;
    const int TargetFpsMs = 33;
    const int MaxFrames = 500;

    static void Main()
    {
        RunGame();
    }

    static void RunGame()
    {
        var points = new List<Vector3>
        {
            new Vector3(5, -2, 10),
            new Vector3(5, 2, 10)
        };

        int frames = 0;

        while (frames < MaxFrames)
        {
            Update(points);
            Render(points);

            Thread.Sleep(TargetFpsMs);
            frames++;
        }
    }

    static void Update(List<Vector3> points)
    {
        for (int i = 0; i < points.Count; i++)
        {
            var point = points[i];

            // Move point closer
            if (point.Z > MinDepth)
                point.Z -= MovementSpeed;
            else
                point.Z = ResetDepth;

            points[i] = point; 
        }
    }

    static void Render(List<Vector3> points)
    {
        Console.Clear();

        int width = Console.WindowWidth;
        int height = Console.WindowHeight;
        float fov = height / 2.0f;

        foreach (var point in points)
        {
            // Projection Logic
            int screenX = (int)(point.X / point.Z * fov) + (width / 2);
            int screenY = (int)(point.Y / point.Z * fov) + (height / 2);

            // Bounds Check
            if (IsOnScreen(screenX, screenY, width, height))
            {
                Console.SetCursorPosition(screenX, screenY);
                Console.Write("0");
            }
        }
    }

    static bool IsOnScreen(int x, int y, int width, int height)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }
}