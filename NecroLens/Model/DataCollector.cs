using System.Collections.ObjectModel;

namespace NecroLens.Model;

public class DataCollector
{
    public uint Version => 2;
    
    public string? Sender { get; set; }
    
    public string? Party { get; set; }

    public Collection<MobData> Data { get; set; } = [];
    
    public class MobData
    {
        public uint DataId { get; set; }
        public uint NameId { get; set; }
        public int ContentId { get; set; }
        public int Floor { get; set; }
        public float? HitboxRadius { get; set; }
        public Collection<float>? MoveTimes { get; set; }
        public Collection<float>? AggroDistances { get; set; }
    }
}
