using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using ClosedXML.Excel;
using NecroLens.Model;

namespace NecroLensDataTools;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class DatabaseConverter
{
       public static void Main(String[] args)
    {
        var inFile = Path.Combine(Directory.GetCurrentDirectory(), "../../../../Data/deepDungeonMobDatabase.xlsx");
        var csvInfo = new List<DbNpcName>();

        using (var workbook = new XLWorkbook(inFile))
        {
            var worksheet = workbook.Worksheet(1);
            var rows = worksheet.RangeUsed()!.RowsUsed().Skip(1);

            foreach (var row in rows)
            {
                var entry = new DbNpcName
                {
                    PotD = row.Cell(2).GetValue<bool?>(),
                    HoH = row.Cell(3).GetValue<bool?>(),
                    EO = row.Cell(4).GetValue<bool?>(),
                    PT = row.Cell(5).GetValue<bool?>(),
                    Id = int.Parse(row.Cell(6).GetValue<string>()), 
                    
                    Aggro = row.Cell(7).GetValue<string>(),
                    DangerLevel = row.Cell(8).GetValue<string>(),
                    
                    Patrol = row.Cell(9).GetValue<bool?>(),
                    BossOrAdd = row.Cell(10).GetValue<bool?>(),
                    Special = row.Cell(11).GetValue<bool?>()
                };

                csvInfo.Add(entry);
            }
        }

        var mobList = new List<MobInfo>();
        foreach (var info in csvInfo)
        {
            if ((info.PotD ?? false) || (info.HoH ?? false) || (info.EO ?? false) || (info.PT ?? false))
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
        }
        
        var outFile = Path.Combine(Directory.GetCurrentDirectory(), "../../../../../NecroLens/Data/allMobs.json");
        File.WriteAllText(outFile, JsonSerializer.Serialize(mobList));
    }
}
