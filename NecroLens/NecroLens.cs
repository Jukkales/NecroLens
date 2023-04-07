#undef DEBUG

using System.Diagnostics.CodeAnalysis;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
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

    public NecroLens(DalamudPluginInterface? pluginInterface)
    {
        pluginInterface?.Create<PluginService>();
        PluginService.Plugin = this;

        PluginService.Configuration =
            PluginService.PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();

        pluginCommands = new PluginCommands();
        configWindow = new ConfigWindow();
        mainWindow = new MainWindow();

        WindowSystem.AddWindow(mainWindow);
        WindowSystem.AddWindow(configWindow);

        mobInfoService = new MobInfoService();
        PluginService.MobInfoService = mobInfoService;

        espService = new ESPService();

        deepDungeonService = new DeepDungeonService();
        PluginService.DeepDungeonService = deepDungeonService;
#if DEBUG
        espTestService = new ESPTestService();
#endif
        PluginService.PluginInterface.UiBuilder.Draw += DrawUI;
        PluginService.PluginInterface.UiBuilder.OpenConfigUi += ShowConfigWindow;
    }

    public string Name => "NecroLens";


    public void Dispose()
    {
        WindowSystem.RemoveAllWindows();

        PluginService.PluginInterface.UiBuilder.Draw -= DrawUI;
        PluginService.PluginInterface.UiBuilder.OpenConfigUi -= ShowConfigWindow;

        configWindow.Dispose();
        pluginCommands.Dispose();
        mainWindow.Dispose();
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
