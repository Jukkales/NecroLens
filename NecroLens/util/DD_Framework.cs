using ECommons;
using FFXIVClientStructs.FFXIV.Client.Game.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecroLens.util;

public static class DD_Framework
{
    public static float TimeRemaining = 0f;
    public static int CurrentFloor = 0;
    public static int PassageProgress = 0;

    public static unsafe void UpdateInfo()
    {
        var ef = EventFramework.Instance();
        if (ef == null)
        {
            return;
        }

        var dd = ef->GetInstanceContentDeepDungeon();
        if (dd == null)
        {
            return;
        }

        CurrentFloor = dd->Floor;
        PassageProgress = dd->PassageProgress;
        TimeRemaining = dd->ContentTimeLeft;
    }
}
