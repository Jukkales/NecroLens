using System;
using System.Drawing;
using System.Linq;
using System.Numerics;
using Dalamud.Interface;
using Dalamud.Interface.Components;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using NecroLens.Data;
using NecroLens.Model;
using NecroLens.Service;
using NecroLens.util;

namespace NecroLens.Windows;

public class MainWindow : Window, IDisposable
{
    public MainWindow() : base("NecroLens",
                               ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse |
                               ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize |
                               ImGuiWindowFlags.NoFocusOnAppearing)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(370, 260),
            MaximumSize = new Vector2(370, 260)
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
        return PluginService.DeepDungeonService.InDeepDungeon() && PluginService.DeepDungeonService.ready;
    }

    private void DrawTrapStatus()
    {
        var status = PluginService.DeepDungeonService.trapStatus;

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
        var progress = PluginService.DeepDungeonService.passageProgress;
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
                // ImGui.Text($"({progress}%% - approx {PluginService.DeepDungeonService.RemainingKills()} kills left)");
                ImGui.Text($"({progress}%%)");
            }
        }
    }

    private static void DrawTimeSetLine(int floor, int time)
    {
        ImGui.Text(floor.ToString("000:"));
        ImGui.SameLine();
        var text = FormatTime(time);
        var color = time <= 0 ? Color.DimGray.ToV4() : Color.White.ToV4();
        ImGui.TextColored(PluginService.DeepDungeonService.currentFloor == floor ? Color.Yellow.ToV4() : color, text);
    }

    private void DrawTimeSet()
    {
        ImGui.BeginGroup();
        ImGui.Text(Strings.MainWindow_TimeSet_Title);

        var first = PluginService.DeepDungeonService.floorTimes.Take(5);
        ImGui.BeginGroup();
        foreach (var floor in first)
            DrawTimeSetLine(floor.Key, floor.Value);

        ImGui.EndGroup();
        ImGui.SameLine(100);

        var second = PluginService.DeepDungeonService.floorTimes.Skip(5).Take(5);
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
        var effects = PluginService.DeepDungeonService.floorEffects;
        var colorWhite = Color.White.ToV4();
        var colorGrey = Color.DimGray.ToV4();
        ImGui.BeginGroup();
        ImGui.Text(Strings.MainWindow_CurrentFloorEffects_Title);
        ImGui.Indent(15);
        ImGui.TextColored(effects.Contains(Pomander.Affluence) ? colorWhite : colorGrey,
                          Strings.MainWindow_CurrentFloorEffects_Affluence);
        if (PluginService.DeepDungeonService.nextFloorAffluence)
            DrawNextFloorMark();

        ImGui.TextColored(effects.Contains(Pomander.Flight) ? colorWhite : colorGrey,
                          Strings.MainWindow_CurrentFloorEffects_Flight);
        if (PluginService.DeepDungeonService.nextFloorFlight)
            DrawNextFloorMark();

        ImGui.TextColored(effects.Contains(Pomander.Alteration) ? colorWhite : colorGrey,
                          Strings.MainWindow_CurrentFloorEffects_Alteration);
        if (PluginService.DeepDungeonService.nextFloorAlteration)
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
        ImGui.Text(string.Format(Strings.MainWindow_Floor, PluginService.DeepDungeonService.currentFloor));

        if (PluginService.DeepDungeonService.HasRespawn())
        {
            ImGui.SameLine(80);
            ImGui.Text(string.Format(Strings.MainWindow_Respawns,
                                     FormatTime(PluginService.DeepDungeonService.TimeTillRespawn())));
        }

        ImGui.Spacing();
        DrawTrapStatus();
        DrawPassageStatus();

        ImGui.EndGroup();
        ImGui.SameLine();
        ImGui.BeginGroup();

        ImGui.SetCursorPosX(ImGui.GetCursorPosX() + ImGui.GetColumnWidth() - 160);
        var showAggro = PluginService.Configuration.ShowMobViews;
        if (ImGui.Checkbox(Strings.MainWindow_ShowAggro, ref showAggro))
        {
            PluginService.Configuration.ShowMobViews = showAggro;
            PluginService.Configuration.Save();
        }

        ImGui.SameLine();
        ImGui.SetCursorPosX(ImGui.GetCursorPosX() + ImGui.GetColumnWidth() - 20);
        if (ImGuiComponents.IconButton(FontAwesomeIcon.Cog)) PluginService.Plugin.ShowConfigWindow();

        ImGui.SetCursorPosX(ImGui.GetCursorPosX() + ImGui.GetColumnWidth() - 160);
        var openChests = PluginService.Configuration.OpenChests;
        if (ImGui.Checkbox(Strings.MainWindow_OpenChests, ref openChests))
        {
            PluginService.Configuration.OpenChests = openChests;
            PluginService.Configuration.Save();
        }

        ImGui.SameLine();
        HelpMarker(Strings.MainWindow_OpenChests_Help);
        
        ImGui.SameLine();
        ImGui.SetCursorPosX(ImGui.GetCursorPosX() + ImGui.GetColumnWidth() - 20);
        if (ImGuiComponents.IconButton(FontAwesomeIcon.Toolbox)) PluginService.DeepDungeonService.TryNearestOpenChest();
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
