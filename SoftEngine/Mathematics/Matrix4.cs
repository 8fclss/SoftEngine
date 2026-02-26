namespace SoftEngine.Mathematics;

public struct Matrix4
{
    public float M11, M12, M13, M14;
    public float M21, M22, M23, M24;
    public float M31, M32, M33, M34;
    public float M41, M42, M43, M44;

    public static Matrix4 Identity => new Matrix4
    {
        M11 = 1, M22 = 1, M33 = 1, M44 = 1
    };

    public static Matrix4 CreateRotationX(float radians)
    {
        float cos = (float)Math.Cos(radians);
        float sin = (float)Math.Sin(radians);
        var res = Identity;
        res.M22 = cos; res.M23 = -sin;
        res.M32 = sin; res.M33 = cos;
        return res;
    }

    public static Matrix4 CreateRotationY(float radians)
    {
        float cos = (float)Math.Cos(radians);
        float sin = (float)Math.Sin(radians);
        var res = Identity;
        res.M11 = cos; res.M13 = sin;
        res.M31 = -sin; res.M33 = cos;
        return res;
    }

    public static Matrix4 CreateRotationZ(float radians)
    {
        float cos = (float)Math.Cos(radians);
        float sin = (float)Math.Sin(radians);
        var res = Identity;
        res.M11 = cos; res.M12 = -sin;
        res.M21 = sin; res.M22 = cos;
        return res;
    }

    public static Matrix4 CreateTranslation(float x, float y, float z)
    {
        var res = Identity;
        res.M14 = x; res.M24 = y; res.M34 = z;
        return res;
    }

    public static Matrix4 Multiply(Matrix4 a, Matrix4 b)
    {
        return new Matrix4
        {
            M11 = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31 + a.M14 * b.M41,
            M12 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32 + a.M14 * b.M42,
            M13 = a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33 + a.M14 * b.M43,
            M14 = a.M11 * b.M14 + a.M12 * b.M24 + a.M13 * b.M34 + a.M14 * b.M44,

            M21 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31 + a.M24 * b.M41,
            M22 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32 + a.M24 * b.M42,
            M23 = a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33 + a.M24 * b.M43,
            M24 = a.M21 * b.M14 + a.M22 * b.M24 + a.M23 * b.M34 + a.M24 * b.M44,

            M31 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31 + a.M34 * b.M41,
            M32 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32 + a.M34 * b.M42,
            M33 = a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33 + a.M34 * b.M43,
            M34 = a.M31 * b.M14 + a.M32 * b.M24 + a.M33 * b.M34 + a.M34 * b.M44,

            M41 = a.M41 * b.M11 + a.M42 * b.M21 + a.M43 * b.M31 + a.M44 * b.M41,
            M42 = a.M41 * b.M12 + a.M42 * b.M22 + a.M43 * b.M32 + a.M44 * b.M42,
            M43 = a.M41 * b.M13 + a.M42 * b.M23 + a.M43 * b.M33 + a.M44 * b.M43,
            M44 = a.M41 * b.M14 + a.M42 * b.M24 + a.M43 * b.M34 + a.M44 * b.M44
        };
    }

    public static Matrix4 operator *(Matrix4 a, Matrix4 b) => Multiply(a, b);

    public Vector3 Transform(Vector3 v)
    {
        return new Vector3(
            v.X * M11 + v.Y * M12 + v.Z * M13 + M14,
            v.X * M21 + v.Y * M22 + v.Z * M23 + M24,
            v.X * M31 + v.Y * M32 + v.Z * M33 + M34
        );
    }
}
