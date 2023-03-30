using System;
using Dalamud.Game.Command;
using NecroLens.Service;

namespace NecroLens;

public class PluginCommands : IDisposable
{
    public PluginCommands()
    {
        PluginService.CommandManager.AddHandler("/necrolens", new CommandInfo(DefaultCommand)
        {
            HelpMessage = "Opens the overlay.",
            ShowInHelp = true
        });

        PluginService.CommandManager.AddHandler("/necrolenscfg", new CommandInfo(ConfigCommand)
        {
            HelpMessage = "Opens the configuration.",
            ShowInHelp = true
        });
    }

    public void Dispose()
    {
        PluginService.CommandManager.RemoveHandler("/necrolens");
        PluginService.CommandManager.RemoveHandler("/necrolenscfg");
    }

    private void DefaultCommand(string command, string args)
    {
        PluginService.Plugin.ShowMainWindow();
    }

    private void ConfigCommand(string command, string args)
    {
        PluginService.Plugin.ShowConfigWindow();
    }
}
