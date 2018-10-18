using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLike.Data.Abstract;
using RogueLike.Data.Components.GeneralComponents;
using RogueLike.Data.Entities;

namespace RogueLike.Core.Systems.ChunkingSystem
{
    public class ChunkingSystem : IChunkingSystem
    {
        private readonly IEntityManager entityManager;
        private PositionComponent topLeftCorner;
        public ICollection<Entity>[][] chunks;
        private readonly int widthOfChunks;
        public ChunkingSystem(IEntityManager entityManager)
        {
            if (entityManager != null)
            {
                this.entityManager = entityManager;
            }
            else
            {
                throw new NullReferenceException();
            }

            widthOfChunks = 10;
        }

        public int WidthOfChunks
        {
            get { return widthOfChunks; }
            
        }

        public ICollection<Entity>[][] Chunks
        {
            get { return chunks; }
        }

        public PositionComponent TopLeftCorner
        {
            get { return topLeftCorner; }
            set { topLeftCorner = value; }
        }

        public void Initialize(PositionComponent chunkTopLeftCorner)
        {
            topLeftCorner = chunkTopLeftCorner;
            chunks = new List<Entity>[3][];
            for (int y = 0; y < 3; y++)
            {
                chunks[y] = new List<Entity>[3];
                for (int x = 0; x < 3; x++)
                {
                    chunks[y][x] = LoadChunk(topLeftCorner.YCoord +y* widthOfChunks, topLeftCorner.XCoord + x*widthOfChunks);
                }
            }
        }

        public ICollection<Entity> LoadChunk(int YtopLeftC, int XtopLeftC)
        {
            var positionType = typeof(PositionComponent);
            var positionEntities = entityManager.GetEntitiesWithComponent(positionType);
            var entitiesInChunk = new List<Entity>();
            foreach (var entity in positionEntities)
            {
                if (entity.GetComponent(positionType) is PositionComponent position)
                {
                    if (position.YCoord < YtopLeftC || position.XCoord < XtopLeftC
                        || position.YCoord >= YtopLeftC + widthOfChunks || position.XCoord >= XtopLeftC + widthOfChunks
                        )
                    {
                        entitiesInChunk.Add(entity);
                    }
                }
            }

            return entitiesInChunk;
        }

        public ICollection<Entity> LoadChunk(PositionComponent chunkTopLeftCorner)
        { 
           return LoadChunk(chunkTopLeftCorner.YCoord, chunkTopLeftCorner.XCoord);
        }

    }
}
