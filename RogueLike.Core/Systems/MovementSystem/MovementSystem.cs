using Autofac;
using RogueLike.Core.ComponentExtensions;
using RogueLike.Core.Systems.MovementSystem.Actions;
using RogueLike.Core.Systems.TimeTracking;
using RogueLike.Data.Components.GeneralComponents;
using System.Linq;

namespace RogueLike.Core.Systems.MovementSystem
{
    public enum MovementDirection
    {
        North, West, South, East,
        NorthEast, NorthWest,
        SouthEast, SouthWest
    }
    public class MovementSystem
    {
        private readonly WorldSystem.WorldSystem _worldSystem;
        private readonly TimeTrackingSystem _timeTrackingSystem;
        private readonly IContainer _container;

        public MovementSystem(WorldSystem.WorldSystem world, TimeTrackingSystem timeTrackingSystem)
        {
            this._worldSystem = world;
            this._timeTrackingSystem = timeTrackingSystem; 
        }

        public bool CanBeMovedTo(int positionY, int positionX)
        {
            int yLocal = positionY - _worldSystem.TopLeftCorner.YCoord;
            int xLocal = positionX - _worldSystem.TopLeftCorner.XCoord;


            var walkablePosition = _worldSystem.LocalMap[yLocal][xLocal]
                .Any(entity => entity.HasComponents(typeof(IsWalkableComponent)));

            var notBlockedPosition = !_worldSystem.LocalMap[yLocal][xLocal]
                .Any(entity => entity.HasComponents(typeof(MovementBlockingComponent)));

            return walkablePosition && notBlockedPosition;
        }

        public bool CanBeMovedTo(PositionComponent position)
        {
            return CanBeMovedTo(position.YCoord, position.XCoord);
        }
        public bool CanBeMovedTo(PositionComponent position, MovementDirection direction)
        {
           return CanBeMovedTo(position.GetNewPositionByDirection(direction));
        }
        public void Move(PositionComponent currentPosition, PositionComponent targerPosition, long movementTime)
        { 
            var movementAction = new MoveToAction(positionToChange: currentPosition, targetPosition: targerPosition, activationTime: movementTime);
            _timeTrackingSystem.AddAction(movementAction);
        }
        public void Move(PositionComponent currentPosition, MovementDirection direction,  long movementTime)
        {
            PositionComponent newPosition = currentPosition.GetNewPositionByDirection(direction);             

            Move(currentPosition, newPosition, movementTime);
        }

         
    }
}
