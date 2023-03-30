using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json;
using CsvHelper;
using CsvHelper.Configuration;
using NecroLens.Model;

namespace NecroLensDataTools;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class DatabaseConverter
{
    public static void Main(String[] args)
    {
        var inFile = Path.Combine(Directory.GetCurrentDirectory(), "../../../../Data/deepDungeonMobDatabase.csv");
        var csvInfo = new List<DbNpcName>();
        var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", HasHeaderRecord = true };
        using var reader = new StreamReader(inFile);
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<DbNpcName>();
        csvInfo.AddRange(records);
        csv.Dispose();
        reader.Dispose();

        var mobList = new List<MobInfo>();
        foreach (var info in csvInfo)
            if ((info.PotD ?? false) || (info.HoH ?? false) || (info.EO ?? false))
            {
                var mob = new MobInfo
                {
                    Id = (uint)info.Id,
                    AggroType = (ESPObject.ESPAggroType)Enum.Parse(typeof(ESPObject.ESPAggroType), info.Aggro!),
                    DangerLevel =
                        (ESPObject.ESPDangerLevel)Enum.Parse(typeof(ESPObject.ESPDangerLevel), info.DangerLevel!),
                    BossOrAdd = info.BossOrAdd ?? false,
                    Patrol = info.Patrol ?? false,
                    Special = info.Special ?? false
                };
                mobList.Add(mob);
            }

        var outFile = Path.Combine(Directory.GetCurrentDirectory(), "../../../../../NecroLens/Data/allMobs.json");
        File.WriteAllText(outFile, JsonSerializer.Serialize(mobList));
    }
}
