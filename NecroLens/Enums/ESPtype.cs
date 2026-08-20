using System;
using System.Collections.Generic;
using System.Text;

namespace NecroLens.Enums;

public enum ESPType
{
    Player = 1 << 0,
    Enemy = 1 << 1,
    Mimic = 1 << 2,
    FriendlyEnemy = 1 << 3,

    BronzeChest = 1 << 4,
    SilverChest = 1 << 5,
    GoldChest = 1 << 6,

    AccursedHoard = 1 << 7, 
    AccursedHoardCoffer = 1 << 8,
    MimicChest = 1 << 9,

    Trap = 1 << 10,
    Return = 1 << 11,
    Passage = 1 << 12,
    Votife = 1 << 13
}
