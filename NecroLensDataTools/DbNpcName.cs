using System.Diagnostics.CodeAnalysis;

namespace NecroLensDataTools;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class DbNpcName
{
    public string Name { get; set; } = "";
    public bool? PotD { get; set; }
    public bool? HoH { get; set; }
    public bool? EO { get; set; }
    public bool? PT { get; set; }
    public int Id { get; set; }
    public string? Aggro { get; set; }
    public string? DangerLevel { get; set; }
    public bool? Patrol { get; set; }
    public bool? BossOrAdd { get; set; }
    public bool? Special { get; set; }
}
