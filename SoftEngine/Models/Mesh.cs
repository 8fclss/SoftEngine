using SoftEngine.Mathematics;

namespace SoftEngine.Models;

public class Mesh
{
    public Vector3[] Vertices { get; set; }
    public (int a, int b)[] Edges { get; set; }

    public Mesh(Vector3[] vertices, (int, int)[] edges)
    {
        Vertices = vertices;
        Edges = edges;
    }
}
