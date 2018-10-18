using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLike.Core.Systems.ChunkingSystem;
using RogueLike.Data.Components;
using RogueLike.Data.Entities;

namespace RogueLike.Core.Systems.MapSystem
{
    class MapSystem
    {
        private readonly IChunkingSystem chunkingSystem;
        private TileEntity[][] localMap;
        public MapSystem(IChunkingSystem chunkingSystem)
        {
            this.chunkingSystem = chunkingSystem;
        }

        public void Initialize()
        {
            localMap = new TileEntity[chunkingSystem.WidthOfChunks*3][];
            for (int y = 0; y < chunkingSystem.WidthOfChunks * 3; y++)
            {
                    localMap[y] = new TileEntity[chunkingSystem.WidthOfChunks * 3];
                for (int x = 0; x < chunkingSystem.WidthOfChunks * 3; x++)
                {
                    localMap[y][x] = new TileEntity(chunkingSystem.TopLeftCorner.YCoord + y, chunkingSystem.TopLeftCorner.XCoord +x);
                }
               
            }
            for (int y = 0; y < 3; y++)
            {
                
                for (int x = 0; 3 < x; x++)
                {
                    foreach (var entity in chunkingSystem.Chunks[y][x])
                    {
                        if (entity.HasComponent(typeof(IsTileComponent)))
                        {
                            var asTile = entity as TileEntity;
                            int localXCoord = asTile.PositionComponent.XCoord - chunkingSystem.TopLeftCorner.XCoord;
                            int localYCoord = asTile.PositionComponent.YCoord - chunkingSystem.TopLeftCorner.YCoord;

                            localMap[localYCoord][localXCoord] = asTile;

                        }
                    }
                }
            }
        }

        public void ClearSurrounding()
        {
            for (int y = 0; y < chunkingSystem.WidthOfChunks * 3; y++)
            {
                localMap[y] = new TileEntity[chunkingSystem.WidthOfChunks * 3];
                for (int x = 0; x < chunkingSystem.WidthOfChunks * 3; x++)
                {
                    localMap[y][x] = new TileEntity(chunkingSystem.TopLeftCorner.YCoord + y, chunkingSystem.TopLeftCorner.XCoord + x);
                }

            }
        }
    }
}
