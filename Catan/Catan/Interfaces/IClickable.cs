﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Catan.Interfaces
{
    public interface IClickable
    {
        int ScreenX { get; }
        int ScreenY { get; }

        uint DX { get; }

        uint DY { get; }
        bool CLickBelongToObject(int clcikedX, int clickedY);

    }
}