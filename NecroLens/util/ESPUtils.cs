using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Numerics;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Bindings.ImGui;
using NecroLens.Model;

namespace NecroLens.util;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class ESPUtils
{
    public const float DefaultCircleThickness = 2f;
    public const float DefaultFilledOpacity = 0.4f;
    public const int CircleSegments = 50;
    public const float CircleSegmentFullRotation = 2 * MathF.PI / CircleSegments;

    public static float Distance2D(this Vector3 v, Vector3 v2)
    {
        return new Vector2(v.X - v2.X, v.Z - v2.Z).Length();
    }

    public static bool IsIgnoredObject(IGameObject gameObject)
    {
        if (DataIds.IgnoredDataIDs.Contains(gameObject.BaseId)) return true;
        if (gameObject.IsDead || gameObject is IBattleNpc { CurrentHp: <= 0 }) return true;

        return false;
    }

    public static void DrawName(ImDrawListPtr drawList, ESPObject espObject, Vector2 position)
    {
        var name = espObject.Name();

        if (espObject.Type == ESPObject.ESPType.GoldChest && espObject.ContainingPomander != null)
        {
            name += "\n" + DungeonService.PomanderNames[espObject.ContainingPomander.Value];
        }

        var textSize = ImGui.CalcTextSize(name);
        // Center name on position
        var textPosition = new Vector2(position.X - (textSize.X / 2f), position.Y + (textSize.Y / 2f));
        drawList.AddText(textPosition, espObject.RenderColor(), name);
    }

    public static void DrawPlayerDot(ImDrawListPtr drawList, Vector2 position)
    {
        drawList.AddCircleFilled(position, 3f, Config.PlayerDotColor, 100);
    }

    public static void DrawInteractionCircle(ImDrawListPtr drawList, ESPObject espObject, float radius)
    {
        var color = Color.White.ToUint(1 - (espObject.Distance() / (radius + 5)));
        DrawCircleInternal(drawList, espObject, radius, color, false, 1f);
        drawList.PathClear();
    }

    public static void DrawConeFromCenterPoint(
        ImDrawListPtr drawList, ESPObject espObject, float angleRadian, float radius, uint outlineColor)
    {
        var position = espObject.GameObject.Position;
        var rotation = espObject.GameObject.Rotation + (MathF.PI / 4);
        var partialCircleSegmentRotation = angleRadian / CircleSegments;
        var coneColor = outlineColor.SetAlpha(0.2f);

        GameGui.WorldToScreen(new Vector3(position.X, position.Y, position.Z),
                                            out var originPositionOnScreen);
        drawList.PathLineTo(originPositionOnScreen);
        for (var i = 0; i <= CircleSegments; i++)
        {
            var currentRotation = rotation - (i * partialCircleSegmentRotation);
            var xValue = radius * MathF.Sin(currentRotation);
            var yValue = radius * MathF.Cos(currentRotation);
            GameGui.WorldToScreen(new Vector3(position.X + xValue, position.Y, position.Z + yValue),
                                                out var segmentVectorOnCircle);
            drawList.PathLineTo(segmentVectorOnCircle);
        }

        drawList.PathFillConvex(coneColor);
        drawList.PathClear();
        drawList.PathLineTo(originPositionOnScreen);
        for (var i = 0; i <= CircleSegments; i++)
        {
            var currentRotation = rotation - (i * partialCircleSegmentRotation);
            var xValue = radius * MathF.Sin(currentRotation);
            var yValue = radius * MathF.Cos(currentRotation);
            GameGui.WorldToScreen(new Vector3(position.X + xValue, position.Y, position.Z + yValue),
                                                out var segmentVectorOnCircle);
            drawList.PathLineTo(segmentVectorOnCircle);
        }

        drawList.PathLineTo(originPositionOnScreen);

        drawList.PathStroke(outlineColor);
    }

    public static void DrawCircleFilled(
        ImDrawListPtr drawList, ESPObject espObject, float radius, uint circleColor,
        float thickness = DefaultCircleThickness)
    {
        var filledColor = circleColor.SetAlpha(0.15f);

        DrawCircleInternal(drawList, espObject, radius, circleColor, false, thickness);
        DrawCircleInternal(drawList, espObject, radius, filledColor, true, thickness);
        drawList.PathClear();
    }

    public static void DrawCircle(
        ImDrawListPtr drawList, ESPObject espObject, float radius, uint color,
        float opacity = 1f, float thickness = DefaultCircleThickness)
    {
        DrawCircleInternal(drawList, espObject, radius, color.SetAlpha(opacity), false, thickness);
        drawList.PathClear();
    }

    private static void DrawDottedCircle(
        ImDrawListPtr drawList, ESPObject espObject, float radius, uint color, float thickness)
    {
        var position = espObject.GameObject.Position;
        var circleSegmentFullRotation = 2 * MathF.PI / 60;
        for (var i = 0; i <= 60; i++)
        {
            var currentRotation = i * circleSegmentFullRotation;
            var xValue = radius * MathF.Sin(currentRotation);
            var yValue = radius * MathF.Cos(currentRotation);
            GameGui.WorldToScreen(new Vector3(position.X + xValue, position.Y, position.Z + yValue),
                                                out var segment);
            // drawList.PathLineTo(segment);
            drawList.PathArcTo(segment, 1f, 1f, 1f);
            drawList.PathStroke(color, ImDrawFlags.RoundCornersDefault, thickness);
        }
    }

    public static void DrawFacingDirectionArrow(
        ImDrawListPtr drawList, ESPObject espObject, uint color,
        float opacity = 1f, float thickness = DefaultCircleThickness)
    {
        var points = new List<(float, float)>
        {
            (4, -40), (6f, 0), (4, 40), (3.9f, 11), (2f, 22), (2f, -22), (3.9f, -11), (4, -40)
        };
        foreach (var (radian, steps) in points)
            drawList.PathLineTo(CreatePointAroundObjectOnScreen(espObject, radian, steps));

        drawList.PathStroke(color.SetAlpha(opacity), ImDrawFlags.RoundCornersDefault, thickness);
        drawList.PathClear();
    }

    private static void DrawCircleInternal(
        ImDrawListPtr drawList, ESPObject espObject, float radius, uint color, bool filled,
        float thickness)
    {
        var position = espObject.GameObject.Position;
        for (var i = 0; i <= CircleSegments; i++)
        {
            var currentRotation = i * CircleSegmentFullRotation;
            var xValue = radius * MathF.Sin(currentRotation);
            var yValue = radius * MathF.Cos(currentRotation);
            GameGui.WorldToScreen(new Vector3(position.X + xValue, position.Y, position.Z + yValue),
                                                out var segment);
            drawList.PathLineTo(segment);
        }

        if (filled)
            drawList.PathFillConvex(color);
        else
            drawList.PathStroke(color, ImDrawFlags.RoundCornersDefault, thickness);
    }

    private static Vector2 CreatePointAroundObjectOnScreen(ESPObject espObject, float radian, float steps)
    {
        var pos = espObject.GameObject.Position;
        var rotation = espObject.GameObject.Rotation;
        var partialCircleSegmentRotation = 2 * MathF.PI / 1000;
        var stepRotation = rotation - (steps * partialCircleSegmentRotation);
        var xValue = radian * MathF.Sin(stepRotation);
        var yValue = radian * MathF.Cos(stepRotation);
        var stepPos = pos with { X = pos.X + xValue, Z = pos.Z + yValue };
        GameGui.WorldToScreen(stepPos, out var segment);
        return segment;
    }
}
