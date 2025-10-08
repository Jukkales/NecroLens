using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NecroLens.Model;

namespace NecroLens.util;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class DeepDungeonContentInfo
{
    public enum MimicChests
    {
        Bronze,
        Silver,
        Gold
    }

    public static readonly Dictionary<int, DeepDungeonFloorSetInfo> ContentInfo = new()
    {
        // PotD
        { 60001, new DeepDungeonFloorSetInfo { StartFloor = 1, RespawnTime = 40, MimicChests = MimicChests.Bronze } },
        { 60002, new DeepDungeonFloorSetInfo { StartFloor = 11, RespawnTime = 60, MimicChests = MimicChests.Bronze } },
        { 60003, new DeepDungeonFloorSetInfo { StartFloor = 21, RespawnTime = 60, MimicChests = MimicChests.Bronze } },
        { 60004, new DeepDungeonFloorSetInfo { StartFloor = 31, RespawnTime = 60, MimicChests = MimicChests.Silver } },
        { 60005, new DeepDungeonFloorSetInfo { StartFloor = 41, RespawnTime = 120, MimicChests = MimicChests.Gold } },
        { 60006, new DeepDungeonFloorSetInfo { StartFloor = 51, RespawnTime = 60, MimicChests = MimicChests.Gold } },
        { 60007, new DeepDungeonFloorSetInfo { StartFloor = 61, RespawnTime = 60, MimicChests = MimicChests.Gold } },
        { 60008, new DeepDungeonFloorSetInfo { StartFloor = 71, RespawnTime = 60, MimicChests = MimicChests.Gold } },
        { 60009, new DeepDungeonFloorSetInfo { StartFloor = 81, RespawnTime = 60, MimicChests = MimicChests.Gold } },
        { 60010, new DeepDungeonFloorSetInfo { StartFloor = 91, RespawnTime = 120, MimicChests = MimicChests.Gold } },
        { 60011, new DeepDungeonFloorSetInfo { StartFloor = 101, RespawnTime = 90, MimicChests = MimicChests.Gold } },
        { 60012, new DeepDungeonFloorSetInfo { StartFloor = 111, RespawnTime = 90, MimicChests = MimicChests.Gold } },
        { 60013, new DeepDungeonFloorSetInfo { StartFloor = 121, RespawnTime = 90, MimicChests = MimicChests.Gold } },
        { 60014, new DeepDungeonFloorSetInfo { StartFloor = 131, RespawnTime = 90, MimicChests = MimicChests.Gold } },
        { 60015, new DeepDungeonFloorSetInfo { StartFloor = 141, RespawnTime = 90, MimicChests = MimicChests.Gold } },
        { 60016, new DeepDungeonFloorSetInfo { StartFloor = 151, RespawnTime = 300, MimicChests = MimicChests.Gold } },
        { 60017, new DeepDungeonFloorSetInfo { StartFloor = 161, RespawnTime = 300, MimicChests = MimicChests.Gold } },
        { 60018, new DeepDungeonFloorSetInfo { StartFloor = 171, RespawnTime = 300, MimicChests = MimicChests.Gold } },
        { 60019, new DeepDungeonFloorSetInfo { StartFloor = 181, RespawnTime = 300, MimicChests = MimicChests.Gold } },
        { 60020, new DeepDungeonFloorSetInfo { StartFloor = 191, RespawnTime = 300, MimicChests = MimicChests.Gold } },

        // Heaven on High
        { 60021, new DeepDungeonFloorSetInfo { StartFloor = 1, RespawnTime = 60, MimicChests = MimicChests.Bronze } },
        { 60022, new DeepDungeonFloorSetInfo { StartFloor = 11, RespawnTime = 60, MimicChests = MimicChests.Bronze } },
        { 60023, new DeepDungeonFloorSetInfo { StartFloor = 21, RespawnTime = 60, MimicChests = MimicChests.Bronze } },
        { 60024, new DeepDungeonFloorSetInfo { StartFloor = 31, RespawnTime = 600, MimicChests = MimicChests.Silver } },
        { 60025, new DeepDungeonFloorSetInfo { StartFloor = 41, RespawnTime = 600, MimicChests = MimicChests.Silver } },
        { 60026, new DeepDungeonFloorSetInfo { StartFloor = 51, RespawnTime = 600, MimicChests = MimicChests.Silver } },
        { 60027, new DeepDungeonFloorSetInfo { StartFloor = 61, RespawnTime = 600, MimicChests = MimicChests.Gold } },
        { 60028, new DeepDungeonFloorSetInfo { StartFloor = 71, RespawnTime = 600, MimicChests = MimicChests.Gold } },
        { 60029, new DeepDungeonFloorSetInfo { StartFloor = 81, RespawnTime = 600, MimicChests = MimicChests.Gold } },
        { 60030, new DeepDungeonFloorSetInfo { StartFloor = 91, RespawnTime = 600, MimicChests = MimicChests.Gold } },

        // Eureka Orthos
        { 60031, new DeepDungeonFloorSetInfo { StartFloor = 1, RespawnTime = 60, MimicChests = MimicChests.Bronze } },
        { 60032, new DeepDungeonFloorSetInfo { StartFloor = 11, RespawnTime = 60, MimicChests = MimicChests.Bronze } },
        { 60033, new DeepDungeonFloorSetInfo { StartFloor = 21, RespawnTime = 60, MimicChests = MimicChests.Bronze } },
        { 60034, new DeepDungeonFloorSetInfo { StartFloor = 31, RespawnTime = 600, MimicChests = MimicChests.Silver } },
        { 60035, new DeepDungeonFloorSetInfo { StartFloor = 41, RespawnTime = 600, MimicChests = MimicChests.Silver } },
        { 60036, new DeepDungeonFloorSetInfo { StartFloor = 51, RespawnTime = 600, MimicChests = MimicChests.Silver } },
        { 60037, new DeepDungeonFloorSetInfo { StartFloor = 61, RespawnTime = 600, MimicChests = MimicChests.Gold } },
        { 60038, new DeepDungeonFloorSetInfo { StartFloor = 71, RespawnTime = 600, MimicChests = MimicChests.Gold } },
        { 60039, new DeepDungeonFloorSetInfo { StartFloor = 81, RespawnTime = 600, MimicChests = MimicChests.Gold } },
        { 60040, new DeepDungeonFloorSetInfo { StartFloor = 91, RespawnTime = 600, MimicChests = MimicChests.Gold } },

        // Pilgrims Traverse
        { 60041, new DeepDungeonFloorSetInfo { StartFloor = 1, RespawnTime = 60, MimicChests = MimicChests.Bronze } },
        { 60042, new DeepDungeonFloorSetInfo { StartFloor = 11, RespawnTime = 60, MimicChests = MimicChests.Bronze } },
        { 60043, new DeepDungeonFloorSetInfo { StartFloor = 21, RespawnTime = 60, MimicChests = MimicChests.Bronze } },
        { 60044, new DeepDungeonFloorSetInfo { StartFloor = 31, RespawnTime = 600, MimicChests = MimicChests.Silver } },
        { 60045, new DeepDungeonFloorSetInfo { StartFloor = 41, RespawnTime = 600, MimicChests = MimicChests.Silver } },
        { 60046, new DeepDungeonFloorSetInfo { StartFloor = 51, RespawnTime = 600, MimicChests = MimicChests.Silver } },
        { 60047, new DeepDungeonFloorSetInfo { StartFloor = 61, RespawnTime = 600, MimicChests = MimicChests.Gold } },
        { 60048, new DeepDungeonFloorSetInfo { StartFloor = 71, RespawnTime = 600, MimicChests = MimicChests.Gold } },
        { 60049, new DeepDungeonFloorSetInfo { StartFloor = 81, RespawnTime = 600, MimicChests = MimicChests.Gold } },
        { 60050, new DeepDungeonFloorSetInfo { StartFloor = 91, RespawnTime = 600, MimicChests = MimicChests.Gold } },
    };

    // Some Mobs on PotD are used multiple times with different settings
    public static readonly Dictionary<int, List<MobInfo>> ContentMobInfoChanges = new()
    {
        {
            60006, new List<MobInfo>
            {
                // palace pudding (4996) 11-20 Patrol, 51-60 Not
                new() { Id = 4996, Patrol = false }
            }
        },
        {
            60019, new List<MobInfo>
            {
                // deep palace sprite (5480) 101+ Sound, 181+ Sight
                new() { Id = 5480, AggroType = ESPObject.ESPAggroType.Sight }
            }
        },
        {
            60020, new List<MobInfo>
            {
                // Onyx Dragon (5420) 141+ Sight, 191+ Proximity
                new() { Id = 5420, AggroType = ESPObject.ESPAggroType.Proximity },
                // Hippogryph (5364) 100+ proximity, 191+ Sight
                new() { Id = 5364, AggroType = ESPObject.ESPAggroType.Sight }
            }
        }
    };

    public class DeepDungeonFloorSetInfo
    {
        public int StartFloor { get; internal init; }
        public int RespawnTime { get; internal init; }
        public MimicChests MimicChests { get; internal init; }
    }
}
