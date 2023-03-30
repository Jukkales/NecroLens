using System.Diagnostics.CodeAnalysis;
using CsvHelper.Configuration.Attributes;

namespace NecroLensDataTools;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class DbNpcName
{
    [Index(0)]
    public string Name { get; set; } = "";

    [Index(1)]
    public bool? PotD { get; set; }

    [Index(2)]
    public bool? HoH { get; set; }

    [Index(3)]
    public bool? EO { get; set; }

    [Index(4)]
    public int Id { get; set; }

    [Index(5)]
    public string? Aggro { get; set; }

    [Index(6)]
    public string? DangerLevel { get; set; }

    [Index(7)]
    public bool? Patrol { get; set; }

    [Index(8)]
    public bool? BossOrAdd { get; set; }

    [Index(9)]
    public bool? Special { get; set; }
}
