using System;
using System.Collections.Generic;
using System.Text;

namespace NecroLens.Enums
{
    public enum TrapStatus
    {
        Active = 1 << 1,
        Visible = 1 << 2,
        Inactive = 1 << 3,
    }
}
