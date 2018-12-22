using RogueLike.Core.Systems.MovementSystem;
using RogueLike.Data.Components.GeneralComponents;

namespace RogueLike.Core.ComponentExtensions
{
    public static class PositionComponentExtensions
    {
        /// <summary>
        /// Returns new Position Component with the same coordinates
        /// </summary>
        /// <param name="thisComponent"></param>
        /// <returns></returns>
        public static PositionComponent GetCopy(this PositionComponent thisComponent)
        {
            return new PositionComponent(thisComponent.YCoord, thisComponent.XCoord);
        }
        /// <summary>
        /// Changes the coordinates with a the given values
        /// </summary>
        /// <param name="thisComponent"></param>
        /// <param name="deltaY"></param>
        /// <param name="deltaX"></param>
        public static PositionComponent MoveBy(this PositionComponent thisComponent, int deltaY, int deltaX)
        {
            thisComponent.YCoord += deltaY;
            thisComponent.XCoord += deltaX;
            return thisComponent;
        }

        public static PositionComponent GetMovedByCopy(this PositionComponent thisComponent, int deltaY, int deltaX)
        {
            return thisComponent.GetCopy().MoveBy(deltaY, deltaX);
        }

        /// <summary>
        /// Copies the coordinates without copying references 
        /// </summary>
        /// <param name="thisComponent"></param>
        /// <param name="otherComponent"></param>
        /// <returns></returns>
        public static PositionComponent MoveTo(this PositionComponent thisComponent, PositionComponent otherComponent)
        {
            thisComponent.YCoord = otherComponent.YCoord;
            thisComponent.XCoord = otherComponent.XCoord;
            return thisComponent;
        }

        public static PositionComponent ToLocalCoordiantes(this PositionComponent position,
            PositionComponent topLeftCornerOfLocalArea)
        {
            return new PositionComponent(position.YCoord - topLeftCornerOfLocalArea.YCoord, position.XCoord - topLeftCornerOfLocalArea.XCoord);
        }

        public static PositionComponent GetNewPositionByDirection(this PositionComponent position, MovementDirection direction)
        {
            var newPosition = position.GetCopy();
            switch (direction)
            {
                case MovementDirection.North:
                    newPosition.MoveBy(deltaY: -1, deltaX: 0);
                    break;

                case MovementDirection.West:
                    newPosition.MoveBy(deltaY: 0, deltaX: -1);
                    break;

                case MovementDirection.South:
                    newPosition.MoveBy(deltaY: 1, deltaX: 0);
                    break;

                case MovementDirection.East:
                    newPosition.MoveBy(deltaY: 0, deltaX: 1);
                    break;

                case MovementDirection.NorthEast:
                    newPosition.MoveBy(deltaY: -1, deltaX: 1);
                    break;

                case MovementDirection.NorthWest:
                    newPosition.MoveBy(deltaY: -1, deltaX: -1);
                    break;

                case MovementDirection.SouthEast:
                    newPosition.MoveBy(deltaY: 1, deltaX: 1);
                    break;

                case MovementDirection.SouthWest:
                    newPosition.MoveBy(deltaY: 1, deltaX: -1);
                    break;

            }
            return newPosition;
        }
    }
}
