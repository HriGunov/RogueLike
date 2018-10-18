using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunshineConsole.Sections.Interfaces;

namespace SunshineConsole.Sections
{
    public struct Corners : ICorners
    {

        

        private Section section;
        public Corners(Section section)
        {
            this.section = section;
        }
        
        public Corner TopLeft()
        {
            return new Corner(section.PinX, section.PinY);
        }
        public Corner TopRight()
        {
            return new Corner(section.PinX + section.Width, section.PinY);
        }
        public Corner BottomLeft()
        {
            return new Corner(section.PinX, section.PinY + section.Height);
        }
        public Corner BottomRight()
        {
            return new Corner(section.PinX + section.Width, section.PinY + section.Height);
        }
    } 
}
