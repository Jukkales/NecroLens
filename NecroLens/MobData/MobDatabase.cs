using NecroLens.Enums;
using System.Collections.Generic;
using System.Linq;

namespace NecroLens.MobData;

public static partial class MobDatabase
{
    public class MobInfo
    {
        public required uint Id { get; set; } = 0;
        public uint BNcpId { get; set; } = 0;
        public string Name { get; set; } = "???";
        public uint TerritoryId { get; set; } = 0;
        public DeepDungeon Dungeon { get; set; } = DeepDungeon.Unk;
        public ESPType MobType { get; set; } = ESPType.Enemy;
        public AggroType AggroType { get; set; } = AggroType.Proximity;
        public DangerLevel DangerLevel { get; set; } = DangerLevel.Danger;
        public bool Patrol { get; set; } = false;
        public bool BossOrAdd { get; set; } = false;
        public bool Special { get; set; } = false;
        public List<AggroInfo> FloorAgro { get; set; } = new();

        public AggroInfo GetAggroInfo(uint TerritoryId)
        {
            var floorOverride = FloorAgro.FirstOrDefault(f => f.TerritoryId == TerritoryId);

            return new AggroInfo
            {
                TerritoryId = TerritoryId,
                AggroType = floorOverride?.AggroType ?? AggroType,
                DangerLevel = floorOverride?.DangerLevel ?? DangerLevel,
                Patrol = floorOverride?.Patrol ?? Patrol
            };
        }
    }

    public class AggroInfo
    {
        public uint TerritoryId { get; set; } = 0;
        public AggroType? AggroType { get; set; }
        public DangerLevel? DangerLevel { get; set; }
        public bool? Patrol { get; set; }
    }

    public static Dictionary<uint, MobInfo> MobInformation = new();

    public static void UpdateMobInfo()
    {
        Register_PotD();
        Register_HoH();
        Register_EO();
        Register_PT();
        AddMimics();
    }

    public static void AddMimics()
    {
        foreach (var mimicId in DataIds.MimicIDs)
        {
            MobInformation[mimicId] = new()
            {
                Id = mimicId,
                MobType = ESPType.Mimic,
                Dungeon = DeepDungeon.PotD | DeepDungeon.HoH | DeepDungeon.EO | DeepDungeon.PT,
                AggroType = AggroType.Proximity,
                DangerLevel = DangerLevel.Danger,
            };
        }
    }
}
