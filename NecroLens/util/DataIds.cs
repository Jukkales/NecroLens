using System.Collections.Generic;

namespace NecroLens.util;

public static class DataIds
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // Network IDs
    public const uint SystemLogPomanderUsed = 0x1C56;
    public const uint SystemLogDutyEnded = 0x5FE;
    public const uint SystemLogTransferenceInitiated = 0x1C50;
    public const uint ActorControlSelfDirectorUpdate = 0x6D;
    public const int DirectorUpdateDutyCommenced = 0x1;
    public const int DirectorUpdateDutyRecommenced = 0x6;

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // DataIds of Objects
    public const uint SilverChest = 2007357;
    public const uint GoldChest = 2007358;
    public const uint MimicChest = 2006020;

    public static readonly HashSet<uint> PalaceOfTheDeadMapIds = new()
    {
        561, 562, 563, 564, 565, 593, 594, 595, 596, 597, 598, 599, 600, 601, 602, 603, 604, 605, 606, 607
    };

    public static readonly HashSet<uint> HeavenOnHighMapIds = new()
    {
        770, 771, 772, 782, 773, 783, 774, 784, 775, 785
    };

    public static readonly HashSet<uint> EurekaOrthosMapIds = new()
    {
        1099, 1100, 1101, 1102, 1103, 1104, 1105, 1106, 1107, 1108
    };

    public static readonly HashSet<uint> IgnoredDataIDs = new()
    {
        0,       // Players
        6388,    // Triggered Trap
        1023070, // ??? Object way out
        2000608, // ??? Object in Boss Room
        2005809, // Exit
        2001168, // Twistaaa

        // Demiclones
        15898, 15899, 15860
    };

    public static readonly HashSet<uint> MimicIDs = new()
    {
        2566, 6362, 6363, 7392, 7393, 7394, 5832, 15997, 15998, 15999, 16002, 16003
    };

    public static readonly HashSet<uint> BronzeChestIDs = new()
    {
        // PotD
        782, 783, 784, 785, 786, 787, 788, 789, 790, 802, 803, 804, 805,
        // HoH
        1036, 1037, 1038, 1039, 1040, 1041, 1042, 1043, 1044, 1045, 1046, 1047, 1048, 1049,
        // EO
        1541, 1542, 1543, 1544, 1545, 1546, 1547, 1548, 1549, 1550, 1551, 1552, 1553, 1554
    };

    public static readonly HashSet<uint> AccursedHoardIDs = new()
    {
        2007542, 2007543
    };

    public static readonly Dictionary<uint, string> TrapIDs = new()
    {
        { 2007182, "Landmine" },
        { 2007183, "Luring Trap" },
        { 2007184, "Enfeebling Trap" },
        { 2007185, "Impeding Trap" },
        { 2007186, "Toad Trap" },
        { 2009504, "Odder Trap" },
        { 2013284, "Owlet Trap" }
    };

    public static readonly HashSet<uint> PassageIDs = new()
    {
        2007188, // PotD
        2009507, // HoH
        2013287  // EO
    };

    public static readonly HashSet<uint> ReturnIDs = new()
    {
        2007187, // PotD
        2009506, // HoH
        2013286  // EO
    };

    public static readonly HashSet<uint> FriendlyIDs = new()
    {
        // PotD
        5840, 5041, 7610,
        // HoH
        7396, 7397, 7398,
        // EO
        16007, 16008, 16009, 16012
    };
}
