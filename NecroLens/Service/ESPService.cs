using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Bindings.ImGui;
using NecroLens.Model;
using NecroLens.util;
using ECommons.DalamudServices;

namespace NecroLens.Service;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class ESPService : IDisposable
{
    private readonly List<ESPObject> mapObjects;

    public ESPService()
    {
        PluginLog.Debug("ESP Service loading...");

        mapObjects = new List<ESPObject>();

        ClientState.TerritoryChanged += OnCleanup;
    }

    public void Dispose()
    {
        ClientState.TerritoryChanged -= OnCleanup;
        mapObjects.Clear();
        PluginLog.Information("ESP Service unloaded");
    }


    /**
     * Clears the drawable GameObjects on MapChange.
     */
    private void OnCleanup(uint e)
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
                foreach (var gameObject in mapObjects)
                {
                    // DrawEspObject(drawList, gameObject);
                }

                Monitor.Exit(mapObjects);
            }
        }
        catch (Exception e)
        {
            PluginLog.Error(e.ToString());
        }
    }

    /*
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
            ESPObject.ESPType.Votife => conf.ShowVotife,
            _ => false
        };
    }
    */

    /**
     * Draws every Object for the ESP-Overlay.
     */
    /*
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

            if (conf.ShowVotife && type == ESPObject.ESPType.Votife)
                DrawCircleFilled(drawList, espObject, 2f, espObject.RenderColor());
        }

        if (Config.ShowMobViews &&
            (type == ESPObject.ESPType.Enemy || type == ESPObject.ESPType.Mimic) &&
            BattleNpcSubKind.Combatant.Equals((BattleNpcSubKind)espObject.GameObject.SubKind) &&
            !espObject.InCombat())
        {
            if (conf.ShowPatrolArrow && espObject.IsPatrol())
                DrawFacingDirectionArrow(drawList, espObject, Color.Red.ToUint(), 0.6f);

            if (espObject.Distance() <= 50)
            {
                switch (espObject.ReturnAgroType())
                {
                    case AggroType.Proximity:
                        DrawCircle(drawList, espObject, espObject.AggroDistance(),
                                   conf.NormalAggroColor, DefaultFilledOpacity);
                        break;
                    case AggroType.Sound:
                        DrawCircle(drawList, espObject, espObject.AggroDistance(),
                                   conf.SoundAggroColor, DefaultFilledOpacity);
                        DrawCircleFilled(drawList, espObject, espObject.GameObject.HitboxRadius,
                                         conf.SoundAggroColor, DefaultFilledOpacity);
                        break;
                    case AggroType.Sight:
                        DrawConeFromCenterPoint(drawList, espObject, espObject.SightRadian,
                                                espObject.AggroDistance(), conf.NormalAggroColor);
                        break;
                    default:
                        PluginLog.Error(
                            $"Unable to process AggroType {espObject.ReturnAgroType().ToString()}");
                        break;
                }
            }
        }
    }
    */


    /**
     * Not-Drawing Scanner method updating mapObjects every Tick.
     */
    /*
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
                        
                        if (obj.BaseId == DataIds.GoldChest 
                            && DungeonService.FloorDetails.DoubleChests.TryGetValue(obj.EntityId, out var value))
                        {
                            espObj.ContainingPomander = value;
                        }

                        DungeonService.TryInteract(espObj);

                        entityList.Add(espObj);
                        DungeonService.TrackFloorObjects(espObj);
                    }

                    if (ObjectTable.LocalPlayer != null &&
                        PlayerState.EntityId == obj.EntityId)
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
    */

    /**
    * Method returns true if the ESP is Enabled, In valid state and in DeepDungeon
    */
    private bool ShouldDraw()
    {
        bool enabled = C.EnableESP;
        bool conditions = !(Svc.Condition[ConditionFlag.LoggingOut] || Svc.Condition[ConditionFlag.BetweenAreas] || Svc.Condition[ConditionFlag.BetweenAreas51]);
        bool available = Svc.Objects.LocalPlayer != null;
        bool contentId = Svc.PlayerState.ContentId > 0;
        bool inDeepDungeon = DeepDungeonUtil.InDeepDungeon;

        return enabled && conditions && available && contentId && inDeepDungeon;
    }
}
