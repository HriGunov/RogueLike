using System.Collections.Generic;
using RogueLike.Data.Components.GeneralComponents;
using RogueLike.Data.Entities;

namespace RogueLike.Core.Systems.ChunkingSystem
{
    public interface IChunkingSystem
    {
        void Initialize(PositionComponent chunkTopLeftCorner);
        ICollection<Entity> LoadChunk(int YtopLeftC, int XtopLeftC);
        ICollection<Entity> LoadChunk(PositionComponent chunkTopLeftCorner);
        int WidthOfChunks { get; }
        ICollection<Entity>[][] Chunks { get; }
        PositionComponent TopLeftCorner { get; }
    }
}