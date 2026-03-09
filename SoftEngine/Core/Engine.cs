using SoftEngine.Display;
using SoftEngine.Mathematics;
using SoftEngine.Models;

namespace SoftEngine.Core;

public class Engine
{
    private readonly Screen _screen;
    private readonly Mesh _mesh;
    private float _angleX;
    private float _angleY;
    private float _angleZ;

    public Engine(Screen screen, Mesh mesh)
    {
        _screen = screen;
        _mesh = mesh;
    }

    public void Run()
    {
        while (true)
        {
            Update();
            Render();
            Thread.Sleep(33);
        }
    }

    private void Update()
    {
        _angleX += 0.02f;
        _angleY += 0.03f;
        _angleZ += 0.01f;
    }

    private void Render()
    {
        _screen.Clear();

        Matrix4 rotX = Matrix4.CreateRotationX(_angleX);
        Matrix4 rotY = Matrix4.CreateRotationY(_angleY);
        Matrix4 rotZ = Matrix4.CreateRotationZ(_angleZ);
        Matrix4 translation = Matrix4.CreateTranslation(0, 0, 10);

        Matrix4 world = translation * rotX * rotY * rotZ;

        var worldPoints = new Vector3[_mesh.Vertices.Length];
        var projectedPoints = new (int x, int y)[_mesh.Vertices.Length];
        float fov = _screen.Height;

        for (int i = 0; i < _mesh.Vertices.Length; i++)
        {
            Vector3 v = world.Transform(_mesh.Vertices[i]);
            worldPoints[i] = v;
            
            float pZ = v.Z;
            if (pZ < 0.1f) pZ = 0.1f;

            float projectedX = (v.X / pZ) * fov;
            float projectedY = (v.Y / pZ) * fov;

            projectedPoints[i] = (
                (int)(projectedX + (_screen.Width / 2.0f)),
                (int)(projectedY + (_screen.Height / 2.0f))
            );
        }

        foreach (var face in _mesh.Faces)
        {
            Vector3 v0 = worldPoints[face.a];
            Vector3 v1 = worldPoints[face.b];
            Vector3 v2 = worldPoints[face.c];

            Vector3 side1 = v1 - v0;
            Vector3 side2 = v2 - v0;
            Vector3 normal = Vector3.Cross(side1, side2);
            
            Vector3 viewDir = v0;

            if (Vector3.Dot(normal, viewDir) < 0)
            {
                var p0 = projectedPoints[face.a];
                var p1 = projectedPoints[face.b];
                var p2 = projectedPoints[face.c];

                Vector3 lightDir = new Vector3(1, -1, -1);
                float normalLen = (float)Math.Sqrt(normal.X * normal.X + normal.Y * normal.Y + normal.Z * normal.Z);
                float lightLen = (float)Math.Sqrt(lightDir.X * lightDir.X + lightDir.Y * lightDir.Y + lightDir.Z * lightDir.Z);
                
                float dot = -Vector3.Dot(normal, lightDir) / (normalLen * lightLen);
                
                char shade = GetShade(dot);

                _screen.DrawTriangle(p0.x, p0.y, p1.x, p1.y, p2.x, p2.y, shade);
            }
        }

        _screen.Present();
    }

    private char GetShade(float intensity)
    {
        intensity += + 1.2f;
        string shades = " .:-=+*#%@";
        int index = (int)(intensity * (shades.Length - 1));
        if (index < 0) index = 0;
        if (index >= shades.Length) index = shades.Length - 1;
        return shades[index];
    }
}
