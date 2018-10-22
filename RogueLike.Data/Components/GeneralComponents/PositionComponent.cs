using RogueLike.Data.Components.Abstract;

namespace RogueLike.Data.Components.GeneralComponents
{
    public class PositionComponent : Component
    {
        public PositionComponent()
        {

        }

           public PositionComponent(int yCoord, int xCoord)
        {
            YCoord = yCoord;
            XCoord = xCoord;
        }

        public int YCoord { get; set; }
        public int XCoord { get; set; }


    }
}
