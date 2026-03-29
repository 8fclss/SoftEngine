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

        // Build per-vertex normals by accumulating adjacent face normals
        var vertexNormals = new Vector3[_mesh.Vertices.Length];
        foreach (var face in _mesh.Faces)
        {
            Vector3 s1 = worldPoints[face.b] - worldPoints[face.a];
            Vector3 s2 = worldPoints[face.c] - worldPoints[face.a];
            Vector3 faceNormal = Vector3.Cross(s1, s2).Normalize();
            vertexNormals[face.a] = vertexNormals[face.a] + faceNormal;
            vertexNormals[face.b] = vertexNormals[face.b] + faceNormal;
            vertexNormals[face.c] = vertexNormals[face.c] + faceNormal;
        }
        for (int i = 0; i < vertexNormals.Length; i++)
            vertexNormals[i] = vertexNormals[i].Normalize();

        Vector3 lightDir = new Vector3(-0.5f, -0.5f, -1.0f).Normalize();

        foreach (var face in _mesh.Faces)
        {
            Vector3 v0 = worldPoints[face.a];
            Vector3 v1 = worldPoints[face.b];
            Vector3 v2 = worldPoints[face.c];

            Vector3 side1 = v1 - v0;
            Vector3 side2 = v2 - v0;
            Vector3 normal = Vector3.Cross(side1, side2);

            // Backface culling
            if (Vector3.Dot(normal, v0) < 0)
            {
                var p0 = projectedPoints[face.a];
                var p1 = projectedPoints[face.b];
                var p2 = projectedPoints[face.c];

                float z0 = worldPoints[face.a].Z;
                float z1 = worldPoints[face.b].Z;
                float z2 = worldPoints[face.c].Z;

                // Per-vertex intensity for smooth interpolation across the face
                float i0 = Math.Clamp(Vector3.Dot(vertexNormals[face.a], lightDir), 0, 1) + 0.2f;
                float i1 = Math.Clamp(Vector3.Dot(vertexNormals[face.b], lightDir), 0, 1) + 0.2f;
                float i2 = Math.Clamp(Vector3.Dot(vertexNormals[face.c], lightDir), 0, 1) + 0.2f;

                _screen.DrawTriangle(p0.x, p0.y, z0, p1.x, p1.y, z1, p2.x, p2.y, z2, i0, i1, i2);
            }
        }

        _screen.Present();
    }
}
