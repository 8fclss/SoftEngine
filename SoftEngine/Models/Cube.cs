using SoftEngine.Mathematics;

namespace SoftEngine.Models;

public class Cube : Mesh
{
    public Cube() : base(
        new Vector3[]
        {
            new Vector3(-2, -2, -2), // 0 BLF
            new Vector3( 2, -2, -2), // 1 BRF
            new Vector3( 2,  2, -2), // 2 TRF
            new Vector3(-2,  2, -2), // 3 TLF
            new Vector3(-2, -2,  2), // 4 BLB
            new Vector3( 2, -2,  2), // 5 BRB
            new Vector3( 2,  2,  2), // 6 TRB
            new Vector3(-2,  2,  2)  // 7 TLB
        },
        new (int, int)[]
        {
            (0, 1), (1, 2), (2, 3), (3, 0),
            (4, 5), (5, 6), (6, 7), (7, 4),
            (0, 4), (1, 5), (2, 6), (3, 7)
        },
        new (int, int, int)[]
        {
            (3, 2, 1), (3, 1, 0),   // Front
            (4, 5, 6), (4, 6, 7),   // Back
            (0, 7, 3), (0, 4, 7),   // Left
            (1, 2, 6), (1, 6, 5),   // Right
            (3, 6, 2), (3, 7, 6),   // Top
            (0, 1, 5), (0, 5, 4)    // Bottom
        })
    {}
}
