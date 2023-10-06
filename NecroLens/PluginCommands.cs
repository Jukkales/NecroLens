using System;
using Dalamud.Game.Command;
using NecroLens.Data;
using NecroLens.Service;

namespace NecroLens;

public class PluginCommands : IDisposable
{
    public PluginCommands()
    {
        PluginService.CommandManager.AddHandler("/necrolens",
            new CommandInfo((_, _) => PluginService.Plugin.ShowMainWindow())
            {
                HelpMessage = Strings.PluginCommands_OpenOverlay_Help,
                ShowInHelp = true
            });

        PluginService.CommandManager.AddHandler("/necrolenscfg",
            new CommandInfo((_, _) => PluginService.Plugin.ShowConfigWindow())
            {
                HelpMessage = Strings.PluginCommands_OpenConfig_Help,
                ShowInHelp = true
            });
    }

    public void Dispose()
    {
        PluginService.CommandManager.RemoveHandler("/necrolens");
        PluginService.CommandManager.RemoveHandler("/necrolenscfg");
    }
}
