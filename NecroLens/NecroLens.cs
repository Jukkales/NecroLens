#undef DEBUG


using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Dalamud.Game;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using ECommons;
using ECommons.Configuration;
using NecroLens.Data;
using NecroLens.Model;
using NecroLens.Service;
using NecroLens.Windows;
using Pictomancy;

namespace NecroLens;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public sealed class NecroLens : IDalamudPlugin
{
    public string Name => "NecroLens";
    public static NecroLens P = null!;

    private Configuration config;
    public static Configuration C => P.config;

    // window systems
    internal WindowSystem windowSystem;
    internal ConfigWindow configWindow;
    internal MainWindow mainWindow;

    // services, need to figure out how these work...

    private DeepDungeonService deepDungeonService;
    private ESPService espService;
    private MobInfoService mobInfoService;
    private PluginCommands pluginCommands;

    public readonly WindowSystem WindowSystem = new("NecroLens");

#if DEBUG
    private readonly ESPTestService espTestService;
#endif

    public NecroLens(IDalamudPluginInterface pi)
    {
        P = this;
        ECommonsMain.Init(pi, P, Module.DalamudReflector, Module.ObjectFunctions);
        new ECommons.Schedulers.TickScheduler(Load);
        PictoService.Initialize(pi);
    }

    public void Load()
    {
        // Config Initiation
        EzConfig.Migrate<Configuration>();
        config = EzConfig.Init<Configuration>();

        // Initializing Windows
        windowSystem = new();
        configWindow = new();
        mainWindow = new();

        // Ticking Stuff
        Svc.PluginInterface.UiBuilder.Draw += windowSystem.Draw;
        Svc.PluginInterface.UiBuilder.Draw += OnDraw;
        Svc.PluginInterface.UiBuilder.OpenMainUi += () =>
        {
            mainWindow.IsOpen = true;
        };
        Svc.PluginInterface.UiBuilder.OpenConfigUi += () =>
        {
            configWindow.IsOpen = true;
        };

        // Commands
        EzCmd.Add("/necrolens", (_, _) => ShowMainWindow(), Strings.PluginCommands_OpenOverlay_Help);
        EzCmd.Add("/necrolenscfg", (_, _) => ShowConfigWindow(), Strings.PluginCommands_OpenConfig_Help);
        EzCmd.Add("/openchest", (_, _) => DungeonService.TryNearestOpenChest(), Strings.PluginCommands_OpenChest_Help);
        EzCmd.Add("/pomander", (_, args) => DungeonService.OnPomanderCommand(args), "Try to use the pomander with given name");

        // Language Loading, to make sure that this gets transered properly (No reason to lose this)
        if (Config.Language == "")
        {
            CultureInfo.DefaultThreadCurrentUICulture = ClientState.ClientLanguage switch
            {
                ClientLanguage.French => CultureInfo.GetCultureInfo("fr"),
                ClientLanguage.German => CultureInfo.GetCultureInfo("de"),
                ClientLanguage.Japanese => CultureInfo.GetCultureInfo("ja"),
                _ => CultureInfo.GetCultureInfo("en")
            };
        }
        else
        {
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo(Config.Language);
        }

        // Services that I need to sort through
        mobInfoService = new MobInfoService();
        MobService = mobInfoService;

        espService = new ESPService();

        deepDungeonService = new DeepDungeonService();
        DungeonService = deepDungeonService;
#if DEBUG
        espTestService = new ESPTestService();
#endif

    }

    public void OnDraw()
    {

    }

    private void Tick(object _)
    {

    }

    public void Dispose()
    {
        Safe(() => Svc.Framework.Update -= Tick);
        Safe(() => Svc.PluginInterface.UiBuilder.Draw -= windowSystem.Draw);
        Safe(() => Svc.PluginInterface.UiBuilder.Draw -= OnDraw);
        ECommonsMain.Dispose();
        PictoService.Dispose();

        // Old ones, need to sort through
        espService.Dispose();
        deepDungeonService.Dispose();
#if DEBUG
        espTestService.Dispose();
#endif
        mobInfoService.Dispose();
    }

    private void DrawUI()
    {
        WindowSystem.Draw();
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
