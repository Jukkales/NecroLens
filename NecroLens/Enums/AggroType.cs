namespace NecroLens.Enums
{
    public enum AggroType
    {
        Sight = 1 << 0,
        Sound = 1 << 1,
        Proximity = 1 << 2,

        Unknown = 1 << 10,
    }
}
