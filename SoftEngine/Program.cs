using SoftEngine;


class Program
{
    static void GameLoop()
    {
        // Setup
        int width = Console.WindowWidth;
        int height = Console.WindowHeight;
        int frames = 0;

        Vector3 cubePoint = new Vector3(5, -2, 10);
        Vector3 cubePoint2 = new Vector3(5, 2, 10);

        // Game loop
        while (frames < 500)
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

            if (cubePoint2.Z > 1)
            {
                cubePoint2.Z -= 0.1f;
            }
            else
            {
                cubePoint2.Z = 20;
            }
            
            // Rendering
            Console.Clear();

            // Projection formula
            float fov = height / 2.0f;
            
            // Calculate raw 2D position
            int screenX = (int)(cubePoint.X / cubePoint.Z * fov);
            int screenY = (int)(cubePoint.Y / cubePoint.Z * fov); 
            
            int screenX2 = (int)(cubePoint2.X / cubePoint2.Z * fov); 
            int screenY2 = (int)(cubePoint2.Y / cubePoint2.Z * fov);
            
            // Centering on screen
            screenX += width / 2;
            screenY += height / 2;
           
            screenX2 += width / 2;
            screenY2 += height / 2;
            
            // Bounds check 
            if (screenX >= 0 && screenX < width && screenY >= 0 && screenY < height)
            {
                Console.SetCursorPosition(screenX, screenY);
                Console.Write("0");
            }
            
            if (screenX2 >= 0 && screenX2 < width && screenY2 >= 0 && screenY2 < height)
            {
                Console.SetCursorPosition(screenX2, screenY2);
                Console.Write("0");
            }
            
            // Timing
            Thread.Sleep(33);
            frames++;
        } 
    }
    static void Main()
    {
        GameLoop();
    }
}