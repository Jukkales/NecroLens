#undef DEBUG


using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Dalamud.Game;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using ECommons;
using NecroLens.Model;
using NecroLens.Service;
using NecroLens.Windows;

namespace NecroLens;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public sealed class NecroLens : IDalamudPlugin
{
    private readonly ConfigWindow configWindow;
    private readonly DeepDungeonService deepDungeonService;
    private readonly ESPService espService;
    private readonly MainWindow mainWindow;
    private readonly MobInfoService mobInfoService;
    private readonly PluginCommands pluginCommands;

    public readonly WindowSystem WindowSystem = new("NecroLens");

#if DEBUG
    private readonly ESPTestService espTestService;
#endif

    public NecroLens(IDalamudPluginInterface? pluginInterface)
    {
        pluginInterface?.Create<PluginService>();
        Plugin = this;

        ECommonsMain.Init(pluginInterface, this, Module.DalamudReflector);

        Config = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();

        pluginCommands = new PluginCommands();
        configWindow = new ConfigWindow();
        mainWindow = new MainWindow();

        WindowSystem.AddWindow(mainWindow);
        WindowSystem.AddWindow(configWindow);

        mobInfoService = new MobInfoService();
        MobService = mobInfoService;

        espService = new ESPService();

        deepDungeonService = new DeepDungeonService();
        DungeonService = deepDungeonService;
#if DEBUG
        espTestService = new ESPTestService();
#endif
        PluginInterface.UiBuilder.Draw += DrawUI;
        PluginInterface.UiBuilder.OpenConfigUi += ShowConfigWindow;

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
    }

    public void Dispose()
    {
        WindowSystem.RemoveAllWindows();

        PluginInterface.UiBuilder.Draw -= DrawUI;
        PluginInterface.UiBuilder.OpenConfigUi -= ShowConfigWindow;

        configWindow.Dispose();
        pluginCommands.Dispose();
        mainWindow.Dispose();
        espService.Dispose();
        deepDungeonService.Dispose();
#if DEBUG
        espTestService.Dispose();
#endif
        mobInfoService.Dispose();
        
        ECommonsMain.Dispose();
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
