using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Bindings.ImGui;
using NecroLens.Model;
using NecroLens.util;

namespace NecroLens.Service;

/**
 * Test Class for stuff drawing tests -not loaded by default
 */
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class ESPTestService : IDisposable
{
    public ESPTestService()
    {
        PluginInterface.UiBuilder.Draw += OnUpdate;
    }

    public void Dispose()
    {
        PluginInterface.UiBuilder.Draw -= OnUpdate;
    }

    private void OnUpdate()
    {
        if (ShouldDraw())
        {
            var drawList = ImGui.GetBackgroundDrawList();
            var player = ClientState.LocalPlayer;
            var espObject = new ESPObject(player!);

            var onScreen = GameGui.WorldToScreen(player!.Position, out _);
            if (onScreen)
            {
                //drawList.AddCircleFilled(position2D, 3f, ColorUtils.ToUint(Color.Red, 0.8f), 100);

                // drawList.PathArcTo(position2D, 2f, 2f, 2f);
                // drawList.PathStroke(ColorUtils.ToUint(Color.Red, 0.8f), ImDrawFlags.RoundCornersDefault, 2f);
                // drawList.PathClear();

                ESPUtils.DrawFacingDirectionArrow(drawList, espObject, Color.Red.ToUint(), 1f, 4f);
            }
        }
    }

    private bool ShouldDraw()
    {
        return !(Condition[ConditionFlag.LoggingOut] ||
                 Condition[ConditionFlag.BetweenAreas] ||
                 Condition[ConditionFlag.BetweenAreas51]) &&
               ClientState.LocalPlayer != null &&
               ClientState.LocalContentId > 0 && ObjectTable.Length > 0;
    }
}
