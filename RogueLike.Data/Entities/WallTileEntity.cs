using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLike.Data.Components.GeneralComponents;

namespace RogueLike.Data.Entities
{
    class WallTileEntity : TileEntity
    {
        public WallTileEntity()
        {
            VisualizationComponent.AsChar = '#';
        }
    }
}
