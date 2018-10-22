using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLike.Data.Components.GeneralComponents;

namespace RogueLike.Core.Systems.MovementSystem
{
    public class Movement
    {
        private readonly MapSystem.MapSystem map;

        public Movement(MapSystem.MapSystem map)
        {
            this.map = map;
        }

        public bool CanBeMovedTo(int positionY, int positionX)
        {
            int yChunk = (positionY - map.TopLeftCorner.YCoord) / map.WidthOfChunks;
            int xChunk = (positionX - map.TopLeftCorner.XCoord) / map.WidthOfChunks;
            return map.Chunks[yChunk][xChunk]
                .Where(ent => ent.HasComponents(typeof(PositionComponent), typeof(IsWalkableComponent))).Any(
                    e =>
                    {
                        var pos = e.GetComponent(typeof(PositionComponent)) as PositionComponent;
                        if (pos.YCoord != positionY)
                        {
                            return false;
                        }

                        if (pos.XCoord != positionX)
                        {
                            return false;

                        }

                        return true;
                    });
             
        }

        public bool CanBeMovedTo(PositionComponent position)
        {
           return CanBeMovedTo(position.YCoord, position.XCoord);
        }
    }
}
