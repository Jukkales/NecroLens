using System;
using System.Diagnostics;
using System.Drawing;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using NecroLens.Data;
using NecroLens.Model;
using NecroLens.Service;
using NecroLens.util;

namespace NecroLens.Windows;

public class ConfigWindow : Window, IDisposable
{
    private readonly Configuration conf;

    public ConfigWindow() : base(Strings.ConfigWindow_Title, ImGuiWindowFlags.AlwaysAutoResize)
    {
        conf = PluginService.Configuration;
    }

    public void Dispose() { }

    public override void Draw()
    {
        if (ImGui.Button("Want to help with localization?"))
            Process.Start(new ProcessStartInfo
                              { FileName = "https://crowdin.com/project/necrolens", UseShellExecute = true });
        if (ImGui.BeginTabBar("MyTabBar", ImGuiTabBarFlags.None))
        {
            if (ImGui.BeginTabItem(Strings.ConfigWindow_Tab_General))
            {
                DrawGeneralTab();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem(Strings.ConfigWindow_Tab_ESPSettings))
            {
                DrawEspTab();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem(Strings.ConfigWindow_Tab_Chests))
            {
                DrawChestsTab();
                ImGui.EndTabItem();
            }
            if (ImGui.BeginTabItem(Strings.ConfigWindow_Tab_Debug))
            {
                DrawDebugTab();
                ImGui.EndTabItem();
            }

            ImGui.EndTabBar();
        }
    }

    private void DrawDebugTab()
    {
        var showDebugInformation = conf.ShowDebugInformation;
        if (ImGui.Checkbox(Strings.ConfigWindow_DebugTab_ShowDebugInformation, ref showDebugInformation))
        {
            conf.ShowDebugInformation = showDebugInformation;
            PluginService.Configuration.Save();
        }

        ImGui.Indent(15);
        ImGui.Text(Strings.ConfigWindow_DebugTab_ShowDebugInformation_Details);
        ImGui.Unindent(15);
        ImGui.Separator();
    }
    
    private void DrawChestsTab()
    {
        var openChests = conf.OpenChests;
        if (ImGui.Checkbox(Strings.ConfigWindow_ChestsTab_OpenChests, ref openChests))
        {
            conf.OpenChests = openChests;
            PluginService.Configuration.Save();
        }

        ImGui.SameLine();
        ImGui.TextColored(Color.Red.ToV4(), Strings.ConfigWindow_ChestsTab_OpenChests_EXPERIMENTAL);
        ImGui.Indent(15);
        ImGui.Text(Strings.ConfigWindow_ChestsTab_OpenChests_Details);
        ImGui.Unindent(15);
        ImGui.Separator();
        ImGui.Spacing();
        ImGui.Text(Strings.ConfigWindow_ChestsTab_OpenFollowingChests);
        ImGui.Indent(15);

        var openBronzeCoffers = conf.OpenBronzeCoffers;
        if (ImGui.Checkbox(Strings.ConfigWindow_ChestsTab_OpenFollowingChests_Bronze, ref openBronzeCoffers))
        {
            conf.OpenBronzeCoffers = openBronzeCoffers;
            PluginService.Configuration.Save();
        }

        ImGui.SameLine();
        var openSilverCoffers = conf.OpenSilverCoffers;
        if (ImGui.Checkbox(Strings.ConfigWindow_ChestsTab_OpenFollowingChests_Silver, ref openSilverCoffers))
        {
            conf.OpenSilverCoffers = openSilverCoffers;
            PluginService.Configuration.Save();
        }

        ImGui.SameLine();
        var openGoldCoffers = conf.OpenGoldCoffers;
        if (ImGui.Checkbox(Strings.ConfigWindow_ChestsTab_OpenFollowingChests_Gold, ref openGoldCoffers))
        {
            conf.OpenGoldCoffers = openGoldCoffers;
            PluginService.Configuration.Save();
        }

        ImGui.SameLine();
        var openHoards = conf.OpenHoards;
        if (ImGui.Checkbox(Strings.ConfigWindow_ChestsTab_OpenFollowingChests_Hoards, ref openHoards))
        {
            conf.OpenHoards = openHoards;
            PluginService.Configuration.Save();
        }

        ImGui.Unindent(15);
        ImGui.Separator();
        ImGui.Spacing();

        var openUnsafeChests = conf.OpenUnsafeChests;
        if (ImGui.Checkbox(Strings.ConfigWindow_ChestsTab_OpenUnsafeChests, ref openUnsafeChests))
        {
            conf.OpenUnsafeChests = openUnsafeChests;
            PluginService.Configuration.Save();
        }

        ImGui.Indent(15);
        ImGui.Text(Strings.ConfigWindow_ChestsTab_OpenUnsafeChests_Details);
        ImGui.Unindent(15);
    }

