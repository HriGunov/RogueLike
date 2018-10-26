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
            return  new PositionComponent(position.YCoord - topLeftCornerOfLocalArea.YCoord,position.XCoord - topLeftCornerOfLocalArea.XCoord);
        }

    }
}
