using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Data.Entities
{
    class FloorTileEntity : TileEntity
    {
        public FloorTileEntity() : base()
        {
            VisualizationComponent.AsChar = '.';
        }
    }
}
