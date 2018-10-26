using System.Collections.Generic;
using RogueLike.Core.ComponentExtensions;
using RogueLike.Data.Components.GeneralComponents;
using System.Linq;
using RogueLike.Data.Entities;

namespace RogueLike.Core.Systems
{
    class VisionSystem
    {
        private readonly MathSystem mathSystem;
        private readonly WorldSystem.WorldSystem worldSystem;
        private readonly Dictionary<PositionComponent,Entity> effectedPositions;
        private byte[][] visionMask;
        public VisionSystem(MathSystem mathSystem, WorldSystem.WorldSystem worldSystem)
        {
            this.mathSystem = mathSystem;
            this.worldSystem = worldSystem;
            effectedPositions = new Dictionary<PositionComponent, Entity>();
            Initialize();
        }

        public byte[][] VisionMask
        {
            get { return visionMask; }
        }

        public void Initialize()
        {
             visionMask = new byte[worldSystem.WidthOfChunks * 3][];
            for (int i = 0; i < worldSystem.WidthOfChunks*3; i++)
            {
                visionMask[i] = new byte[worldSystem.WidthOfChunks*3];
            }
        }

        public void ClearMask()
        {
            for (int i = 0; i < worldSystem.WidthOfChunks * 3; i++)
            {
                for (int j = 0; j < worldSystem.WidthOfChunks * 3; j++)
                {
                    visionMask[i][j] = 0;
                }
            }
        }
        public bool SightIsBlocked(IEnumerable<PositionComponent> rayPath)
        {
            foreach (var position in rayPath)
            {
                var posInLocal = position.ToLocalCoordiantes(worldSystem.TopLeftCorner);

                var sightIsBlocked = worldSystem.LocalMap[posInLocal.YCoord][posInLocal.XCoord]
                    .Any(entity => entity.HasComponent(typeof(SightBlockingComponent)));
                if (sightIsBlocked)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if "ray trace" is blocked
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
        public bool SightIsBlocked(PositionComponent startPos, PositionComponent endPos)
        {
            var ray = mathSystem.GetLine(startPos, endPos);

            return SightIsBlocked(ray);
        }

        /// <summary>
        /// Updates visibility mask with what can be seen from the visionCenter
        /// </summary>
        /// <param name="visionCenter"></param>
        /// <param name="range"></param>
        public void SetVisiblePositions(PositionComponent visionCenter, int range)
        {
            //
            var perimeterOfViewRange = mathSystem.GetPerimeter(visionCenter.YCoord, visionCenter.XCoord, range-2)
                .Union(mathSystem.GetPerimeter(visionCenter.YCoord, visionCenter.XCoord, range))
                .Union(mathSystem.GetPerimeter(visionCenter.YCoord, visionCenter.XCoord, range - 1));
              
            foreach (var perimeterPosition in perimeterOfViewRange)
            {
                var inLocalCircle = perimeterPosition.ToLocalCoordiantes(worldSystem.TopLeftCorner);
                visionMask[inLocalCircle.YCoord][inLocalCircle.XCoord] = 2;
                var visionCenterToPerimeterLine = mathSystem.GetLine(visionCenter, perimeterPosition);

                foreach (var position in visionCenterToPerimeterLine)
                {
                    var inLocal = position.ToLocalCoordiantes(worldSystem.TopLeftCorner);
                   // visionMask[inLocal.YCoord][inLocal.XCoord] = 1;/*
                    var isSightBlocking = worldSystem.LocalMap[inLocal.YCoord][inLocal.XCoord]
                        .Any(e => e.HasComponent(typeof(SightBlockingComponent)));

                    if (isSightBlocking)
                    {
                        visionMask[inLocal.YCoord][inLocal.XCoord] = 1;
                        break;
                    }
                    else
                    {
                        visionMask[inLocal.YCoord][inLocal.XCoord] = 1;
                    }
                    
                }
            }
        }

         
    }
}