    private void DrawEspTab()
    {
        var playerDotColor = ImGui.ColorConvertU32ToFloat4(conf.PlayerDotColor).WithoutAlpha();
        if (ImGui.ColorEdit3("##playerDot", ref playerDotColor, ImGuiColorEditFlags.NoInputs))
        {
            conf.PlayerDotColor = ImGui.ColorConvertFloat4ToU32(playerDotColor.WithAlpha(0xCC));
            conf.Save();
        }

        ImGui.SameLine();

        var showPlayerDot = conf.ShowPlayerDot;
        if (ImGui.Checkbox(Strings.ConfigWindow_ESPTab_ShowPlayerDot, ref showPlayerDot))
        {
            conf.ShowPlayerDot = showPlayerDot;
            PluginService.Configuration.Save();
        }

        ImGui.Indent(15);
        ImGui.Text(Strings.ConfigWindow_ESPTab_ShowPlayerDot_Details);
        ImGui.Unindent(15);
        ImGui.Separator();
        ImGui.Spacing();

        ImGui.BeginGroup();
        var showMobViews = conf.ShowMobViews;
        if (ImGui.Checkbox(Strings.ConfigWindow_ESPTab_ShowAggroRange, ref showMobViews))
        {
            conf.ShowMobViews = showMobViews;
            PluginService.Configuration.Save();
        }

        ImGui.Indent(15);
        ImGui.Text(Strings.ConfigWindow_ESPTab_ShowAggroRange_Details);
        ImGui.Unindent(15);

        var normalAggroColor = ImGui.ColorConvertU32ToFloat4(conf.NormalAggroColor).WithoutAlpha();
        if (ImGui.ColorEdit3(Strings.ConfigWindow_ESPTab_ShowAggroRange_Proximity_and_Sight, ref normalAggroColor,
                             ImGuiColorEditFlags.NoInputs))
        {
            conf.NormalAggroColor = ImGui.ColorConvertFloat4ToU32(normalAggroColor.WithAlpha(0xFF));
            conf.Save();
        }

        ImGui.SameLine();
        var soundAggroColor = ImGui.ColorConvertU32ToFloat4(conf.SoundAggroColor).WithoutAlpha();
        if (ImGui.ColorEdit3(Strings.ConfigWindow_ESPTab_ShowAggroRange_Sound, ref soundAggroColor,
                             ImGuiColorEditFlags.NoInputs))
        {
            conf.SoundAggroColor = ImGui.ColorConvertFloat4ToU32(soundAggroColor.WithAlpha(0xFF));
            conf.Save();
        }

        ImGui.EndGroup();

        ImGui.SameLine();

        ImGui.BeginGroup();
        var showPatrolArrow = conf.ShowPatrolArrow;
        if (ImGui.Checkbox(Strings.ConfigWindow_ESPTab_ShowPatrolArrow, ref showPatrolArrow))
        {
            conf.ShowPatrolArrow = showPatrolArrow;
            PluginService.Configuration.Save();
        }

        ImGui.Indent(15);
        ImGui.Text(Strings.ConfigWindow_ESPTab_ShowPatrolArrow_Details);
        ImGui.Unindent(15);
        ImGui.EndGroup();

        ImGui.Separator();
        ImGui.Spacing();

        var showCofferInteractionRange = conf.ShowCofferInteractionRange;
        if (ImGui.Checkbox(Strings.ConfigWindow_ESPTab_ShowCofferInteractionRange, ref showCofferInteractionRange))
        {
            conf.ShowCofferInteractionRange = showCofferInteractionRange;
            PluginService.Configuration.Save();
        }

        ImGui.Indent(15);
        ImGui.Text(Strings.ConfigWindow_ESPTab_ShowCofferInteractionRange_Details);
        ImGui.Unindent(15);

        ImGui.Separator();
        ImGui.Spacing();

        ImGui.Text(Strings.ConfigWindow_ESPTab_HighlightObjects);

        ImGui.Indent(15);
        var highlightCoffers = conf.HighlightCoffers;
        if (ImGui.Checkbox(Strings.ConfigWindow_ESPTab_HighlightObjects_TreasureChests, ref highlightCoffers))
        {
            conf.HighlightCoffers = highlightCoffers;
            PluginService.Configuration.Save();
        }

        var passageColor = ImGui.ColorConvertU32ToFloat4(conf.PassageColor).WithoutAlpha();
        if (ImGui.ColorEdit3("##passage", ref passageColor, ImGuiColorEditFlags.NoInputs))
        {
            conf.PassageColor = ImGui.ColorConvertFloat4ToU32(passageColor.WithAlpha(0xFF));
            conf.Save();
        }

        ImGui.SameLine();
        var highlightPassage = conf.HighlightPassage;
        if (ImGui.Checkbox(Strings.ConfigWindow_ESPTab_HighlightObjects_Passage, ref highlightPassage))
        {
            conf.HighlightPassage = highlightPassage;
            PluginService.Configuration.Save();
        }

        ImGui.Unindent(15);

        ImGui.Separator();
        ImGui.Spacing();
        ImGui.Text(Strings.ConfigWindow_ESPTab_HighlightTreasureChests);
        ImGui.Indent(15);

        var bronzeCofferColor = ImGui.ColorConvertU32ToFloat4(conf.BronzeCofferColor).WithoutAlpha();
        if (ImGui.ColorEdit3("##bronzeCoffer", ref bronzeCofferColor, ImGuiColorEditFlags.NoInputs))
        {
            conf.BronzeCofferColor = ImGui.ColorConvertFloat4ToU32(bronzeCofferColor.WithAlpha(0xFF));
            conf.Save();
        }

        ImGui.SameLine();
        var showBronzeCoffers = conf.ShowBronzeCoffers;
        if (ImGui.Checkbox(Strings.ConfigWindow_ESPTab_HighlightTreasureChests_Bronze, ref showBronzeCoffers))
        {
            conf.ShowBronzeCoffers = showBronzeCoffers;
            PluginService.Configuration.Save();
        }

        var silverCofferColor = ImGui.ColorConvertU32ToFloat4(conf.SilverCofferColor).WithoutAlpha();
        if (ImGui.ColorEdit3("##silverCoffer", ref silverCofferColor, ImGuiColorEditFlags.NoInputs))
        {
            conf.SilverCofferColor = ImGui.ColorConvertFloat4ToU32(silverCofferColor.WithAlpha(0xFF));
            conf.Save();
        }

        ImGui.SameLine();
        var showSilverCoffers = conf.ShowSilverCoffers;
        if (ImGui.Checkbox(Strings.ConfigWindow_ESPTab_HighlightTreasureChests_Silver, ref showSilverCoffers))
        {
            conf.ShowSilverCoffers = showSilverCoffers;
            PluginService.Configuration.Save();
        }

        var goldCofferColor = ImGui.ColorConvertU32ToFloat4(conf.GoldCofferColor).WithoutAlpha();
        if (ImGui.ColorEdit3("##goldCoffer", ref goldCofferColor, ImGuiColorEditFlags.NoInputs))
        {
            conf.GoldCofferColor = ImGui.ColorConvertFloat4ToU32(goldCofferColor.WithAlpha(0xFF));
            conf.Save();
        }

        ImGui.SameLine();
        var showGoldCoffers = conf.ShowGoldCoffers;
        if (ImGui.Checkbox(Strings.ConfigWindow_ESPTab_HighlightTreasureChests_Gold, ref showGoldCoffers))
        {
            conf.ShowGoldCoffers = showGoldCoffers;
            PluginService.Configuration.Save();
        }

        var hoardColor = ImGui.ColorConvertU32ToFloat4(conf.HoardColor).WithoutAlpha();
        if (ImGui.ColorEdit3("##hoard", ref hoardColor, ImGuiColorEditFlags.NoInputs))
        {
            conf.HoardColor = ImGui.ColorConvertFloat4ToU32(hoardColor.WithAlpha(0xFF));
            conf.Save();
        }

        ImGui.SameLine();
        var showHoards = conf.ShowHoards;
        if (ImGui.Checkbox(Strings.ConfigWindow_ESPTab_HighlightTreasureChests_Hoards, ref showHoards))
        {
            conf.ShowHoards = showHoards;
            PluginService.Configuration.Save();
        }

        ImGui.Unindent(15);
    }


