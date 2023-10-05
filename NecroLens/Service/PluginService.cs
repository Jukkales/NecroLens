using Dalamud.Data;
using Dalamud.Game;
using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects;
using Dalamud.Game.ClientState.Party;
using Dalamud.Game.Command;
using Dalamud.Game.Gui;
using Dalamud.Game.Gui.FlyText;
using Dalamud.Game.Network;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using NecroLens.Model;

namespace NecroLens.Service;

public class PluginService
{
    public static NecroLens Plugin = null!;

    [PluginService]
    public static DalamudPluginInterface PluginInterface { get; private set; } = null!;

    [PluginService]
    public static IChatGui ChatGui { get; private set; } = null!;

    [PluginService]
    public static IClientState ClientState { get; private set; } = null!;

    [PluginService]
    public static ICommandManager CommandManager { get; private set; } = null!;

    [PluginService]
    public static ICondition Condition { get; private set; } = null!;

    [PluginService]
    public static IDataManager DataManager { get; private set; } = null!;

    [PluginService]
    public static IFlyTextGui FlyTextGui { get; private set; } = null!;

    [PluginService]
    public static IFramework Framework { get; private set; } = null!;

    [PluginService]
    public static IGameGui GameGui { get; private set; } = null!;

    [PluginService]
    public static IGameNetwork GameNetwork { get; private set; } = null!;

    [PluginService]
    public static IObjectTable ObjectTable { get; private set; } = null!;

    [PluginService]
    public static IPartyList PartyList { get; private set; } = null!;

    [PluginService]
    public static ITargetManager TargetManager { get; private set; } = null!;
    
    [PluginService]
    public static IPluginLog PluginLog { get; private set; } = null!;

    public static MobInfoService MobInfoService { get; set; } = null!;
    public static Configuration Configuration { get; set; } = null!;
    public static DeepDungeonService DeepDungeonService { get; set; } = null!;
}
