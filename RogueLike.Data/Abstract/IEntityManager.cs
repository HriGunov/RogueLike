using System;
using System.Collections.Generic;
using RogueLike.Data.Entities;

namespace RogueLike.Data.Abstract
{
    public interface IEntityManager
    {
        List<Entity> WorldEntities { get; set; }
        ICollection<Entity> GetWorldEntitiesWithComponent(params Type[] componentTypes);

        List<Entity> NonWorldEntities { get; set; }

        ICollection<Entity> GetNonWorldEntitiesWithComponent(params Type[] componentTypes);
    }
}