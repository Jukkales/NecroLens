using System.Globalization;
using Dalamud.Game;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using ECommons;
using ECommons.Configuration;
using NecroLens.Data;
using NecroLens.Configuration;
using NecroLens.Service;
using NecroLens.Windows;
using Pictomancy;
using ECommons.DalamudServices;
using NecroLens.PictoRender;
using ECommons.GameHelpers;
using NecroLens.MobData;
using NecroLens.util;

namespace NecroLens;

public sealed class NecroLens : IDalamudPlugin
{

    internal static NecroLens P = null!;

    private Config config;
    public static Config C => P.config;
    public static PctContext PictoService;

    // Windows
    internal WindowSystem windowSystem;
    internal ConfigWindow configWindow;
    internal MainWindow mainWindow;

    public NecroLens(IDalamudPluginInterface pi)
    {
        P = this;
        ECommonsMain.Init(pi, P, Module.DalamudReflector);
        new ECommons.Schedulers.TickScheduler(Load);
        PictoService = PctService.Initialize(pi);
    }

    public void Load()
    {
        EzConfig.Migrate<Config>();
        config = EzConfig.Init<Config>();

        MobDatabase.UpdateMobInfo();

        windowSystem = new();
        configWindow = new();
        mainWindow = new();

        EzCmd.Add("/necrolens", OnCommand, $""" {Strings.PluginCommands_OpenOverlay_Help}""");
        EzCmd.Add("/necrolenscfg", OnCommand, $"""{Strings.PluginCommands_OpenConfig_Help}""");
        EzCmd.Add("/openchest", OnCommand, $"""{Strings.PluginCommands_OpenChest_Help}""");
        EzCmd.Add("/pomander", OnCommand, "Try to use the ponander with given name");

        Svc.Framework.Update += Tick;
        Svc.PluginInterface.UiBuilder.Draw += OnDraw;
        Svc.PluginInterface.UiBuilder.Draw += windowSystem.Draw;
        Svc.PluginInterface.UiBuilder.OpenMainUi += ShowMainWindow;
        Svc.PluginInterface.UiBuilder.OpenConfigUi += ShowConfigWindow;

        if (config.Language == "")
        {
            CultureInfo.DefaultThreadCurrentUICulture = Svc.ClientState.ClientLanguage switch
            {
                ClientLanguage.French => CultureInfo.GetCultureInfo("fr"),
                ClientLanguage.German => CultureInfo.GetCultureInfo("de"),
                ClientLanguage.Japanese => CultureInfo.GetCultureInfo("ja"),
                _ => CultureInfo.GetCultureInfo("en")
            };
        }
        else
        {
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo(config.Language);
        }
    }

    public void Dispose()
    {
        GenericHelpers.Safe(() => Svc.Framework.Update -= Tick);
        GenericHelpers.Safe(() => Svc.PluginInterface.UiBuilder.Draw -= OnDraw);
        GenericHelpers.Safe(() => Svc.PluginInterface.UiBuilder.Draw -= windowSystem.Draw);
        GenericHelpers.Safe(() => Svc.PluginInterface.UiBuilder.OpenMainUi -= ShowMainWindow);
        GenericHelpers.Safe(() => Svc.PluginInterface.UiBuilder.OpenConfigUi -= ShowConfigWindow);
        GenericHelpers.Safe(() => ECommonsMain.Dispose());

        // Windows to be removed
        GenericHelpers.Safe(() => configWindow.Dispose());
        GenericHelpers.Safe(() => mainWindow.Dispose());
        GenericHelpers.Safe(() => PictoService.Dispose());
    }

    private void Tick(object _)
    {
        if (DeepDungeonUtil.InDeepDungeon)
        {
            // PictoManager.CheckObjects();
        }
    }

    private void OnDraw()
    {
        if (Player.Available && DeepDungeonUtil.InDeepDungeon)
        {
            PictoManager.DrawPicto();
            PictoManager.CheckObjects();
        }
    }

    private void OnCommand(string command, string args)
    {
        if (command == "/necrolens")
        {
            Svc.Log.Verbose("Opening Necrolens Window");
            ShowMainWindow();
            return;
        }
        else if (command == "/necrolenscfg")
        {
            Svc.Log.Verbose("Opening Necrolens Config");
            ShowConfigWindow();
            return;
        }
        else if (command == "/openchest")
        {
            Svc.Log.Verbose("Attempting to open chest");
            DungeonService.TryNearestOpenChest();
            return;
        }
        else if (command == "/pomander")
        {
            Svc.Log.Verbose($"Using pomander: {args}");
            DungeonService.OnPomanderCommand(args);
            return;
        }
    }

    public void ShowMainWindow()
    {
        mainWindow.IsOpen = true;
    }

    public void CloseMainWindow()
    {
        mainWindow.IsOpen = false;
    }

    public void ShowConfigWindow()
    {
        configWindow.IsOpen = true;
    }
}
