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
            int yLocal = positionY - map.TopLeftCorner.YCoord;
            int xLocal = positionX - map.TopLeftCorner.XCoord;


            var walkablePosition = map.LocalMap[yLocal][xLocal]
                .Any(entity => entity.HasComponents(typeof(IsWalkableComponent)));

            var notBlockedPosition = !map.LocalMap[yLocal][xLocal]
                .Any(entity => entity.HasComponents(typeof(MovementBlockingComponent)));

            return walkablePosition && notBlockedPosition;
        }

        public bool CanBeMovedTo(PositionComponent position)
        {
           return CanBeMovedTo(position.YCoord, position.XCoord);
        }
    }
}
