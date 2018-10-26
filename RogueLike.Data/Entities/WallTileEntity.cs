using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLike.Data.Components.GeneralComponents;

namespace RogueLike.Data.Entities
{
    public class WallTileEntity : TileEntity
    {

        public WallTileEntity(int yCoord = 0, int xCoord = 0) : this(new PositionComponent(yCoord, xCoord))
        {
            
        }

        public WallTileEntity(PositionComponent position) : base(position)
        {
            VisualizationComponent.AsChar = '#';
            this.AddComponent(new SightBlockingComponent());
            
        }
 
    }
}
