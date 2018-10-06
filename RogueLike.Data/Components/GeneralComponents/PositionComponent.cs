using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Data.Components.GeneralComponents
{
    class PositionComponent
    {
        public PositionComponent()
        {

        }

        public PositionComponent(int yCoord, int xCoord)
        {
            YCoord = yCoord;
            XCoord = xCoord;
        }

        public int YCoord { get; set; }
        public int XCoord { get; set; }


    }
}
