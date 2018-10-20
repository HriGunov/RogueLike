using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLike.Data.Components.GeneralComponents;

namespace RogueLike.Data.Entities
{
    public class FloorTileEntity : TileEntity
    {
       

        public FloorTileEntity(PositionComponent position) : base(position)
        {
            VisualizationComponent.AsChar = '.';

        }

        public FloorTileEntity(int yCoord = 0, int xCoord = 0) : this(new PositionComponent(yCoord, xCoord))
        {
        }
    }
}
