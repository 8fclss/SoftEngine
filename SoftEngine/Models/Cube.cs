using SoftEngine.Mathematics;

namespace SoftEngine.Models;

public class Cube : Mesh
{
    public Cube() : base(
        new Vector3[]
        {
            new Vector3(-2, -2, -2),
            new Vector3( 2, -2, -2),
            new Vector3( 2,  2, -2),
            new Vector3(-2,  2, -2),
            new Vector3(-2, -2,  2),
            new Vector3( 2, -2,  2),
            new Vector3( 2,  2,  2),
            new Vector3(-2,  2,  2)
        },
        new (int, int)[]
        {
            (0, 1), (1, 2), (2, 3), (3, 0),
            (4, 5), (5, 6), (6, 7), (7, 4),
            (0, 4), (1, 5), (2, 6), (3, 7)
        })
    {
    }
}
