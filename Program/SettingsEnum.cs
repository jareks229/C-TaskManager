using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    [Flags]
    public enum SettingsEnum
    {
        IsAllDayTask = 1,
        IsImportant =2
    }
}
