#undef DEBUG

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.Types;
using NecroLens.Service;
using NecroLens.util;

namespace NecroLens.Model;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[Serializable]
public class ESPObject
{
    public enum ESPAggroType
    {
        Sight,
        Sound,
        Proximity
    }

    public enum ESPDangerLevel
    {
        Easy,
        Caution,
        Danger
    }

    public enum ESPType
    {
        Player,
        Enemy,
        Mimic,
        FriendlyEnemy,
        BronzeChest,
        SilverChest,
        GoldChest,
        AccursedHoard,
        MimicChest,
        Trap,
        Return,
        Passage
    }

    private ClientState clientState;
    private MobInfo? mobInfo;

    public ESPObject(GameObject gameObject, ClientState clientState, MobInfo? mobInfo)
    {
        this.clientState = clientState;
        GameObject = gameObject;
        this.mobInfo = mobInfo;

        // Mob info exists? check floor overrides
        if (this.mobInfo != null)
        {
            if (DeepDungeonContentInfo.ContentMobInfoChanges.TryGetValue(
                    PluginService.DeepDungeonService.currentContentId, out var overrideInfos))
            {
                var npc = (BattleNpc)gameObject;
                var mob = overrideInfos.FirstOrDefault(m => m.Id == npc.NameId);
                if (mob != null)
                {
                    this.mobInfo.Patrol = mob.Patrol ?? this.mobInfo.Patrol;
                    this.mobInfo.AggroType = mob.AggroType ?? this.mobInfo.AggroType;
                }
            }
        }

        // No MobInfo? Must be an other object
        else
        {
            var dataId = gameObject.DataId;

            if (clientState.LocalPlayer != null && clientState.LocalPlayer.ObjectId == gameObject.ObjectId)
                Type = ESPType.Player;
            else if (DataIds.BronzeChestIDs.Contains(dataId))
                Type = ESPType.BronzeChest;
            else if (DataIds.SilverChest == dataId)
                Type = ESPType.SilverChest;
            else if (DataIds.GoldChest == dataId)
                Type = ESPType.GoldChest;
            else if (DataIds.MimicChest == dataId)
                Type = ESPType.MimicChest;
            else if (DataIds.AccursedHoardIDs.Contains(dataId))
                Type = ESPType.AccursedHoard;
            else if (DataIds.PassageIDs.Contains(dataId))
                Type = ESPType.Passage;
            else if (DataIds.ReturnIDs.Contains(dataId))
                Type = ESPType.Return;
            else if (DataIds.TrapIDs.ContainsKey(dataId))
                Type = ESPType.Trap;
            else if (DataIds.FriendlyIDs.Contains(dataId))
                Type = ESPType.FriendlyEnemy;
            else if (DataIds.MimicIDs.Contains(dataId))
                Type = ESPType.Mimic;
        }
    }

    public GameObject GameObject { get; }

    public ESPType Type { get; set; } = ESPType.Enemy;

    /**
     * Default view of a Sight mob is 90° in front. We use the radian value of cos 90°.
     */
    public float SightRadian { get; set; } = 1.571f;

    /**
     * Most monsters have different aggro distances. 10.8y is roughly a safe value. Expect PotD Mimics ... 14 ._.
     */
    public float AggroDistance()
    {
        return Type == ESPType.Mimic && DataIds.PalaceOfTheDeadMapIds.Contains(clientState.TerritoryType) ? 14f : 10.8f;
    }

    public ESPAggroType AggroType()
    {
        return mobInfo?.AggroType ?? ESPAggroType.Proximity;
    }

    public ESPDangerLevel DangerLevel()
    {
        return mobInfo?.DangerLevel ?? ESPDangerLevel.Easy;
    }

    public bool IsBossOrAdd()
    {
        return mobInfo?.BossOrAdd ?? false;
    }

    public bool IsSpecialMob()
    {
        return mobInfo?.Special ?? false;
    }

    public bool IsPatrol()
    {
        return mobInfo?.Patrol ?? false;
    }

