using System.Linq;
using RogueLike.Data.Components.GeneralComponents;

namespace RogueLike.Core.Systems.CameraSystem
{
    class Camera
    {
        private PositionComponent position;
        private int width;

        public Camera(PositionComponent position, int Width)
        {
            width = Width;
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

        public PositionComponent Position
        {
            get { return position; }
        }

        public char[][] GetCurrentView(MapSystem.MapSystem map)
        {
            char[][] view = new char[width][];
            for (int i = 0; i < width; i++)
            {
                view[i] = new char[width];
            }

            int yOffSet = position.YCoord - width / 2;
            int xOffSet = position.XCoord - width / 2;

            var yInLocalCoords = yOffSet - map.TopLeftCorner.YCoord;
            var xInLocalCoords = xOffSet - map.TopLeftCorner.XCoord;



            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (map.LocalMap[yInLocalCoords + y][xInLocalCoords + x] != null)
                    {
                        var entitiesToBeVisualizedAtPos = map.LocalMap[yInLocalCoords + y][xInLocalCoords + x].
                            FirstOrDefault(e => e.HasComponent(typeof(VisualizationComponent)));

                        if (entitiesToBeVisualizedAtPos == null)
                        {
                            view[y][x] = ' ';
                        }
                        else
                        {
                            var visualization = entitiesToBeVisualizedAtPos.GetComponent(
                                typeof(VisualizationComponent)) as VisualizationComponent;

                            view[y][x] = visualization.AsChar;
                        }
                      
                    }
                    else
                    {
                        view[y][x] = '?';
                    }
                }
            }


            return view;
        }
    }
}
