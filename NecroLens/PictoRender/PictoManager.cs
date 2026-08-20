using Dalamud.Game.ClientState.Objects.Types;
using ECommons.DalamudServices;
using ECommons.GameHelpers;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game.Character;
using NecroLens.Configuration;
using NecroLens.Enums;
using NecroLens.MobData;
using NecroLens.util;
using Pictomancy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NecroLens.PictoRender
{
    internal static partial class PictoManager
    {
        private static readonly List<Action<PctDrawList>> drawCommands = new();
        private static readonly object lockObject = new();

        public static void AddDrawCommand(Action<PctDrawList> drawAction)
        {
            if (drawAction == null) return;

            lock (lockObject)
            {
                drawCommands.Add(drawAction);
            }
        }

        public static void DrawPicto()
        {
            try
            {
                using (var pictoDraw = PctService.Draw())
                {
                    if (pictoDraw == null)
                        return;

                    lock (lockObject)
                    {
                        // Execute all queued draw commands
                        foreach (var command in drawCommands)
                        {
                            try
                            {
                                command(pictoDraw);
                            }
                            catch (Exception ex)
                            {
                                Svc.Log.Error($"Error executing draw command: {ex}");
                            }
                        }

                        // Clear the queue for next frame
                        drawCommands.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                Svc.Log.Error($"Error in DrawPicto: {ex}");
            }
        }

        public static void CheckObjects()
        {
            if (Player.Available)
            {
                var objectList = Svc.Objects;
                if (objectList != null)
                {
                    foreach (var entity in objectList)
                    {
                        if (entity == null || !entity.IsValid())
                            continue;

                        var baseId = entity.BaseId;
                        var distance = Player.DistanceTo(entity);

                        if (!InCombat(entity) && entity is IBattleNpc battleNpc)
                        {
                            uint nameId = battleNpc.NameId;
                            if (MobDatabase.MobInformation.TryGetValue(nameId, out var mobInfo))
                            {
                                if (!C.GatheredMobData.ContainsKey(baseId))
                                {
                                    var aggroInfo = mobInfo.GetAggroInfo(Player.Territory.RowId);
                                    var aggroType = aggroInfo.AggroType != null ? aggroInfo.AggroType.Value : AggroType.Unknown;
                                    var dangerLevel = aggroInfo.DangerLevel != null ? aggroInfo.DangerLevel.Value : DangerLevel.Danger;


                                    // new mob data to be created
                                    MobDatabase.MobInfo newMobData = new()
                                    {
                                        Id = baseId,
                                        BNcpId = nameId,
                                        Name = $"{entity.Name}",
                                        TerritoryId = Player.Territory.RowId,
                                        Dungeon = mobInfo.Dungeon,
                                        MobType = mobInfo.MobType,
                                        AggroType = aggroType,
                                        DangerLevel = dangerLevel,
                                        Patrol = mobInfo.Patrol,
                                        BossOrAdd = mobInfo.BossOrAdd,
                                        Special = mobInfo.Special,
                                    };
                                    C.GatheredMobData[baseId] = newMobData;
                                    C.Save();
                                }

                                // default range of 50 for now
                                if (distance <= 50)
                                {
                                    if (EzThrottler.Throttle($"Object Check {baseId}", 1000))
                                        Svc.Log.Verbose($"Found an entitiy that needs to be drawn {baseId} _ {nameId}!");
                                    DrawAggroInfo($"{baseId}_{entity.EntityId}", entity, mobInfo);
                                }
                            }
                            else
                            {
                                if (!C.GatheredMobData.ContainsKey(baseId))
                                {
                                    // New mob that isn't contained in the info stored, so going to create a new one
                                    DeepDungeon currentDD = DeepDungeon.Unk;
                                    if (DeepDungeonUtil.InPotD)
                                        currentDD = DeepDungeon.PotD;
                                    else if (DeepDungeonUtil.InHoH)
                                        currentDD = DeepDungeon.HoH;
                                    else if (DeepDungeonUtil.InEO)
                                        currentDD = DeepDungeon.EO;
                                    else if (DeepDungeonUtil.InPT)
                                        currentDD = DeepDungeon.PT;

                                    var currentTerritory = Player.Territory.RowId;

                                    MobDatabase.MobInfo newMobData = new()
                                    {
                                        Id = baseId,
                                        BNcpId = nameId,
                                        Name = $"{entity.Name}",
                                        TerritoryId = Player.Territory.RowId,
                                        Dungeon = currentDD,
                                        MobType = ESPType.Enemy,
                                        AggroType = AggroType.Unknown,
                                    };
                                    C.GatheredMobData[baseId] = newMobData;
                                    C.Save();
                                }
                            }
                        }
                    }
                }
            }
        }

        private unsafe static bool InCombat(IGameObject npc)
        {
            try
            {
                if (!npc.IsValid() || npc is not IBattleNpc) return true;
                return ((BattleChara*)npc.Address)->Character.InCombat;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}
