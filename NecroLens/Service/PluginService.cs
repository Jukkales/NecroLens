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
using NecroLens.Model;

namespace NecroLens.Service;

public class PluginService
{
    public static NecroLens Plugin = null!;

    [PluginService]
    public static DalamudPluginInterface PluginInterface { get; private set; } = null!;

    [PluginService]
    public static ChatGui ChatGui { get; private set; } = null!;

    [PluginService]
    public static ClientState ClientState { get; private set; } = null!;

    [PluginService]
    public static CommandManager CommandManager { get; private set; } = null!;

    [PluginService]
    public static Condition Condition { get; private set; } = null!;

    [PluginService]
    public static DataManager DataManager { get; private set; } = null!;

    [PluginService]
    public static FlyTextGui FlyTextGui { get; private set; } = null!;

    [PluginService]
    public static Framework Framework { get; private set; } = null!;

    [PluginService]
    public static GameGui GameGui { get; private set; } = null!;

    [PluginService]
    public static GameNetwork GameNetwork { get; private set; } = null!;

    [PluginService]
    public static ObjectTable ObjectTable { get; private set; } = null!;

    [PluginService]
    public static PartyList PartyList { get; private set; } = null!;

    [PluginService]
    public static SigScanner SigScanner { get; private set; } = null!;

    [PluginService]
    public static TargetManager TargetManager { get; private set; } = null!;

    public static MobInfoService MobInfoService { get; set; } = null!;
    public static Configuration Configuration { get; set; } = null!;
    public static DeepDungeonService DeepDungeonService { get; set; } = null!;
}
