using RogueLike.Data.Abstract;
using RogueLike.Data.Components.GeneralComponents;
using RogueLike.Data.Entities;
using System;
using System.Collections.Generic;

namespace RogueLike.Core.Systems.ChunkingSystem
{
    public class ChunkingSystem : IChunkingSystem
    {
        public enum Chunk
        {
            TopLeft, TopMiddle, TopRight, MiddleLeft, Center, MiddleRight, BottomLeft, BottomMiddle, BottomRight, None
        }
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

            widthOfChunks = 50;
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

        public bool CenterHasChanged { get; set; }
        public void Initialize(PositionComponent chunkTopLeftCorner)
        {
            topLeftCorner = chunkTopLeftCorner;
            chunks = new List<Entity>[3][];
            for (int y = 0; y < 3; y++)
            {
                chunks[y] = new List<Entity>[3];
                for (int x = 0; x < 3; x++)
                {
                    chunks[y][x] = LoadChunk(topLeftCorner.YCoord + y * widthOfChunks, topLeftCorner.XCoord + x * widthOfChunks);
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
                    if (!(position.YCoord < YtopLeftC || position.XCoord < XtopLeftC
                        || position.YCoord >= YtopLeftC + widthOfChunks || position.XCoord >= XtopLeftC + widthOfChunks
                        ))
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
        /// <summary>
        /// Returns the chunk which contains the position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Chunk PositionInChunk(PositionComponent position)
        {
            var positionInLocalCoords = new PositionComponent(position.YCoord - topLeftCorner.YCoord, position.XCoord - topLeftCorner.XCoord);

            if (positionInLocalCoords.YCoord < 0 || positionInLocalCoords.XCoord < 0
                || positionInLocalCoords.YCoord >= widthOfChunks * 3 || positionInLocalCoords.XCoord >= widthOfChunks * 3)
            {
                return Chunk.None;
            }
            else
            {
                // Chunk [0][x]
                if (positionInLocalCoords.YCoord < widthOfChunks)
                {
                    // Chunk [0][0]

                    if (positionInLocalCoords.XCoord < widthOfChunks)
                    {
                        return Chunk.TopLeft;
                    }
                    // Chunk [0][1]

                    if (positionInLocalCoords.XCoord < widthOfChunks * 2)
                    {

                        return Chunk.TopMiddle;

                    }
                    // Chunk [0][2]
                    else
                    {
                        return Chunk.TopRight;
                    }

                }
                // Chunk [1][x]
                else if (positionInLocalCoords.YCoord < widthOfChunks * 2)
                {

                    // Chunk [1][0]

                    if (positionInLocalCoords.XCoord < widthOfChunks)
                    {
                        return Chunk.MiddleLeft;
                    }
                    // Chunk [1][1]

                    if (positionInLocalCoords.XCoord < widthOfChunks * 2)
                    {

                        return Chunk.Center;

                    }
                    // Chunk [1][2]
                    else
                    {
                        return Chunk.MiddleRight;
                    }

                }
                // Chunk [2][x]
                else
                {
                    // Chunk [2][0]

                    if (positionInLocalCoords.XCoord < widthOfChunks)
                    {
                        return Chunk.BottomLeft;
                    }
                    // Chunk [2][1]
                    if (positionInLocalCoords.XCoord < widthOfChunks * 2)
                    {
                        return Chunk.BottomMiddle;
                    }
                    // Chunk [2][2]
                    else
                    {
                        return Chunk.BottomRight;
                    }
                }
            }
        }

        public void TrackPosition(PositionComponent position)
        {
            while (Chunk.Center != PositionInChunk(position))
            {
                var inChunk = PositionInChunk(position);
                ChangeCenterChunk(inChunk);
            }


        }

        public void ChangeCenterChunk(Chunk newCenterChunk)
        {
            if (!(newCenterChunk == Chunk.None || newCenterChunk == Chunk.Center))
            {
                switch (newCenterChunk)
                {
                    case Chunk.TopMiddle:
                        MoveCenterToTopMiddleChunk();
                        break;
                    case Chunk.BottomMiddle:
                        MoveCenterToBottomMiddleChunk();
                        break;
                    case Chunk.MiddleLeft:
                        MoveCenterToMiddleLeftChunk();
                        break;
                    case Chunk.MiddleRight:
                        MoveCenterToMiddleRightChunk();
                        break;
                }

                CenterHasChanged = true;
            }

        }

        private void MoveCenterToTopLeftChunk()
        {
            TopLeftCorner.YCoord -= WidthOfChunks;
            TopLeftCorner.XCoord -= WidthOfChunks;

            chunks[2][2] = chunks[1][1];
            chunks[1][2] = chunks[0][1];
            chunks[2][1] = chunks[1][0];
            chunks[1][1] = chunks[0][0];


            for (int i = 0; i < 3; i++)
            {
                chunks[0][i] = LoadChunk(TopLeftCorner.YCoord, TopLeftCorner.XCoord + widthOfChunks * i);
            }
            chunks[1][0] = LoadChunk(TopLeftCorner.YCoord + widthOfChunks, TopLeftCorner.XCoord);
            chunks[2][0] = LoadChunk(TopLeftCorner.YCoord + widthOfChunks * 2, TopLeftCorner.XCoord);

        }

        private void MoveCenterToTopMiddleChunk()
        {
            TopLeftCorner.YCoord -= WidthOfChunks;


            chunks[2][1] = chunks[1][1];
            chunks[2][0] = chunks[1][0];
            chunks[2][2] = chunks[1][2];

            chunks[1][0] = chunks[0][0];
            chunks[1][1] = chunks[0][1];
            chunks[1][2] = chunks[0][2];


            for (int i = 0; i < 3; i++)
            {
                chunks[0][i] = LoadChunk(TopLeftCorner.YCoord, TopLeftCorner.XCoord + widthOfChunks * i);
            }
        }
        private void MoveCenterToBottomMiddleChunk()
        {
            TopLeftCorner.YCoord += WidthOfChunks;

            chunks[0][0] = chunks[1][0];
            chunks[0][1] = chunks[1][1];
            chunks[0][2] = chunks[1][2];

            chunks[1][0] = chunks[2][0];
            chunks[1][1] = chunks[2][1];
            chunks[1][2] = chunks[2][2];



            for (int i = 0; i < 3; i++)
            {
                chunks[2][i] = LoadChunk(TopLeftCorner.YCoord + widthOfChunks * 2, TopLeftCorner.XCoord + widthOfChunks * i);
            }
        }

        private void MoveCenterToMiddleLeftChunk()
        {
            TopLeftCorner.XCoord -= WidthOfChunks;

            chunks[0][2] = chunks[0][1];
            chunks[1][2] = chunks[1][1];
            chunks[2][2] = chunks[2][1];

            chunks[0][1] = chunks[0][0];
            chunks[1][1] = chunks[1][0];
            chunks[2][1] = chunks[2][0];



            for (int i = 0; i < 3; i++)
            {
                chunks[i][0] = LoadChunk(TopLeftCorner.YCoord + widthOfChunks * i, TopLeftCorner.XCoord);
            }
        }
        private void MoveCenterToMiddleRightChunk()
        {
            TopLeftCorner.XCoord += WidthOfChunks;

            chunks[0][0] = chunks[0][1];
            chunks[1][0] = chunks[1][1];
            chunks[2][0] = chunks[2][1];

            chunks[0][1] = chunks[0][2];
            chunks[1][1] = chunks[1][2];
            chunks[2][1] = chunks[2][2];


            for (int i = 0; i < 3; i++)
            {
                chunks[i][2] = LoadChunk(TopLeftCorner.YCoord + widthOfChunks * i, TopLeftCorner.XCoord + 2 * widthOfChunks);
            }
        }
    }
}
