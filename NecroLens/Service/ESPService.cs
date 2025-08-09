using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Threading;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Plugin.Services;
using Dalamud.Bindings.ImGui;
using NecroLens.Model;
using NecroLens.util;
using static NecroLens.util.ESPUtils;

namespace NecroLens.Service;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class ESPService : IDisposable
{
    private readonly Configuration conf;

    private readonly List<ESPObject> mapObjects;

    public ESPService()
    {
        PluginLog.Debug("ESP Service loading...");

        mapObjects = new List<ESPObject>();
        conf = Config;

        PluginInterface.UiBuilder.Draw += OnUpdate;
        ClientState.TerritoryChanged += OnCleanup;
        Framework.Update += OnTick;
    }

    public void Dispose()
    {
        PluginInterface.UiBuilder.Draw -= OnUpdate;
        ClientState.TerritoryChanged -= OnCleanup;
        Framework.Update -= OnTick;
        mapObjects.Clear();
        PluginLog.Information("ESP Service unloaded");
    }


    /**
     * Clears the drawable GameObjects on MapChange.
     */
    private void OnCleanup(ushort e)
    {
        Monitor.Enter(mapObjects);
        mapObjects.Clear();
        Monitor.Exit(mapObjects);
    }

    /**
     * Main-Drawing method.
     */
    private void OnUpdate()
    {
        try
        {
            if (ShouldDraw())
            {
                if (!Monitor.TryEnter(mapObjects)) return;

                var drawList = ImGui.GetBackgroundDrawList();
                foreach (var gameObject in mapObjects) DrawEspObject(drawList, gameObject);

                Monitor.Exit(mapObjects);
            }
        }
        catch (Exception e)
        {
            PluginLog.Error(e.ToString());
        }
    }

    private bool DoDrawName(ESPObject espObject)
    {
        return espObject.Type switch
        {
            ESPObject.ESPType.Player => false,
            ESPObject.ESPType.Enemy => !espObject.InCombat(),
            ESPObject.ESPType.Mimic => !espObject.InCombat(),
            ESPObject.ESPType.FriendlyEnemy => !espObject.InCombat(),
            ESPObject.ESPType.BronzeChest => conf.ShowBronzeCoffers,
            ESPObject.ESPType.SilverChest => conf.ShowSilverCoffers,
            ESPObject.ESPType.GoldChest => conf.ShowGoldCoffers,
            ESPObject.ESPType.AccursedHoard => conf.ShowHoards && !DungeonService.FloorDetails.HoardFound,
            ESPObject.ESPType.MimicChest => conf.ShowMimicCoffer,
            ESPObject.ESPType.Trap => conf.ShowTraps,
            ESPObject.ESPType.Return => conf.ShowReturn,
            ESPObject.ESPType.Passage => conf.ShowPassage,
            _ => false
        };
    }

