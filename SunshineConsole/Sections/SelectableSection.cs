using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace SunshineConsole.Sections
{
    class SelectableSection : Section
    {
        public SelectableSection(Section section) :
            this(section.PinY, section.PinX, section.Height, section.Width, section.Layer)
        {

        }
        public SelectableSection(int pinY, int pinX, int height, int width, int layer = 0) : base(pinY, pinX, height, width, layer)
        {
        }
        
    }
}
