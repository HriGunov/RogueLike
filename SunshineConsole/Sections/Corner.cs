using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunshineConsole.Sections
{
    public class Corner
    {
        private int x;
        private int y;

        public Corner(int x, int y)
        {

            if (x < 0)
                throw new ArgumentException("Corner's x position cannot be less than 0");

            this.x = x;

            if (y < 0)
                throw new ArgumentException("Corner's y position cannot be less than 0");

            this.y = y;
        }

        // Steven dude showed us this awesome syntax (this is not a bribe... for sure!)
        public int X => this.x;
        public int Y => this.y;
    }
}
