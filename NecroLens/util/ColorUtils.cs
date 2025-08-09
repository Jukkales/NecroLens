using System.Drawing;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace NecroLens.util;

public static class ColorUtils
{
    public static uint ToUint(this Color c, float alpha = 1f)
    {
        return ImGui.ColorConvertFloat4ToU32(new Vector4(c.R / 255.0f, c.G / 255.0f, c.B / 255.0f, alpha));
    }

    public static Vector4 ToV4(this Color c, float alpha = 1f)
    {
        return new Vector4(c.R / 255.0f, c.G / 255.0f, c.B / 255.0f, alpha);
    }
    
    public static Vector4 WithAlpha(this Vector3 c, float alpha = 1f)
    {
        return new Vector4(c.X, c.Y, c.Z, alpha);
    }
    
    public static Vector3 WithoutAlpha(this Vector4 c)
    {
        return new Vector3(c.X, c.Y, c.Z);
    }

    public static uint SetAlpha(this uint color, float alpha = 1f)
    {
        var red = (color >> 16) & 0xff;
        var green = (color >> 8) & 0xff;
        var blue = color & 0xff;
        return ((uint) (255f*alpha) << 24) | (red << 16) | (green << 8) | blue;
    }
}
