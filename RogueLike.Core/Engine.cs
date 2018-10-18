using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLike.Core.Systems.ChunkingSystem;
using RogueLike.Data.Abstract;
using RogueLike.Data.Components.GeneralComponents;

namespace RogueLike.Core
{
    class Engine
    {
        private readonly IEntityManager entityManager;

        public Engine(IEntityManager entityManager)
        {
            this.entityManager = entityManager;
        }

        public void Run()
        {
            var chunkingSystem = new ChunkingSytem(entityManager);
            chunkingSystem.Initialize(new PositionComponent(0,0));
        }
    }
}
