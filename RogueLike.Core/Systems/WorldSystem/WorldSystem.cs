using RogueLike.Data.Abstract;
using RogueLike.Data.Components.GeneralComponents;
using RogueLike.Data.Entities;
using System;
using System.Collections.Generic;

namespace RogueLike.Core.Systems.WorldSystem
{
    public class WorldSystem
    {

        public enum Chunk
        {
            TopLeft, TopMiddle, TopRight, MiddleLeft, Center, MiddleRight, BottomLeft, BottomMiddle, BottomRight, None
        }
        private List<Entity>[][] localMap;
        private readonly IEntityManager entityManager;
        private PositionComponent topLeftCorner;
        private readonly int widthOfChunks;
        public WorldSystem(IEntityManager entityManager, int widthOfChunks = 50)
        {
            if (entityManager != null)
            {
                this.entityManager = entityManager;
            }
            else
            {
                throw new NullReferenceException();
            }

            this.widthOfChunks = widthOfChunks;
            InitializeLocalMap(new PositionComponent(0, 0));
        }

        #region Properties

        public List<Entity>[][] LocalMap
        {
            get { return localMap; }
        }
        public int WidthOfChunks
        {
            get { return widthOfChunks; }

        }

        public PositionComponent TopLeftCorner
        {
            get { return topLeftCorner; }
            set { topLeftCorner = value; }
        }
        
        #endregion

        /// <summary>
        /// Initializes LocalMap
        /// </summary>
        public void InitializeLocalMap(PositionComponent centerOfWorld)
        {
            topLeftCorner = centerOfWorld;
            localMap = new List<Entity>[WidthOfChunks * 3][];
            for (int y = 0; y < WidthOfChunks * 3; y++)
            {
                localMap[y] = new List<Entity>[WidthOfChunks * 3];
                for (int x = 0; x < WidthOfChunks * 3; x++)
                {
                    localMap[y][x] = new List<Entity>();
                }
            }


            for (int yChunk = 0; yChunk < 3; yChunk++)
            {
                for (int xChunk = 0; xChunk < 3; xChunk++)
                {
                    LoadAndUpdateLocalMapChunk(yChunk, xChunk);
                }
            }

        }

