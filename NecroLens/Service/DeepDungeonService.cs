﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Timers;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.Network;
using ECommons.Automation;
using ECommons.Automation.NeoTaskManager;
using FFXIVClientStructs.FFXIV.Client.Game.Control;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;
using Lumina.Excel.Sheets;
using NecroLens.Model;
using NecroLens.util;
using static NecroLens.util.DeepDungeonUtil;
using GameObject = FFXIVClientStructs.FFXIV.Client.Game.Object.GameObject;

namespace NecroLens.Service;

/**
 * Tracks the progress when inside a DeepDungeon.
 */
public class DeepDungeonService : IDisposable
{
    private readonly Configuration conf;
    private readonly Timer floorTimer;
    public readonly Dictionary<int, int> FloorTimes;
    public int CurrentContentId;
    public DeepDungeonContentInfo.DeepDungeonFloorSetInfo? FloorSetInfo;
    public bool Ready;
    private readonly TaskManager taskManager;
    public readonly FloorDetails FloorDetails;
    public readonly Dictionary<Pomander, string> PomanderNames;

    public DeepDungeonService()
    {
        GameNetwork.NetworkMessage += NetworkMessage;
        FloorTimes = new Dictionary<int, int>();
        floorTimer = new Timer();
        floorTimer.Elapsed += OnTimerUpdate;
        floorTimer.Interval = 1000;
        Ready = false;
        conf = Config;
        FloorDetails = new FloorDetails();
        taskManager = new TaskManager(new TaskManagerConfiguration
        {
            TimeoutSilently = true
        });
        PomanderNames = new Dictionary<Pomander, string>();
        
        foreach (var pomander in DataManager.GetExcelSheet<DeepDungeonItem>(ClientState.ClientLanguage)!.Skip(1))
        {
            PomanderNames[(Pomander)pomander.RowId] = pomander.Name.ToString();
        }
    }

    public void Dispose()
    {
        GameNetwork.NetworkMessage -= NetworkMessage;
    }

    private void EnterDeepDungeon(int contentId, DeepDungeonContentInfo.DeepDungeonFloorSetInfo info)
    {
        FloorSetInfo = info;
        CurrentContentId = contentId;
        PluginLog.Debug($"Entering ContentID {CurrentContentId}");

        FloorTimes.Clear();

        MobService.TryReloadIfEmpty();

        for (var i = info.StartFloor; i < info.StartFloor + 10; i++)
            FloorTimes[i] = 0;

        FloorDetails.CurrentFloor = info.StartFloor - 1; // NextFloor() adds 1
        FloorDetails.RespawnTime = info.RespawnTime;
        FloorDetails.FloorTransfer = true;
        FloorDetails.NextFloor();

        if (Config.AutoOpenOnEnter)
            Plugin.ShowMainWindow();

        floorTimer.Start();
        Ready = true;
    }

    private void ExitDeepDungeon()
    {
        PluginLog.Debug($"ContentID {CurrentContentId} - Exiting");

        FloorDetails.DumpFloorObjects(CurrentContentId);

        floorTimer.Stop();
        FloorSetInfo = null;
        FloorDetails.Clear();
        Ready = false;
        Plugin.CloseMainWindow();
    }

    private void OnTimerUpdate(object? sender, ElapsedEventArgs e)
    {
        if (!InDeepDungeon)
        {
            PluginLog.Debug("Failsafe exit");
            ExitDeepDungeon();
        }

        // If the plugin is loaded mid-dungeon then verify the floor
        if (!FloorDetails.FloorVerified)
            FloorDetails.VerifyFloorNumber();

        var time = FloorDetails.UpdateFloorTime();
        FloorTimes[FloorDetails.CurrentFloor] = time;
    }

    private void NetworkMessage(
        IntPtr dataPtr, ushort opCode, uint sourceActorId, uint targetActorId, NetworkMessageDirection direction)
    {
        if (direction == NetworkMessageDirection.ZoneDown)
        {
            switch (opCode)
            {
                case (int)ServerZoneIpcType.SystemLogMessage:
                    OnSystemLogMessage(dataPtr, ReadNumber(dataPtr, 4, 4));
                    break;
                case (int)ServerZoneIpcType.ActorControlSelf:
                    OnActorControlSelf(dataPtr);
                    break;
            }
        }
    }

    private void OnActorControlSelf(IntPtr dataPtr)
    {
        // OnDirectorUpdate
        if (Marshal.ReadByte(dataPtr) == DataIds.ActorControlSelfDirectorUpdate)
        {
            switch (Marshal.ReadByte(dataPtr, 8))
            {
                // OnDutyCommenced
                case DataIds.DirectorUpdateDutyCommenced:
                {
                    var contentId = ReadNumber(dataPtr, 4, 2);
                    if (!Ready && DeepDungeonContentInfo.ContentInfo.TryGetValue(contentId, out var info))
                        EnterDeepDungeon(contentId, info);
                    break;
                }
                // OnDutyRecommenced
                case DataIds.DirectorUpdateDutyRecommenced:
                    if (Ready && FloorDetails.FloorTransfer)
                    {
                        FloorDetails.NextFloor();
                    }

                    break;
            }
        }
    }

