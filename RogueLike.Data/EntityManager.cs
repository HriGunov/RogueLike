using RogueLike.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Data
{
    class EntityManager
    {
        public List<Entity> Entities { get; set; }

        ICollection<Entity> GetEntitiesWithComponent(Type typeOfComponent)
        {
            List<Entity> matchingEntities = new List<Entity>();
            foreach (var entity in Entities)
            {
                if (entity.HasComponent(typeOfComponent))
                {
                    matchingEntities.Add(entity);
                }
            }
            return matchingEntities;
        }
    }
}