        public void ClearLocalMap()
        {
            for (int y = 0; y <  3; y++)
            {

                for (int x = 0; x < 3; x++)
                {
                    ClearChunkOfLocalMap(y, x);
                }

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="yOfChunk">TopleftCorner of target Chunk</param>
        /// <param name="xOfChunk">TopleftCorner of target Chunk</param>
        public void LoadAndUpdateLocalMapChunk(int yOfChunk, int xOfChunk)
        {
            ClearChunkOfLocalMap(yOfChunk, xOfChunk);


            var chunk = LoadChunk(topLeftCorner.YCoord + yOfChunk * widthOfChunks,
                topLeftCorner.XCoord + xOfChunk * widthOfChunks);

            var positionType = typeof(PositionComponent).Name;

            foreach (var entity in chunk)
            {

                var entityPosition = entity.GetComponent(positionType) as PositionComponent;
                int localXCoord = entityPosition.XCoord - TopLeftCorner.XCoord;
                int localYCoord = entityPosition.YCoord - TopLeftCorner.YCoord;

                localMap[localYCoord][localXCoord].Add(entity);

            }
        }

        public void ClearChunkOfLocalMap(int yChunk, int xChunk)
        {
            if (xChunk >= 3 || xChunk < 0 || yChunk < 0 || yChunk >= 3)
            {
                throw new ArgumentOutOfRangeException("Trying to access invalid chunk.");
            }

            for (int y = 0; y < WidthOfChunks; y++)
            {

                for (int x = 0; x < WidthOfChunks; x++)
                {
                    localMap[yChunk * widthOfChunks + y][xChunk * widthOfChunks + x] = new List<Entity>();
                }

            }
        }

        public ICollection<Entity> LoadChunk(int YtopLeftC, int XtopLeftC)
        {
            var positionType = typeof(PositionComponent);
            var positionEntities = entityManager.GetWorldEntitiesWithComponent(positionType);
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

        public void MoveChunk(int yChunkToOveride, int xChunkToOveride, int yChunkToMove, int xChunkToMove)
        {
            
            //Moves local map
            for (int y = 0; y < widthOfChunks; y++)
            {
                for (int x = 0; x < widthOfChunks; x++)
                {
                    localMap[yChunkToOveride * widthOfChunks + y][xChunkToOveride * widthOfChunks + x] =
                        localMap[yChunkToMove * widthOfChunks + y][xChunkToMove * widthOfChunks + x];
                }
            }

        }


        /// <summary>
        /// If the position is not the center chunk, the chunking system moves  and loads the correct chunks.  
        /// </summary>
        /// <param name="position"></param>
        public void CheckAndMoveChunks(PositionComponent position)
        {
            var copyOfPosition = new PositionComponent(position.YCoord, position.XCoord);

            var inChunk = PositionInChunk(position);
            if (inChunk == Chunk.None)
            {
                var deltaChunkY = (position.YCoord - TopLeftCorner.YCoord) / widthOfChunks - 1;
                var deltaChunkX = (position.XCoord - TopLeftCorner.XCoord) / widthOfChunks - 1;

                topLeftCorner.XCoord += deltaChunkX * widthOfChunks;
                topLeftCorner.YCoord += deltaChunkY * widthOfChunks;

            }
            inChunk = PositionInChunk(position);
            while (Chunk.Center != inChunk)
            { 
                ChangeCenterChunk(inChunk);
                inChunk = PositionInChunk(position);
            }
        }

        

        #region Chunk Rotations




        public void ChangeCenterChunk(Chunk newCenterChunk)
        {
            if (!(newCenterChunk == Chunk.None || newCenterChunk == Chunk.Center))
            {
                switch (newCenterChunk)
                {
                    case Chunk.TopLeft:
                        MoveCenterToTopLeftChunk();
                        break;
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
               
            }

        }

        private void MoveCenterToTopLeftChunk()
        {
            TopLeftCorner.YCoord -= WidthOfChunks;
            TopLeftCorner.XCoord -= WidthOfChunks;

            MoveChunk(2, 2, 1, 1);
            MoveChunk(1, 2, 0, 1);
            MoveChunk(2, 1, 1, 0);
            MoveChunk(1, 1, 0, 0);


            for (int i = 0; i < 3; i++)
            {
                LoadAndUpdateLocalMapChunk(0, i);
            }

            LoadAndUpdateLocalMapChunk(1, 0);
            LoadAndUpdateLocalMapChunk(2, 0);

        }

        private void MoveCenterToTopMiddleChunk()
        {
            TopLeftCorner.YCoord -= WidthOfChunks;

            MoveChunk(2, 0, 1, 0);
            MoveChunk(2, 1, 1, 1);
            MoveChunk(2, 2, 1, 2);

            MoveChunk(1, 0, 0, 0);
            MoveChunk(1, 1, 0, 1);
            MoveChunk(1, 2, 0, 2);


            for (int i = 0; i < 3; i++)
            {
                LoadAndUpdateLocalMapChunk(0, i);
            }
        }
        private void MoveCenterToBottomMiddleChunk()
        {
            TopLeftCorner.YCoord += WidthOfChunks;


            MoveChunk(0, 0, 1, 0);
            MoveChunk(0, 1, 1, 1);
            MoveChunk(0, 2, 1, 2);

            MoveChunk(1, 0, 2, 0);
            MoveChunk(1, 1, 2, 1);
            MoveChunk(1, 2, 2, 2);




            for (int i = 0; i < 3; i++)
            {
                LoadAndUpdateLocalMapChunk(2, i);
            }
        }

        private void MoveCenterToMiddleLeftChunk()
        {
            TopLeftCorner.XCoord -= WidthOfChunks;


            MoveChunk(0, 2, 0, 1);
            MoveChunk(1, 2, 1, 1);
            MoveChunk(2, 2, 2, 1);

            MoveChunk(0, 1, 0, 0);
            MoveChunk(1, 1, 1, 0);
            MoveChunk(2, 1, 2, 0);

            for (int i = 0; i < 3; i++)
            {
                LoadAndUpdateLocalMapChunk(i, 0);
            }
        }
        private void MoveCenterToMiddleRightChunk()
        {
            TopLeftCorner.XCoord += WidthOfChunks;

            MoveChunk(0, 0, 0, 1);
            MoveChunk(1, 0, 1, 1);
            MoveChunk(2, 0, 2, 1);

            MoveChunk(0, 1, 0, 2);
            MoveChunk(1, 1, 1, 2);
            MoveChunk(2, 1, 2, 2);

            for (int i = 0; i < 3; i++)
            {
                LoadAndUpdateLocalMapChunk(i, 2);
            }
        }
        #endregion



    }
}
