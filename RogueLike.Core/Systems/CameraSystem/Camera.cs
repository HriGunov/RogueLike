using RogueLike.Data.Components.GeneralComponents;
using System.Linq;

namespace RogueLike.Core.Systems.CameraSystem
{
    class Camera
    {
        private PositionComponent position;
        private readonly VisionSystem visionSystem;
        private int width;

        public Camera(PositionComponent position, int Width, VisionSystem visionSystem)
        {
            this.visionSystem = visionSystem;
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

        public char[][] GetCurrentView(WorldSystem.WorldSystem world)
        {
            char[][] view = new char[width][];
            for (int i = 0; i < width; i++)
            {
                view[i] = new char[width];
            }

            int yOffSet = position.YCoord - width / 2;
            int xOffSet = position.XCoord - width / 2;

            var yInLocalCoords = yOffSet - world.TopLeftCorner.YCoord;
            var xInLocalCoords = xOffSet - world.TopLeftCorner.XCoord;



            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (world.LocalMap[yInLocalCoords + y][xInLocalCoords + x] != null)
                    {
                        var entitiesToBeVisualizedAtPos = world.LocalMap[yInLocalCoords + y][xInLocalCoords + x].
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

        public char[][] GetCurrentViewWithLight(WorldSystem.WorldSystem world)
        {
            char[][] view = new char[width][];
            for (int i = 0; i < width; i++)
            {
                view[i] = new char[width];

                for (int j = 0; j < width; j++)
                {
                    view[i][j] = ' ';
                }

            }

            int yOffSet = position.YCoord - width / 2;
            int xOffSet = position.XCoord - width / 2;

            var yInLocalCoords = yOffSet - world.TopLeftCorner.YCoord;
            var xInLocalCoords = xOffSet - world.TopLeftCorner.XCoord;



            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (visionSystem.VisionMask[yInLocalCoords + y][xInLocalCoords + x] != 0)
                    {
                        var entitiesToBeVisualizedAtPos = world.LocalMap[yInLocalCoords + y][xInLocalCoords + x]
                            .FirstOrDefault(e => e.HasComponent(typeof(VisualizationComponent)));

                        if (entitiesToBeVisualizedAtPos == null)
                        {
                            view[y][x] = '?';
                        }
                        else
                        {
                            var visualization = entitiesToBeVisualizedAtPos.GetComponent(
                                typeof(VisualizationComponent)) as VisualizationComponent;

                            view[y][x] = visualization.AsChar;
                        }

                        if (visionSystem.VisionMask[yInLocalCoords + y][xInLocalCoords + x] == 2)
                        {
                            view[y][x] = ' ';
                        }
                    }
                }
            }
            return view;
        }

    }
}
