using RogueLike.Data.Entities;
using RogueLike.Data.Abstract;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Data
{
    public class EntityManager : IEntityManager
    {
        public List<Entity> Entities { get; set; }

        public ICollection<Entity> GetEntitiesWithComponent(params Type[] componentTypes)
        {
            List<Entity> matchingEntities = new List<Entity>();
            foreach (var entity in Entities)
            {
                if (entity.HasComponents(componentTypes))
                {
                    matchingEntities.Add(entity);
                }
            }
            return matchingEntities;
        }
    }
}
