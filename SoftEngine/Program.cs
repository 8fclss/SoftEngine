using SoftEngine;


class Program
{
    static void Main()
    {
        // Setup
        int width = Console.WindowWidth;
        int height = Console.WindowHeight;

        Vector3 cubePoint = new Vector3(5, -2, 10);

        // Game loop
        while (true)
        {
            // Logic
            if (cubePoint.Z > 1)
            {
                cubePoint.Z -= 0.1f;
            }
            else
            {
                cubePoint.Z = 20;
            }
            
            // Rendering
            Console.Clear();

            // Projection formula
            float fov = height / 2.0f;
            
            // Calculate raw 2D position
            int screenX = (int)(cubePoint.X / cubePoint.Z * fov);
            int screenY = (int)(cubePoint.Y / cubePoint.Z * fov);
            
            // Centering on screen
            screenX += width / 2;
            screenY += height / 2;
            
            // Bounds check 
            if (screenX >= 0 && screenX < width && screenY >= 0 && screenY < height)
            {
                Console.SetCursorPosition(screenX, screenY);
                Console.Write("0");
            }
            
            // Timing
            Thread.Sleep(33);
        }
    }
}