using SoftEngine.Mathematics;

namespace SoftEngine.Models;

public class Mesh
{
    public Vector3[] Vertices { get; set; }
    public (int a, int b)[] Edges { get; set; }
    public (int a, int b, int c)[] Faces { get; set; }

    public Mesh(Vector3[] vertices, (int, int)[] edges, (int, int, int)[]? faces = null)
    {
        Vertices = vertices;
        Edges = edges;
        Faces = faces ?? Array.Empty<(int, int, int)>();
    }
}
