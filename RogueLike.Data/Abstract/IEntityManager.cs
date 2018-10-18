using System;
using System.Collections.Generic;
using RogueLike.Data.Entities;

namespace RogueLike.Data.Abstract
{
    public interface IEntityManager
    {
        List<Entity> Entities { get; set; }

        ICollection<Entity> GetEntitiesWithComponent(params Type[] componentTypes);
    }
}