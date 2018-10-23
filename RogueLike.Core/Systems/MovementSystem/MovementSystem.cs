using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLike.Data.Components.GeneralComponents;

namespace RogueLike.Core.Systems.MovementSystem
{
    public class MovementSystem
    {
        private readonly WorldSystem.WorldSystem world;

        public MovementSystem(WorldSystem.WorldSystem world)
        {
            this.world = world;
        }

        public bool CanBeMovedTo(int positionY, int positionX)
        {
            int yLocal = positionY - world.TopLeftCorner.YCoord;
            int xLocal = positionX - world.TopLeftCorner.XCoord;


            var walkablePosition = world.LocalMap[yLocal][xLocal]
                .Any(entity => entity.HasComponents(typeof(IsWalkableComponent)));

            var notBlockedPosition = !world.LocalMap[yLocal][xLocal]
                .Any(entity => entity.HasComponents(typeof(MovementBlockingComponent)));

            return walkablePosition && notBlockedPosition;
        }

        public bool CanBeMovedTo(PositionComponent position)
        {
           return CanBeMovedTo(position.YCoord, position.XCoord);
        }
    }
}
