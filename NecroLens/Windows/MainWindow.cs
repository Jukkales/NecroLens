using System;
using System.Drawing;
using System.Linq;
using System.Numerics;
using Dalamud.Interface;
using Dalamud.Interface.Components;
using Dalamud.Interface.Windowing;
using Dalamud.Bindings.ImGui;
using NecroLens.Data;
using NecroLens.Model;
using NecroLens.util;

namespace NecroLens.Windows;

public class MainWindow : Window, IDisposable
{
    public MainWindow() : base("NecroLens",
                               ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse |
                               ImGuiWindowFlags.NoCollapse |
                               ImGuiWindowFlags.NoFocusOnAppearing)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(370, 260),
            MaximumSize = new Vector2(640, 280)
        };
        RespectCloseHotkey = false;
    }

    public void Dispose() { }

    private static void HelpMarker(String desc)
    {
        ImGui.TextDisabled("(?)");
        if (ImGui.IsItemHovered(ImGuiHoveredFlags.AllowWhenDisabled))
        {
            ImGui.BeginTooltip();
            ImGui.PushTextWrapPos(ImGui.GetFontSize() * 35.0f);
            ImGui.TextUnformatted(desc);
            ImGui.PopTextWrapPos();
            ImGui.EndTooltip();
        }
    }

    private static String FormatTime(int seconds)
    {
        return TimeSpan.FromSeconds(seconds).ToString(@"mm\:ss");
    }

    public override bool DrawConditions()
    {
        return DeepDungeonUtil.InDeepDungeon && DungeonService.Ready;
    }

    private void DrawTrapStatus()
    {
        var status = DungeonService.FloorDetails.TrapStatus();

        ImGui.Text(Strings.MainWindow_TrapStatus_Title);
        ImGui.SameLine();

        switch (status)
        {
            case DeepDungeonTrapStatus.Active:
                ImGui.TextColored(Color.Red.ToV4(), Strings.MainWindow_TrapStatus_Active);
                break;
            case DeepDungeonTrapStatus.Visible:
                ImGui.TextColored(Color.Yellow.ToV4(), Strings.MainWindow_TrapStatus_Visible);
                break;
            case DeepDungeonTrapStatus.Inactive:
                ImGui.TextColored(Color.Green.ToV4(), Strings.MainWindow_TrapStatus_Inactive);
                break;
        }
    }

    private void DrawPassageStatus()
    {
        var progress = DungeonService.FloorDetails.PassageProgress();
        ImGui.Text(Strings.MainWindow_PassageStatus_Title);
        ImGui.SameLine();
        if (progress == 100)
            ImGui.TextColored(Color.Green.ToV4(), Strings.MainWindow_PassageStatus_Open);
        else
        {
            ImGui.TextColored(Color.Red.ToV4(), Strings.MainWindow_PassageStatus_Closed);

            if (progress > 0)
            {
                ImGui.SameLine();
                // ImGui.Text($"({progress}% - approx {DeepDungeonService.RemainingKills()} kills left)");
                ImGui.Text($"({progress}%)");
            }
        }
    }

    private static void DrawTimeSetLine(int floor, int time)
    {
        ImGui.Text(floor.ToString("000:"));
        ImGui.SameLine();
        var text = FormatTime(time);
        var color = time <= 0 ? Color.DimGray.ToV4() : Color.White.ToV4();
        ImGui.TextColored(DungeonService.FloorDetails.CurrentFloor == floor ? Color.Yellow.ToV4() : color, text);
    }

    private void DrawTimeSet()
    {
        ImGui.BeginGroup();
        ImGui.Text(Strings.MainWindow_TimeSet_Title);

        var first = DungeonService.FloorTimes.Take(5);
        ImGui.BeginGroup();
        foreach (var floor in first)
            DrawTimeSetLine(floor.Key, floor.Value);

        ImGui.EndGroup();
        ImGui.SameLine(100);

        var second = DungeonService.FloorTimes.Skip(5).Take(5);
        ImGui.BeginGroup();
        foreach (var floor in second)
            DrawTimeSetLine(floor.Key, floor.Value);

        ImGui.EndGroup();

        ImGui.EndGroup();
    }

    private void DrawNextFloorMark()
    {
        ImGui.SameLine();
        ImGui.TextColored(Color.Green.ToV4(), "(+)");
        if (ImGui.IsItemHovered(ImGuiHoveredFlags.AllowWhenDisabled))
        {
            ImGui.BeginTooltip();
            ImGui.PushTextWrapPos(ImGui.GetFontSize() * 35.0f);
            ImGui.TextUnformatted(Strings.MainWindow_NextFloorMark_ActiveNextFloor);
            ImGui.PopTextWrapPos();
            ImGui.EndTooltip();
        }
    }

    private void DrawCurrentFloorEffects()
    {
        var effects = DungeonService.FloorDetails.GetFloorEffects();
        var colorWhite = Color.White.ToV4();
        var colorGrey = Color.DimGray.ToV4();
        ImGui.BeginGroup();
        ImGui.Text(Strings.MainWindow_CurrentFloorEffects_Title);
        ImGui.Indent(15);
        ImGui.TextColored(effects.Contains(Pomander.Affluence) ? colorWhite : colorGrey,
                          Strings.MainWindow_CurrentFloorEffects_Affluence);
        if (DungeonService.FloorDetails.IsNextFloorWith(Pomander.Affluence))
            DrawNextFloorMark();

        ImGui.TextColored(effects.Contains(Pomander.Flight) ? colorWhite : colorGrey,
                          Strings.MainWindow_CurrentFloorEffects_Flight);
        if (DungeonService.FloorDetails.IsNextFloorWith(Pomander.Flight))
            DrawNextFloorMark();

        ImGui.TextColored(effects.Contains(Pomander.Alteration) ? colorWhite : colorGrey,
                          Strings.MainWindow_CurrentFloorEffects_Alteration);
        if (DungeonService.FloorDetails.IsNextFloorWith(Pomander.Alteration))
            DrawNextFloorMark();

        ImGui.TextColored(effects.Contains(Pomander.Safety) ? colorWhite : colorGrey,
                          Strings.MainWindow_CurrentFloorEffects_Safety);
        ImGui.TextColored(effects.Contains(Pomander.Sight) ? colorWhite : colorGrey,
                          Strings.MainWindow_CurrentFloorEffects_Sight);
        ImGui.TextColored(effects.Contains(Pomander.Fortune) ? colorWhite : colorGrey,
                          Strings.MainWindow_CurrentFloorEffects_Fortune);
        ImGui.EndGroup();
    }

    public override void Draw()
    {
        ImGui.BeginGroup();
        ImGui.Text(string.Format(Strings.MainWindow_Floor, DungeonService.FloorDetails.CurrentFloor));

        if (DungeonService.FloorDetails.HasRespawn())
        {
            ImGui.SameLine(80);
            ImGui.Text(string.Format(Strings.MainWindow_Respawns,
                                     FormatTime(DungeonService.FloorDetails.TimeTillRespawn())));
        }

        ImGui.Spacing();
        DrawTrapStatus();
        DrawPassageStatus();

        ImGui.EndGroup();
        ImGui.SameLine();
        ImGui.BeginGroup();

        ImGui.SetCursorPosX(ImGui.GetCursorPosX() + ImGui.GetColumnWidth() - 160);
        var showAggro = Config.ShowMobViews;
        if (ImGui.Checkbox(Strings.MainWindow_ShowAggro, ref showAggro))
        {
            Config.ShowMobViews = showAggro;
            Config.Save();
        }

        ImGui.SameLine();
        ImGui.SetCursorPosX(ImGui.GetCursorPosX() + ImGui.GetColumnWidth() - 20);
        if (ImGuiComponents.IconButton(FontAwesomeIcon.Cog)) Plugin.ShowConfigWindow();

        ImGui.SetCursorPosX(ImGui.GetCursorPosX() + ImGui.GetColumnWidth() - 160);
        var openChests = Config.OpenChests;
        if (ImGui.Checkbox(Strings.MainWindow_OpenChests, ref openChests))
        {
            Config.OpenChests = openChests;
            Config.Save();
        }

        ImGui.SameLine();
        HelpMarker(Strings.MainWindow_OpenChests_Help);

        ImGui.SameLine();
        ImGui.SetCursorPosX(ImGui.GetCursorPosX() + ImGui.GetColumnWidth() - 20);
        if (ImGuiComponents.IconButton(FontAwesomeIcon.Toolbox)) DungeonService.TryNearestOpenChest();
        if (ImGui.IsItemHovered())
        {
            ImGui.BeginTooltip();
            ImGui.PushTextWrapPos(ImGui.GetFontSize() * 35.0f);
            ImGui.TextUnformatted(Strings.MainWindow_OpenChestButton_Help);
            ImGui.PopTextWrapPos();
            ImGui.EndTooltip();
        }

        ImGui.EndGroup();

        ImGui.Separator();
        DrawTimeSet();
        ImGui.SameLine();
        DrawCurrentFloorEffects();
    }
}
