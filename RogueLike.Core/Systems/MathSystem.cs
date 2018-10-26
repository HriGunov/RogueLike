using RogueLike.Data.Components.GeneralComponents;
using System;
using System.Collections.Generic;

namespace RogueLike.Core.Systems
{
    class MathSystem
    {
        public MathSystem()
        {

        }

        public IEnumerable<PositionComponent> GetLine(PositionComponent startPosition,
            PositionComponent endPosition)
        {
            return GetLine(startPosition.YCoord, startPosition.XCoord, endPosition.YCoord, endPosition.XCoord);
        }
        public IEnumerable<PositionComponent> GetLine(int startY, int startX, int endY, int endX)
        {

            int deltaY = endY - startY;
            int deltaX = endX - startX;

            double currentError = 0;

            List<PositionComponent> path = new List<PositionComponent>();
            if (deltaY == 0)
            {
                // Horizontal Line
                if (deltaX > 0)
                {
                    for (int position = startX; position <= endX; position++)
                    {
                        path.Add(new PositionComponent(startY, position));
                    }
                }
                else
                {
                    for (int position = startX; position >= endX; position--)
                    {
                        path.Add(new PositionComponent(startY, position));
                    }
                }
                return path;
            }
            else if (deltaX == 0)
            {

                if (deltaY > 0)
                {
                    for (int position = startY; position <= endY; position++)
                    {
                        path.Add(new PositionComponent(position, startX));
                    }
                }
                else
                {
                    for (int position = startY; position >= endY; position--)
                    {
                        path.Add(new PositionComponent(position, startX));
                    }
                }

                return path;
            }

            if (Math.Abs(deltaX) >= Math.Abs(deltaY))
            {
                bool positiveDeltaY = deltaY > 0;
                bool positiveDeltaX = deltaX > 0;
                int y = startY;
                double errorPerStep = Math.Abs(deltaY / (double)deltaX);

                for (int step = 0; step <= Math.Abs(deltaX); step++)
                {
                    if (positiveDeltaX)
                    {
                        path.Add(new PositionComponent(y, startX + step));
                    }
                    else
                    {
                        path.Add(new PositionComponent(y, startX - step));
                    }
                    currentError += errorPerStep;
                    if (currentError >= 1)
                    {
                        if (positiveDeltaY)
                        {
                            y++;
                        }
                        else
                        {
                            y--;
                        }

                        currentError--;
                    }
                }
            }
            else
            {
                bool positiveDeltaY = deltaY > 0;
                bool positiveDeltaX = deltaX > 0;
                int x = startX;
                double errorPerStep = Math.Abs(deltaX / (double)deltaY);

                for (int step = 0; step <= Math.Abs(deltaY); step++)
                {
                    if (positiveDeltaY)
                    {
                        path.Add(new PositionComponent(startY + step, x));
                    }
                    else
                    {
                        path.Add(new PositionComponent(startY - step, x));
                    }
                    currentError += errorPerStep;
                    if (currentError >= 1)
                    {
                        if (positiveDeltaX)
                        {
                            x++;
                        }
                        else
                        {
                            x--;
                        }

                        currentError--;
                    }
                }


            }
            return path;
        }

        public List<PositionComponent> GetPerimeter(PositionComponent centerPosition, int radius)
        {
            return GetPerimeter(centerPosition.YCoord, centerPosition.XCoord, radius);
        }
        public List<PositionComponent> GetPerimeter(int y0, int x0, int radius)
        {
            List<PositionComponent> listToReturn = new List<PositionComponent>();
            int x = radius;
            int y = 0;
            int dx = 1;
            int dy = 1;
            int err = dx - (radius +1);

            while (x >= y)
            {
                listToReturn.Add(new PositionComponent(y0 + y, x0 + x));
                listToReturn.Add(new PositionComponent(y0 + x, x0 + y));
                listToReturn.Add(new PositionComponent(y0 + x, x0 - y));
                listToReturn.Add(new PositionComponent(y0 + y, x0 - x));
                listToReturn.Add(new PositionComponent(y0 - y, x0 - x));
                listToReturn.Add(new PositionComponent(y0 - x, x0 - y));
                listToReturn.Add(new PositionComponent(y0 - x, x0 + y));
                listToReturn.Add(new PositionComponent(y0 - y, x0 + x));

                if (err <= 0)
                {
                    y++;
                    err += dy;
                    dy += 2;
                }

                if (err > 0)
                {
                    x--;
                    dx += 2;
                    err += dx - (radius << 1);
                }
            }

            return listToReturn;
        }

    }
}
