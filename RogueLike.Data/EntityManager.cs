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
        public List<Entity> NonWorldEntities { get; set; } = new List<Entity>();

        public ICollection<Entity> GetWorldEntitiesWithComponent(params Type[] componentTypes)
        {
            return GetEntitiesWithComponents(WorldEntities, componentTypes);
        }

        public ICollection<Entity> GetNonWorldEntitiesWithComponent(params Type[] componentTypes)
        {
          return  GetEntitiesWithComponents(NonWorldEntities, componentTypes);
        }

        public List<Entity> WorldEntities { get; set; }= new List<Entity>(); 

        private ICollection<Entity> GetEntitiesWithComponents(IEnumerable<Entity> targetCollection,params Type[] componentTypes)
        {
            List<Entity> matchingEntities = new List<Entity>();
            foreach (var entity in targetCollection)
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