    private void OnSystemLogMessage(IntPtr dataPtr, int logId)
    {
        if (InDeepDungeon)
        {
            switch ((uint)logId)
            {
                case DataIds.SystemLogPomanderUsed:
                    FloorDetails.OnPomanderUsed((Pomander)Marshal.ReadByte(dataPtr, 16));
                    break;
                case DataIds.SystemLogDutyEnded:
                    ExitDeepDungeon();
                    break;
                case DataIds.SystemLogTransferenceInitiated:
                    FloorDetails.FloorTransfer = true;
                    FloorDetails.DumpFloorObjects(CurrentContentId);
                    FloorDetails.FloorObjects.Clear();
                    break;
                case 0x1C6A:
                case 0x1C6B:
                case 0x1C6C:
                    FloorDetails.HoardFound = true;
                    break;
                case 0x1C36:
                case 0x23F8:
                // case 0x282F: // Demiclone
                    var pomander = (Pomander)Marshal.ReadByte(dataPtr, 12);
                    if (pomander > 0)
                    {
                        var player = ClientState.LocalPlayer!;
                        var chest = ObjectTable
                                    .Where(o => o.DataId == DataIds.GoldChest)
                                    .FirstOrDefault(o => o.Position.Distance2D(player.Position) <= 4.6f);
                        if (chest != null)
                        {
                            FloorDetails.DoubleChests[chest.EntityId] = pomander;
                        }
                    }

                    break;
            }
        }
    }

    private static int ReadNumber(IntPtr dataPtr, int offset, int size)
    {
        var bytes = new byte[4];
        Marshal.Copy(dataPtr + offset, bytes, 0, size);
        return BitConverter.ToInt32(bytes);
    }

    private bool CheckChestOpenSafe(ESPObject.ESPType type)
    {
        var info = DungeonService.FloorSetInfo;
        var unsafeChest = false;
        if (info != null)
        {
            unsafeChest = (info.MimicChests == DeepDungeonContentInfo.MimicChests.Silver &&
                           type == ESPObject.ESPType.SilverChest) ||
                          (info.MimicChests == DeepDungeonContentInfo.MimicChests.Gold &&
                           type == ESPObject.ESPType.GoldChest);
        }

        return !unsafeChest || (unsafeChest && conf.OpenUnsafeChests);
    }

    internal unsafe void TryInteract(ESPObject espObj)
    {
        var player = ClientState.LocalPlayer!;
        if ((player.StatusFlags & StatusFlags.InCombat) == 0 && conf.OpenChests && espObj.IsChest())
        {
            var type = espObj.Type;

            if (!conf.OpenBronzeCoffers && type == ESPObject.ESPType.BronzeChest) return;
            if (!conf.OpenSilverCoffers && type == ESPObject.ESPType.SilverChest) return;
            if (!conf.OpenGoldCoffers && type == ESPObject.ESPType.GoldChest) return;
            if (!conf.OpenHoards && type == ESPObject.ESPType.AccursedHoardCoffer) return;

            // We dont want to kill the player
            if (type == ESPObject.ESPType.SilverChest && player.CurrentHp <= player.MaxHp * 0.77) return;

            if (CheckChestOpenSafe(type) && espObj.Distance() <= espObj.InteractionDistance()
                                         && !FloorDetails.InteractionList.Contains(espObj.GameObject.EntityId))
            {
                TargetSystem.Instance()->InteractWithObject((GameObject*)espObj.GameObject.Address);
                FloorDetails.InteractionList.Add(espObj.GameObject.EntityId);
            }
        }
    }

    public unsafe void TryNearestOpenChest()
    {
        // Checks every object to be a chest and try to open the  
        foreach (var obj in ObjectTable)
            if (obj.IsValid())
            {
                var dataId = obj.DataId;
                if (DataIds.BronzeChestIDs.Contains(dataId) || DataIds.SilverChest == dataId ||
                    DataIds.GoldChest == dataId || DataIds.AccursedHoardCoffer == dataId)
                {
                    var espObj = new ESPObject(obj);
                    if (CheckChestOpenSafe(espObj.Type) && espObj.Distance() <= espObj.InteractionDistance())
                    {
                        TargetSystem.Instance()->InteractWithObject((GameObject*)espObj.GameObject.Address);
                        break;
                    }
                }
            }
    }

    public unsafe void OnPomanderCommand(string pomanderName)
    {
        if (TryFindPomanderByName(pomanderName, out var pomander) && IsPomanderUsable(pomander))
        {
            PrintChatMessage($"Using found pomander: {pomander}");
            if (!TryGetAddonByName<AtkUnitBase>("DeepDungeonStatus", out _))
            {
                AgentDeepDungeonStatus.Instance()->AgentInterface.Show();
            }

            taskManager.Enqueue(() => TryGetAddonByName<AtkUnitBase>("DeepDungeonStatus", out var addon) &&
                                      IsAddonReady(addon));
            taskManager.Enqueue(() =>
            {
                TryGetAddonByName<AtkUnitBase>("DeepDungeonStatus", out var addon);
                Callback.Fire(addon, true, 11, (int)pomander);
            });
        }
    }

    public void TrackFloorObjects(ESPObject espObj)
    {
        FloorDetails.TrackFloorObjects(espObj, CurrentContentId);
    }
}
