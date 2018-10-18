using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLike.Data.Components.GeneralComponents;
using SunshineConsole;

namespace RogueLike.Core.Systems.CameraSystem
{
    class Camera
    {
        private PositionComponent position;
        private  int width;

        public Camera(PositionComponent position,int Width)
        {
            this.width = Width;
            this.position = position;
        }

        public void SetPosition(int yCoord, int xCoord)
        {
            position.YCoord = yCoord;
            position.XCoord = xCoord;
        }

        public void ChangeYCoordBy(int value)
        {
            position.YCoord += value;
        }
        public void ChangeXCoordBy(int value)
        {
            position.XCoord += value;
        }

        public void UpdateTargetPostion(PositionComponent newTarget)
        {
            position = newTarget;
        }

        public int Width => width;

        public Symbol[][] GetCurrentView()
        {
            throw new NotImplementedException();
        }
    }
}
