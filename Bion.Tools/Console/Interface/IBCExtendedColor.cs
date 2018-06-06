﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.Tools
{
    public interface IBCColorExtended: IBColor
    {
        ConsoleColor? ToConsoleColor();
        bool HasColor { get; }
    }
}
