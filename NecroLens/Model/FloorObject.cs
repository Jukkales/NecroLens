namespace NecroLens.Model;

public class FloorObject
{
    public uint DataId { get; set; }
    public uint NameId { get; set; }
    public string? Name { get; set; }
    public int ContentId { get; set; }
    public int Floor { get; set; }
    public float? HitboxRadius { get; set; }
}
