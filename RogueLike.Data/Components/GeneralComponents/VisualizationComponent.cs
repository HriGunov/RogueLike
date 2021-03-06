﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLike.Data.Components.Abstract;

namespace RogueLike.Data.Components.GeneralComponents
{
    public class VisualizationComponent : Component
    {
        public VisualizationComponent(char asChar)
        {
            AsChar = asChar;
        }

        public char AsChar { get; set; }
    }
}
