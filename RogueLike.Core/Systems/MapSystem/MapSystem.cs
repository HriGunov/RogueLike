using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLike.Core.Systems.ChunkingSystem;
using RogueLike.Data.Components;
using RogueLike.Data.Components.GeneralComponents;
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

        public PositionComponent TopLeftCorner
        {
            get { return chunkingSystem.TopLeftCorner; }
        }

        public TileEntity[][] LocalMap
        {
            get { return localMap; }
        }

        public void Initialize()
        {
            localMap = new TileEntity[chunkingSystem.WidthOfChunks*3][];
            for (int y = 0; y < chunkingSystem.WidthOfChunks * 3; y++)
            {
                    localMap[y] = new TileEntity[chunkingSystem.WidthOfChunks * 3]; 
            }

            ClearSurrounding();
            for (int yChunk = 0; yChunk < 3; yChunk++)
            {
                for (int xChunk = 0; xChunk < 3; xChunk++)
                {
                    foreach (var entity in chunkingSystem.Chunks[yChunk][xChunk])
                    {
                        if (entity.HasComponents(typeof(IsTileComponent)))
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

        public void CheckForUpdate()
        {
            if (chunkingSystem.CenterHasChanged)
            { 
                Initialize();
            }

            chunkingSystem.CenterHasChanged = false;
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
