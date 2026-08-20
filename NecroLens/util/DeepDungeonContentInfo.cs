using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NecroLens.Enums;
using NecroLens.Model;

namespace NecroLens.util;

public class DeepDungeonContentInfo
{
    public static readonly Dictionary<int, DeepDungeonFloorSetInfo> ContentInfo = new()
    {
        // PotD
        [60001] = new() { StartFloor = 1, RespawnTime = 40, MimicChests = MimicChests.Bronze },
        [60002] = new() { StartFloor = 11, RespawnTime = 60, MimicChests = MimicChests.Bronze },
        [60003] = new() { StartFloor = 21, RespawnTime = 60, MimicChests = MimicChests.Bronze },
        [60004] = new() { StartFloor = 31, RespawnTime = 60, MimicChests = MimicChests.Silver },
        [60005] = new() { StartFloor = 41, RespawnTime = 120, MimicChests = MimicChests.Gold },
        [60006] = new() { StartFloor = 51, RespawnTime = 60, MimicChests = MimicChests.Gold },
        [60007] = new() { StartFloor = 61, RespawnTime = 60, MimicChests = MimicChests.Gold },
        [60008] = new() { StartFloor = 71, RespawnTime = 60, MimicChests = MimicChests.Gold },
        [60009] = new() { StartFloor = 81, RespawnTime = 60, MimicChests = MimicChests.Gold },
        [60010] = new() { StartFloor = 91, RespawnTime = 120, MimicChests = MimicChests.Gold },
        [60011] = new() { StartFloor = 101, RespawnTime = 90, MimicChests = MimicChests.Gold },
        [60012] = new() { StartFloor = 111, RespawnTime = 90, MimicChests = MimicChests.Gold },
        [60013] = new() { StartFloor = 121, RespawnTime = 90, MimicChests = MimicChests.Gold },
        [60014] = new() { StartFloor = 131, RespawnTime = 90, MimicChests = MimicChests.Gold },
        [60015] = new() { StartFloor = 141, RespawnTime = 90, MimicChests = MimicChests.Gold },
        [60016] = new() { StartFloor = 151, RespawnTime = 300, MimicChests = MimicChests.Gold },
        [60017] = new() { StartFloor = 161, RespawnTime = 300, MimicChests = MimicChests.Gold },
        [60018] = new() { StartFloor = 171, RespawnTime = 300, MimicChests = MimicChests.Gold },
        [60019] = new() { StartFloor = 181, RespawnTime = 300, MimicChests = MimicChests.Gold },
        [60020] = new() { StartFloor = 191, RespawnTime = 300, MimicChests = MimicChests.Gold },

        // Heaven on High
        [60021] = new() { StartFloor = 1, RespawnTime = 60, MimicChests = MimicChests.Bronze },
        [60022] = new() { StartFloor = 11, RespawnTime = 60, MimicChests = MimicChests.Bronze },
        [60023] = new() { StartFloor = 21, RespawnTime = 60, MimicChests = MimicChests.Bronze },
        [60024] = new() { StartFloor = 31, RespawnTime = 600, MimicChests = MimicChests.Silver },
        [60025] = new() { StartFloor = 41, RespawnTime = 600, MimicChests = MimicChests.Silver },
        [60026] = new() { StartFloor = 51, RespawnTime = 600, MimicChests = MimicChests.Silver },
        [60027] = new() { StartFloor = 61, RespawnTime = 600, MimicChests = MimicChests.Gold },
        [60028] = new() { StartFloor = 71, RespawnTime = 600, MimicChests = MimicChests.Gold },
        [60029] = new() { StartFloor = 81, RespawnTime = 600, MimicChests = MimicChests.Gold },
        [60030] = new() { StartFloor = 91, RespawnTime = 600, MimicChests = MimicChests.Gold },

        // Eureka Orthos
        [60031] = new() { StartFloor = 1, RespawnTime = 60, MimicChests = MimicChests.Bronze },
        [60032] = new() { StartFloor = 11, RespawnTime = 60, MimicChests = MimicChests.Bronze },
        [60033] = new() { StartFloor = 21, RespawnTime = 60, MimicChests = MimicChests.Bronze },
        [60034] = new() { StartFloor = 31, RespawnTime = 600, MimicChests = MimicChests.Silver },
        [60035] = new() { StartFloor = 41, RespawnTime = 600, MimicChests = MimicChests.Silver },
        [60036] = new() { StartFloor = 51, RespawnTime = 600, MimicChests = MimicChests.Silver },
        [60037] = new() { StartFloor = 61, RespawnTime = 600, MimicChests = MimicChests.Gold },
        [60038] = new() { StartFloor = 71, RespawnTime = 600, MimicChests = MimicChests.Gold },
        [60039] = new() { StartFloor = 81, RespawnTime = 600, MimicChests = MimicChests.Gold },
        [60040] = new() { StartFloor = 91, RespawnTime = 600, MimicChests = MimicChests.Gold },

        // Pilgrims Traverse
        [60041] = new() { StartFloor = 1, RespawnTime = 60, MimicChests = MimicChests.Bronze },
        [60042] = new() { StartFloor = 11, RespawnTime = 60, MimicChests = MimicChests.Bronze },
        [60043] = new() { StartFloor = 21, RespawnTime = 60, MimicChests = MimicChests.Bronze },
        [60044] = new() { StartFloor = 31, RespawnTime = 600, MimicChests = MimicChests.Silver },
        [60045] = new() { StartFloor = 41, RespawnTime = 600, MimicChests = MimicChests.Silver },
        [60046] = new() { StartFloor = 51, RespawnTime = 600, MimicChests = MimicChests.Silver },
        [60047] = new() { StartFloor = 61, RespawnTime = 600, MimicChests = MimicChests.Gold },
        [60048] = new() { StartFloor = 71, RespawnTime = 600, MimicChests = MimicChests.Gold },
        [60049] = new() { StartFloor = 81, RespawnTime = 600, MimicChests = MimicChests.Gold },
        [60050] = new() { StartFloor = 91, RespawnTime = 600, MimicChests = MimicChests.Gold },
    };

    public class DeepDungeonFloorSetInfo
    {
        public int StartFloor { get; internal init; }
        public int RespawnTime { get; internal init; }
        public MimicChests MimicChests { get; internal init; }
    }
}