    public float InteractionDistance()
    {
        return Type switch
        {
            ESPType.BronzeChest => 3.1f,
            ESPType.SilverChest => 4.4f,
            ESPType.GoldChest => 4.4f,
            _ => 2f
        };
    }

    public float Distance()
    {
        return clientState.LocalPlayer != null ? GameObject.Position.Distance2D(clientState.LocalPlayer.Position) : 0;
    }

    public bool IsChest()
    {
        return Type is ESPType.BronzeChest or ESPType.SilverChest or ESPType.GoldChest or ESPType.AccursedHoard;
    }

    public uint RenderColor()
    {
        switch (Type)
        {
            case ESPType.Enemy:
                return DangerLevel() switch
                {
                    ESPDangerLevel.Danger => Color.Red.ToUint(),
                    ESPDangerLevel.Caution => Color.OrangeRed.ToUint(),
                    _ => Color.White.ToUint()
                };
            case ESPType.FriendlyEnemy:
                return Color.LightGreen.ToUint();
            case ESPType.Mimic:
            case ESPType.MimicChest:
            case ESPType.Trap:
                return Color.Red.ToUint();
            case ESPType.Return:
                return Color.LightBlue.ToUint();
            case ESPType.Passage:
                return PluginService.Configuration.PassageColor;
            case ESPType.AccursedHoard:
                return PluginService.Configuration.HoardColor;
            case ESPType.GoldChest:
                return PluginService.Configuration.GoldCofferColor;
            case ESPType.SilverChest:
                return PluginService.Configuration.SilverCofferColor;
            case ESPType.BronzeChest:
                return PluginService.Configuration.BronzeCofferColor;
            default:
                return Color.White.ToUint();
        }
    }

    public bool InCombat()
    {
        try
        {
            return GameObject is BattleNpc npc && (npc.StatusFlags & StatusFlags.InCombat) != 0;
        }
        catch (AccessViolationException ave)
        {
            // 6.4: accessing StatusFlags sometimes causes access violations
            // we ignore them and assume "yes" here to disable rendering
            return true;
        }
    }

    public string? NameSymbol()
    {
        if (IsSpecialMob()) return "\uE0C0";
        if (IsPatrol()) return "\uE05E";

        return Type switch
        {
            ESPType.Trap => "\uE0BF",
            ESPType.AccursedHoard => "\uE03C",
            ESPType.BronzeChest => "\uE03D",
            ESPType.SilverChest => "\uE03D",
            ESPType.GoldChest => "\uE03D",
            ESPType.Return => "\uE03B",
            ESPType.Passage => "\uE035",
            ESPType.FriendlyEnemy => "\uE034",
            _ => null
        };
    }

    public string Name()
    {
        // We dont wanna see Bosses and Adds
        if (IsBossOrAdd()) return "";

        // No name for all "Enemies" (default type) which are not hostile
        if (Type == ESPType.Enemy && !BattleNpcSubKind.Enemy.Equals((BattleNpcSubKind)GameObject.SubKind))
            return "";

        var name = "";
        var symbol = NameSymbol();
        if (symbol != null)
            name += symbol + " ";

        name += Type switch
        {
            ESPType.Trap => DataIds.TrapIDs.TryGetValue(GameObject.DataId, out var value) ? value : "Trap",
            ESPType.AccursedHoard => "Accursed Hoard",
            ESPType.BronzeChest => "Bronze Chest",
            ESPType.SilverChest => "Silver Chest",
            ESPType.GoldChest => "Gold Chest",
            ESPType.MimicChest => "Mimic",
            _ => GameObject.Name.TextValue
        };

        name += Type switch
        {
            ESPType.Passage => " - " + Distance().ToString("0.0"),
            _ => ""
        };

#if DEBUG
        if (Type == ESPType.Enemy) // Enemy is default
        {
            name += "\nD: " + GameObject.DataId;
            if (GameObject is BattleNpc npc2) name += " - N: " + npc2.NameId;
        }
#endif
        return name;
    }
}
