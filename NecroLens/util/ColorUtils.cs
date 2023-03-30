using System.Drawing;
using System.Numerics;
using ImGuiNET;

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
}