    /**
     * Draws every Object for the ESP-Overlay.
     */
    private void DrawEspObject(ImDrawListPtr drawList, ESPObject espObject)
    {
        var type = espObject.Type;
        var onScreen = GameGui.WorldToScreen(espObject.GameObject.Position, out var position2D);
        if (onScreen)
        {
            var distance = espObject.Distance();

            if (conf.ShowPlayerDot && type == ESPObject.ESPType.Player)
                DrawPlayerDot(drawList, position2D);

            if (DoDrawName(espObject))
                DrawName(drawList, espObject, position2D);

            if (espObject.Type == ESPObject.ESPType.AccursedHoard && conf.ShowHoards && !DungeonService.FloorDetails.HoardFound)
            {
                var chestRadius = type == ESPObject.ESPType.AccursedHoard ? 2.0f : 1f; // Make Hoards bigger

                if (distance <= 35 && conf.HighlightCoffers)
                    DrawCircleFilled(drawList, espObject, chestRadius, espObject.RenderColor(), 1f);
            }

            if (espObject.IsChest())
            {
                if (!conf.ShowBronzeCoffers && type == ESPObject.ESPType.BronzeChest) return;
                if (!conf.ShowSilverCoffers && type == ESPObject.ESPType.SilverChest) return;
                if (!conf.ShowGoldCoffers && type == ESPObject.ESPType.GoldChest) return;
                if (!conf.ShowHoards && type == ESPObject.ESPType.AccursedHoardCoffer) return;

                if (distance <= 35 && conf.HighlightCoffers)
                    DrawCircleFilled(drawList, espObject, 1f, espObject.RenderColor(), 1f);
                if (distance <= 10 && conf.ShowCofferInteractionRange)
                    DrawInteractionCircle(drawList, espObject, espObject.InteractionDistance());
            }

            if (conf.ShowTraps && type == ESPObject.ESPType.Trap)
                DrawCircleFilled(drawList, espObject, 1.7f, espObject.RenderColor());

            if (conf.ShowMimicCoffer && type == ESPObject.ESPType.MimicChest)
                DrawCircleFilled(drawList, espObject, 1f, espObject.RenderColor());

            if (conf.HighlightPassage && type == ESPObject.ESPType.Passage)
                DrawCircleFilled(drawList, espObject, 2f, espObject.RenderColor());
        }

        if (Config.ShowMobViews &&
            (type == ESPObject.ESPType.Enemy || type == ESPObject.ESPType.Mimic) &&
            BattleNpcSubKind.Enemy.Equals((BattleNpcSubKind)espObject.GameObject.SubKind) &&
            !espObject.InCombat())
        {
            if (conf.ShowPatrolArrow && espObject.IsPatrol())
                DrawFacingDirectionArrow(drawList, espObject, Color.Red.ToUint(), 0.6f);

            if (espObject.Distance() <= 50)
            {
                switch (espObject.AggroType())
                {
                    case ESPObject.ESPAggroType.Proximity:
                        DrawCircle(drawList, espObject, espObject.AggroDistance(),
                                   conf.NormalAggroColor, DefaultFilledOpacity);
                        break;
                    case ESPObject.ESPAggroType.Sound:
                        DrawCircle(drawList, espObject, espObject.AggroDistance(),
                                   conf.SoundAggroColor, DefaultFilledOpacity);
                        DrawCircleFilled(drawList, espObject, espObject.GameObject.HitboxRadius,
                                         conf.SoundAggroColor, DefaultFilledOpacity);
                        break;
                    case ESPObject.ESPAggroType.Sight:
                        DrawConeFromCenterPoint(drawList, espObject, espObject.SightRadian,
                                                espObject.AggroDistance(), conf.NormalAggroColor);
                        break;
                    default:
                        PluginLog.Error(
                            $"Unable to process AggroType {espObject.AggroType().ToString()}");
                        break;
                }
            }
        }
    }

    /**
     * Method returns true if the ESP is Enabled, In valid state and in DeepDungeon
     */
    private bool ShouldDraw()
    {
        return Config.EnableESP &&
               !(Condition[ConditionFlag.LoggingOut] ||
                 Condition[ConditionFlag.BetweenAreas] ||
                 Condition[ConditionFlag.BetweenAreas51]) &&
               ClientState is { LocalPlayer: not null, LocalContentId: > 0 }
                && DeepDungeonUtil.InDeepDungeon;
    }

    /**
     * Not-Drawing Scanner method updating mapObjects every Tick.
     */
    private void OnTick(IFramework framework)
    {
        try
        {
            if (ShouldDraw())
            {
                var entityList = new List<ESPObject>();
                foreach (var obj in ObjectTable)
                {
                    // Ignore every player object
                    if (obj.IsValid() && !IsIgnoredObject(obj))
                    {
                        MobInfo mobInfo = null!;
                        if (obj is IBattleNpc npcObj)
                            MobService.MobInfoDictionary.TryGetValue(npcObj.NameId, out mobInfo!);

                        var espObj = new ESPObject(obj, mobInfo);
                        
                        if (obj.DataId == DataIds.GoldChest 
                            && DungeonService.FloorDetails.DoubleChests.TryGetValue(obj.EntityId, out var value))
                        {
                            espObj.ContainingPomander = value;
                        }

                        DungeonService.TryInteract(espObj);

                        entityList.Add(espObj);
                        DungeonService.TrackFloorObjects(espObj);
                    }

                    if (ClientState.LocalPlayer != null &&
                        ClientState.LocalPlayer.EntityId == obj.EntityId)
                        entityList.Add(new ESPObject(obj));
                }

                Monitor.Enter(mapObjects);
                mapObjects.Clear();
                mapObjects.AddRange(entityList);
                Monitor.Exit(mapObjects);
            }
        }
        catch (Exception e)
        {
            PluginLog.Error(e.ToString());
        }

    }

}
