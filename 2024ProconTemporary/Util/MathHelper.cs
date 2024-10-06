namespace _2024ProconTemporary.Util;

public class MathHelper
{
    public static bool Approximately(float a, float b)
    {
        return Math.Abs(a - b) < 0.000001f;
    }

    public static bool Approximately(double a, double b)
    {
        return Math.Abs(a - b) < 0.000000000000001d;
    }
}