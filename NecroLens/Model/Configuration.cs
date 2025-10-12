using System;
using System.Diagnostics.CodeAnalysis;
using Dalamud.Configuration;

namespace NecroLens.Model;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[Serializable]
public class Configuration : IPluginConfiguration
{
    public bool AutoOpenOnEnter { get; set; } = true;

    public bool EnableESP { get; set; } = true;

    public bool ShowPlayerDot { get; set; } = true;
    public bool ShowMobViews { get; set; } = true;
    public bool ShowCofferInteractionRange { get; set; } = true;
    public bool ShowPatrolArrow { get; set; } = true;

    public bool HighlightCoffers { get; set; } = true;
    public bool HighlightPassage { get; set; } = true;

    public bool ShowBronzeCoffers { get; set; } = true;
    public bool ShowSilverCoffers { get; set; } = true;
    public bool ShowGoldCoffers { get; set; } = true;
    public bool ShowHoards { get; set; } = true;

    public bool ShowPassage { get; set; } = true;
    public bool ShowReturn { get; set; } = true;
    public bool ShowTraps { get; set; } = true;
    public bool ShowMimicCoffer { get; set; } = true;
    public bool ShowVotife { get; set; } = true;

    public bool OpenChests { get; set; } = false;
    public bool OpenBronzeCoffers { get; set; } = true;
    public bool OpenSilverCoffers { get; set; } = true;
    public bool OpenGoldCoffers { get; set; } = true;
    public bool OpenHoards { get; set; } = true;

    public bool OpenUnsafeChests { get; set; } = false;

    public int Version { get; set; } = 1;
    
    public uint PlayerDotColor { get; set; } = 0xCC0000FF;
    public uint NormalAggroColor { get; set; } = 0xFF2A2AA5;
    public uint SoundAggroColor { get; set; } = 0xFFFF00FF;
    public uint PassageColor { get; set; } = 0xFFD0E040;
    public uint VotifeColor { get; set; } = 0xFFD0E040;
    
    public uint BronzeCofferColor { get; set; } = 0xFF13458B;
    public uint SilverCofferColor { get; set; } = 0xFFC0C0C0;
    public uint GoldCofferColor { get; set; } = 0xFF00D7FF;
    public uint HoardColor { get; set; } = 0xFF00D7FF;
    
    public bool ShowDebugInformation { get; set; } = false;
    
    public string? UniqueId { get; set; }
    public bool OptInDataCollection { get; set; } = false;
    public string Language { get; set; } = "";

    public void Save()
    {
        PluginInterface.SavePluginConfig(this);
    }
}
