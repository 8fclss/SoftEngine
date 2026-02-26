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

        var projectedPoints = new (int x, int y)[_mesh.Vertices.Length];
        float fov = _screen.Height;

        for (int i = 0; i < _mesh.Vertices.Length; i++)
        {
            Vector3 v = world.Transform(_mesh.Vertices[i]);
            
            float pZ = v.Z;
            if (pZ < 0.1f) pZ = 0.1f;

            float projectedX = (v.X / pZ) * fov;
            float projectedY = (v.Y / pZ) * fov;

            projectedPoints[i] = (
                (int)(projectedX + (_screen.Width / 2.0f)),
                (int)(projectedY + (_screen.Height / 2.0f))
            );
        }

        foreach (var edge in _mesh.Edges)
        {
            var p1 = projectedPoints[edge.a];
            var p2 = projectedPoints[edge.b];
            _screen.DrawLine(p1.x, p1.y, p2.x, p2.y);
        }

        _screen.Present();
    }
}