    private void DrawGeneralTab()
    {
        var autoOpen = conf.AutoOpenOnEnter;
        if (ImGui.Checkbox(Strings.ConfigWindow_GeneralTab_AutomaticallyOpen, ref autoOpen))
        {
            conf.AutoOpenOnEnter = autoOpen;
            PluginService.Configuration.Save();
        }

        ImGui.Indent(15);
        ImGui.Text(Strings.ConfigWindow_GeneralTab_AutomaticallyOpen_Details);
        ImGui.Unindent(15);
        ImGui.Separator();

        var enableEsp = conf.EnableESP;
        if (ImGui.Checkbox(Strings.ConfigWindow_GeneralTab_EnableOverlay, ref enableEsp))
        {
            conf.EnableESP = enableEsp;
            PluginService.Configuration.Save();
        }

        ImGui.Indent(15);
        ImGui.Text(Strings.ConfigWindow_GeneralTab_EnableOverlay_Details);
        ImGui.Unindent(15);
        ImGui.Separator();

        var openChests = conf.OpenChests;
        if (ImGui.Checkbox(Strings.ConfigWindow_GeneralTab_OpenChests, ref openChests))
        {
            conf.OpenChests = openChests;
            PluginService.Configuration.Save();
        }

        ImGui.Indent(15);
        ImGui.Text(Strings.ConfigWindow_GeneralTab_OpenChests_Details);
        ImGui.Unindent(15);
    }
}
