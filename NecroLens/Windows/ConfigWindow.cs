using System;
using System.Drawing;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using NecroLens.Model;
using NecroLens.Service;
using NecroLens.util;

namespace NecroLens.Windows;

public class ConfigWindow : Window, IDisposable
{
    private readonly Configuration conf;

    public ConfigWindow() : base("NecroLens Configuration", ImGuiWindowFlags.AlwaysAutoResize)
    {
        conf = PluginService.Configuration;
    }

    public void Dispose() { }

    public override void Draw()
    {
        if (ImGui.BeginTabBar("MyTabBar", ImGuiTabBarFlags.None))
        {
            if (ImGui.BeginTabItem("General"))
            {
                DrawGeneralTab();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("ESP Settings"))
            {
                DrawESPTab();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("Chests"))
            {
                DrawChestsTab();
                ImGui.EndTabItem();
            }

            ImGui.EndTabBar();
        }
    }

    private void DrawChestsTab()
    {
        var openChests = conf.OpenChests;
        if (ImGui.Checkbox("Open Chests", ref openChests))
        {
            conf.OpenChests = openChests;
            PluginService.Configuration.Save();
        }

        ImGui.SameLine();
        ImGui.TextColored(Color.Red.ToV4(), "EXPERIMENTAL");
        ImGui.Indent(15);
        ImGui.Text("Automatically opens chest for you once you're in interaction range.\n" +
                   "The plugin only tries to open a chest one time!");
        ImGui.Unindent(15);
        ImGui.Separator();
        ImGui.Spacing();
        ImGui.Text("Open the following chests:");
        ImGui.Indent(15);

        var openBronzeCoffers = conf.OpenBronzeCoffers;
        if (ImGui.Checkbox("Bronze", ref openBronzeCoffers))
        {
            conf.OpenBronzeCoffers = openBronzeCoffers;
            PluginService.Configuration.Save();
        }

        ImGui.SameLine();
        var openSilverCoffers = conf.OpenSilverCoffers;
        if (ImGui.Checkbox("Silver", ref openSilverCoffers))
        {
            conf.OpenSilverCoffers = openSilverCoffers;
            PluginService.Configuration.Save();
        }

        ImGui.SameLine();
        var openGoldCoffers = conf.OpenGoldCoffers;
        if (ImGui.Checkbox("Gold", ref openGoldCoffers))
        {
            conf.OpenGoldCoffers = openGoldCoffers;
            PluginService.Configuration.Save();
        }

        ImGui.SameLine();
        var openHoards = conf.OpenHoards;
        if (ImGui.Checkbox("Hoards", ref openHoards))
        {
            conf.OpenHoards = openHoards;
            PluginService.Configuration.Save();
        }

        ImGui.Unindent(15);
        ImGui.Separator();
        ImGui.Spacing();

        var openUnsafeChests = conf.OpenUnsafeChests;
        if (ImGui.Checkbox("Open unsafe chests", ref openUnsafeChests))
        {
            conf.OpenUnsafeChests = openUnsafeChests;
            PluginService.Configuration.Save();
        }

        ImGui.Indent(15);
        ImGui.Text("Even open chests which can be Mimics on the current floor");
        ImGui.Unindent(15);
    }

    private void DrawESPTab()
    {
        
        var playerDotColor = ImGui.ColorConvertU32ToFloat4(conf.PlayerDotColor).WithoutAlpha();
        if (ImGui.ColorEdit3("##playerDot", ref playerDotColor, ImGuiColorEditFlags.NoInputs))
        {
            conf.PlayerDotColor = ImGui.ColorConvertFloat4ToU32(playerDotColor.WithAlpha(0xCC));
            conf.Save();
        }
        ImGui.SameLine();
        
        var showPlayerDot = conf.ShowPlayerDot;
        if (ImGui.Checkbox("Show Player Dot", ref showPlayerDot))
        {
            conf.ShowPlayerDot = showPlayerDot;
            PluginService.Configuration.Save();
        }

        ImGui.Indent(15);
        ImGui.Text("A little dot showing your hit box.");
        ImGui.Unindent(15);
        ImGui.Separator();
        ImGui.Spacing();

        ImGui.BeginGroup();
        var showMobViews = conf.ShowMobViews;
        if (ImGui.Checkbox("Show aggro range", ref showMobViews))
        {
            conf.ShowMobViews = showMobViews;
            PluginService.Configuration.Save();
        }

        ImGui.Indent(15);
        ImGui.Text("Draw sight-, proximity- and sound-range.\nSound Mobs will also have hit box.");
        ImGui.Unindent(15);
        
        var normalAggroColor = ImGui.ColorConvertU32ToFloat4(conf.NormalAggroColor).WithoutAlpha();
        if (ImGui.ColorEdit3("Proximity and Sight   ", ref normalAggroColor, ImGuiColorEditFlags.NoInputs))
        {
            conf.NormalAggroColor = ImGui.ColorConvertFloat4ToU32(normalAggroColor.WithAlpha(0xFF));
            conf.Save();
        }
        ImGui.SameLine();
        var soundAggroColor = ImGui.ColorConvertU32ToFloat4(conf.SoundAggroColor).WithoutAlpha();
        if (ImGui.ColorEdit3("Sound", ref soundAggroColor, ImGuiColorEditFlags.NoInputs))
        {
            conf.SoundAggroColor = ImGui.ColorConvertFloat4ToU32(soundAggroColor.WithAlpha(0xFF));
            conf.Save();
        }
        ImGui.EndGroup();
        
        ImGui.SameLine();
        
        ImGui.BeginGroup();
        var showPatrolArrow = conf.ShowPatrolArrow;
        if (ImGui.Checkbox("Show Arrow on Patrol", ref showPatrolArrow))
        {
            conf.ShowPatrolArrow = showPatrolArrow;
            PluginService.Configuration.Save();
        }

        ImGui.Indent(15);
        ImGui.Text("Draw an arrow pointing in movement direction.");
        ImGui.Unindent(15);
        ImGui.EndGroup();

        ImGui.Separator();
        ImGui.Spacing();

        var showCofferInteractionRange = conf.ShowCofferInteractionRange;
        if (ImGui.Checkbox("Show interaction range for coffers", ref showCofferInteractionRange))
        {
            conf.ShowCofferInteractionRange = showCofferInteractionRange;
            PluginService.Configuration.Save();
        }

        ImGui.Indent(15);
        ImGui.Text("Once you approach a treasure coffer a light circle shows up the maximum distance to open.");
        ImGui.Unindent(15);

        ImGui.Separator();
        ImGui.Spacing();

        ImGui.Text("Highlight the following objects once near:");

        ImGui.Indent(15);
        var highlightCoffers = conf.HighlightCoffers;
        if (ImGui.Checkbox("Treasure Chests", ref highlightCoffers))
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
        if (ImGui.Checkbox("Passage", ref highlightPassage))
        {
            conf.HighlightPassage = highlightPassage;
            PluginService.Configuration.Save();
        }

        ImGui.Unindent(15);

        ImGui.Separator();
        ImGui.Spacing();
        ImGui.Text("Show the following treasure coffers in the overlay:");
        ImGui.Indent(15);

        var bronzeCofferColor = ImGui.ColorConvertU32ToFloat4(conf.BronzeCofferColor).WithoutAlpha();
        if (ImGui.ColorEdit3("##bronzeCoffer", ref bronzeCofferColor, ImGuiColorEditFlags.NoInputs))
        {
            conf.BronzeCofferColor = ImGui.ColorConvertFloat4ToU32(bronzeCofferColor.WithAlpha(0xFF));
            conf.Save();
        }
        ImGui.SameLine();
        var showBronzeCoffers = conf.ShowBronzeCoffers;
        if (ImGui.Checkbox("Bronze Chests", ref showBronzeCoffers))
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
        if (ImGui.Checkbox("Silver Chests", ref showSilverCoffers))
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
        if (ImGui.Checkbox("Gold Chests", ref showGoldCoffers))
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
        if (ImGui.Checkbox("Accursed Hoards", ref showHoards))
        {
            conf.ShowHoards = showHoards;
            PluginService.Configuration.Save();
        }

        ImGui.Unindent(15);
    }


    private void DrawGeneralTab()
    {
        var autoOpen = conf.AutoOpenOnEnter;
        if (ImGui.Checkbox("Automatically open in DeepDungeon", ref autoOpen))
        {
            conf.AutoOpenOnEnter = autoOpen;
            PluginService.Configuration.Save();
        }

        ImGui.Indent(15);
        ImGui.Text("Opens the Main window when you enter a DeepDungeon.\n" +
                   "It shows some handy information and quick access to Settings.");
        ImGui.Unindent(15);
        ImGui.Separator();

        var enableEsp = conf.EnableESP;
        if (ImGui.Checkbox("Enable ESP Overlay", ref enableEsp))
        {
            conf.EnableESP = enableEsp;
            PluginService.Configuration.Save();
        }

        ImGui.Indent(15);
        ImGui.Text("Draws the ESP overlay showing the positions of Chests, Mobs and all aggro ranges.\n" +
                   "You can configure everything you want to see under ESP Settings.\n" +
                   "Important: The ESP can only show objects in the games render distance.\n" +
                   "This is fixed and CAN NOT be changed!");
        ImGui.Unindent(15);
        ImGui.Separator();

        var openChests = conf.OpenChests;
        if (ImGui.Checkbox("Enable OpenChests", ref openChests))
        {
            conf.OpenChests = openChests;
            PluginService.Configuration.Save();
        }

        ImGui.Indent(15);
        ImGui.Text("EXPERIMENTAL FEATURE\n" +
                   "Automatically opens chest for you once you're in interaction range.\n" +
                   "By default it will NOT open chests which can be Mimics this set!\n\n" +
                   "This feature can cause your character being stuck next to a chest!\n" +
                   "If this happens disable this feature.");
        ImGui.Unindent(15);
    }
}
